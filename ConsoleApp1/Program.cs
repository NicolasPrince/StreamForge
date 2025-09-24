using ConsoleApp1.Utils;
using ConsoleApp1.Utils.Misc;
using System;
using System.Drawing.Text;
using System.Windows.Forms;

namespace StreamForge
{
    class Program
    {
        private static string SettingsJsonPath = "";

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
            // Loader das configuraçõe Padrões do STForge
            SettingsJsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StreamForge", "STFSettings.json");
            var LastCFG = new SettingsPattern();
            LastCFG = SettingsManager.Load(SettingsJsonPath);
            
            Headers Text = new Headers();
            bool escolheuSair = false;
            string programVersion = "0.1.1v-alpha";
            
            string URL = "";
            FolderSelector Folder = new FolderSelector();
            
            DownloadArgsMenu opcaoArgs = new DownloadArgsMenu();

            //Loader de Programas Úteis
            EnsureTools Checker = new EnsureTools();
            var YtDlpPath = Checker.EnsureTool("StreamForge.EmbeddedTools.yt_dlp.exe", "yt-dlp.exe");

            int DownloadArgs = LastCFG.LastUsedPreset;
            Folder.FolderPath = LastCFG.DefaultFolderPath;

            opcaoArgs = (DownloadArgsMenu)DownloadArgs;

            while (!escolheuSair)
            {
                var CFG = new SettingsPattern
                {
                    DefaultFolderPath = Folder.FolderPath,
                    LastUsedPreset = DownloadArgs,
                };

                SettingsManager.Save(CFG);

                Text.Header1();
                Console.WriteLine("StreamForger " + programVersion);
                Console.WriteLine("1 - Baixar\n2 - DownloadArgs\n3 - Escolher Pasta\n4 - Sair");
                Console.WriteLine($"\n\n\n\n\nArgumento: {opcaoArgs}\nPasta de Download:{Folder.FolderPath}");

                var ReadKey = Console.ReadLine();
                int intOp = 0;

                if (ReadKey.Length <= 0)
                {
                    Console.WriteLine("Insira algo para prosseguir!");
                    Console.ReadLine();
                }
                else if (!int.TryParse(ReadKey, out _))
                {
                    Console.WriteLine("Opção inválida!");
                    Console.ReadLine();
                }
                else
                {
                    intOp = int.Parse(ReadKey);
                }
                
                if (intOp != 0 && intOp > 4)
                {
                    Console.WriteLine("Opção inválida!");
                    Console.ReadLine();
                }

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
                            URL = Console.ReadLine();
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