using DPA_Musicsheets.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.AttributeHandlers
{
    class TimeSignatureHandler : IAttributeHandler
    {
        public IAttributeHandler clone()
        {
            return new TimeSignatureHandler();
        }

        public void handle(Context context, XElement xml)
        {
            int[] timeSignature = new int[] { Convert.ToInt16(xml.Elements("beats").FirstOrDefault().Value), Convert.ToInt16(xml.Elements("beat-type").FirstOrDefault().Value) };
            context.musicSheet.items.AddFirst(new TimeSignature(timeSignature));
        }
    }
}
