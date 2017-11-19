using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using PSAMControlLibrary;
using PSAMWPFControlLibrary;
using DPA_Musicsheets.Adapter;

namespace DPA_Musicsheets.Visitor
{
    class StaffVisitor : IVisitor
    {        
        ClefAdapter clefAdapter;
        NootAdapter nootAdapter;
        List<MusicalSymbol> staff;

        public StaffVisitor(List<MusicalSymbol> staff)
        {
            this.staff = staff;
            clefAdapter = new ClefAdapter();
            nootAdapter = new NootAdapter();
        }

        public void updateStaff()
        {
            MusicalSymbol symbol = clefAdapter.ModelToLibrary(new DPA_Musicsheets.classes.Clef());
            staff.Add(symbol);
        }

        public void visit(DPA_Musicsheets.classes.Note noot)
        {
            MusicalSymbol symbol = nootAdapter.NootModelToLibrary(noot);
            staff.Add(symbol);
        }

        public void visit(RustNote rustNode)
        {
            MusicalSymbol symbol = nootAdapter.RestModelToLibrary(rustNode);
            staff.Add(symbol);
        }

        public void visit(DPA_Musicsheets.classes.Clef clef)
        {
            if(staff.First().Type != MusicalSymbolType.Clef)
            {
                MusicalSymbol symbol = clefAdapter.ModelToLibrary(clef);
                staff.Add(symbol);
            }
        }

        public void visit(DPA_Musicsheets.classes.TimeSignature timeSignature)
        {
            MusicalSymbol symbol = new PSAMControlLibrary.TimeSignature(TimeSignatureType.Numbers, Convert.ToUInt32(timeSignature.timeSignature[0]), Convert.ToUInt32(timeSignature.timeSignature[1]));
            staff.Add(symbol);
        }

        public void visit(MaatStreep maatstreep)
        {
            staff.Add(new Barline());
        }

        public void visit(Tempo tempo)
        {
            
        }

        public void visit(Repeater repeater)
        {
            LinkedListNode<IMusicSymbol> currentNote = repeater.items.First;

            while (currentNote != null)
            {
                IMusicSymbol symbol = currentNote.Value;
                symbol.accept(this);
                currentNote = currentNote.Next;
            }

            Barline b = new Barline();
            b.RepeatSign = RepeatSignType.Backward;

            staff.Add(b);
        }

        public List<MusicalSymbol> getSymbolList()
        {
            return staff;
        }
    }
}
