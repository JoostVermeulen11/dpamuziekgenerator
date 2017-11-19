using DPA_Musicsheets.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers.NoteAttributeHandlers
{
    class StepHandler : INoteAttributeHandler
    {
        public INoteAttributeHandler clone()
        {
            return new StepHandler();
        }

        public void handle(AbstractNote note, XElement element)
        {
            note.toonHoogte = element.Value.ToString();
        }
    }
}
