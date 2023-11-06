namespace CloudStorageModifier
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.CloudTypeComboBox = new System.Windows.Forms.ComboBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.UploadButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.ListButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloudTypeComboBox
            // 
            this.CloudTypeComboBox.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloudTypeComboBox.FormattingEnabled = true;
            this.CloudTypeComboBox.Items.AddRange(new object[] {
            "Switch",
            "Android",
            "Pc",
            "GFNMobile",
            "GFN",
            "XSXHelios",
            "XSXHeliosMobile"});
            this.CloudTypeComboBox.Location = new System.Drawing.Point(31, 47);
            this.CloudTypeComboBox.Name = "CloudTypeComboBox";
            this.CloudTypeComboBox.Size = new System.Drawing.Size(427, 33);
            this.CloudTypeComboBox.TabIndex = 0;
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.InfoLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InfoLabel.Location = new System.Drawing.Point(28, 19);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(93, 21);
            this.InfoLabel.TabIndex = 1;
            this.InfoLabel.Text = "Cloud type";
            // 
            // UploadButton
            // 
            this.UploadButton.BackColor = System.Drawing.Color.DimGray;
            this.UploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UploadButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.UploadButton.Location = new System.Drawing.Point(31, 87);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(141, 34);
            this.UploadButton.TabIndex = 2;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.BackColor = System.Drawing.Color.DimGray;
            this.DownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DownloadButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DownloadButton.Location = new System.Drawing.Point(321, 87);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(137, 34);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ListButton
            // 
            this.ListButton.BackColor = System.Drawing.Color.DimGray;
            this.ListButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ListButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ListButton.Location = new System.Drawing.Point(178, 87);
            this.ListButton.Name = "ListButton";
            this.ListButton.Size = new System.Drawing.Size(137, 34);
            this.ListButton.TabIndex = 4;
            this.ListButton.Text = "List";
            this.ListButton.UseVisualStyleBackColor = false;
            this.ListButton.Click += new System.EventHandler(this.ListButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(479, 144);
            this.Controls.Add(this.ListButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.CloudTypeComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud Storage Modifier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CloudTypeComboBox;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button ListButton;
    }
}

