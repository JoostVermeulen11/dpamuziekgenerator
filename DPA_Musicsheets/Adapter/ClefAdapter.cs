using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Adapter
{
    class ClefAdapter
    {
        public PSAMControlLibrary.Clef ModelToLibrary(DPA_Musicsheets.classes.Clef modelclef)
        {
            PSAMControlLibrary.Clef c;

            switch (modelclef.cleftype)
            {
                case DPA_Musicsheets.enums.ClefType.CClef:
                    c = new PSAMControlLibrary.Clef(PSAMControlLibrary.ClefType.CClef, modelclef.location);
                    return c;

                case DPA_Musicsheets.enums.ClefType.GClef:
                    c = new PSAMControlLibrary.Clef(PSAMControlLibrary.ClefType.GClef, modelclef.location);
                    return c;

                case DPA_Musicsheets.enums.ClefType.FClef:
                    c = new PSAMControlLibrary.Clef(PSAMControlLibrary.ClefType.FClef, modelclef.location);
                    return c;
                default:
                    return null;
            }
        }
    }
}
