using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    class InsertClefCommand : ICommand
    {
        private FileHandler fileHandler;

        public InsertClefCommand(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public string pattern
        {
            get
            {
                return "LeftAltC";
            }
        }

        public string commandName
        {
            get
            {
                return "insertClef";
            }
        }

        public void execute()
        {
            fileHandler.InsertIntoSheet(fileHandler.CursorLocation, "\\clef ");      
        }
    }
}
