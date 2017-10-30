using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    class TimeSignature : IMusicSymbol
    {
        public int[] timeSignature = new int[2];

        public TimeSignature(int[] timeSignature)
        {
            this.timeSignature = timeSignature;
        }

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
            
        public MusicType getType()
        {
            return MusicType.TimeSignature;
        }
    }
}
