using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    class OpenFileCommand : ICommand
    {
        private FileHandler fileHandler;

        public OpenFileCommand(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public string pattern
        {
            get
            {
                return "LeftCtrlO";
            }
        }

        public string commandName
        {
            get
            {
                return "OpenPDF";
            }
        }

        public void execute()
        {
            fileHandler.OpenFile();
        }
    }
}
