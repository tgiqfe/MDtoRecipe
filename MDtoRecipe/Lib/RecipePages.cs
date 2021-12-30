using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MDtoRecipe.Lib
{
    internal class RecipePages
    {
        public Dictionary<string, List<RecipePagePart>> Collection { get; set; }

        private int _Index { get; set; }

        private Regex pattern_ymlStart = new Regex(@"```ya?ml[\s:].+\.ya?ml$");
        private string pattern_ymlEnd = "```";
        private Regex pattern_yamlFileName = new Regex(@"(?<=```ya?ml[\s:]).+");

        public RecipePages()
        {
            this.Collection = new Dictionary<string, List<RecipePagePart>>();
        }

        public void ReadFile(string filePath)
        {
            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string readLine = "";
                bool during = false;
                RecipePagePart part = null;
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (during)
                    {
                        StringBuilder sb = new StringBuilder();
                        string codeBlockLine = "";
                        while ((codeBlockLine = sr.ReadLine()) != null)
                        {
                            if (codeBlockLine == pattern_ymlEnd)
                            {
                                break;
                            }
                            sb.AppendLine(codeBlockLine);
                        }
                        part.Content = sb.ToString();

                        if (!this.Collection.ContainsKey(part.FileName))
                        {
                            this.Collection[part.FileName] = new List<RecipePagePart>();
                        }
                        this.Collection[part.FileName].Add(part);

                        during = false;
                    }
                    if (pattern_ymlStart.IsMatch(readLine))
                    {
                        part = new RecipePagePart()
                        {
                            Index = ++_Index,
                            FileName = pattern_yamlFileName.Match(readLine).Value,
                        };
                        during = true;
                    }
                }
            }
        }
    }
}
