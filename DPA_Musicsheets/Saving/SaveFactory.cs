using DPA_Musicsheets.Interfaces;
using DPA_Musicsheets.Saving.Savers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Saving
{
    class SaveFactory
    {
        static Dictionary<String, ISave> saver;
        static SaveFactory()
        {
            saver = new Dictionary<String, ISave>();
            saver.Add("lilypond", new LilypondSaver());
            saver.Add("pdf", new PdfSaver());
        }

        public static ISave getSaver(string readerName)
        {
            if (saver.ContainsKey(readerName))
            {
                return saver[readerName].clone();
            }
            else
            {
                //De saver bestaat niet
                return null;
            }
        }
    }
}
