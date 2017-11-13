using DPA_Musicsheets.Managers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DPA_Musicsheets.ViewModels
{
    class MenuViewModel : ViewModelBase
    {
        private FileHandler _fileHandler;

        public ICommand MenuButtonCommand { get; set; }

        public MenuViewModel(FileHandler _fileHandler)
        {
            this._fileHandler = _fileHandler;

            MenuButtonCommand = new RelayCommand<object>(ExecuteMenuButtonCommand);
        }

        private void ExecuteMenuButtonCommand(object obj)
        {
            if (obj.ToString().Equals("exit"))
            {
                Application.Current.MainWindow.Close();
            }
        }
    }
}
