using DPA_Musicsheets.classes;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MusicXml
{
    class Context
    {
        public bool EndOfSheet { get; set; }
        public MusicSheet musicSheet { get; set; }
        public Sequence _sequence { get; set; }
        public int Divisions { get; set; }

        public Context()
        {
            musicSheet = new MusicSheet();
            EndOfSheet = false;
        }
    }
}
