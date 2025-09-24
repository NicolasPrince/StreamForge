using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public class SettingsPattern
    {
        [JsonPropertyName("Default Folder Path")]
        public string DefaultFolderPath { get; set; } = "";

        [JsonPropertyName("Retries Amount")]
        public int RetriesAmount { get; set; } = 30;

        [JsonPropertyName("Last Used Preset")]
        public int LastUsedPreset { get; set; } = 0;
    }
}
