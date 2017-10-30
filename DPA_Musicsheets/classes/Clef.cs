using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    class Clef : IMusicSymbol
    {
        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }

        public ClefType cleftype { get; set; }
        public int location { get; set; }

        public Clef()
        {
            cleftype = ClefType.GClef;
            location = 2;
        }

        public Clef(ClefType type, int location)
        {
            this.cleftype = type;
            this.location = location;
        }

        public MusicType getType()
        {
            return MusicType.Clef;
        }
    }
}
