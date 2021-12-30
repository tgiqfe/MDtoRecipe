using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinBM.PowerShell.Lib.MDtoRecipe
{
    internal class RecipeFile
    {
        public string FileName { get; set; }
        public string Content { get; set; }

        public void Write(string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            using (var sw = new StreamWriter(Path.Combine(outputDirectory, FileName), false, Encoding.UTF8))
            {
                sw.WriteLine(this.Content);
            }
        }
    }
}
