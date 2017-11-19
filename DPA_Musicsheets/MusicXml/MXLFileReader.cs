using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;

namespace DPA_Musicsheets.MusicXml
{
    class MXLFileReader : IInputReader
    {
        public IInputReader clone()
        {
            return new MXLFileReader();
        }

        public string GetText(string fileName)
        {
            return "";
        }

        public MusicSheet readNotes(string fileName)
        {
            MusicXmlReader reader = new MusicXmlReader();
            string xmlText = "";
            FileInfo f = new FileInfo(fileName);
            FileStream originalFileStream = f.OpenRead();

            ZipArchive z = new ZipArchive(originalFileStream, ZipArchiveMode.Read);
            foreach (ZipArchiveEntry e in z.Entries)
            {
                if (!e.Name.Contains("container.xml"))
                {
                    e.ExtractToFile(Path.Combine(System.IO.Path.GetTempPath(), "temp.mxl"), true);
                    xmlText = File.ReadAllText(Path.Combine(System.IO.Path.GetTempPath(), "temp.mxl"));
                    break;
                }
            }
            
            return reader.readNotes(xmlText);
        }
    }
}
