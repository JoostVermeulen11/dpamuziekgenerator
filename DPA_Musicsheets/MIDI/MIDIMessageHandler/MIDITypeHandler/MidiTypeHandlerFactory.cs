using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler.MIDITypeHandler
{
    class MidiTypeHandlerFactory
    {
        static Dictionary<MetaType, IMidiTypeHandler> handlers;
        static MidiTypeHandlerFactory()
        {
            handlers = new Dictionary<MetaType, IMidiTypeHandler>();
            handlers.Add(MetaType.Tempo, new TempoHandler());
            handlers.Add(MetaType.TrackName, new TrackNameHandler());
            handlers.Add(MetaType.TimeSignature, new TimeSignatureHandler());
        }

        public static IMidiTypeHandler getHandler(MetaType type)
        {
            if (handlers.ContainsKey(type))
            {
                return handlers[type].clone();
            }
            else
            {
                //De reader bestaad niet
                return null;
            }
        }
    }
}
