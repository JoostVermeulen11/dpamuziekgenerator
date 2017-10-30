using DPA_Musicsheets.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    abstract class AbstractNote
    {
        public int octaaf;
        public String toonHoogte;
        public double duur;
        public NoteItem nootItem = NoteItem.Geen;
        public TieType tied;
        public int punten;
        public int kommas { get; set; }
        public int apostrof { get; set; }

        public TieType isTied()
        {
            return tied;
        }

        public int getOctaaf()
        {
            return octaaf;
        }

        public String getToonhoogte()
        {
            return toonHoogte;
        }

        public double getDuur()
        {
            return duur;
        }

        public NoteItem getNootItem()
        {
            return nootItem;
        }
        public void setTied(TieType tied)
        {
            this.tied = tied;
        }

        public void setOctaaf(int octaaf)
        {
            this.octaaf = octaaf;
        }

        public void setToonhoogte(String toonHoogte)
        {
            this.toonHoogte = toonHoogte;
        }

        public void setDuur(double duur)
        {
            this.duur = duur;
        }

        public void setNootItem(NoteItem item)
        {
            this.nootItem = item;
        }
    }
}
