using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    class MusicSheet
    {
        public LinkedList<IMusicSymbol> items { get; set; }
        public string Name { get; set; }
        public int startOctaaf { get; set; }

        public MusicSheet()
        {
            items = new LinkedList<IMusicSymbol>();
        }

        public void addmusicSymbol(IMusicSymbol symbol)
        {
            items.AddLast(symbol);
        }
    }
}
