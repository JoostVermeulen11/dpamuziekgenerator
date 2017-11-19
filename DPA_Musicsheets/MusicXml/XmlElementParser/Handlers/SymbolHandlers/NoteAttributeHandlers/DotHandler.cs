using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DPA_Musicsheets.classes;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers.NoteAttributeHandlers
{
    class DotHandler : INoteAttributeHandler
    {
        public INoteAttributeHandler clone()
        {
            return new DotHandler();
        }

        public void handle(AbstractNote note, XElement value)
        {
            note.punten = 1;
        }
    }
}
