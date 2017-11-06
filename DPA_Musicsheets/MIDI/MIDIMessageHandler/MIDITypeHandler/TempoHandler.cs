using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using DPA_Musicsheets.classes;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler.MIDITypeHandler
{
    class TempoHandler : IMidiTypeHandler
    {
        public IMidiTypeHandler clone()
        {
            return new TempoHandler();
        }

        public void handle(Context context, MidiEvent midiEvent)
        {
            IMidiMessage midiMessage = midiEvent.MidiMessage;
            var metaMessage = midiMessage as MetaMessage;

            byte[] bytes = metaMessage.GetBytes();
            int tempo = (bytes[0] & 0xff) << 16 | (bytes[1] & 0xff) << 8 | (bytes[2] & 0xff);
            int bpm = 60000000 / tempo;
            Tempo tempoObj = new Tempo(bpm, 4);
            context.musicSheet.addmusicSymbol(tempoObj);
        }
    }
}
