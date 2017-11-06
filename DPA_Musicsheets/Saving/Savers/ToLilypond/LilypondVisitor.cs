using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using DPA_Musicsheets.enums;
using DPA_Musicsheets.Saving.Savers.ToLilypond.Symbolhandlers;

namespace DPA_Musicsheets.Saving.Savers.ToLilypond
{
    class LilypondVisitor : IVisitor
    {
        public string data { get; private set; }
        private List<ISymbolHandler> symbolHandlers;
        private Dictionary<ClefType, String> clefTypeLookup;

        public LilypondVisitor()
        {
            data = "\\relative c' { ";
            symbolHandlers = new List<ISymbolHandler>();
            symbolHandlers.Add(new LinkHandler());
            symbolHandlers.Add(new ToonhoogteHandler());
            symbolHandlers.Add(new MolKruisHandler());
            symbolHandlers.Add(new ApostrofHandler());
            symbolHandlers.Add(new KommaHandler());
            symbolHandlers.Add(new TimeHandler());
            symbolHandlers.Add(new PuntHandler());

            clefTypeLookup = new Dictionary<ClefType, string>();
            clefTypeLookup.Add(ClefType.GClef, "treble");

            //data += "\\clef "  + clefTypeLookup[ClefType.GClef] + " ";
        }

        public void finish()
        {
            data += "}";
        }

        public void visit(Note noot)
        {
            string tempNoot = "";

            for (int i = 0; i < symbolHandlers.Count; i++)
            {
                tempNoot += symbolHandlers[i].Handle(noot);
            }

            data += tempNoot + " ";
        }

        public void visit(RustNote rustNode)
        {
            string tempNoot = "r" + rustNode.duur;
            data += tempNoot + " ";
        }

        public void visit(Clef clef)
        {
            data += "\\clef " + " " + clefTypeLookup[clef.cleftype] + " ";
        }

        public void visit(TimeSignature timeSignature)
        {
            data += "\\time " + timeSignature.timeSignature[0] + "/" + timeSignature.timeSignature[1] + " ";
        }

        public void visit(MaatStreep maatstreep)
        {
            data += "| ";
        }

        public void visit(Tempo tempo)
        {
            data += "\\tempo " + tempo.nootLength + "=" + tempo.tempo + " ";
        }

        public void visit(Repeater repeater)
        {
            data += "\\repeat volta " + repeater.repeats + " { ";
            LinkedListNode<IMusicSymbol> currentNote = repeater.items.First;

            while (currentNote != null)
            {
                IMusicSymbol symbol = currentNote.Value;
                symbol.accept(this);
                currentNote = currentNote.Next;
            }

            data += " }";
        }
    }
}
