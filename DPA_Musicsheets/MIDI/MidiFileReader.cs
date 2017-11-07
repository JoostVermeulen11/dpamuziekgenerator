using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;

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
            throw new NotImplementedException();
        }

        public MusicSheet readNotes(string data)
        {
            MidiAdapter midiAdapter = new MidiAdapter();
            return midiAdapter.ProcessMidi(data);
        }
    }
}
