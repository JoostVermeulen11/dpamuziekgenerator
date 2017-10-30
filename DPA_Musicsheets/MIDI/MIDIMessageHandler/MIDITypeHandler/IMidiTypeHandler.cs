using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler.MIDITypeHandler
{
    interface IMidiTypeHandler
    {
        void handle(Context context, MidiEvent midiEvent);
        IMidiTypeHandler clone();
    }
}
