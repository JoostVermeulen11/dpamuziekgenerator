using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    class InsertTime44Command : ICommand
    {
        private FileHandler fileHandler;

        public InsertTime44Command(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public string pattern
        {
            get
            {
                return "LeftAltT4";
            }
        }

        public string commandName
        {
            get
            {
                return "LeftAltT";
            }
        }

        public void execute()
        {
            fileHandler.InsertIntoSheet(fileHandler.CursorLocation, "\\time 4/4 ");
        }
    }
}
