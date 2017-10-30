using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler.MIDITypeHandler
{
    class TimeSignatureHandler : IMidiTypeHandler
    {
        public IMidiTypeHandler clone()
        {
            return new TimeSignatureHandler();
        }

        public void handle(Context context, MidiEvent midiEvent)
        {
            IMidiMessage midiMessage = midiEvent.MidiMessage;
            var metaMessage = midiMessage as MetaMessage;

            int[] TimeSignature = new int[2];
            byte[] timeSignatureBytes = metaMessage.GetBytes();
            context._beatNote = timeSignatureBytes[0];
            context._beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));

            TimeSignature[0] = timeSignatureBytes[0];
            TimeSignature[1] = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
            context.currentTimesignature = new classes.TimeSignature(TimeSignature);
            context.musicSheet.addmusicSymbol(context.currentTimesignature);
        }
    }
}
