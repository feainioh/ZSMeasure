namespace ZSMeasure
{
    partial class myCCDHelp
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_Y = new System.Windows.Forms.Label();
            this.lbl_X = new System.Windows.Forms.Label();
            this.btn_config = new System.Windows.Forms.Button();
            this.halconCCD1 = new HalconCCD.HalconCCD();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lbl_Y);
            this.groupBox1.Controls.Add(this.lbl_X);
            this.groupBox1.Controls.Add(this.btn_config);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 247);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 44);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(242, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(16, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "T";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_Y
            // 
            this.lbl_Y.AutoSize = true;
            this.lbl_Y.Location = new System.Drawing.Point(147, 19);
            this.lbl_Y.Name = "lbl_Y";
            this.lbl_Y.Size = new System.Drawing.Size(17, 12);
            this.lbl_Y.TabIndex = 4;
            this.lbl_Y.Text = "Y:";
            // 
            // lbl_X
            // 
            this.lbl_X.AutoSize = true;
            this.lbl_X.Location = new System.Drawing.Point(27, 19);
            this.lbl_X.Name = "lbl_X";
            this.lbl_X.Size = new System.Drawing.Size(17, 12);
            this.lbl_X.TabIndex = 3;
            this.lbl_X.Text = "X:";
            // 
            // btn_config
            // 
            this.btn_config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_config.BackColor = System.Drawing.Color.PaleGreen;
            this.btn_config.Location = new System.Drawing.Point(264, 10);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(61, 31);
            this.btn_config.TabIndex = 2;
            this.btn_config.Text = "CCD配置";
            this.btn_config.UseVisualStyleBackColor = false;
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // halconCCD1
            // 
            this.halconCCD1.CCDName = "";
            this.halconCCD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.halconCCD1.Location = new System.Drawing.Point(0, 0);
            this.halconCCD1.Name = "halconCCD1";
            this.halconCCD1.Size = new System.Drawing.Size(329, 247);
            this.halconCCD1.TabIndex = 3;
            // 
            // myCCDHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.halconCCD1);
            this.Controls.Add(this.groupBox1);
            this.Name = "myCCDHelp";
            this.Size = new System.Drawing.Size(329, 291);
            this.Load += new System.EventHandler(this.myCCDHelp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public  System.Windows.Forms.Button btn_config;
        private HalconCCD.HalconCCD halconCCD1;
        private System.Windows.Forms.Label lbl_Y;
        private System.Windows.Forms.Label lbl_X;
        private System.Windows.Forms.Button button1;
    }
}
