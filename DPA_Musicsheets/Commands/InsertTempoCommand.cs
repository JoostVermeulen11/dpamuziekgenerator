using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    class InsertTempoCommand : ICommand
    {
        private FileHandler fileHandler;

        public InsertTempoCommand(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public string pattern
        {
            get
            {
                return "LeftAltS";
            }
        }

        public string commandName
        {
            get
            {
                return "LeftAltS";
            }
        }

        public void execute()
        {
            // still need to implement
        }
    }
}
