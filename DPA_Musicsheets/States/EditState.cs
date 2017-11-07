﻿using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.enums;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Commands;

namespace DPA_Musicsheets.States
{
    class EditState : IState
    {
        public List<ICommand> Commands { get; set; }
        public StateType Type { get; set; }
        public ICommand ExecutableCommand { get; set; }

        private FileHandler fileHandler; 
        public EditState(FileHandler fileHandler)
        {         
            this.fileHandler = fileHandler;
            Type = StateType.Edit;

            fileHandler.EditorText = fileHandler.GetLilypond();

            Commands = new List<ICommand>();

            //Commands.Add(new InsertBarLinesCommand(controller));
            //Commands.Add(new InsertClefCommand(controller));
            //Commands.Add(new InsertTempoCommand(controller));
            //Commands.Add(new InsertTimeCommand(controller));
            //Commands.Add(new InsertTime3_4Command(controller));
            //Commands.Add(new InsertTime6_8Command(controller));
            

            Commands.Add(new OpenFileCommand(fileHandler));
            Commands.Add(new SaveAsPDFCommand(fileHandler));
            Commands.Add(new SaveAsLilypondCommand(fileHandler));
        }

        public bool CanExecuteCommand(string keys)
        {
            foreach (ICommand command in Commands)
            {
                if (keys.Contains(command.pattern))
                {
                    ExecutableCommand = command;
                    return true;
                }
            }

            return false;
        }

        public void ExecuteCommand()
        {
            ExecutableCommand.execute();
        }

        public void SwitchState()
        {
            fileHandler.CurrentState = new PlayState(fileHandler);
        }
    }
}
