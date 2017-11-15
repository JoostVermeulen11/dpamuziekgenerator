using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DPA_Musicsheets.ViewModels
{
    class LilypondViewModel : ViewModelBase
    {
        private FileHandler _fileHandler;

        private string _text;
        private string _previousText;
        private bool _IsEditingEnabled;
        private string _EditButtonContent;
        private bool _SaveMethodSelected = false;
        private int _CursorLocation = 0;

        public bool CanForward
        {
            get { return _fileHandler.memento.canForward; }
            set
            {
                _fileHandler.memento.canForward = value;
                RaisePropertyChanged(() => CanForward);
            }
        }

        public bool CanBackward
        {
            get { return _fileHandler.memento.canBackward; }
            set
            {
                _fileHandler.memento.canBackward = value;
                RaisePropertyChanged(() => CanBackward);
            }
        }

        public int CursorLocation
        {
            get { return _CursorLocation; }
            set
            {
                _CursorLocation = value;
                RaisePropertyChanged(() => CursorLocation);
            }
        }

        public bool SaveMethodSelected
        {
            get { return _SaveMethodSelected; }
            set
            {
                _SaveMethodSelected = value;
                RaisePropertyChanged(() => SaveMethodSelected);
            }
        }

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

        private List<string> _saveMethods;
        private string _saveMethod;

        public List<string> SaveMethods
        {
            get { return _saveMethods; }
        }

        public string SaveMethod
        {
            get { return _saveMethod; }
            set
            {
                if (_saveMethod == value) return;
                _saveMethod = value;
                SaveMethodSelected = true;
                RaisePropertyChanged(() => SaveMethod);
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

            _saveMethods = new List<string>();
            SaveMethods.Add("Lilypond");
            SaveMethods.Add("Pdf");

            EditModeFactory(_fileHandler.CurrentState.getEditString());

            _fileHandler.EditorTextChanged += (src, e) =>
            {
                _textChangedByLoad = true;
                LilypondText = _previousText = e.Text;
                _textChangedByLoad = false;
            };
                         
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
                    
                        _fileHandler.EditorText = LilypondText;
                        _fileHandler.memento.NewNode(LilypondText);
                        CanBackward = true;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext()); // Request from main thread.
            }            
        });

        public ICommand SelectionChangedCommand => new RelayCommand<RoutedEventArgs>((args) =>
        {
            int currentSelection = (args.OriginalSource as System.Windows.Controls.TextBox).SelectionStart;
            _fileHandler.CursorLocation = currentSelection;
        });

        public RelayCommand UndoCommand => new RelayCommand(() =>
        {                
            _fileHandler.memento.Back();
            CanForward = true;
        });

        public RelayCommand RedoCommand => new RelayCommand(() =>
        {
            _fileHandler.memento.Forward();                      
        });

        public ICommand SaveAsCommand => new RelayCommand(() =>
        {
            this._fileHandler.TryExecuteCommand(SaveMethod);    
        });

        public ICommand EditStateCommand => new RelayCommand(() =>
        {
            _fileHandler.CurrentState.SwitchState();
            CanBackward = false;
            CanForward = false;
        });
    }
}
