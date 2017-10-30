using DPA_Musicsheets.enums;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    class Note : AbstractNote, IMusicSymbol
    {
        public Note(int octaaf, String toonHoogte, double duur, NoteItem item, TieType tied)
        {
            setOctaaf(octaaf);
            setDuur(duur);
            setNootItem(item);
            setToonhoogte(toonHoogte);
            setTied(tied);
        }

        public Note()
        {
            tied = TieType.None;
        }

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }

        public MusicType getType()
        {
            return MusicType.Note;
        }
    }
}
