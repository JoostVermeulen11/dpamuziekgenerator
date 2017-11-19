using DPA_Musicsheets.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers.NoteAttributeHandlers
{
    interface INoteAttributeHandler
    {
        INoteAttributeHandler clone();

        void handle(AbstractNote note, XElement value);
    }
}
