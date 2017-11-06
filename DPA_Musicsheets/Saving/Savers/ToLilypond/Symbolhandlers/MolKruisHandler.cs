using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using DPA_Musicsheets.enums;

namespace DPA_Musicsheets.Saving.Savers.ToLilypond.Symbolhandlers
{
    class MolKruisHandler : ISymbolHandler
    {
        private Dictionary<NoteItem, string> noteItemLookup = new Dictionary<NoteItem, string>();

        public MolKruisHandler()
        {
            noteItemLookup.Add(NoteItem.Mol, "es");
            noteItemLookup.Add(NoteItem.Kruis, "is");
            noteItemLookup.Add(NoteItem.Geen, "");
        }
        public string Handle(Note noot)
        {
            return noteItemLookup[noot.nootItem];
        }
    }
}
