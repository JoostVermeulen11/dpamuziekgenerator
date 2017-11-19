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
        public IElementHandler clone()
        {
            return new AttributesHandler();
        }

        public void handle(Context context, XElement xml)
        {
           
        }
    }
}
