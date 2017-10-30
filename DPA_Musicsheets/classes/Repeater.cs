using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    class Repeater : IMusicSymbol
    {
        public LinkedList<IMusicSymbol> items { get; set; }
        public int repeats { get; set; }

        public Repeater()
        {
            items = new LinkedList<IMusicSymbol>();
        }

        public void addmusicSymbol(IMusicSymbol symbol)
        {
            items.AddLast(symbol);
        }


        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
