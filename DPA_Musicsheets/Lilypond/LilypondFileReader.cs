using DPA_Musicsheets.classes;
using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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

        public String GetText(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in File.ReadAllLines(fileName))
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
