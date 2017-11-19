using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DPA_Musicsheets.classes;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers.NoteAttributeHandlers
{
    class AlterHandler : INoteAttributeHandler
    {
        public INoteAttributeHandler clone()
        {
            return new AlterHandler();
        }

        // kijken of het een mol of kruis is
        public void handle(AbstractNote note, XElement value)
        {
            if (Convert.ToInt16(value.Value) > 0)
            {
                note.nootItem = enums.NoteItem.Kruis;
            }
            else if(Convert.ToInt16(value.Value) < 0)
            {
                note.nootItem = enums.NoteItem.Mol;
            }
        }
    }
}
