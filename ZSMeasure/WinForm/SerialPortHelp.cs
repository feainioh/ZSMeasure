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
    public partial class SerialPortHelp : Form
    {
        string m_fileConfig = Application.StartupPath + "\\CONFIG\\Config.ini";
        private double PluseMM = 6400 * 1.0 / 95; //脉冲距离比例
        #region 属性
        public double _step1Pulse
        {
            get { return Convert.ToDouble(numericUpDown_step1.Value); }
            set
            {
                try { numericUpDown_step1.Value = Convert.ToDecimal(value); }
                catch { numericUpDown_step1.Value = 9; }
            }
        }
        public int _ACCS
        {
            get { return Convert.ToInt32(numericUpDown_a.Value); }
            set
            {
                try { numericUpDown_a.Value = Convert.ToInt32(value); }
                catch { numericUpDown_a.Value = 10; }
            }
        }
        public int _VELS
        {
            get { return Convert.ToInt32(numericUpDown_v.Value); }
            set
            {
                try { numericUpDown_v.Value = Convert.ToInt32(value); }
                catch { numericUpDown_v.Value = 10; }
            }
        }
        public int _DCLS
        {
            get { return Convert.ToInt32(numericUpDown_d.Value); }
            set
            {
                try { numericUpDown_d.Value = Convert.ToInt32(value); }
                catch { numericUpDown_d.Value = 10; }
            }
        }
        /// <summary>
        /// 条码枪串口
        /// </summary>
        public string scanPort
        {
            get { return comboBox_scanport.Text.ToString().Trim(); }
            set
            {
                this.Invoke(new EventHandler(delegate
                {
                    for (int i = 0; i < comboBox_scanport.Items.Count; i++)
                    {
                        if (comboBox_scanport.Items[i].ToString() == value)
                        {
                            comboBox_scanport.SelectedIndex = i;
                            break;
                        }
                    }
                    
                }));
            }
        }
        /// <summary>
        /// 标准片量测标准
        /// </summary>
        public double CalibValue
        {
            get { return Convert.ToDouble(numericUpDown_calib.Value); }
            set
            {
                try { numericUpDown_calib.Value = Convert.ToDecimal(value); }
                catch { numericUpDown_calib.Value = 9; }
            }
        }
        public double CalibSpace
        {
            get { return Convert.ToDouble(numericUpDown_calibSpace.Value); }
            set
            {
                try { numericUpDown_calibSpace.Value = Convert.ToDecimal(value); }
                catch { numericUpDown_calibSpace.Value = 12; }
            }
        }
        public int TestCount
        {
            get { return Convert.ToInt32(numericUpDown_TestCount.Value); }
            set
            {
                try { numericUpDown_TestCount.Value = Convert.ToDecimal(value); }
                catch { numericUpDown_TestCount.Value = 0; }
            }
        }
        public int TestSub
        {
            get { return Convert.ToInt32(numericUpDown_TestSub.Value); }
            set
            {
                try { numericUpDown_TestSub.Value = Convert.ToDecimal(value); }
                catch { numericUpDown_TestSub.Value = 9; }
            }
        }
        //补偿值
        public double OffsetFAI1
        {
            get
            {
                try
                {
                    return Convert.ToDouble(txtbox_offsetFAI1.Text.ToString());
                }
                catch { return 0; }
            }
            set
            {
                try
                {
                    txtbox_offsetFAI1.Text = value.ToString();
                }
                catch { txtbox_offsetFAI1.Text = "0"; }
            }
        }
        public double OffsetFAI2
        {
            get
            {
                try
                {
                    return Convert.ToDouble(txtbox_offsetFAI2.Text.ToString());
                }
                catch { return 0; }
            }
            set
            {
                try
                {
                    txtbox_offsetFAI2.Text = value.ToString();
                }
                catch { txtbox_offsetFAI2.Text = "0"; }
            }
        }
        public double OffsetFAI3
        {
            get
            {
                try
                {
                    return Convert.ToDouble(txtbox_offsetFAI3.Text.ToString());
                }
                catch { return 0; }
            }
            set
            {
                try
                {
                    txtbox_offsetFAI3.Text = value.ToString();
                }
                catch { txtbox_offsetFAI3.Text = "0"; }
            }
        }
        public double OffsetFAI4
        {
            get
            {
                try
                {
                    return Convert.ToDouble(txtbox_offsetFAI4.Text.ToString());
                }
                catch { return 0; }
            }
            set
            {
                try
                {
                    txtbox_offsetFAI4.Text = value.ToString();
                }
                catch { txtbox_offsetFAI4.Text = "0"; }
            }
        }
        #endregion

        public SerialPortHelp()
        {
            InitializeComponent();
            axSerialPort1.event_DataReceived += new AxSerialPort.dele_DataRecevice(axSerialPort1_event_DataReceived);
            string[] m_strListSerialPorts = System.IO.Ports.SerialPort.GetPortNames();
            comboBox_scanport.Items.Clear();
            for (int i = 0; i < m_strListSerialPorts.Length; i++)
            {
                comboBox_scanport.Items.Add(m_strListSerialPorts[i]);
            }
        }

        private void SerialPortHelp_Load(object sender, EventArgs e)
        {
            
        }

        private void SerialPortHelp_Shown(object sender, EventArgs e)
        {
            if(GlobalVar.gl_bNeedScanBarcode)
                scanPort = GlobalVar.gl_sp_Scan.PortName.ToString();
            checkBox_scanBarcode.Checked = GlobalVar.gl_bNeedScanBarcode;
            if (GlobalVar.gl_nScanMode == 0)
                radioButton_manualScan.Checked = true;
            if (GlobalVar.gl_nScanMode == 1)
                radioButton_scanPort.Checked = true;
            groupBox_offset.Visible = (GlobalVar.gl_bAdmin == GlobalVar.AdminMode.ZhengZhaolei) ? true : false;
            panel_calibSpace.Visible = (GlobalVar.gl_bAdmin == GlobalVar.AdminMode.ZhengZhaolei) ? true : false;
            checkBox_xld.Checked = GlobalVar.m_bUseXLD;
            checkBox_useOffset.Checked = GlobalVar.m_bUseOffset;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public void OpenSP()
        {
            axSerialPort1.Open();
        }

        public void AllowWork()
        {
            try
            {
                axSerialPort1.Send("E1", true, true); //允许作业
            }
            catch (Exception ex)
            {
                CommonFunc.writeLog("发送允许作业失败：" + ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="bIsHostSend">是否PC主发</param>
        /// <param name="bNeedReply">是否需要下位机回复</param>
        public void Send(string str, bool bIsHostSend, bool bNeedReply)
        {
            if (GlobalVar.m_bManualStart) return;
            try
            {
                axSerialPort1.Send(str, bIsHostSend, bNeedReply);
            }
            catch (Exception ex)
            {
                CommonFunc.writeLog("发送失败：" + ex.ToString());
            }
        }

        private void axSerialPort1_event_DataReceived(string strRecv)
        {
            int nIdxQuestion = strRecv.IndexOf("?");
            int nIdxAnswer = strRecv.IndexOf("!");
            //下位机主发
            if (nIdxQuestion >= 0)
            {
                switch (strRecv.Substring(nIdxQuestion + 1, 1).ToUpper())
                {
                    case "B":
                        CommonFunc.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_StartScan, (IntPtr)0, (IntPtr)0); //开始扫条码
                        break;
                    case "W":
                        CommonFunc.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_ReadyForTest, (IntPtr)0, (IntPtr)0); //开始拍照
                        break;
                }
            }
            //下位机回复
            else if (nIdxAnswer >= 0)
            {
                switch (strRecv.Substring(nIdxAnswer + 1, 1))
                {
                    case "R":      //读取下位机步进脉冲
                        string str_pos = strRecv.Substring(nIdxAnswer + 2, strRecv.IndexOf("#") - 2);
                        IntPtr ptr = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(str_pos);
                        string str1 = str_pos.Substring(2);
                        this.Invoke(new Action(() => {
                            _step1Pulse = Convert.ToDouble(str1) / PluseMM;
                        }));
                        break;
                    case "V":     //读取电机速度
                        string strV = strRecv.Substring(nIdxAnswer + 2, strRecv.IndexOf("#") - 2);
                        IntPtr ptrV = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(strV);
                        string _strA = strV.Substring(0, 2);
                        string _strV = strV.Substring(2, 2);
                        string _strD = strV.Substring(4, 2);
                        this.Invoke(new Action(() => {
                            _ACCS = Convert.ToInt32(_strA);
                            _VELS = Convert.ToInt32(_strV);
                            _DCLS = Convert.ToInt32(_strD);
                        }));
                        break;
                    case "E":  //允许作业
                        GlobalVar.gl_bAllowWork = true;
                        break;
                }
            }
        }

        private void btn_step1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "R01";
                axSerialPort1.Send(str, true, true);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void btn_v_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "V";
                axSerialPort1.Send(str, true, true);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void numericUpDown_pulse_ValueChanged(object sender, EventArgs e)
        {
            //CommonFunc.Write("Pulse", ""_Pulse.ToString());
        }
                
        private void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                axSerialPort1.Send("U", true, true); //复位
            }
            catch { }
        }

        private void btn_manualOpen_Click(object sender, EventArgs e)
        {
            try
            {
                axSerialPort1.Send("M1", true, true); //手动模式启动
                btn_manualOpen.BackColor = Color.DarkGray;
                btn_manualClose.BackColor = Color.SteelBlue;
            }
            catch{}
        }

        private void btn_manualClose_Click(object sender, EventArgs e)
        {
            axSerialPort1.Send("M0", true, true); //手动模式关闭
            btn_manualOpen.BackColor = Color.SteelBlue;
            btn_manualClose.BackColor = Color.DarkGray;
        }

        private void btn_step1W_Click(object sender, EventArgs e)
        {
            try
            {
                if (_step1Pulse == 0) return;
                double stepPulse = _step1Pulse * PluseMM;
                string str = "S01" + Math.Round(stepPulse, 0);
                axSerialPort1.Send(str, true, true);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void btn_vW_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "A" + _ACCS.ToString("00") + _VELS.ToString("00") + _DCLS.ToString("00");
                axSerialPort1.Send(str, true, true);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void comboBox_scanport_DropDown(object sender, EventArgs e)
        {
            string[] m_strListSerialPorts = System.IO.Ports.SerialPort.GetPortNames();
            comboBox_scanport.Items.Clear();
            for(int i=0;i<m_strListSerialPorts.Length;i++)
            {
                comboBox_scanport.Items.Add(m_strListSerialPorts[i]);
            }
        }

        private void comboBox_scanport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string commPort = axSerialPort1.m_strPortName;
                string scanPort = comboBox_scanport.Text.ToString();
                if (scanPort == commPort)
                {
                    MessageBox.Show("串口" + scanPort + "被占用");
                    return;
                }
                if (GlobalVar.gl_sp_Scan.IsOpen)
                {
                    GlobalVar.gl_sp_Scan.Close();
                }
                GlobalVar.gl_sp_Scan.PortName = scanPort;
                GlobalVar.gl_sp_Scan.DtrEnable = true; //更换条码枪需要拉高两个信号
                GlobalVar.gl_sp_Scan.RtsEnable = true;
                GlobalVar.gl_sp_Scan.Open();
                CommonFunc.Write(GlobalVar.gl_iniSection_SPScan, GlobalVar.gl_inikey_SerialPort, scanPort, m_fileConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show("条码枪串口打开失败:" + ex.ToString());
            }
        }

        private void checkBox_scanBarcode_CheckedChanged(object sender, EventArgs e)
        {
            bool bl = checkBox_scanBarcode.Checked;
            panel_scan.Enabled = bl;
            GlobalVar.gl_bNeedScanBarcode = bl;
            CommonFunc.Write(GlobalVar.gl_iniSection_SPScan, GlobalVar.gl_inikey_NeedScan, GlobalVar.gl_bNeedScanBarcode.ToString(), m_fileConfig);
        }

        private void radioButton_manualScan_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_manualScan.Checked)
                GlobalVar.gl_nScanMode = 0;
            else
                GlobalVar.gl_nScanMode = 1;
            CommonFunc.Write(GlobalVar.gl_iniSection_SPScan, GlobalVar.gl_inikey_ScanMode, GlobalVar.gl_nScanMode.ToString(), m_fileConfig);
        }

        private void radioButton_scanPort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_scanPort.Checked)
                GlobalVar.gl_nScanMode = 1;
            else
                GlobalVar.gl_nScanMode = 0;
            CommonFunc.Write(GlobalVar.gl_iniSection_SPScan, GlobalVar.gl_inikey_ScanMode, GlobalVar.gl_nScanMode.ToString(), m_fileConfig);
        }

        private void checkBox_useOffset_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.m_bUseOffset = checkBox_useOffset.Checked;
            txtbox_offsetFAI1.Enabled = txtbox_offsetFAI2.Enabled = txtbox_offsetFAI3.Enabled = txtbox_offsetFAI4.Enabled = GlobalVar.m_bUseOffset;
        }


        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SerialPortHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void checkBox_xld_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.m_bUseXLD = checkBox_xld.Checked;
        }




    }
}
