using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    public class LogPattern
    {
        [JsonPropertyName("STDERR")]
        public List<string> STDERR { get; set; } = new();

        [JsonPropertyName("STDOUT")]
        public List<string> STDOUT { get; set; } = new();
    }
}
