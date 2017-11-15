
using DPA_Musicsheets.classes;
using DPA_Musicsheets.EventArgs;
using DPA_Musicsheets.Factories;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond;
using DPA_Musicsheets.Memento;
using DPA_Musicsheets.Models;
using DPA_Musicsheets.Saving;
using DPA_Musicsheets.States;
using Microsoft.Win32;
using PSAMControlLibrary;
using PSAMWPFControlLibrary;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Managers
{
    class FileHandler
    {
        private string _editorText;
        public string EditorText
        {
            get { return _editorText; }
            set
            {
                _editorText = value;
                if (CurrentState.getEditString().Equals("Edit"))
                    FileSavedChanged?.Invoke(this, new FileSavedEventArgs() { HasSaved = false });

                EditorTextChanged?.Invoke(this, new TextEventArgs() { Text = value });
            }
        }

        private string _currentStateText;
        public string CurrentStateText
        {
            get { return _currentStateText; }
            set
            {
                _currentStateText = value;
                StateChanged?.Invoke(this, new CurrentStateEventArgs() { State = value });
            }
        }
        
        private IInputReader inputReader;
        private MusicSheet musicSheet;    
        private List<INoteObserver> noteObservers;
        private StaffDrawer drawer;
        public IState CurrentState { get; set; }
        public Memento.Memento memento { get; set; }

        public int CursorLocation { get; set; }
        private string fileName;
        public List<MusicalSymbol> WPFStaffs { get; set; } = new List<MusicalSymbol>();
        
        public event EventHandler<TextEventArgs> EditorTextChanged;
        public event EventHandler<WPFStaffsEventArgs> WPFStaffsChanged;
        public event EventHandler<SequenceEventArgs> SequenceChanged;
        public event EventHandler<FilenameEventArgs> FilenameChanged;
        public event EventHandler<CurrentStateEventArgs> StateChanged;
        public event EventHandler<FileSavedEventArgs> FileSavedChanged;

        public FileHandler()
        {
            CurrentState = new PlayState(this);
            noteObservers = new List<INoteObserver>();
            drawer = new StaffDrawer(WPFStaffs);
            this.attachObserver(drawer);
            memento = new Memento.Memento(this);
        }
        
        public void OpenFile()
        {
            //reset the box
            if (CurrentState.getEditString().Equals("Edit"))
            {
                CurrentState.SwitchState();
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ConvertFile(openFileDialog.FileName);
                FilenameChanged?.Invoke(this, new FilenameEventArgs() { Filename = openFileDialog.FileName });
                
                fileName = openFileDialog.FileName;
            }
        }

        public void ConvertFile(string fileName)
        {
            WPFStaffs.Clear();
            EditorText = "";

            inputReader = ReaderFactory.getReader(System.IO.Path.GetExtension(fileName));
            musicSheet = inputReader.readNotes(fileName);
                                      
            notifyAll();
                           
            WPFStaffsChanged?.Invoke(this, new WPFStaffsEventArgs() { Symbols = WPFStaffs, Message = "" });
            SequenceChanged?.Invoke(this, new SequenceEventArgs() { PlayableSequence = drawer.PlayableSequence });
        }

        public void SetEditText(string fileName)
        {
            if (fileName == "" || fileName == null)
                return;

            EditorText = inputReader.GetText(fileName);
            memento.NewNode(EditorText);
        }

        public void SaveFile(string fileFormat)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Sla je muziek op";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                save(fileFormat, dialog.FileName);
                FileSavedChanged?.Invoke(this, new FileSavedEventArgs() { HasSaved = true });  
            }
        }

        public void save(string type, string fileLocation)
        {
            ISave saver = SaveFactory.getSaver(type);
            saver.save(EditorText, fileLocation);
        }

        void attachObserver(INoteObserver observer)
        {
            if (!noteObservers.Contains(observer))
            {
                noteObservers.Add(observer);
            }
        }

        public void TryExecuteCommand(string command)
        {
            if (CurrentState.CanExecuteCommand(command))
            {
                CurrentState.ExecuteCommand();
            }
        }

        private void notifyAll()
        {

            for (int i = 0; i < noteObservers.Count; i++)
            {
                noteObservers[i].update(musicSheet);
            }
        }    

        public void InsertIntoSheet(int position, string data)
        {
            EditorText = EditorText.Insert(position, data);
            RedrawStaff();
        }
        
        public void RedrawStaff()
        {
            WPFStaffs.Clear();

            inputReader = ReaderFactory.getReader("lilypond");
            musicSheet = inputReader.readNotes(EditorText);
            notifyAll();

            WPFStaffsChanged?.Invoke(this, new WPFStaffsEventArgs() { Symbols = WPFStaffs, Message = "" });

            SequenceChanged?.Invoke(this, new SequenceEventArgs() { PlayableSequence = drawer.PlayableSequence });
        }

        public string GetFileName()
        {
            return fileName;
        }
    }
}
