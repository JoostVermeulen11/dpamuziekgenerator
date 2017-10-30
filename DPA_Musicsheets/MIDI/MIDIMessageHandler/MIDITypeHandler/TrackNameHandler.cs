using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler.MIDITypeHandler
{
    class TrackNameHandler : IMidiTypeHandler
    {
        public IMidiTypeHandler clone()
        {
            return new TrackNameHandler();
        }

        public void handle(Context context, MidiEvent midiEvent)
        {
            IMidiMessage midiMessage = midiEvent.MidiMessage;
            var metaMessage = midiMessage as MetaMessage;

            context.musicSheet.Name = Encoding.Default.GetString(metaMessage.GetBytes());
        }
    }
}
