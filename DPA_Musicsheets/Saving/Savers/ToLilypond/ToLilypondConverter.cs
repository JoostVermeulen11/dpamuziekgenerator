using DPA_Musicsheets.classes;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Saving.Savers.ToLilypond
{
    class ToLilypondConverter
    {
        public String ToLilypond(MusicSheet musicSheet)
        {
            LilypondVisitor visitor = new LilypondVisitor();

            foreach (IMusicSymbol item in musicSheet.items)
            {
                item.accept(visitor);
            }

            visitor.finish();

            return visitor.data;
        }
    }
}
