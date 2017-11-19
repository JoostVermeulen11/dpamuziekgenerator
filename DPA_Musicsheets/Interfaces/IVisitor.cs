using DPA_Musicsheets.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Interfaces
{
    interface IVisitor
    {
        void visit(Note noot);
        void visit(RustNote rustNode);
        void visit(Clef clef);
        void visit(TimeSignature timeSignature);
        void visit(MaatStreep maatstreep);
        void visit(Tempo tempo);
        void visit(Repeater repeater);
        void visit(EndOfTrack endOfTrack);
    }
}
