using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MusicXml.XmlElementParser
{
    class ElementHandlerFactory
    {
        static Dictionary<string, IElementHandler> handlers;
        static ElementHandlerFactory()
        {
            handlers = new Dictionary<string, IElementHandler>();
            handlers.Add("attributes", new AttributesHandler());
            handlers.Add("measure", new MeasureHandler());
        }

        public static IElementHandler getHandler(string xmlString)
        {
            if (handlers.ContainsKey(xmlString))
            {
                return handlers[xmlString].clone();
            }
            else
            {                
                //De handler bestaad niet
                return null;
            }
        }
    }
}
