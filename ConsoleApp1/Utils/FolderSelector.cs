using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConsoleApp1.Utils
{
    class FolderSelector
    {
        public string FolderPath = "";

        public void ChooseFolder()
        {
            Console.Clear();
            using (var Dialog = new FolderBrowserDialog())
            {
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    FolderPath = Dialog.SelectedPath;
                    Console.WriteLine($"Pasta Configurada! \r\n {FolderPath}");
                    Console.WriteLine($"\n \n \nAperte enter para ir ao Menu...");
                    Console.ReadLine();
                }
            }
        }
    }
}
