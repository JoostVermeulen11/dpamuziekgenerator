
using DPA_Musicsheets.classes;
using DPA_Musicsheets.Factories;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond;
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
                EditorTextChanged?.Invoke(this, new TextEventArgs() { Text = value });
            }
        }

        //zelf erbij gezet
        private IInputReader inputReader;
        private MusicSheet musicSheet;    
        private List<INoteObserver> noteObservers;
        private StaffDrawer drawer;
        public IState CurrentState { get; set; }
        public Memento.Memento memento { get; set; }
        // tot hier

        public List<MusicalSymbol> WPFStaffs { get; set; } = new List<MusicalSymbol>();
        private static List<Char> notesorder = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };

        public FileHandler()
        {
            noteObservers = new List<INoteObserver>();
            drawer = new StaffDrawer(WPFStaffs);
            this.attachObserver(drawer);
            memento = new Memento.Memento(this);

            CurrentState = new PlayState(this);
        }

        public event EventHandler<TextEventArgs> EditorTextChanged;
        public event EventHandler<WPFStaffsEventArgs> WPFStaffsChanged;
        public event EventHandler<SequenceEventArgs> SequenceChanged;
        public event EventHandler<FilenameEventArgs> FilenameChanged;

        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ConvertFile(openFileDialog.FileName);
                FilenameChanged?.Invoke(this, new FilenameEventArgs() { Filename = openFileDialog.FileName });
            }
        }

        public void ConvertFile(string fileName)
        {
            WPFStaffs.Clear();

            inputReader = ReaderFactory.getReader(System.IO.Path.GetExtension(fileName));
            musicSheet = inputReader.readNotes(fileName);

            if (Path.GetExtension(fileName).EndsWith(".ly"))
            {
                EditorText = inputReader.GetText(fileName); 
            }

            notifyAll();
                           
            WPFStaffsChanged?.Invoke(this, new WPFStaffsEventArgs() { Symbols = WPFStaffs, Message = "" });

            SequenceChanged?.Invoke(this, new SequenceEventArgs() { PlayableSequence = drawer.PlayableSequence });
        }

        public void SaveFile(string fileFormat)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Sla je muziek op";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                save(fileFormat, dialog.FileName);
                //HasSaved = true;
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
        
        public void RedrawStaff()
        {
            WPFStaffs.Clear();

            inputReader = ReaderFactory.getReader("lilypond");
            musicSheet = inputReader.readNotes(EditorText);
            notifyAll();

            WPFStaffsChanged?.Invoke(this, new WPFStaffsEventArgs() { Symbols = WPFStaffs, Message = "" });

            SequenceChanged?.Invoke(this, new SequenceEventArgs() { PlayableSequence = drawer.PlayableSequence });
        }
    }
}
