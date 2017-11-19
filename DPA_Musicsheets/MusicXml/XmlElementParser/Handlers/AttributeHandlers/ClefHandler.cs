using DPA_Musicsheets.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.AttributeHandlers
{
    class ClefHandler : IAttributeHandler
    {
        public IAttributeHandler clone()
        {
            return new ClefHandler();
        }

        public void handle(Context context, XElement xml)
        {
            Clef clef = new Clef();
            clef.cleftype = enums.ClefType.GClef;
            context.musicSheet.items.AddFirst(clef);
        }
    }
}
