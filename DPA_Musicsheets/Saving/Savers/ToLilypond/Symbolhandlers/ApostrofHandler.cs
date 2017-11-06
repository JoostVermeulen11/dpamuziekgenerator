﻿using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;

namespace DPA_Musicsheets.Saving.Savers.ToLilypond.Symbolhandlers
{
    class ApostrofHandler : ISymbolHandler
    {
        public string Handle(Note noot)
        {
            string temp = "";
            for (int i = 0; i < noot.apostrof; i++)
            {
                temp += "'";
            }

            return temp;
        }
    }
}