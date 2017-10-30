using DPA_Musicsheets.classes;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Lilypond
{
    class LilypondFileReader : IInputReader
    {
        public IInputReader clone()
        {
            return new LilypondFileReader();
        }

        public MusicSheet readNotes(string fileLocation)
        {
            LilypondReader reader = new LilypondReader();
            return reader.readNotes(System.IO.File.ReadAllText(fileLocation));
        }
    }
}
