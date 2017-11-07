using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using System.IO;

namespace DPA_Musicsheets.MIDI
{
    class MidiFileReader : IInputReader
    {
        public IInputReader clone()
        {
            return new MidiFileReader();
        }

        public string GetText(string fileName)
        {
            throw new Exception("Staat nergens dat we Midi moeten kunnen bewerken :)");
        }

        public MusicSheet readNotes(string data)
        {
            MidiAdapter midiAdapter = new MidiAdapter();
            return midiAdapter.ProcessMidi(data);
        }
    }
}
