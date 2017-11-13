using DPA_Musicsheets.Managers;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.ViewModels
{
    class MenuViewModel : ViewModelBase
    {
        private FileHandler _fileHandler;

        public MenuViewModel(FileHandler _fileHandler)
        {
            this._fileHandler = _fileHandler;
        }
    }
}
