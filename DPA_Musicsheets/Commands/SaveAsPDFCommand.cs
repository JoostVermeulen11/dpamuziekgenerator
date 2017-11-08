using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    class SaveAsPDFCommand : ICommand
    {
        private FileHandler fileHandler;

        public SaveAsPDFCommand(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public string pattern
        {
            get
            {
                return "LeftCtrlSP";
            }
        }

        public string commandName
        {
            get
            {
                return "Pdf";
            }
        }

        public void execute()
        {
            fileHandler.SaveFile("pdf");
        }
    }
}
