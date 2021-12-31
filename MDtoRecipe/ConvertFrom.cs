using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBM.PowerShell.Lib.MDtoRecipe;

namespace MDtoRecipe
{
    internal class ConvertFrom
    {
        public string Source { get; set; }
        public string Output { get; set; }
        public string[] Exclude { get; set; }

        public void MainProcessStart(Setting setting)
        {
            this.Source = setting.Source;
            this.Output = setting.Output;
            this.Exclude = setting.Exclude;
        }

        public void MainProcess()
        {
            PageCollection pages = new PageCollection();
            foreach (string filePath in Directory.GetFiles(Source, "*.md", SearchOption.AllDirectories))
            {
                if (Exclude?.Any(x => x.Equals(filePath, StringComparison.OrdinalIgnoreCase)) ?? false)
                {
                    continue;
                }
                pages.Add(filePath);
            }
            foreach (var recipe in pages.ToRecipeFileList())
            {
                recipe.Write(Output);
            }
        }
    }
}
