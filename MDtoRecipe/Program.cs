using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WinBM.PowerShell.Lib.MDtoRecipe;

namespace MDtoRecipe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Setting setting = Setting.Load();

            //  Setting内に記載が無い場合
            if (string.IsNullOrEmpty(setting.TargetDirectory))
            {
                setting.TargetDirectory = ConfirmTarget();
                if (string.IsNullOrEmpty(setting.TargetDirectory))
                {
                    Console.WriteLine("未指定の為、終了");
                    Console.ReadLine();
                    return;
                }
                setting.Save();
            }

            //  対象フォルダーに変更が無いか確認
            Console.WriteLine("TargetDirectoryは以下のパスです。続行しますか?");
            Console.WriteLine("[" + setting.TargetDirectory + "]");
            Console.Write("Y/n > ");
            string read = Console.ReadLine();
            if (read == "n" || read == "N")
            {
                setting.TargetDirectory = ConfirmTarget();
                if (string.IsNullOrEmpty(setting.TargetDirectory))
                {
                    Console.WriteLine("未指定の為、終了");
                    Console.ReadLine();
                    return;
                }
                setting.Save();
            }

            PageCollection pages = new PageCollection();
            foreach (string filePath in Directory.GetFiles(setting.TargetDirectory, "*.md", SearchOption.AllDirectories))
            {
                if (setting.ExcludePaths?.Any(x => x.Equals(filePath, StringComparison.OrdinalIgnoreCase)) ?? false)
                {
                    continue;
                }
                pages.Add(filePath);
            }
            foreach (var recipe in pages.ToRecipeFileList())
            {
                recipe.Write(setting.OutputDirectory);
            }

            Console.ReadLine();
        }

        private static string ConfirmTarget()
        {
            Console.WriteLine("読み込み対象のフォルダーを指定してください");
            Console.Write("Directory: ");
            string targetDirectory = Console.ReadLine();

            return targetDirectory;
        }
    }
}