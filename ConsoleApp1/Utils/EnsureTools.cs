using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace ConsoleApp1.Utils
{
    class EnsureTools
    {
        public string EnsureTool(string LogicalName, string FileName)
        {
            //Pega o Caminho do "AppData" do Sistema! Actual : %AppData%\StreamForge\bin <--
            var BinDirec = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamForge", "bin");
            Directory.CreateDirectory(BinDirec);
            
            //Combina para encontrar os arquivos que forem passados pela String "FileName"
            var Dest = Path.Combine(BinDirec, FileName);

            using var Source = Assembly.GetExecutingAssembly().GetManifestResourceStream(LogicalName)
                ?? throw new InvalidOperationException($"Recurso não encontrado: {LogicalName}");
            if (!File.Exists(Dest) || new FileInfo(Dest).Length != Source.Length)
            {
                using var OutStream = File.Create(Dest);
                Source.CopyTo(OutStream);
            }
            return Dest;
        }
    }
}
