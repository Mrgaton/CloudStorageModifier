using CloudStorageModifier.APIHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

        private JObject defaultAtuth;
        private DateTime authExpiration;

        private async Task<JObject> GetAuth()
        {
            if (defaultAtuth != null && authExpiration > DateTime.Now) return defaultAtuth;

            JObject acessTokenResponse = await Auth.GetAccessToken();

            if (acessTokenResponse["errorMessage"] != null)
            {
                MessageBox.Show(acessTokenResponse["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            authExpiration = DateTime.Now.AddSeconds((double)acessTokenResponse["expires_in"]);

            InfoLabel.Text = InfoLabel.Text.Split(' ')[0] + " (" + acessTokenResponse["displayName"].ToString() + ")";
            LogOutButton.Enabled = true;

            return defaultAtuth = acessTokenResponse;
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            InfoLabel.Text = InfoLabel.Text.Split(' ')[0];
            defaultAtuth = null;
            LogOutButton.Enabled = false;
        }

        private async void DownloadButton_Click(object sender, System.EventArgs e)
        {
            if (CloudTypeComboBox.SelectedIndex < 0)
            {
                NullCloudType();
                return;
            }

            string saveFileName = GetFileName(CloudTypeComboBox.SelectedItem.ToString());

            JObject acessTokenResponse = await GetAuth();

            if (acessTokenResponse["errorMessage"] != null)
            {
                MessageBox.Show(acessTokenResponse["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (await Cloud.Exist(saveFileName, acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString()))
            {
                string destPath = AskSaveFile(null, acessTokenResponse["displayName"].ToString() + "_" + saveFileName, "SaveFile | *.sav");

                if (destPath == null)
                {
                    MessageBox.Show("Error failed selecting file to save :C", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.WriteAllBytes(destPath, await Cloud.Download(saveFileName, acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString()));

                MessageBox.Show("Saved to \"" + Path.GetFileName(destPath) + "\"", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cloud save file has not been found on the account storage", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string AskSaveFile(string path, string defaultFileName, string filter)
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

        private async void UploadButton_Click(object sender, System.EventArgs e)
        {
            if (CloudTypeComboBox.SelectedIndex < 0)
            {
                NullCloudType();
                return;
            }

            string saveFileName = GetFileName(CloudTypeComboBox.SelectedItem.ToString());

            string destPath = AskOpenFile(null, "SaveFile | *.sav");

            if (destPath == null)
            {
                MessageBox.Show("Error failed selecting file to open", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show($"Are you sure the file {Path.GetFileName(destPath)} is a saveFile for {CloudTypeComboBox.SelectedItem}?", Application.ProductVersion, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            }

            JObject acessTokenResponse = await GetAuth();

            JObject response = null;

            if (await Cloud.Exist(saveFileName, acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString()))
            {
                response = await Cloud.Update(saveFileName, File.ReadAllBytes(destPath), acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString());
            }
            else
            {
                response = await Cloud.Create(saveFileName, File.ReadAllBytes(destPath), acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString());
            }

            if (response["errorMessage"] != null) MessageBox.Show(response["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            JObject acessTokenResponse = await GetAuth();

            if (acessTokenResponse["errorMessage"] != null)
            {
                MessageBox.Show(acessTokenResponse["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("This is the availables cloudFiles on the account:\n\n (" + string.Join(", ", (await Cloud.List(acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString())).Children<JObject>().Select(jsonObject => GetFileType(jsonObject["filename"].ToString()))) + ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void NullCloudType() => MessageBox.Show("Error cloud type is null", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

        private static Dictionary<string, string> typesFileNames = new Dictionary<string, string>
        {
            {"Switch" ,"ClientSettingsSwitch.Sav"},
            {"Android" ,"ClientSettingsAndroid.Sav"},
            {"Pc" ,"ClientSettings.Sav"},
            {"GFNMobile" ,"ClientSettingsGFNMobile.Sav"},
            {"GFN" ,"ClientSettingsGFN.Sav"},
            {"XSXHelios" ,"ClientSettingsXSXHelios.Sav"},
            {"XSXHeliosMobile" ,"ClientSettingsXSXHeliosMobile.Sav"},
            {"IOS" ,"ClientSettingsIOS.Sav"},
            {"PS4","ClientSettingsPS4.sav"},
            {"PS5","ClientSettingsPS5.sav"}
        };

        private static string GetFileName(string type) => typesFileNames.TryGetValue(type, out string value) ? value : null;

        private static string GetFileType(string name) => (!name.StartsWith("ClientSettings") || name.Length <= 18) ? (typesFileNames.FirstOrDefault(file => file.Value == name).Key ?? Path.GetFileNameWithoutExtension(name)) : string.Join("", name.Skip(14).Take(name.Length - (18)));
    }
}