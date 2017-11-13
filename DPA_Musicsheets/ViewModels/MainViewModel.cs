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
        bool hasSaved = true;
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

        private string _currentState;
        public string CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; RaisePropertyChanged(() => CurrentState); }
        }

        private FileHandler _fileHandler;

        public MainViewModel(FileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
            FileName = @"Files/Alle-eendjes-zwemmen-in-het-water.mid";

            _fileHandler.FilenameChanged += (src, args) =>
            {
                FileName = args.Filename;
            };

            _fileHandler.FileSavedChanged += (src, args) =>
            {
                hasSaved = args.HasSaved;
            };


            MessengerInstance.Register<CurrentStateMessage>(this, (message) => CurrentState = message.State);
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
            string commandString = "";
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (Keyboard.IsKeyDown(Key.S))
                {
                    if (Keyboard.IsKeyDown(Key.P))
                        commandString = "LeftCtrlSP";
                    else
                        commandString = "LeftCtrlS";
                }
                if (Keyboard.IsKeyDown(Key.O))
                    commandString = "LeftCtrlO";
            }
            if (Keyboard.Modifiers == ModifierKeys.Alt)
            {
                if (Keyboard.IsKeyDown(Key.C))
                    commandString = "LeftAltC";
                if (Keyboard.IsKeyDown(Key.S))
                    commandString = "LeftAltS";
                if (Keyboard.IsKeyDown(Key.T))
                {
                    if (Keyboard.IsKeyDown(Key.D4) || Keyboard.IsKeyDown(Key.NumPad4))
                        commandString = "LeftAltT4";
                    if (Keyboard.IsKeyDown(Key.D3) || Keyboard.IsKeyDown(Key.NumPad3))
                        commandString = "LeftAltT3";
                    else if (Keyboard.IsKeyDown(Key.D6) || Keyboard.IsKeyDown(Key.NumPad6))
                        commandString = "LeftAltT6";
                    else
                        commandString = "LeftAltT";
                }
            }
            if (commandString != "")
                _fileHandler.TryExecuteCommand(commandString);
        });

        public ICommand OnKeyUpCommand => new RelayCommand<KeyEventArgs>((e) =>
        {
            
        });

        public ICommand ExecuteCommand => new RelayCommand<object>((object obj) =>
        {
            if (obj.ToString().Equals("exit"))
            {
                if (!hasSaved)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to quit without saving?", "Closing application", MessageBoxButton.YesNo);

                    // End session, if specified
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                ViewModelLocator.Cleanup();
                Application.Current.MainWindow.Close();
            }

            executeCommand(obj.ToString());
        });

        private void executeCommand(string command)
        {
            this._fileHandler.TryExecuteCommand(command);
        }

        public ICommand OnWindowClosingCommand => new RelayCommand<System.ComponentModel.CancelEventArgs>((e) =>
        {
            ////als de hasSaved false is, vragen of hij op wilt slaan
            //if (!hasSaved)
            //{
            //    MessageBoxResult result = MessageBox.Show("Do you want to quit without saving?", "Closing application", MessageBoxButton.YesNo);

            //    // End session, if specified
            //    if (result == MessageBoxResult.No)
            //    {
            //        e.Cancel = true;
            //    }
            //}

            //ViewModelLocator.Cleanup();
        });
    }
}
