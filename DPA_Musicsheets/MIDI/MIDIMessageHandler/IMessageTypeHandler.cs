using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler
{
    interface IMessageTypeHandler
    {
        IMessageTypeHandler clone();

        void handle(Context context, MidiEvent midiEvent);
    }
}
