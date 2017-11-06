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
    class LinkHandler : ISymbolHandler
    {
        public string Handle(Note noot)
        {
            if (noot.tied == TieType.stop)
            {
                return "~";
            }

            return "";
        }
    }
}
