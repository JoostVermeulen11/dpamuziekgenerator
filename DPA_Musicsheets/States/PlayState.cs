using DPA_Musicsheets.Interfaces;
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
    class PlayState : IState
    {
        public List<ICommand> Commands { get; set; }
        public StateType Type { get; set; }
        public ICommand ExecutableCommand { get; set; }

        private FileHandler fileHandler;

        public PlayState(FileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
            this.Type = StateType.Play;

            Commands = new List<ICommand>();

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
            fileHandler.CurrentState = new EditState(fileHandler);
        }
    }
}
