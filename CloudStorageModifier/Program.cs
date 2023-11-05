using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CloudStorageModifier
{
    internal static class Program
    {
        public static HttpClient client = new HttpClient();

        [STAThread]
        static void Main()
        {
            string credentials = APIHelper.Auth.GetCredentialsToken().Result;


            MessageBox.Show(APIHelper.Auth.GetAccessToken(credentials).Result);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
