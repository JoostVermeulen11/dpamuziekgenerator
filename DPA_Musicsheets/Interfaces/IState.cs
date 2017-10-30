using DPA_Musicsheets.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Interfaces
{
    interface IState
    {
        List<ICommand> Commands { get; set; }
        StateType Type { get; set; }

        bool CanExecuteCommand(string keys);

        void ExecuteCommand();

        void SwitchState();
    }
}
