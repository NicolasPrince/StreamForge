using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public class LogSaver
    {
        private readonly LogPattern _entry;
        private readonly string _FullPath;

        public LogSaver(LogPattern STD, string Name)
        {
            _entry = STD;
            _FullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamForge", "Logs", $"{Name} - log.json");
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamForge", "Logs"));

            var JsonOptions = new JsonSerializerOptions { WriteIndented = true };

            string line = JsonSerializer.Serialize(_entry, JsonOptions) + Environment.NewLine;
            File.AppendAllText(_FullPath, line);
        }
    }
}
