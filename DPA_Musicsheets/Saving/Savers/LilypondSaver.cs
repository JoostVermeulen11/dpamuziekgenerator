using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using DPA_Musicsheets.Saving.Savers.ToLilypond;

namespace DPA_Musicsheets.Saving.Savers
{
    class LilypondSaver : ISave
    {
        public ISave clone()
        {
            return new LilypondSaver();
        }

        public void save(MusicSheet musicsheet, string fileLocation)
        {
            ToLilypondConverter converter = new ToLilypondConverter();
            string data = converter.ToLilypond(musicsheet);
            System.IO.File.WriteAllText(fileLocation + ".ly", data);
        }
    }
}
