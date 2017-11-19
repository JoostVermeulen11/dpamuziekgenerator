using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using System.Xml.Linq;
using System.Xml;
using DPA_Musicsheets.MusicXml.XmlElementParser;

namespace DPA_Musicsheets.MusicXml
{
    class MusicXmlReader : IInputReader
    {
        private Context context;

        public IInputReader clone()
        {
            return new MusicXmlReader();
        }

        public MusicXmlReader()
        {
            context = new Context();
        }

        public string GetText(string fileName)
        {
            return "";
        }
                
        public MusicSheet readNotes(string data)
        {
            
            XDocument doc = XDocument.Parse(data);

            foreach (var node in doc.Elements().Descendants())
            {                
                //string abc = node.Name.ToString();
                IElementHandler handler = ElementHandlerFactory.getHandler(node.Name.ToString());

                if (handler != null)
                {
                    handler.handle(context, node);
                }                
            }

            return context.musicSheet;
        }       
    }
}
