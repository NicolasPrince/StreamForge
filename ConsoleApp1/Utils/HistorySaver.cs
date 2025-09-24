using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public class HistorySaver
    {
        private readonly HistoryPattern _entry;
        private readonly string _FullPath;

        public HistorySaver(HistoryPattern entry)
        {
            _entry = entry;
            _FullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamForge", "HistorySaver.Json");
        }

        public void Save()
        {
            var JsonOptions = new JsonSerializerOptions { WriteIndented = true };

            string line = JsonSerializer.Serialize(_entry, JsonOptions) + Environment.NewLine;
            File.AppendAllText(_FullPath, line);
        }
    }
}
