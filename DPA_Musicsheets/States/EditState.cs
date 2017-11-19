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

            Commands = new List<ICommand>();

            fileHandler.SetEditText(fileHandler.GetFileName());
                      
            Commands.Add(new InsertTime34Command(fileHandler));
            Commands.Add(new InsertTime44Command(fileHandler));
            Commands.Add(new InsertTime68Command(fileHandler));
            Commands.Add(new InsertTempoCommand(fileHandler));
            Commands.Add(new InsertClefCommand(fileHandler));
            Commands.Add(new OpenFileCommand(fileHandler));
            Commands.Add(new SaveAsPDFCommand(fileHandler));
            Commands.Add(new SaveAsLilypondCommand(fileHandler));
        }

        public bool CanExecuteCommand(string keys)
        {
            foreach (ICommand command in Commands)
            {
                if (keys.Contains(command.pattern) || keys.Contains(command.commandName))
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
            if (ExecutableCommand.commandName != "OpenPDF" && ExecutableCommand.commandName != "Lilypond" && ExecutableCommand.commandName != "Pdf")
            {
                fileHandler.memento.NewNode(fileHandler.EditorText);
            }

        }

        public void SwitchState()
        {
            PlayState playState = new PlayState(fileHandler);
            fileHandler.CurrentState = playState;
            fileHandler.CurrentStateText = playState.getEditString();
        }

        public string getEditString()
        {
            return "Edit";
        }
    }
}
