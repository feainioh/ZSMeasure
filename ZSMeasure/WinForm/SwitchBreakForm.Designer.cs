namespace ZSMeasure
{
    partial class SwitchBreakForm
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
            this.btn_status = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.lbl_barcode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_status
            // 
            this.btn_status.BackColor = System.Drawing.Color.Red;
            this.btn_status.Enabled = false;
            this.btn_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_status.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_status.ForeColor = System.Drawing.Color.White;
            this.btn_status.Location = new System.Drawing.Point(29, 26);
            this.btn_status.Name = "btn_status";
            this.btn_status.Size = new System.Drawing.Size(637, 304);
            this.btn_status.TabIndex = 2;
            this.btn_status.Text = "-------";
            this.btn_status.UseVisualStyleBackColor = false;
            // 
            // btn_close
            // 
            this.btn_close.Image = global::ZSMeasure.Properties.Resources.close1;
            this.btn_close.Location = new System.Drawing.Point(664, 1);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(27, 23);
            this.btn_close.TabIndex = 3;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // lbl_barcode
            // 
            this.lbl_barcode.AutoSize = true;
            this.lbl_barcode.Location = new System.Drawing.Point(7, 7);
            this.lbl_barcode.Name = "lbl_barcode";
            this.lbl_barcode.Size = new System.Drawing.Size(0, 12);
            this.lbl_barcode.TabIndex = 4;
            // 
            // SwitchBreakForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(694, 360);
            this.Controls.Add(this.lbl_barcode);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_status);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SwitchBreakForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SwitchBreakForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_status;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label lbl_barcode;
    }
}