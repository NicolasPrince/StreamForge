using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    class DownloadTask
    {
        public string URL;
        public int DownloadArgs;
        public string FolderPath;
        public string YtDlpPath;
        public int DownloadRetries;

        public DownloadTask(string URL, int DownloadArgs, string DownloadPath, string YtDlpPath, int DownloadRetries)
        {
            this.URL = URL;
            this.DownloadArgs = DownloadArgs;
            this.FolderPath = DownloadPath;
            this.YtDlpPath = YtDlpPath;
            this.DownloadRetries = DownloadRetries;
        }

        public async Task Baixar()
        {
            Console.Clear();

            if (this.URL.Length == 0)
            {
                Console.WriteLine("Nenhum Link identificado!\n...");
                Console.ReadLine();
                return;
            }

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
                    Args = $"--ppa \"ThumbnailsConvertor+ffmpeg_o:-vf crop=ih:ih:(iw-ih)/2:0\" -x --audio-format mp3 -f bestaudio --retries {this.DownloadRetries} --fragment-retries {this.DownloadRetries}";
                    break;
            }

            string ffmpegPath = "--ffmpeg-location C:/ProgramData/chocolatey/lib/ffmpeg-full/tools/ffmpeg/bin";
            
            string Arguments = $"-w -o \"{FolderPath}\\%(title)s.%(ext)s\" --newline {ffmpegPath} {Args} {this.URL}";

            var startInfo = new ProcessStartInfo()
            {
                FileName = this.YtDlpPath,
                Arguments = Arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };

            var qOut = new ConcurrentQueue<string>();

            var qErr = new ConcurrentQueue<string>();

            using var p = new Process() { StartInfo = startInfo, EnableRaisingEvents = true };
            p.OutputDataReceived += (_, e) => { if (e.Data is not null) qOut.Enqueue(e.Data); };
            p.ErrorDataReceived += (_, e) => { if (e.Data is not null) qErr.Enqueue(e.Data); };

            p.StartInfo = startInfo;
            p.Start();
            p.BeginErrorReadLine();
            p.BeginOutputReadLine();

            await p.WaitForExitAsync();
            Console.WriteLine("Download Finalizado!");

            // Download Finalizado, Iniciando o Processo de Coleta de Informações para salvar os .JSON

            var OutReg = new LogPattern
            {
                STDERR = qErr.ToList(),
                STDOUT = qOut.ToList(),
            };

            var saver1 = new LogSaver(OutReg, "BBBBBB");

            saver1.Save();

            var DownReg = new HistoryPattern
            {
                Title = "",
                STDOUT_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamForge", "Logs"),
                DArgs = Args,
                Url = this.URL,
                ArchivePath = FolderPath
            };

            var saver = new HistorySaver(DownReg);

            saver.Save();
        }
    }
}
