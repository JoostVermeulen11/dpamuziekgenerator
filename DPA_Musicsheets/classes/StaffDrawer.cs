using DPA_Musicsheets.Adapter;
using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Visitor;
using PSAMControlLibrary;
using PSAMWPFControlLibrary;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.classes
{
    class StaffDrawer : INoteObserver
    {        
        StaffVisitor visitor;
        List<MusicalSymbol> staff;
        public Sequence PlayableSequence;
        public SequenceAdapter sequenceConverter;

        public StaffDrawer(List<MusicalSymbol> staff)
        {
            this.staff = staff;
            visitor = new StaffVisitor(staff);
            PlayableSequence = new Sequence();
            sequenceConverter = new SequenceAdapter();
        }

        public void update(MusicSheet data)
        {
            visitor.updateStaff();

            foreach (IMusicSymbol item in data.items)
            {
                item.accept(visitor);
            }

            PlayableSequence = sequenceConverter.GetSequenceFromSymbolList(visitor.getSymbolList());
        }



    }
}
