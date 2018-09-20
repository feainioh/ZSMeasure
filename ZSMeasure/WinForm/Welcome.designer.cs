namespace ZSMeasure
{
    partial class Welcome
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_init = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel_Title = new System.Windows.Forms.Panel();
            this.panel_init.SuspendLayout();
            this.panel_Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(22, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "系統初始化中......請稍候";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(21, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(12, 48);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(38, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(12, 48);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(55, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(12, 48);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(72, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(12, 48);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(89, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(12, 48);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Location = new System.Drawing.Point(106, 16);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(12, 48);
            this.panel6.TabIndex = 1;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(331, 56);
            this.label2.TabIndex = 0;
            this.label2.Text = "胀缩量测系统";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_init
            // 
            this.panel_init.Controls.Add(this.panel1);
            this.panel_init.Controls.Add(this.label1);
            this.panel_init.Controls.Add(this.panel15);
            this.panel_init.Controls.Add(this.panel14);
            this.panel_init.Controls.Add(this.panel13);
            this.panel_init.Controls.Add(this.panel12);
            this.panel_init.Controls.Add(this.panel11);
            this.panel_init.Controls.Add(this.panel10);
            this.panel_init.Controls.Add(this.panel9);
            this.panel_init.Controls.Add(this.panel8);
            this.panel_init.Controls.Add(this.panel7);
            this.panel_init.Controls.Add(this.panel6);
            this.panel_init.Controls.Add(this.panel2);
            this.panel_init.Controls.Add(this.panel4);
            this.panel_init.Controls.Add(this.panel3);
            this.panel_init.Controls.Add(this.panel5);
            this.panel_init.Location = new System.Drawing.Point(23, 62);
            this.panel_init.Name = "panel_init";
            this.panel_init.Size = new System.Drawing.Size(284, 109);
            this.panel_init.TabIndex = 3;
            this.panel_init.Visible = false;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.White;
            this.panel15.Location = new System.Drawing.Point(259, 16);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(12, 48);
            this.panel15.TabIndex = 1;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.White;
            this.panel14.Location = new System.Drawing.Point(242, 16);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(12, 48);
            this.panel14.TabIndex = 1;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.White;
            this.panel13.Location = new System.Drawing.Point(225, 16);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(12, 48);
            this.panel13.TabIndex = 1;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.White;
            this.panel12.Location = new System.Drawing.Point(208, 16);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(12, 48);
            this.panel12.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.White;
            this.panel11.Location = new System.Drawing.Point(191, 16);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(12, 48);
            this.panel11.TabIndex = 1;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.Location = new System.Drawing.Point(174, 16);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(12, 48);
            this.panel10.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Location = new System.Drawing.Point(157, 16);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(12, 48);
            this.panel9.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Location = new System.Drawing.Point(140, 16);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(12, 48);
            this.panel8.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Location = new System.Drawing.Point(123, 16);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(12, 48);
            this.panel7.TabIndex = 1;
            // 
            // panel_Title
            // 
            this.panel_Title.BackColor = System.Drawing.Color.LightGray;
            this.panel_Title.Controls.Add(this.label2);
            this.panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Title.Location = new System.Drawing.Point(0, 0);
            this.panel_Title.Name = "panel_Title";
            this.panel_Title.Size = new System.Drawing.Size(331, 56);
            this.panel_Title.TabIndex = 4;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.ClientSize = new System.Drawing.Size(331, 182);
            this.Controls.Add(this.panel_Title);
            this.Controls.Add(this.panel_init);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Welcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            this.panel_init.ResumeLayout(false);
            this.panel_init.PerformLayout();
            this.panel_Title.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_init;
        private System.Windows.Forms.Panel panel_Title;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel8;

    }
}