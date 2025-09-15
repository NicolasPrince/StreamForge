using ConsoleApp1.Utils;
using System;
using System.Windows.Forms;

namespace StreamForge
{
    class Program
    {
        

        enum Menu
        {
            Baixar =1,
            DownloadArgs,
            ChooseFolder,
            Sair
        }

        enum DownloadArgsMenu
        {
            Nenhum,
            Video =1,
            Audio,
            AudioComCapa
        }

        [STAThread]
        static void Main(string[] args)
        {
            bool escolheuSair = false;
            string programVersion = "0.1.1v";
            int DownloadArgs = 0;
            FolderSelector Folder = new FolderSelector();
            DownloadArgsMenu opcaoArgs = new DownloadArgsMenu();
            var ExeDir = AppContext.BaseDirectory;
            EnsureTools Checker = new EnsureTools();
            var YtDlpPath = Checker.EnsureTool("StreamForge.EmbeddedTools.yt_dlp.exe", "yt-dlp.exe");

            while (!escolheuSair)
            {
                Console.WriteLine("StreamForger " + programVersion);
                Console.WriteLine("1 - Baixar\n2 - DownloadArgs\n3 - Escolher Pasta\n4 - Sair");
                Console.WriteLine($"\n\n\n\n\nArgumento: {opcaoArgs}\nPasta de Download:{Folder.FolderPath}");
                Console.WriteLine($"{ExeDir}");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;
                
                switch (opcao)
                {
                    case Menu.Baixar:
                        if (DownloadArgs == 0)
                        {
                            Console.WriteLine("Escolha os argumentos para Download no 2 Menu!");
                            Console.ReadLine();
                        } else
                        {
                            Console.WriteLine("Insira a URL: ");
                            var URL = Console.ReadLine();
                            DownloadTask Download = new DownloadTask(URL, DownloadArgs, Folder.FolderPath, YtDlpPath);
                            Download.Baixar();
                        }
                        break;
                    case Menu.ChooseFolder:
                        Folder.ChooseFolder();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                    case Menu.DownloadArgs:
                        Console.Clear();
                        Console.WriteLine("1 - Vídeo\n2 - Áudio\n3 - Áudio + Capa");
                        int intOpArgs = int.Parse(Console.ReadLine());
                        opcaoArgs = (DownloadArgsMenu)intOpArgs;
                        DownloadArgs = intOpArgs;
                        break;
                }
                Console.Clear();
            }
        }
    }
}