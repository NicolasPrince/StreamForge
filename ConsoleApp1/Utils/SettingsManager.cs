using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public static class SettingsManager
    {
        private static string _FullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamForge", "STFSettings.json");

        public static void Save(SettingsPattern json)
        {
            var JsonOptions = new JsonSerializerOptions { WriteIndented = true };

            string line = JsonSerializer.Serialize(json, JsonOptions) + Environment.NewLine;
            File.WriteAllText(_FullPath, line);
        }

        public static SettingsPattern Load(string Path)
        {
            if (!File.Exists(Path))
            {
                Directory.CreateDirectory(Path);
                var DefaultCFG = new SettingsPattern();
                Save(DefaultCFG);
                return DefaultCFG;
            }
            string json = File.ReadAllText(Path);
            return JsonSerializer.Deserialize<SettingsPattern>(json) ?? new SettingsPattern();
        }
    }
}
