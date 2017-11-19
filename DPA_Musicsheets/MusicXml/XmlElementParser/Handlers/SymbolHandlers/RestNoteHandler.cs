using DPA_Musicsheets.classes;
using DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers.NoteAttributeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers
{
    class RestNoteHandler : ISymbolHandler
    {
        private Dictionary<string, INoteAttributeHandler> NoteAttributeHandlers;

        public RestNoteHandler()
        {
            NoteAttributeHandlers = new Dictionary<string, INoteAttributeHandler>();
            NoteAttributeHandlers.Add("type", new TypeHandler());
        }

        public ISymbolHandler clone()
        {
            return new RestNoteHandler();
        }

        public void handle(Context context, XElement xml)
        {
            IEnumerable<XElement> Elements = xml.Elements();
            RustNote noot = new RustNote();

            foreach (XElement childElement in Elements)
            {
                if(childElement.Name.ToString() == "duration")
                {
                    noot.duur = Convert.ToInt16(childElement.Value);
                }
                if (NoteAttributeHandlers.ContainsKey(childElement.Name.ToString()))
                {
                    NoteAttributeHandlers[childElement.Name.ToString()].clone().handle(noot, childElement);
                }
            }

            context.musicSheet.addmusicSymbol(noot);
        }
    }
}
