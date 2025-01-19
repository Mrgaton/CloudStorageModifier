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
            this.cloudInfoLabel = new System.Windows.Forms.Label();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.ListButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.LogOutButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.loginInfoLabel = new System.Windows.Forms.Label();
            this.LoginLinkButton = new System.Windows.Forms.Button();
            this.LaunchFnButton = new System.Windows.Forms.Button();
            this.LoginDeviceButton = new System.Windows.Forms.Button();
            this.CreateDeviceAuthButton = new System.Windows.Forms.Button();
            this.AccountSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CloudTypeComboBox
            // 
            this.CloudTypeComboBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.CloudTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CloudTypeComboBox.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloudTypeComboBox.FormattingEnabled = true;
            this.CloudTypeComboBox.Items.AddRange(new object[] {
            "Switch",
            "Android",
            "Pc",
            "GFNMobile",
            "GFN",
            "XSXHelios",
            "XSXHeliosMobile",
            "IOS",
            "PS4",
            "PS5"});
            this.CloudTypeComboBox.Location = new System.Drawing.Point(163, 333);
            this.CloudTypeComboBox.Name = "CloudTypeComboBox";
            this.CloudTypeComboBox.Size = new System.Drawing.Size(515, 33);
            this.CloudTypeComboBox.TabIndex = 0;
            // 
            // cloudInfoLabel
            // 
            this.cloudInfoLabel.AutoSize = true;
            this.cloudInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.cloudInfoLabel.Font = new System.Drawing.Font("Gabriola", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cloudInfoLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cloudInfoLabel.Location = new System.Drawing.Point(20, 321);
            this.cloudInfoLabel.Name = "cloudInfoLabel";
            this.cloudInfoLabel.Size = new System.Drawing.Size(128, 54);
            this.cloudInfoLabel.TabIndex = 1;
            this.cloudInfoLabel.Text = "Cloud type";
            // 
            // DownloadButton
            // 
            this.DownloadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(80)))), ((int)(((byte)(210)))));
            this.DownloadButton.Enabled = false;
            this.DownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DownloadButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.DownloadButton.ForeColor = System.Drawing.Color.White;
            this.DownloadButton.Location = new System.Drawing.Point(163, 392);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(141, 37);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ListButton
            // 
            this.ListButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(50)))), ((int)(((byte)(168)))));
            this.ListButton.Enabled = false;
            this.ListButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ListButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.ListButton.ForeColor = System.Drawing.Color.White;
            this.ListButton.Location = new System.Drawing.Point(26, 391);
            this.ListButton.Name = "ListButton";
            this.ListButton.Size = new System.Drawing.Size(116, 37);
            this.ListButton.TabIndex = 4;
            this.ListButton.Text = "List";
            this.ListButton.UseVisualStyleBackColor = false;
            this.ListButton.Click += new System.EventHandler(this.ListButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(220)))));
            this.UploadButton.Enabled = false;
            this.UploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UploadButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.UploadButton.ForeColor = System.Drawing.Color.White;
            this.UploadButton.Location = new System.Drawing.Point(326, 392);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(123, 37);
            this.UploadButton.TabIndex = 2;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = false;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // LogOutButton
            // 
            this.LogOutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.LogOutButton.Enabled = false;
            this.LogOutButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LogOutButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.LogOutButton.ForeColor = System.Drawing.Color.White;
            this.LogOutButton.Location = new System.Drawing.Point(675, 12);
            this.LogOutButton.Name = "LogOutButton";
            this.LogOutButton.Size = new System.Drawing.Size(113, 37);
            this.LogOutButton.TabIndex = 5;
            this.LogOutButton.Text = "Logout";
            this.LogOutButton.UseVisualStyleBackColor = false;
            this.LogOutButton.Click += new System.EventHandler(this.LogOutButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(80)))), ((int)(((byte)(210)))));
            this.DeleteButton.Enabled = false;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.DeleteButton.ForeColor = System.Drawing.Color.White;
            this.DeleteButton.Location = new System.Drawing.Point(475, 392);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(132, 37);
            this.DeleteButton.TabIndex = 6;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            // 
            // loginInfoLabel
            // 
            this.loginInfoLabel.AutoSize = true;
            this.loginInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.loginInfoLabel.Font = new System.Drawing.Font("Gabriola", 21.75F, System.Drawing.FontStyle.Bold);
            this.loginInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.loginInfoLabel.Location = new System.Drawing.Point(29, 146);
            this.loginInfoLabel.Name = "loginInfoLabel";
            this.loginInfoLabel.Size = new System.Drawing.Size(129, 54);
            this.loginInfoLabel.TabIndex = 7;
            this.loginInfoLabel.Text = "Login Type";
            // 
            // LoginLinkButton
            // 
            this.LoginLinkButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(99)))), ((int)(((byte)(204)))));
            this.LoginLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoginLinkButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.LoginLinkButton.ForeColor = System.Drawing.Color.White;
            this.LoginLinkButton.Location = new System.Drawing.Point(326, 203);
            this.LoginLinkButton.Name = "LoginLinkButton";
            this.LoginLinkButton.Size = new System.Drawing.Size(144, 37);
            this.LoginLinkButton.TabIndex = 8;
            this.LoginLinkButton.Text = "Login Link";
            this.LoginLinkButton.UseVisualStyleBackColor = false;
            this.LoginLinkButton.Click += new System.EventHandler(this.LoginLinkButton_Click);
            // 
            // LaunchFnButton
            // 
            this.LaunchFnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(50)))), ((int)(((byte)(168)))));
            this.LaunchFnButton.Enabled = false;
            this.LaunchFnButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LaunchFnButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.LaunchFnButton.ForeColor = System.Drawing.Color.White;
            this.LaunchFnButton.Location = new System.Drawing.Point(635, 392);
            this.LaunchFnButton.Name = "LaunchFnButton";
            this.LaunchFnButton.Size = new System.Drawing.Size(141, 37);
            this.LaunchFnButton.TabIndex = 9;
            this.LaunchFnButton.Text = "Launch FN";
            this.LaunchFnButton.UseVisualStyleBackColor = false;
            this.LaunchFnButton.Click += new System.EventHandler(this.LaunchFnButton_Click);
            // 
            // LoginDeviceButton
            // 
            this.LoginDeviceButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(99)))), ((int)(((byte)(204)))));
            this.LoginDeviceButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoginDeviceButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.LoginDeviceButton.ForeColor = System.Drawing.Color.White;
            this.LoginDeviceButton.Location = new System.Drawing.Point(176, 203);
            this.LoginDeviceButton.Name = "LoginDeviceButton";
            this.LoginDeviceButton.Size = new System.Drawing.Size(144, 37);
            this.LoginDeviceButton.TabIndex = 10;
            this.LoginDeviceButton.Text = "Login Device";
            this.LoginDeviceButton.UseVisualStyleBackColor = false;
            this.LoginDeviceButton.Click += new System.EventHandler(this.LoginDeviceButton_Click);
            // 
            // CreateDeviceAuthButton
            // 
            this.CreateDeviceAuthButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(91)))), ((int)(((byte)(226)))));
            this.CreateDeviceAuthButton.Enabled = false;
            this.CreateDeviceAuthButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CreateDeviceAuthButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold);
            this.CreateDeviceAuthButton.ForeColor = System.Drawing.Color.White;
            this.CreateDeviceAuthButton.Location = new System.Drawing.Point(475, 203);
            this.CreateDeviceAuthButton.Name = "CreateDeviceAuthButton";
            this.CreateDeviceAuthButton.Size = new System.Drawing.Size(203, 37);
            this.CreateDeviceAuthButton.TabIndex = 11;
            this.CreateDeviceAuthButton.Text = "Create Device";
            this.CreateDeviceAuthButton.UseVisualStyleBackColor = false;
            this.CreateDeviceAuthButton.Click += new System.EventHandler(this.CreateDeviceAuthButton_Click);
            // 
            // AccountSelectorComboBox
            // 
            this.AccountSelectorComboBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.AccountSelectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AccountSelectorComboBox.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountSelectorComboBox.FormattingEnabled = true;
            this.AccountSelectorComboBox.Location = new System.Drawing.Point(176, 158);
            this.AccountSelectorComboBox.Name = "AccountSelectorComboBox";
            this.AccountSelectorComboBox.Size = new System.Drawing.Size(502, 33);
            this.AccountSelectorComboBox.TabIndex = 13;
            this.AccountSelectorComboBox.SelectedIndexChanged += new System.EventHandler(this.AccountSelectorComboBox_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.BackgroundImage = global::CloudStorageModifier.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(800, 452);
            this.Controls.Add(this.AccountSelectorComboBox);
            this.Controls.Add(this.CreateDeviceAuthButton);
            this.Controls.Add(this.LoginDeviceButton);
            this.Controls.Add(this.LaunchFnButton);
            this.Controls.Add(this.LoginLinkButton);
            this.Controls.Add(this.loginInfoLabel);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.LogOutButton);
            this.Controls.Add(this.ListButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.cloudInfoLabel);
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
        private System.Windows.Forms.Label cloudInfoLabel;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button ListButton;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button LogOutButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label loginInfoLabel;
        private System.Windows.Forms.Button LoginLinkButton;
        private System.Windows.Forms.Button LaunchFnButton;
        private System.Windows.Forms.Button LoginDeviceButton;
        private System.Windows.Forms.Button CreateDeviceAuthButton;
        private System.Windows.Forms.ComboBox AccountSelectorComboBox;
    }
}

