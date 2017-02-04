namespace UrbanMShare
{
    partial class login
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblogin = new System.Windows.Forms.Label();
            this.lbpassword = new System.Windows.Forms.Label();
            this.tbusername = new System.Windows.Forms.TextBox();
            this.tbpassword = new System.Windows.Forms.TextBox();
            this.btlogin = new System.Windows.Forms.Button();
            this.btnew = new System.Windows.Forms.Button();
            this.Sair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(343, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 43);
            this.label1.TabIndex = 13;
            this.label1.Text = "UrbanMShare";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UrbanMShare.Properties.Resources.shareicon;
            this.pictureBox1.Location = new System.Drawing.Point(407, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // lblogin
            // 
            this.lblogin.AutoSize = true;
            this.lblogin.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblogin.ForeColor = System.Drawing.Color.White;
            this.lblogin.Location = new System.Drawing.Point(340, 302);
            this.lblogin.Name = "lblogin";
            this.lblogin.Size = new System.Drawing.Size(79, 20);
            this.lblogin.TabIndex = 14;
            this.lblogin.Text = "Utilizador:";
            // 
            // lbpassword
            // 
            this.lbpassword.AutoSize = true;
            this.lbpassword.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbpassword.ForeColor = System.Drawing.Color.White;
            this.lbpassword.Location = new System.Drawing.Point(340, 334);
            this.lbpassword.Name = "lbpassword";
            this.lbpassword.Size = new System.Drawing.Size(76, 20);
            this.lbpassword.TabIndex = 15;
            this.lbpassword.Text = "Password:";
            // 
            // tbusername
            // 
            this.tbusername.Location = new System.Drawing.Point(425, 301);
            this.tbusername.Name = "tbusername";
            this.tbusername.Size = new System.Drawing.Size(200, 20);
            this.tbusername.TabIndex = 16;
            // 
            // tbpassword
            // 
            this.tbpassword.Location = new System.Drawing.Point(425, 336);
            this.tbpassword.Name = "tbpassword";
            this.tbpassword.Size = new System.Drawing.Size(200, 20);
            this.tbpassword.TabIndex = 17;
            this.tbpassword.UseSystemPasswordChar = true;
            // 
            // btlogin
            // 
            this.btlogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.btlogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.btlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btlogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btlogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btlogin.Location = new System.Drawing.Point(343, 392);
            this.btlogin.Name = "btlogin";
            this.btlogin.Size = new System.Drawing.Size(90, 30);
            this.btlogin.TabIndex = 18;
            this.btlogin.Text = "Entrar";
            this.btlogin.UseVisualStyleBackColor = true;
            this.btlogin.Click += new System.EventHandler(this.btlogin_Click);
            // 
            // btnew
            // 
            this.btnew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.btnew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.btnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnew.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnew.Location = new System.Drawing.Point(439, 392);
            this.btnew.Name = "btnew";
            this.btnew.Size = new System.Drawing.Size(90, 30);
            this.btnew.TabIndex = 19;
            this.btnew.Text = "Criar Conta";
            this.btnew.UseVisualStyleBackColor = true;
            this.btnew.Click += new System.EventHandler(this.btnew_Click);
            // 
            // Sair
            // 
            this.Sair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.Sair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.Sair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Sair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sair.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Sair.Location = new System.Drawing.Point(535, 392);
            this.Sair.Name = "Sair";
            this.Sair.Size = new System.Drawing.Size(90, 30);
            this.Sair.TabIndex = 20;
            this.Sair.Text = "Sair";
            this.Sair.UseVisualStyleBackColor = true;
            this.Sair.Click += new System.EventHandler(this.Sair_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(103)))));
            this.Controls.Add(this.Sair);
            this.Controls.Add(this.btnew);
            this.Controls.Add(this.btlogin);
            this.Controls.Add(this.tbpassword);
            this.Controls.Add(this.tbusername);
            this.Controls.Add(this.lbpassword);
            this.Controls.Add(this.lblogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "login";
            this.Size = new System.Drawing.Size(900, 463);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblogin;
        private System.Windows.Forms.Label lbpassword;
        private System.Windows.Forms.Button btlogin;
        private System.Windows.Forms.Button btnew;
        public System.Windows.Forms.TextBox tbusername;
        public System.Windows.Forms.TextBox tbpassword;
        private System.Windows.Forms.Button Sair;
    }
}
