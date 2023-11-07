using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace CloudStorageModifier
{
    internal static class Program
    {
        public static Assembly currentAssembly = Assembly.GetExecutingAssembly();

        public static string programFolder = Path.GetDirectoryName(currentAssembly.Location);

        public static HttpClient client = new HttpClient();

        [STAThread]
        static void Main()
        {
            if (!File.Exists(Path.Combine(programFolder, "Newtonsoft.Json.dll")))
            {

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
