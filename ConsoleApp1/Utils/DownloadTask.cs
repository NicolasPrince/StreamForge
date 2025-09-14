using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    class DownloadTask
    {
        public string URL;
        public int DownloadArgs;
        public string FolderPath;

        public DownloadTask(string URL, int DownloadArgs, string DownloadPath)
        {
            this.URL = URL;
            this.DownloadArgs = DownloadArgs;
            this.FolderPath = DownloadPath;
        }

        public void Baixar()
        {
            Console.Clear();
            if (this.URL.Length == 0)
            {
                Console.WriteLine("Nenhum Link identificado!\n...");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("-----------------------------------\nMétodo Baixar foi inicializado!\n-----------------------------------");
            string Args = "";
            switch (DownloadArgs)
            {
                case 0:
                    Console.WriteLine("Selecione um Argumento de Download!");
                    break;
                case 1:
                    Args = "";
                    break;
                case 2:
                    Args = "-x --audio-format mp3";
                    break;
                case 3:
                    Args = "--ppa \"ThumbnailsConvertor+ffmpeg_o:-vf crop=ih:ih:(iw-ih)/2:0\" -x --audio-format mp3 -f bestaudio --retries 30 --fragment-retries 30";
                    break;
            }

            string ffmpegPath = "--ffmpeg-location C:/ProgramData/chocolatey/lib/ffmpeg-full/tools/ffmpeg/bin";

            Console.WriteLine($"FolderPath: {FolderPath}\nffmpegPath: {ffmpegPath}\nArgs: {Args}\nUrl: {this.URL}" + "\n-----------------------------------");

            string Arguments = $"-w -o \"{FolderPath}\\%(title)s.%(ext)s\" --newline {ffmpegPath} {Args} {this.URL}";

            var startInfo = new ProcessStartInfo()
            {
                FileName = "D:/Desenvolvimento de Softwares/Projetos Pessoais/StreamForge/yt-dlp.exe",
                Arguments = Arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
            };

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            string Output = process.StandardOutput.ReadToEnd();
            string OutputError = process.StandardError.ReadToEnd();
            Console.WriteLine($"{Output}\n------------------\n{OutputError}");
            Console.WriteLine("\n-Aperte Enter para voltar ao menu...");
            Console.ReadLine();
        }
    }
}
