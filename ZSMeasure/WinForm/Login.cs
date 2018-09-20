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
    public partial class logonIn : Form
    {
        private string m_passWord = "1"; //"santec1234";
        private string m_passWordSupper = "2"; //"santec1234";
        private string m_passWordZhengZhaolei = "12346";

        public logonIn()
        {
            InitializeComponent();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (textBox_password.Text.ToUpper() == m_passWord.ToUpper())
            {
                this.DialogResult = DialogResult.OK;
                GlobalVar.gl_bAdmin = GlobalVar.AdminMode.Admin;
            }
            else if (textBox_password.Text.ToUpper() == m_passWordSupper.ToUpper())
            {
                this.DialogResult = DialogResult.OK;
                GlobalVar.gl_bAdmin = GlobalVar.AdminMode.SupperAdmin;
            }
            else if (textBox_password.Text.ToUpper() == m_passWordZhengZhaolei.ToUpper())
            {
                this.DialogResult = DialogResult.OK;
                GlobalVar.gl_bAdmin = GlobalVar.AdminMode.ZhengZhaolei;
            }
            else
            {
                MessageBox.Show("密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                GlobalVar.gl_bAdmin = GlobalVar.AdminMode.Nomal;
            }
        }

        private bool bShowChar = false;
        private void btn_showPW_Click(object sender, EventArgs e)
        {
            if (!bShowChar)
            {
                btn_showPW.BackgroundImage = global::ZSMeasure.Properties.Resources._026;
                textBox_password.PasswordChar = '\0';
            }
            else
            {
                btn_showPW.BackgroundImage = global::ZSMeasure.Properties.Resources._027;
                textBox_password.PasswordChar = '*';
            }
            bShowChar = !bShowChar;
        }
    }
}
