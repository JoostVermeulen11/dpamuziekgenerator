using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DPA_Musicsheets.ViewModels
{
    class LilypondViewModel : ViewModelBase
    {
        private FileHandler _fileHandler;

        private string _text;
        private string _previousText;
        private string _nextText;
        private bool _IsEditingEnabled;
        private string _EditButtonContent;

        public string EditButtonContent
        {
            get { return _EditButtonContent; }
            set
            {
                _EditButtonContent = value;
                RaisePropertyChanged(() => EditButtonContent);
            }
        }

        public bool IsEditingEnabled
        {
            get { return _IsEditingEnabled; }
            set
            {
                _IsEditingEnabled = value;
                RaisePropertyChanged(() => IsEditingEnabled);
            }
        }

        public string LilypondText
        {
            get
            {
                return _text;
            }
            set
            {
                if (!_waitingForRender && !_textChangedByLoad)
                {
                    _previousText = _text;
                }
                _text = value;                
                RaisePropertyChanged(() => LilypondText);
            }
        }

        private bool _textChangedByLoad = false;
        private DateTime _lastChange;
        private static int MILLISECONDS_BEFORE_CHANGE_HANDLED = 1500;
        private bool _waitingForRender = false;

        public LilypondViewModel(FileHandler fileHandler)
        {           
            IsEditingEnabled = true;
            _fileHandler = fileHandler;

            EditModeFactory(_fileHandler.CurrentState.getEditString());

            _fileHandler.EditorTextChanged += (src, e) =>
            {
                _textChangedByLoad = true;
                LilypondText = _previousText = e.Text;
                _textChangedByLoad = false;
            };

            _text = "Click on the edit button to edit something.";
           
            _fileHandler.StateChanged += (src, e) =>
            {
                EditModeFactory(e.State);
            };
        }

        public void EditModeFactory(string state)
        {
            if (state.Equals("Edit"))
            {
                IsEditingEnabled = true;
                EditButtonContent = "Stop editing";      
            }
            else
            {
                IsEditingEnabled = false;
                EditButtonContent = "Start editing";
            }
        }

        public ICommand TextChangedCommand => new RelayCommand<TextChangedEventArgs>((args) =>
        {
            if (!_textChangedByLoad)
            {
                _waitingForRender = true;
                _lastChange = DateTime.Now;
                MessengerInstance.Send<CurrentStateMessage>(new CurrentStateMessage() { State = "Rendering..." });

                Task.Delay(MILLISECONDS_BEFORE_CHANGE_HANDLED).ContinueWith((task) =>
                {
                    if ((DateTime.Now - _lastChange).TotalMilliseconds >= MILLISECONDS_BEFORE_CHANGE_HANDLED)
                    {
                        _waitingForRender = false;
                        UndoCommand.RaiseCanExecuteChanged();

                        _fileHandler.EditorText = LilypondText;
                        _fileHandler.memento.NewNode(LilypondText);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext()); // Request from main thread.
            }
        });

        public RelayCommand UndoCommand => new RelayCommand(() =>
        {
            _nextText = LilypondText;
            LilypondText = _previousText;
            _previousText = null;
        }, () => _previousText != LilypondText);

        public RelayCommand RedoCommand => new RelayCommand(() =>
        {
            _previousText = LilypondText;
            LilypondText = _nextText;
            _nextText = null;
            RedoCommand.RaiseCanExecuteChanged();
        }, () => _nextText != LilypondText);

        public ICommand SaveAsCommand => new RelayCommand(() =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Midi|*.mid|Lilypond|*.ly|PDF|*.pdf" };
            if (saveFileDialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName);
                if (extension.EndsWith(".mid"))
                {
                    // _fileHandler.SaveToMidi(saveFileDialog.FileName);
                }
                else if (extension.EndsWith(".ly"))
                {
                    this._fileHandler.TryExecuteCommand("LeftCtrlS");
                }
                else if (extension.EndsWith(".pdf"))
                {
                    this._fileHandler.TryExecuteCommand("LeftCtrlSP");
                }
            }
        });

        public ICommand EditStateCommand => new RelayCommand(() =>
        {
            _fileHandler.CurrentState.SwitchState();            
        });
    }
}
