﻿using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    class InsertTime34Command : ICommand
    {
        private FileHandler fileHandler;

        public InsertTime34Command(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public string pattern
        {
            get
            {
                return "LeftAltT3";
            }
        }

        public string commandName
        {
            get
            {
                return "LeftAltT3";
            }
        }

        public void execute()
        {
            // still need to implement
        }
    }
}