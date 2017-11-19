using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.AttributeHandlers
{
    class DivisionHandler : IAttributeHandler
    {
        public IAttributeHandler clone()
        {
            return new DivisionHandler();
        }

        public void handle(Context context, XElement xml)
        {
            context.Divisions = Convert.ToInt16(xml.Value);
        }
    }
}
