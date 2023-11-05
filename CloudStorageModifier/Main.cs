using CloudStorageModifier.APIHelper;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CloudStorageModifier
{
    public partial class Main : Form
    {
        public Main()
        {
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            InitializeComponent();
        }


        private async void DownloadButton_Click(object sender, System.EventArgs e)
        {
            if (CloudTypeComboBox.SelectedIndex < 0)
            {
                NullCloudType();
                return;
            }

            string saveFileName = GetFileName(CloudTypeComboBox.SelectedItem.ToString());

            JObject acessTokenResponse = await Auth.GetAccessToken();

            if (acessTokenResponse["errorMessage"] != null)
            {
                MessageBox.Show(acessTokenResponse["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (await Cloud.Exist(saveFileName, acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString()))
            {
                MessageBox.Show("Cloud save file founded downloading...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Cloud.Download(saveFileName, acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString());
            }
            else
            {
                MessageBox.Show("Cloud save file has not been found on the accoutn cloud storage", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Cloud.Download(saveFileName,acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString());
        }

        private static string AskSaveFile(string path, string filter)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = path;
                dialog.Filter = filter;
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() != DialogResult.OK) return null;

                return dialog.FileName;
            }
        }


        private void UploadButton_Click(object sender, System.EventArgs e)
        {
            if (CloudTypeComboBox.SelectedIndex < 0)
            {
                NullCloudType();
                return;
            }
        }
        private static string AskOpenFile(string path, string filter)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = path;
                dialog.Filter = filter;

                if (dialog.ShowDialog() != DialogResult.OK) return null;

                return dialog.FileName;
            }
        }

        private static void NullCloudType() => MessageBox.Show("Error cloud type is null", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
       
        private static string GetFileName(string type)
        {
            switch (type)
            {
                case "Switch":
                    return "ClientSettingsSwitch.Sav";

                case "Android":
                    return "ClientSettingsAndroid.Sav";

                case "Pc":
                    return "ClientSettings.Sav";

                case "GFNMobile":
                    return "ClientSettingsGFNMobile.Sav";

                case "GFN":
                    return "ClientSettingsGFN.Sav";

                case "XSXHelios":
                    return "ClientSettingsXSXHelios.Sav";

                case "XSXHeliosMobile":
                    return "ClientSettingsXSXHeliosMobile.Sav";

                default:
                    return null;
            }
        }
    }
}
