using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.enums;
using DPA_Musicsheets.Managers;

namespace DPA_Musicsheets.States
{
    class EditState : IState
    {
        private FileHandler fileHandler; 
        public EditState(FileHandler fileHandler)
        {
            Type = StateType.Edit;
            this.fileHandler = fileHandler;

            //controller.SetEditText(controller.GetLilypond());

            Commands = new List<ICommand>();

            //Commands.Add(new InsertBarLinesCommand(controller));
            //Commands.Add(new InsertClefCommand(controller));
            //Commands.Add(new InsertTempoCommand(controller));
            //Commands.Add(new InsertTimeCommand(controller));
            //Commands.Add(new InsertTime3_4Command(controller));
            //Commands.Add(new InsertTime6_8Command(controller));
            //Commands.Add(new OpenFileCommand(controller));
            //Commands.Add(new SaveAsPDFCommand(controller));
            //Commands.Add(new SaveAsLilypondCommand(controller));

        }

        public List<ICommand> Commands { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public StateType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CanExecuteCommand(string keys)
        {
            throw new NotImplementedException();
        }

        public void ExecuteCommand()
        {
            throw new NotImplementedException();
        }

        public void SwitchState()
        {
            throw new NotImplementedException();
        }
    }
}
