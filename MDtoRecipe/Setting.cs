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
        public string TargetDirectory { get; set; }

        public string OutputDirectory { get; set; }

        public string[] ExcludePaths { get; set; }

        private static readonly string FilePath = Path.Combine(
            Path.GetTempPath(), "MDtoRecipe", "setting.json");

        public static Setting Deserialize()
        {
            Setting setting = null;

            try
            {
                using (var sr = new StreamReader(FilePath, Encoding.UTF8))
                {
                    _ = JsonSerializer.Deserialize<Setting>(sr.ReadToEnd());
                }
            }
            catch { }
            setting ??= new Setting();

            return setting;
        }

        public void Serialize()
        {
            try
            {
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
