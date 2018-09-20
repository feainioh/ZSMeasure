namespace ZSMeasure
{
    partial class CCDShow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCDShow));
            this.hWindowControl_Player = new HalconDotNet.HWindowControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_oneShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_continuousShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_init = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_save = new System.Windows.Forms.ToolStripButton();
            this.tsb_loadImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_loadmodel = new System.Windows.Forms.ToolStripButton();
            this.tsb_match = new System.Windows.Forms.ToolStripButton();
            this.tsl_initstatus = new System.Windows.Forms.ToolStripButton();
            this.label_Title = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl_Player
            // 
            this.hWindowControl_Player.BackColor = System.Drawing.Color.Black;
            this.hWindowControl_Player.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl_Player.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl_Player.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hWindowControl_Player.ImagePart = new System.Drawing.Rectangle(0, 0, 575, 349);
            this.hWindowControl_Player.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl_Player.Name = "hWindowControl_Player";
            this.hWindowControl_Player.Size = new System.Drawing.Size(239, 251);
            this.hWindowControl_Player.TabIndex = 3;
            this.hWindowControl_Player.WindowSize = new System.Drawing.Size(239, 251);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_oneShot,
            this.toolStripSeparator1,
            this.tsb_continuousShot,
            this.toolStripSeparator2,
            this.tsb_stop,
            this.toolStripSeparator3,
            this.tsb_init,
            this.toolStripSeparator4,
            this.toolStripSeparator6,
            this.tsb_save,
            this.tsb_loadImage,
            this.toolStripSeparator5,
            this.toolStripSeparator7,
            this.tsb_loadmodel,
            this.tsb_match,
            this.tsl_initstatus});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(239, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(34, 251);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_oneShot
            // 
            this.tsb_oneShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_oneShot.Enabled = false;
            this.tsb_oneShot.Image = ((System.Drawing.Image)(resources.GetObject("tsb_oneShot.Image")));
            this.tsb_oneShot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_oneShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_oneShot.Name = "tsb_oneShot";
            this.tsb_oneShot.Size = new System.Drawing.Size(31, 29);
            this.tsb_oneShot.Text = "OneShot";
            this.tsb_oneShot.Click += new System.EventHandler(this.tsb_oneShot_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(31, 6);
            // 
            // tsb_continuousShot
            // 
            this.tsb_continuousShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_continuousShot.Enabled = false;
            this.tsb_continuousShot.Image = ((System.Drawing.Image)(resources.GetObject("tsb_continuousShot.Image")));
            this.tsb_continuousShot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_continuousShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_continuousShot.Name = "tsb_continuousShot";
            this.tsb_continuousShot.Size = new System.Drawing.Size(31, 31);
            this.tsb_continuousShot.Text = "Continue";
            this.tsb_continuousShot.Click += new System.EventHandler(this.tsb_continuousShot_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(31, 6);
            // 
            // tsb_stop
            // 
            this.tsb_stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_stop.Enabled = false;
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
            this.tsb_init.Size = new System.Drawing.Size(31, 27);
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(31, 6);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(31, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(31, 6);
            // 
            // tsb_loadmodel
            // 
            this.tsb_loadmodel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_loadmodel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_loadmodel.Image")));
            this.tsb_loadmodel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_loadmodel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_loadmodel.Name = "tsb_loadmodel";
            this.tsb_loadmodel.Size = new System.Drawing.Size(31, 29);
            this.tsb_loadmodel.Text = "LoadModel";
            this.tsb_loadmodel.Visible = false;
            this.tsb_loadmodel.Click += new System.EventHandler(this.tsb_loadmodel_Click);
            // 
            // tsb_match
            // 
            this.tsb_match.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_match.Image = ((System.Drawing.Image)(resources.GetObject("tsb_match.Image")));
            this.tsb_match.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_match.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_match.Name = "tsb_match";
            this.tsb_match.Size = new System.Drawing.Size(31, 29);
            this.tsb_match.Text = "MatchShape";
            this.tsb_match.Visible = false;
            this.tsb_match.Click += new System.EventHandler(this.tsb_match_Click);
            // 
            // tsl_initstatus
            // 
            this.tsl_initstatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsl_initstatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsl_initstatus.Name = "tsl_initstatus";
            this.tsl_initstatus.Size = new System.Drawing.Size(31, 4);
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.BackColor = System.Drawing.Color.Black;
            this.label_Title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_Title.Location = new System.Drawing.Point(2, 2);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(41, 12);
            this.label_Title.TabIndex = 4;
            this.label_Title.Text = "label1";
            // 
            // CCDShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.hWindowControl_Player);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CCDShow";
            this.Size = new System.Drawing.Size(273, 251);
            this.Load += new System.EventHandler(this.CCDShow_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl_Player;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_oneShot;
        private System.Windows.Forms.ToolStripButton tsb_continuousShot;
        private System.Windows.Forms.ToolStripButton tsb_stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsb_init;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsb_save;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsb_loadImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsl_initstatus;
        private System.Windows.Forms.ToolStripButton tsb_loadmodel;
        private System.Windows.Forms.ToolStripButton tsb_match;
    }
}
