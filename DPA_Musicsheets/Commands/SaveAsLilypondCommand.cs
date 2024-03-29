﻿using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    class SaveAsLilypondCommand : ICommand
    {
        private FileHandler fileHandler;

        public SaveAsLilypondCommand(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public string pattern
        {
            get
            {
                return "LeftCtrlS";
            }
        }

        public string commandName
        {
            get
            {
                return "Lilypond";
            }
        }

        public void execute()
        {
            fileHandler.SaveFile("lilypond");
        }
    }
}
