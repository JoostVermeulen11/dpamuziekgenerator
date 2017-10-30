using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PSAMWPFControlLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DPA_Musicsheets.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        List<Key> _pressedKeys;

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                RaisePropertyChanged(() => FileName);
            }
        }

        private string keyCombination;

        private string _currentState;
        public string CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; RaisePropertyChanged(() => CurrentState); }
        }

        
        private FileHandler _fileHandler;

        public MainViewModel(FileHandler fileHandler)
        {
            Console.WriteLine("\n");
            this._fileHandler = fileHandler;
            FileName = @"Files/Alle-eendjes-zwemmen-in-het-water.mid";

            MessengerInstance.Register<CurrentStateMessage>(this, (message) => CurrentState = message.State);
            _pressedKeys = new List<Key>();
            keyCombination = "";
        }

        public ICommand OpenFileCommand => new RelayCommand(() =>
        {
            _fileHandler.OpenFile();           
        });

        public ICommand LoadCommand => new RelayCommand(() =>
        {
            _fileHandler.ConvertFile(FileName);
        });
        
        public ICommand OnLostFocusCommand => new RelayCommand(() =>
        {
            Console.WriteLine("Maingrid Lost focus");
        });

        public ICommand OnKeyDownCommand => new RelayCommand<KeyEventArgs>((e) =>
        {            
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (!_pressedKeys.Contains(key))
                _pressedKeys.Add(key);

            AddKeys();
            e.Handled = true;
        });
        
        public ICommand OnKeyUpCommand => new RelayCommand<KeyEventArgs>((e) =>
        {
            _pressedKeys.Remove(e.Key);
            AddKeys();
            e.Handled = true;            
            executeCommand(keyCombination);
            keyCombination = "";
        });

        private void executeCommand(string command)
        {
            Console.WriteLine(command);
            this._fileHandler.TryExecuteCommand(command);
        }

        private void AddKeys()
        {
            foreach (Key key in _pressedKeys)
            {              
                if (keyCombination != key.ToString())
                    keyCombination += key.ToString();
            }
            _pressedKeys.Clear();         
        }

        public ICommand OnWindowClosingCommand => new RelayCommand(() =>
        {
            ViewModelLocator.Cleanup();
        });
    }
}
