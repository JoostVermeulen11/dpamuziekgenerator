using DPA_Musicsheets.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers.NoteAttributeHandlers
{
    class TypeHandler : INoteAttributeHandler
    {
        private Dictionary<string, double> noteLengteLookup = new Dictionary<string, double>();

        public INoteAttributeHandler clone()
        {
            return new TypeHandler();
        }

        public TypeHandler()
        {
            noteLengteLookup.Add("whole", 1);
            noteLengteLookup.Add("half", 2);
            noteLengteLookup.Add("quarter", 4);
            noteLengteLookup.Add("eighth", 8);
            noteLengteLookup.Add("16th", 16);
            noteLengteLookup.Add("d32nd", 32);
        }
        
        public void handle(AbstractNote note, XElement element)
        {
            note.duur = noteLengteLookup[element.Value.ToString()];
        }
    }
}
