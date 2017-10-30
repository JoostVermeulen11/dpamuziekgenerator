using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Interfaces
{
    interface ICommand
    {
        string pattern { get; }
        void execute();
    }
}
