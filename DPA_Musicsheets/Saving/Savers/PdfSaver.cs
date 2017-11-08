using DPA_Musicsheets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.classes;
using System.Diagnostics;
using System.IO;

namespace DPA_Musicsheets.Saving.Savers
{
    class PdfSaver : ISave
    {
        public ISave clone()
        {
            return new PdfSaver();
        }

        public void save(string textToSave, string fileLocation)
        {
            string lilypondLocation = @"C:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
            LilypondSaver lilypondSaver = new LilypondSaver();
            lilypondSaver.save(textToSave, fileLocation);   
            
            String[] tempArr = fileLocation.Split('\\');
            string sourcefile = "";
            for (int i = 0; i < tempArr.Length - 1; i++)
            {
                sourcefile += tempArr[i] + "\\";
            }

            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = sourcefile,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = String.Format("--pdf \"{0}\"", tempArr[tempArr.Length - 1]),
                    FileName = lilypondLocation
                }
            };

            process.Start();
            process.WaitForExit();
            File.Delete(sourcefile + tempArr[tempArr.Length - 1] + ".ly");
        }      
    }
}
