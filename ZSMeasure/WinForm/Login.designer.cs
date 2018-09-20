namespace ZSMeasure
{
    partial class logonIn
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
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.btn_showPW = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(122, 13);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(109, 21);
            this.textBox_password.TabIndex = 1;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(46, 49);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "确认";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(156, 49);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "放弃";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_showPW
            // 
            this.btn_showPW.BackColor = System.Drawing.Color.Transparent;
            this.btn_showPW.BackgroundImage = global::ZSMeasure.Properties.Resources._027;
            this.btn_showPW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_showPW.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btn_showPW.FlatAppearance.BorderSize = 0;
            this.btn_showPW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_showPW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_showPW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_showPW.ForeColor = System.Drawing.Color.Transparent;
            this.btn_showPW.Location = new System.Drawing.Point(236, 13);
            this.btn_showPW.Name = "btn_showPW";
            this.btn_showPW.Size = new System.Drawing.Size(23, 20);
            this.btn_showPW.TabIndex = 3;
            this.btn_showPW.UseVisualStyleBackColor = false;
            this.btn_showPW.Click += new System.EventHandler(this.btn_showPW_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "登录密码：";
            // 
            // logonIn
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(275, 94);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_showPW);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "logonIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理模式登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button btn_showPW;
        private System.Windows.Forms.Label label2;
    }
}