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
    class NoteHandler : ISymbolHandler
    {
        //maak een lijst met handlers, zodat dit met chain of responsibility gedaan kan worden
        private Dictionary<string, INoteAttributeHandler> NoteAttributeHandlers;

        public ISymbolHandler clone()
        {
            return new NoteHandler();
        }

        public NoteHandler()
        {
            NoteAttributeHandlers = new Dictionary<string, INoteAttributeHandler>();
            NoteAttributeHandlers.Add("step", new StepHandler());
            NoteAttributeHandlers.Add("octave", new OctaveHandler());
            NoteAttributeHandlers.Add("type", new TypeHandler());
            NoteAttributeHandlers.Add("alter", new AlterHandler());
            NoteAttributeHandlers.Add("dot", new DotHandler());
        }

        public void handle(Context context, XElement xml)
        {
            IEnumerable<XElement> Elements = xml.Elements();
            Note noot = new Note();

            foreach (XElement childElement in Elements)
            {
                if (childElement.Elements().Count() > 1)
                {
                    //regel dit hier verder
                    foreach (XElement child in childElement.Elements())
                    {
                        if (NoteAttributeHandlers.ContainsKey(child.Name.ToString()))
                        {
                            NoteAttributeHandlers[child.Name.ToString()].clone().handle(noot, child);
                        }
                    }
                }
                else
                {
                    if (NoteAttributeHandlers.ContainsKey(childElement.Name.ToString()))
                    {
                        NoteAttributeHandlers[childElement.Name.ToString()].clone().handle(noot, childElement);
                    }
                }
            }

            context.musicSheet.addmusicSymbol(noot);
        }
    }
}
