using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    class Tempo : IMusicSymbol
    {
        public int tempo { set; get; }
        public int nootLength { set; get; }

        public Tempo(int temp, int nootLength)
        {
            this.tempo = temp;
            this.nootLength = nootLength;
        }

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }

        public MusicType getType()
        {
            return MusicType.Tempo;
        }
    }
}
