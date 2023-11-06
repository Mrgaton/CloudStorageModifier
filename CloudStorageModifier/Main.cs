using CloudStorageModifier.APIHelper;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            MessageBox.Show(GetFileType("ClientSettingsSwitch.Sav"));
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
                //MessageBox.Show("Cloud save file founded downloading...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                string destPath = AskSaveFile(null,saveFileName,"SaveFile | *.sav");

                if (destPath == null)
                {
                    MessageBox.Show("Error failed selecting file to save :C", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.WriteAllBytes(destPath,await Cloud.Download(saveFileName, acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString()));


                MessageBox.Show("Saved to \"" + destPath + "\"", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cloud save file has not been found on the accoutn cloud storage", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Cloud.Download(saveFileName,acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString());
        }

        private static string AskSaveFile(string path,string defaultFileName, string filter)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = path;
                dialog.Filter = filter;
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;
                dialog.FileName = defaultFileName;

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
        private async void ListButton_Click(object sender, System.EventArgs e)
        {
            JObject acessTokenResponse = await Auth.GetAccessToken();

            if (acessTokenResponse["errorMessage"] != null)
            {
                MessageBox.Show(acessTokenResponse["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("This is the availables cloudFiles on the account:\n\n (" + string.Join(", " ,(await Cloud.List(acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString())).Children<JObject>().Select(jsonObject => GetFileType(jsonObject["filename"].ToString()))) + ")",Application.ProductName,MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private static void NullCloudType() => MessageBox.Show("Error cloud type is null", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);


        private static Dictionary<string, string> typesFileNames = new Dictionary<string, string>()
        {
            {"Switch" ,"ClientSettingsSwitch.Sav"},
            {"Android" ,"ClientSettingsAndroid.Sav"},
            {"Pc" ,"ClientSettings.Sav"},
            {"GFNMobile" ,"ClientSettingsGFNMobile.Sav"},
            {"GFN" ,"ClientSettingsGFN.Sav"},
            {"XSXHelios" ,"ClientSettingsXSXHelios.Sav"},
            {"XSXHeliosMobile" ,"ClientSettingsXSXHeliosMobile.Sav"},
        };
        private static string GetFileName(string type) => typesFileNames.TryGetValue(type, out string value) ? value : null;
        private static string GetFileType(string name) => (!name.StartsWith("Client") || name.Length <= 18) ? name : string.Join("", name.Skip(14).Take(name.Length - (18)));
    }
}
