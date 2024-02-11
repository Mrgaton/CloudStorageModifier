using CloudStorageModifier.APIHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Environment;

namespace CloudStorageModifier
{
    public partial class Main : Form
    {
        public Main()
        {
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            InitializeComponent();

            loginNeededButtons.Add(LogOutButton);
            loginNeededButtons.Add(CreateDeviceAuthButton);
            loginNeededButtons.Add(ListButton);
            loginNeededButtons.Add(DownloadButton);
            loginNeededButtons.Add(UploadButton);
            loginNeededButtons.Add(LaunchFnButton);
            loginNeededButtons.Add(ActualAnticheatButton);

            ReloadAccounts();
        }

        private void ReloadAccounts()
        {
            AccountSelectorComboBox.Items.Clear();

            foreach (string file in Directory.GetFiles(Program.programFolder, "*.device"))
            {
                AccountSelectorComboBox.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private bool ErrorFounded(JObject json)
        {
            if (json["errorMessage"] == null) return false;

            MessageBox.Show(json["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            return true;
        }

        private JObject defaultAtuth;
        private DateTime authExpiration;

        private async Task<JObject> GetAuth()
        {
            if (defaultAtuth != null && authExpiration > DateTime.Now) return defaultAtuth;
            else LogOutButton_Click(null, null);

            return null;
        }

        private void LoginLinkButton_Click(object sender, EventArgs e) => LoginCredentials();

        private async Task LoginCredentials()
        {
            JObject acessTokenResponse = await Auth.GetAccessToken();

            LoginInternal(acessTokenResponse);
        }

        private void LoginDeviceButton_Click(object sender, EventArgs e)
        {
            string filePath = AskOpenFile(null, "Device Auth | *.device");

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Invalid selected file", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoginDeviceAuth(File.ReadAllText(filePath));
        }

        private async Task LoginDeviceAuth(string fileData)
        {
            string[] deviceAuthData = fileData.Split(':');

            LoginDeviceAuth(deviceAuthData[0], deviceAuthData[1], deviceAuthData[2]);
        }

        private async Task LoginDeviceAuth(string deviceId, string accountId, string secret)
        {
            JObject acessTokenResponse = await Auth.OauthTokenFromDevice(deviceId, accountId, secret);

            LoginInternal(acessTokenResponse);
        }

        private List<Button> loginNeededButtons = new List<Button>();

        private async Task LoginInternal(JObject acessTokenResponse)
        {
            if (acessTokenResponse["errorMessage"] != null)
            {
                MessageBox.Show(acessTokenResponse["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            authExpiration = DateTime.Now.AddSeconds((double)acessTokenResponse["expires_in"]);

            cloudInfoLabel.Text = cloudInfoLabel.Text.Split(' ')[0] + " (" + acessTokenResponse["displayName"].ToString() + ")";

            defaultAtuth = acessTokenResponse;

            ReloadAccounts();

            loginNeededButtons.ForEach(b => b.Enabled = true);
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            cloudInfoLabel.Text = cloudInfoLabel.Text.Split(' ')[0];
            defaultAtuth = null;

            loginNeededButtons.ForEach(b => b.Enabled = false);
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            if (CloudTypeComboBox.SelectedIndex < 0)
            {
                NullCloudType();
                return;
            }

            string saveFileName = GetFileName(CloudTypeComboBox.SelectedItem.ToString());

            JObject acessTokenResponse = await GetAuth();

            if (ErrorFounded(acessTokenResponse)) return;

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

        private async void UploadButton_Click(object sender, EventArgs e)
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

            JObject response;

            if (await Cloud.Exist(saveFileName, acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString()))
            {
                response = await Cloud.Update(saveFileName, File.ReadAllBytes(destPath), acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString());
            }
            else
            {
                response = await Cloud.Create(saveFileName, File.ReadAllBytes(destPath), acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString());
            }

            if (response["errorMessage"] != null) MessageBox.Show(response["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (ErrorFounded(response)) return;

            MessageBox.Show("Successfully uploaded cloud Save", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void ListButton_Click(object sender, EventArgs e)
        {
            JObject acessTokenResponse = await GetAuth();

            if (acessTokenResponse["errorMessage"] != null)
            {
                MessageBox.Show(acessTokenResponse["errorMessage"].ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("This is the availables cloudFiles for the account:\n\n (" + string.Join(", ", (await Cloud.List(acessTokenResponse["account_id"].ToString(), acessTokenResponse["access_token"].ToString())).Children<JObject>().Select(jsonObject => GetFileType(jsonObject["filename"].ToString()))) + ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private static string FortniteLogsPath = Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), "FortniteGame\\Saved\\Logs\\FortniteLauncher.log");
        private async void LaunchFnButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to launch fortnite?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            if (!File.Exists(FortniteLogsPath))
            {
                MessageBox.Show("Fortnite logs are empty cant find obfuscation id please launch fortnite normaly and close it.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string obfuscationId = File.ReadAllText(FortniteLogsPath).Split('\n').Last(l => l.Contains("-obfuscationid=")).Split(' ').First(s => s.StartsWith("-obfuscationid=")).Split('=').Last();

            if (string.IsNullOrWhiteSpace(obfuscationId))
            {
                MessageBox.Show("The obfuscation ID cannot be retrieved correctly.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var item in Process.GetProcessesByName("FortniteLauncher"))
            {
                try
                {
                    item.Kill();
                }
                catch { }
            }

            string fortniteLibrariesPath = Path.Combine(JObject.Parse(File.ReadAllText(Path.Combine(Environment.GetFolderPath(SpecialFolder.CommonApplicationData), @"Epic\UnrealEngineLauncher\LauncherInstalled.dat")))["InstallationList"].AsEnumerable().First(element => (string)element["NamespaceId"] == "fn")["InstallLocation"].ToString().Replace("/", "\\"), @"FortniteGame\Binaries\Win64");

            JObject acessToken = await GetAuth();

            JObject subExchangeCode = await Auth.GetExchangeCode(acessToken["access_token"].ToString());

            JObject reAcessToken = await Auth.OauthTokenFromExchange(subExchangeCode["code"].ToString());

            string accountName = (string)reAcessToken["displayName"];
            string accountId = (string)reAcessToken["account_id"];

            Directory.SetCurrentDirectory(fortniteLibrariesPath);

            LogOutButton_Click(null, null);

            //This has to change from every build sooooo xd

            Process.Start("FortniteLauncher.exe", $"-obfuscationid={obfuscationId} -AUTH_LOGIN=unused -AUTH_PASSWORD={(await Auth.GetExchangeCode(reAcessToken["access_token"].ToString()))["code"].ToString()} -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -EpicPortal -epicusername={accountName} -epicuserid={accountId} -epiclocale=en -epicsandboxid=fn");
        }

        private async void CreateDeviceAuthButton_Click(object sender, EventArgs e)
        {
            var auth = await GetAuth();

            string destPath = AskSaveFile(null, (string)auth["displayName"] + "_Auth.device", "Device Auth | *.device");

            if (destPath == null)
            {
                MessageBox.Show("Canceled action", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var device = await Auth.CreateDeviceAuth(auth["access_token"].ToString(), auth["account_id"].ToString());

            File.WriteAllText(destPath, (string)device["deviceId"] + ":" + (string)device["accountId"] + ":" + (string)device["secret"]);
        }

        private async void ActualAnticheatButton_Click(object sender, EventArgs e)
        {
            JObject acessToken = await GetAuth();

            /*JObject exchangeCode = await Auth.GetExchangeCode(acessToken["access_token"].ToString());

            JObject reAcessToken = await Auth.OauthTokenFromExchange(exchangeCode["code"].ToString());

            if (ErrorFounded(acessToken) || ErrorFounded(reAcessToken)) return;*/

            var result = await Caldera.CalderaRACP((await Auth.GetExchangeCode(acessToken["access_token"].ToString()))["code"].ToString(), (string)acessToken["account_id"]);

            if (ErrorFounded(result)) return;

            MessageBox.Show("Actual anticheat is \"" + (string)result["provider"] + "\"", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void AccountSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tarjetSession = File.ReadAllText(Path.Combine(Program.programFolder, AccountSelectorComboBox.SelectedItem + ".device"));

            await LoginDeviceAuth(tarjetSession);
        }
    }
}