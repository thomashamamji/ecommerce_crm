namespace admin
{
    partial class adminLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.confirm = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.failure = new System.Windows.Forms.Label();
            this.success = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(324, 378);
            this.confirm.Margin = new System.Windows.Forms.Padding(4);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(359, 83);
            this.confirm.TabIndex = 0;
            this.confirm.Text = "Accéder";
            this.confirm.UseVisualStyleBackColor = true;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(324, 267);
            this.password.Margin = new System.Windows.Forms.Padding(4);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(357, 38);
            this.password.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(317, 108);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connexion de l\'administrateur";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(324, 192);
            this.username.Margin = new System.Windows.Forms.Padding(4);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(357, 38);
            this.username.TabIndex = 3;
            // 
            // failure
            // 
            this.failure.AutoSize = true;
            this.failure.ForeColor = System.Drawing.Color.Red;
            this.failure.Location = new System.Drawing.Point(330, 505);
            this.failure.Name = "failure";
            this.failure.Size = new System.Drawing.Size(338, 32);
            this.failure.TabIndex = 4;
            this.failure.Text = "L\'autentification a échoué";
            this.failure.Visible = false;
            // 
            // success
            // 
            this.success.AutoSize = true;
            this.success.ForeColor = System.Drawing.Color.LimeGreen;
            this.success.Location = new System.Drawing.Point(348, 505);
            this.success.Name = "success";
            this.success.Size = new System.Drawing.Size(320, 32);
            this.success.TabIndex = 5;
            this.success.Text = "L\'autentification a réussi";
            this.success.Visible = false;
            // 
            // adminLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 625);
            this.Controls.Add(this.success);
            this.Controls.Add(this.failure);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.confirm);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "adminLogin";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.adminLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button confirm;
        public System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label failure;
        private System.Windows.Forms.Label success;
    }
}