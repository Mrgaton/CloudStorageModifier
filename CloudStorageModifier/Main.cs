using System.Windows.Forms;

namespace CloudStorageModifier
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, System.EventArgs e)
        {
            if (CloudTypeComboBox.SelectedIndex < 0)
            {
                NullCloudType();
                return;
            }
        }

        private void DownloadButton_Click(object sender, System.EventArgs e)
        {
            if (CloudTypeComboBox.SelectedIndex < 0)
            {
                NullCloudType();
                return;
            }
        }
        private static void NullCloudType() => MessageBox.Show("Error cloud type is null", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
