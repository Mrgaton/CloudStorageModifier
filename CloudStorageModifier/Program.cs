using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Windows.Forms;

namespace CloudStorageModifier
{
    internal static class Program
    {
        public static Assembly currentAssembly = Assembly.GetExecutingAssembly();

        public static string programFolder = Path.GetDirectoryName(currentAssembly.Location);

        public static HttpClient client = new HttpClient();

        private static string repoUri = "https://github.com/Mrgaton/CloudStorageModifier/raw/master/CloudStorageModifier/";

        private static Dictionary<string, string> dependencies = new Dictionary<string, string>()
        {
            {"Newtonsoft.Json.dll", repoUri + "Newtonsoft.Json.dll"}
        };

        [STAThread]
        static void Main()
        {
            foreach (var dep in dependencies)
            {
                string tarjetPath = Path.Combine(programFolder, dep.Key);

                if (!File.Exists(tarjetPath)) File.WriteAllBytes(tarjetPath, client.GetByteArrayAsync(dep.Value).Result);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
