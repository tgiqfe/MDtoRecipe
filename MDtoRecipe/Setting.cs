using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MDtoRecipe
{
    internal class Setting
    {
        public string Source { get; set; }

        public string Output { get; set; }

        public string[] Exclude { get; set; }

        private static readonly string FilePath = Path.Combine(
            Path.GetTempPath(), "MDtoRecipe", "setting.json");

        public static Setting Load()
        {
            Setting setting = null;

            try
            {
                using (var sr = new StreamReader(FilePath, Encoding.UTF8))
                {
                    setting = JsonSerializer.Deserialize<Setting>(sr.ReadToEnd());
                }
            }
            catch { }
            setting ??= new Setting();

            return setting;
        }

        public void Save()
        {
            try
            {
                string parent = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(parent))
                {
                    Directory.CreateDirectory(parent);
                }
                using (var sw = new StreamWriter(FilePath, false, Encoding.UTF8))
                {
                    sw.WriteLine(JsonSerializer.Serialize(
                        this,
                        new JsonSerializerOptions() { WriteIndented = true }));
                }
            }
            catch { }
        }

    }
}
