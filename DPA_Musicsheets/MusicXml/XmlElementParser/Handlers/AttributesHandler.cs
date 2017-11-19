using DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.AttributeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser
{
    class AttributesHandler : IElementHandler
    {
        private Dictionary<string, IAttributeHandler> AttributeHandlers;

        public AttributesHandler()
        {
            AttributeHandlers = new Dictionary<string, IAttributeHandler>();
            AttributeHandlers.Add("divisions", new DivisionHandler());
            AttributeHandlers.Add("time", new TimeSignatureHandler());
            AttributeHandlers.Add("clef", new ClefHandler());
        }

        public IElementHandler clone()
        {
            return new AttributesHandler();
        }

        public void handle(Context context, XElement xml)
        {
            foreach (XElement childElement in xml.Elements())
            {
                if (AttributeHandlers.ContainsKey(childElement.Name.ToString()))
                {
                    AttributeHandlers[childElement.Name.ToString()].clone().handle(context, childElement);
                }
            }
        }
    }
}
