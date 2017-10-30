using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using DPA_Musicsheets.MIDI.MIDIMessageHandler.MIDITypeHandler;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler
{
    class MetaHandler : IMessageTypeHandler
    {
        public IMessageTypeHandler clone()
        {
            return new MetaHandler();
        }

        public void handle(Context context, MidiEvent midiEvent)
        {
            var metaMessage = midiEvent.MidiMessage as MetaMessage;
            IMidiTypeHandler handler = MidiTypeHandlerFactory.getHandler(metaMessage.MetaType);

            if (handler != null)
            {
                handler.handle(context, midiEvent);
            }
        }
    }
}
