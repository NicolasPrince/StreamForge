using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public class HistoryPattern
    {
        [JsonPropertyName("ID")]
        public Guid id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("Standard Output")]
        public string STDOUT_Path { get; set; } = "";

        [JsonPropertyName("Argumentos")]
        public string DArgs { get; set; } = "";

        [JsonPropertyName("Titulo")]
        public string Title { get; set; } = "";

        [JsonPropertyName("Url")]
        public string Url { get; set; } = "";

        [JsonPropertyName("Caminho")]
        public string ArchivePath { get; set; } = "";
    }
}
