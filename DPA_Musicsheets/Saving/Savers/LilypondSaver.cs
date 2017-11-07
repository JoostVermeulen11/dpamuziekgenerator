using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using System.IO;

namespace DPA_Musicsheets.Saving.Savers
{
    class LilypondSaver : ISave
    {
        public ISave clone()
        {
            return new LilypondSaver();
        }

        public void save(string textToSave, string fileLocation)
        {
            using (StreamWriter outputFile = new StreamWriter(fileLocation + ".ly"))
            {
                outputFile.Write(textToSave);
                outputFile.Close();
            }
        }
    }
}
