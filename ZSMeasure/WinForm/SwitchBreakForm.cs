using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZSMeasure
{
    public partial class SwitchBreakForm : Form
    {
        public SwitchBreakForm()
        {
            InitializeComponent();
        }

        public void ShowText(string str, Color color, string barcode = "")
        {
            if (barcode != "")
            {
                this.btn_status.Font = new System.Drawing.Font("宋体", 100F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl_barcode.Text = barcode;
            }
            else
            {
                this.btn_status.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            this.btn_status.Text = str;
            this.btn_status.BackColor = color;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            GlobalVar.SWBreakForm = null;
            this.Close(); 
        }
        //监控键盘按键
        string keybord = "";
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                GlobalVar.SWBreakForm = null;
                this.Close(); 
                keybord = "";
                return false;
            }
            if (keyData >= Keys.A && keyData <= Keys.Z)
            {
                if (keyData == Keys.A)
                    keybord = "";
                keybord = keybord + keyData.ToString();
            }
            if (keybord.Length > 3)
                keybord = "";
            return base.ProcessDialogKey(keyData);
        }

    }
}
