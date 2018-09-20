namespace HalconCCD
{
    partial class HalconCCD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HalconCCD));
            this.hWindowControl_Player = new HalconDotNet.HWindowControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_oneShot = new System.Windows.Forms.ToolStripButton();
            this.tsb_continuousShot = new System.Windows.Forms.ToolStripButton();
            this.tsb_stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_init = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_save = new System.Windows.Forms.ToolStripButton();
            this.tsb_loadImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_restImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_SearchArea = new System.Windows.Forms.Panel();
            this.checkBox_SerachArea = new System.Windows.Forms.CheckBox();
            this.btn_SearchArea = new System.Windows.Forms.Button();
            this.label_Title = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_SearchArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl_Player
            // 
            this.hWindowControl_Player.BackColor = System.Drawing.Color.Gray;
            this.hWindowControl_Player.BorderColor = System.Drawing.Color.Gray;
            this.hWindowControl_Player.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl_Player.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hWindowControl_Player.ImagePart = new System.Drawing.Rectangle(0, 0, 575, 349);
            this.hWindowControl_Player.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl_Player.Name = "hWindowControl_Player";
            this.hWindowControl_Player.Size = new System.Drawing.Size(554, 412);
            this.hWindowControl_Player.TabIndex = 4;
            this.hWindowControl_Player.WindowSize = new System.Drawing.Size(554, 412);
            this.hWindowControl_Player.Paint += new System.Windows.Forms.PaintEventHandler(this.hWindowControl_Player_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_oneShot,
            this.tsb_continuousShot,
            this.tsb_stop,
            this.toolStripSeparator3,
            this.tsb_init,
            this.toolStripSeparator4,
            this.tsb_save,
            this.tsb_loadImage,
            this.tsb_restImage,
            this.toolStripSeparator11});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(554, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(34, 412);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_oneShot
            // 
            this.tsb_oneShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_oneShot.Image = ((System.Drawing.Image)(resources.GetObject("tsb_oneShot.Image")));
            this.tsb_oneShot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_oneShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_oneShot.Name = "tsb_oneShot";
            this.tsb_oneShot.Size = new System.Drawing.Size(31, 29);
            this.tsb_oneShot.Text = "OneShot";
            this.tsb_oneShot.Click += new System.EventHandler(this.tsb_oneShot_Click);
            // 
            // tsb_continuousShot
            // 
            this.tsb_continuousShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_continuousShot.Image = ((System.Drawing.Image)(resources.GetObject("tsb_continuousShot.Image")));
            this.tsb_continuousShot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_continuousShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_continuousShot.Name = "tsb_continuousShot";
            this.tsb_continuousShot.Size = new System.Drawing.Size(31, 31);
            this.tsb_continuousShot.Text = "Continue";
            this.tsb_continuousShot.Click += new System.EventHandler(this.tsb_continuousShot_Click);
            // 
            // tsb_stop
            // 
            this.tsb_stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_stop.Image = ((System.Drawing.Image)(resources.GetObject("tsb_stop.Image")));
            this.tsb_stop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_stop.Name = "tsb_stop";
            this.tsb_stop.Size = new System.Drawing.Size(31, 27);
            this.tsb_stop.Text = "Stop";
            this.tsb_stop.Click += new System.EventHandler(this.tsb_stop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(31, 6);
            // 
            // tsb_init
            // 
            this.tsb_init.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_init.Image = ((System.Drawing.Image)(resources.GetObject("tsb_init.Image")));
            this.tsb_init.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_init.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_init.Name = "tsb_init";
            this.tsb_init.Size = new System.Drawing.Size(31, 29);
            this.tsb_init.Text = "Init";
            this.tsb_init.Click += new System.EventHandler(this.tsb_init_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.BackColor = System.Drawing.Color.Black;
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(31, 6);
            // 
            // tsb_save
            // 
            this.tsb_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_save.Image")));
            this.tsb_save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save.Name = "tsb_save";
            this.tsb_save.Size = new System.Drawing.Size(31, 29);
            this.tsb_save.Text = "SaveImage";
            this.tsb_save.Click += new System.EventHandler(this.tsb_save_Click);
            // 
            // tsb_loadImage
            // 
            this.tsb_loadImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_loadImage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_loadImage.Image")));
            this.tsb_loadImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_loadImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_loadImage.Name = "tsb_loadImage";
            this.tsb_loadImage.Size = new System.Drawing.Size(31, 26);
            this.tsb_loadImage.Text = "LoadImage";
            this.tsb_loadImage.Click += new System.EventHandler(this.tsb_loadImage_Click);
            // 
            // tsb_restImage
            // 
            this.tsb_restImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_restImage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_restImage.Image")));
            this.tsb_restImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_restImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_restImage.Name = "tsb_restImage";
            this.tsb_restImage.Size = new System.Drawing.Size(31, 27);
            this.tsb_restImage.Text = "ResetImage";
            this.tsb_restImage.Click += new System.EventHandler(this.tsb_restImage_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(31, 6);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_SearchArea);
            this.panel1.Controls.Add(this.label_Title);
            this.panel1.Controls.Add(this.hWindowControl_Player);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 412);
            this.panel1.TabIndex = 8;
            // 
            // panel_SearchArea
            // 
            this.panel_SearchArea.BackColor = System.Drawing.Color.Transparent;
            this.panel_SearchArea.Controls.Add(this.checkBox_SerachArea);
            this.panel_SearchArea.Controls.Add(this.btn_SearchArea);
            this.panel_SearchArea.Location = new System.Drawing.Point(0, 17);
            this.panel_SearchArea.Name = "panel_SearchArea";
            this.panel_SearchArea.Size = new System.Drawing.Size(66, 23);
            this.panel_SearchArea.TabIndex = 14;
            // 
            // checkBox_SerachArea
            // 
            this.checkBox_SerachArea.AutoSize = true;
            this.checkBox_SerachArea.Checked = true;
            this.checkBox_SerachArea.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SerachArea.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_SerachArea.Location = new System.Drawing.Point(5, 5);
            this.checkBox_SerachArea.Name = "checkBox_SerachArea";
            this.checkBox_SerachArea.Size = new System.Drawing.Size(15, 14);
            this.checkBox_SerachArea.TabIndex = 13;
            this.checkBox_SerachArea.UseVisualStyleBackColor = true;
            this.checkBox_SerachArea.CheckedChanged += new System.EventHandler(this.checkBox_SerachArea_CheckedChanged);
            // 
            // btn_SearchArea
            // 
            this.btn_SearchArea.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SearchArea.Location = new System.Drawing.Point(19, 1);
            this.btn_SearchArea.Name = "btn_SearchArea";
            this.btn_SearchArea.Size = new System.Drawing.Size(44, 21);
            this.btn_SearchArea.TabIndex = 12;
            this.btn_SearchArea.Text = "Area";
            this.btn_SearchArea.UseVisualStyleBackColor = true;
            this.btn_SearchArea.Click += new System.EventHandler(this.btn_SearchArea_Click);
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Location = new System.Drawing.Point(2, 2);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(41, 12);
            this.label_Title.TabIndex = 5;
            this.label_Title.Text = "label1";
            // 
            // HalconCCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "HalconCCD";
            this.Size = new System.Drawing.Size(588, 412);
            this.Load += new System.EventHandler(this.HalconCCD_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_SearchArea.ResumeLayout(false);
            this.panel_SearchArea.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  HalconDotNet.HWindowControl hWindowControl_Player;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_oneShot;
        private System.Windows.Forms.ToolStripButton tsb_continuousShot;
        private System.Windows.Forms.ToolStripButton tsb_stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsb_init;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsb_save;
        private System.Windows.Forms.ToolStripButton tsb_loadImage;
        private System.Windows.Forms.ToolStripButton tsb_restImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Panel panel_SearchArea;
        private System.Windows.Forms.CheckBox checkBox_SerachArea;
        private System.Windows.Forms.Button btn_SearchArea;
    }
}
