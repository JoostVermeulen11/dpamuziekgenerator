using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Interfaces
{
    interface IMusicSymbol
    {
        void accept(IVisitor visitor);
    }
}
