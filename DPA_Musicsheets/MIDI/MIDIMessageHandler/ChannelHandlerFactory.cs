using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MIDI.MIDIMessageHandler
{
    class ChannelHandlerFactory
    {
        static Dictionary<MessageType, IMessageTypeHandler> handlers;
        static ChannelHandlerFactory()
        {
            handlers = new Dictionary<MessageType, IMessageTypeHandler>();
            handlers.Add(MessageType.Channel, new ChannelHandler());
            handlers.Add(MessageType.Meta, new MetaHandler());
        }

        public static IMessageTypeHandler getHandler(MessageType type)
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
