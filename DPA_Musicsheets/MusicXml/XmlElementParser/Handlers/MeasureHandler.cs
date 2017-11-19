using DPA_Musicsheets.classes;
using DPA_Musicsheets.MusicXml.XmlElementParser.Handlers.SymbolHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPA_Musicsheets.MusicXml.XmlElementParser
{
    class MeasureHandler : IElementHandler
    {
        public IElementHandler clone()
        {
            return new MeasureHandler();
        }

        public void handle(Context context, XElement xml)
        {
            foreach (XElement childElement in xml.Elements("note"))
            {
                //process elke noot afzonderlijk, en voeg deze daarna toe aan de musicsheet.
                if (childElement.Elements("rest").Count() > 0)
                {
                    //new restnotehandler
                    new RestNoteHandler().handle(context, childElement);
                }
                else
                {
                    new NoteHandler().handle(context, childElement);
                }
            }
            //als alle noten van de measure zijn geprocessed, kan er dus een maatstreep toe worden gevoegd.
            context.musicSheet.addmusicSymbol(new MaatStreep());
        }
    }
}
