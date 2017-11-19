using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Lilypond;
using DPA_Musicsheets.MIDI;
using DPA_Musicsheets.MusicXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Factories
{
    class ReaderFactory
    {
        static Dictionary<String, IInputReader> readers;
        static ReaderFactory()
        {
            readers = new Dictionary<String, IInputReader>();
            readers.Add(".mid", new MidiFileReader());
            readers.Add(".mxl", new MXLFileReader());
            readers.Add("xmlreader", new MusicXmlReader());
            readers.Add(".ly", new LilypondFileReader());
            readers.Add("lilypond", new LilypondReader());            
        }

        public static IInputReader getReader(string readerName)
        {
            if (readers.ContainsKey(readerName))
            {
                return readers[readerName].clone();
            }
            else
            {
                throw new NotSupportedException($"File extension {readerName} is not supported.");
            }
        }
    }
}
