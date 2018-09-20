using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.IO.Ports;
using HalconDotNet;

namespace ZSMeasure
{
    public partial class MainForm : Form
    {
        int iActulaWidth =0;
        int iActulaHeight = 0;
        private double IFlexZXAngle = 2.50251711; //客户测量胀缩的偏移角度              A85: 2.50251711, 2.5011125 
        private double IFlex14Angle = 17.82382166; //以14点建立坐标系，与制品水平的角度 A85: 17.82382166,17.822417
        private double[] CONST_DFAIA85 = new double[4] { 148.15, 193.94, 148.15, 193.94 }; //胀缩测试标准值
        private double[] CONST_DFAIA86 = new double[4] { 173.404, 47.32, 173.404, 47.32 }; //胀缩测试标准值
        private const int CONST_NeedMode = 100; //100次需要测试菲林片的数据，与校正时的差0.01需要重新校准
        private double m_CalibValue = 0.0; //1小时量测校准片，超过此范围需重新标定相机
        private int CONST_ReCalibCount = 3;//量测校准片小于0.01mm，可重复测试3次，必须标定相机
        private static int m_TestCount = 0; //用于计算量测4次的平均值
        private static int m_TestSub = 0;   //用于计算去掉最大最小值平均值
        private double[] m_arrayFAI1; //处理平均值
        private double[] m_arrayFAI2; //处理平均值
        private double[] m_arrayFAI3; //处理平均值
        private double[] m_arrayFAI4; //处理平均值
        //=========================Flag=========================
        private bool m_bIn9Point = false;     //九点标定
        private bool m_bLocalTest = false;    //单机测试
        private int m_nCalibCount = 0;        //可量测校准片次数
        private int m_nCalcFunc = 3;          //计算方法，默认2. Mark点14建立坐标系
        //=========================INI=========================
        private string ini_Section_TestInfo = "TestInfo";
        private string ini_Key_Last9Point = "Last9PointTime";
        private string ini_Key_CalibValue = "CalibValue";
        private string ini_Key_CalibSpace = "CalibSpace";
        private string ini_Key_TestCount = "TestCount";
        private string ini_Key_TestSub = "TestSub";
        private string ini_Key_ReCalibCount = "ReCalibCount";
        private string ini_Key_ManualStart = "ManualStart";
        private string ini_Key_CalcTime = "CalcTime";
        private string ini_Key_CameraType = "CameraType";

        private string ini_Section_Offset = "Offset";
        private string ini_Key_OffsetFAI = "OffsetFAI";
        private string ini_Key_OffsetAngle = "OffsetAngle";
        private string ini_Key_UseOffset = "UseOffset";
        private string ini_Key_UseXLD = "UseXLD";

        private string ini_Section_ShtBarLen = "ShtBarLen";
        private string ini_Section_RotateAngle = "RotateAngle";
        //=========================Floder=========================
        private string m_pathConfig = Application.StartupPath + "\\CONFIG";
        private string m_pathLog = Application.StartupPath + "\\LOG";
        private string m_pathImage = Application.StartupPath + "\\SaveImage";
        private string m_pathResult = Application.StartupPath + "\\Result";
        private string m_pathResultJC = Application.StartupPath + "\\JC";
        private string m_fileConfig = "";
        //=========================串口配置=========================
        private SerialPortHelp sph = new SerialPortHelp();
        private bool m_bAllHandleOver = true; //所有相机处理完毕
        private myCCDHelp[] CCDHelp = new myCCDHelp[4];
        //=========================菲林片11*11矩阵=========================
        private string m_fileCADMatrix = "";
        private double[] m_FAICADStandard = new double[4]; //标准片标准值
        private double[] m_FAIStandard = new double[4] { 148.15, 193.94, 148.15, 193.94 }; //胀缩测试标准值
        //=========================测试=========================
        private int m_Count = 1;                           //测试次数
        private const int ShowResultRow = 4;               //结果显示几行
        private string m_barcode = "";                     //扫描条码
        private bool m_bShtMasterScanOver = false;         //条码扫描完成
        private double[] m_OffsetFAIConst = new double[4]; //硬补偿
        private double[] m_OffsetFAI = new double[4];      //补偿值
        private double m_OffsetAngle = 0.0;                //补偿角度
        private TextSpeech speaker = TextSpeech.GetSpeaker();//语音播报
        //=========================作业信息=========================
        private string m_strOperator = "";     //作业员
        private string m_strLotNo = "";        //LotNo
        private string m_strProduct = "";      //品目
        private string m_strProductModel = ""; //机种
        private string m_strWorkTime = "";     //班别


        #region 属性
        //public int ShowResultRow
        //{
        //    get { return (int)numericUpDown_showResultRow.Value; }
        //    set { numericUpDown_showResultRow.Value = value; }
        //}
        #endregion

        public MainForm()
        {
            InitializeComponent();
            CCDHelp = new myCCDHelp[4] { myCCDHelp1, myCCDHelp2, myCCDHelp3, myCCDHelp4 };
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].sequence = i;
                CCDHelp[i].event_StatusText += new myCCDHelp.dele_StatusText(event_DisplayLog);
                CCDHelp[i].event_displayMouse += new myCCDHelp.dele_event_displayMouse(MainForm_event_displayMouse);
                CCDHelp[i].event_ChangeForm += new myCCDHelp.dele_ChangeForm(MainForm_event_ChangeForm);
            }
            CheckFloder();
            ReadINI();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetScreenSize();
            GlobalVar.gl_IntPtr_MainWindow = this.Handle;
            tssl_var.Text = "软件版本：" + ProgramInfo.AssemblyVersion;
            SetControlEnable(false);
            //ReadINI();
            if(!GlobalVar.m_bManualStart)
                sph.OpenSP();
            Thread thd = new Thread(new ThreadStart(delegate {
                CommonFunc.deleOldLog();
            }));
            thd.IsBackground = true;
            thd.Start();
            GlobalVar.gl_sp_Scan.DataReceived -= new SerialDataReceivedEventHandler(ScanBarcode_DataReceived);
            GlobalVar.gl_sp_Scan.DataReceived += new SerialDataReceivedEventHandler(ScanBarcode_DataReceived);
        }

        private void SetScreenSize()
        {
            iActulaWidth = Screen.PrimaryScreen.WorkingArea.Width;
            iActulaHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Height = iActulaHeight;
            int workHeight = panel_main.Height;
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].Height = workHeight / 2 - 10;
                if (CCDHelp[i].Location1.X != 0 && CCDHelp[i].Location1.Y != 0)
                {
                    CCDHelp[i].Location = CCDHelp[i].Location1;
                }
                else
                {
                    CCDHelp[i].Location1 = CCDHelp[i].Location;
                    if (i == 1 || i == 2)
                    {
                        Point loca = CCDHelp[i].Location;
                        loca.Y = (int)(workHeight / 2) + 4;
                        CCDHelp[i].Location1 = CCDHelp[i].Location = loca;
                    }
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Thread thdInitCCD = new Thread(new ThreadStart(delegate
            {
                InitCCD();
            }));
            thdInitCCD.IsBackground = true;
            thdInitCCD.Start();
        }

        private void MainForm_event_displayMouse(PointF point)
        {
            try
            {
                tssl_currentPoint.Text = point.X + "," + point.Y;
            }
            catch { }
        }

        private void ScanBarcode_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //string totalstr = GlobalVar.gl_sp_Master.ReadExisting();
                byte[] byteArray = new byte[GlobalVar.gl_sp_Scan.BytesToRead];
                GlobalVar.gl_sp_Scan.Read(byteArray, 0, GlobalVar.gl_sp_Scan.BytesToRead);
                string totalstr = System.Text.Encoding.Default.GetString(byteArray);
                if (totalstr.IndexOf("\r\n") > 0)
                {
                    totalstr = totalstr.Substring(0, totalstr.LastIndexOf("\r\n"));  //删除无效结尾
                    if (totalstr.IndexOf("\r\n") > 0)
                    {
                        totalstr = totalstr.Substring(totalstr.LastIndexOf("\r\n") + 2);  //删除无效结尾
                    }
                }
                m_barcode = totalstr;
            }
            catch (System.Exception ex)
            {
                event_DisplayLog("接收条码异常：" + ex.Message, true);
            }
            finally
            {
                m_bShtMasterScanOver = true;
            }
        }

        private void CheckFloder()
        {
            if (!Directory.Exists(m_pathConfig)) Directory.CreateDirectory(m_pathConfig);
            if (!Directory.Exists(m_pathLog)) Directory.CreateDirectory(m_pathLog);
            if (!Directory.Exists(m_pathImage)) Directory.CreateDirectory(m_pathImage);
            if (!Directory.Exists(m_pathResult)) Directory.CreateDirectory(m_pathResult);
            if (!Directory.Exists(m_pathResultJC)) Directory.CreateDirectory(m_pathResultJC);
            m_fileCADMatrix = m_pathConfig + "\\CADDATA.csv";
            m_fileConfig = m_pathConfig + "\\Config.ini";
        }

        private void ReadINI()
        {
            myCCDHelp1.ReadINI();
            myCCDHelp2.ReadINI();
            myCCDHelp3.ReadINI();
            myCCDHelp4.ReadINI();
            //厂内Sheet条码长度
            string[] strSht = CommonFunc.GetSection(ini_Section_ShtBarLen, m_fileConfig);
            if (strSht != null && strSht.Length > 0)
            {
                try
                {
                    for (int k = 0; k < strSht.Length; k++)
                    {
                        GlobalVar.gl_lsShtBarLen.Add(Convert.ToInt32(strSht[k].Trim()));
                    }
                }
                catch { GlobalVar.gl_lsShtBarLen = new List<int> { 10, 11, 16, 24 }; }
            }
            else
            {
                GlobalVar.gl_lsShtBarLen = new List<int> { 10, 11, 16, 24 };
                string str = "";
                for (int i = 0; i < GlobalVar.gl_lsShtBarLen.Count; i++)
                {
                    str += GlobalVar.gl_lsShtBarLen[i] + "\r\n";
                }
                CommonFunc.SetSection(ini_Section_ShtBarLen, str, m_fileConfig);
            }
            //程序界面启动
            string strManualStart = CommonFunc.Read(ini_Section_TestInfo, ini_Key_ManualStart, "True", m_fileConfig);
            GlobalVar.m_bManualStart = Convert.ToBoolean(strManualStart);
            ////校准片标准数据
            //for (int i = 0; i < m_FAICADStandard.Length; i++)
            //{
            //    string strFAI = CommonFunc.Read(ini_Section_TestInfo, ini_Key_CADFAI + (i + 1), "0", m_fileConfig);
            //    m_FAICADStandard[i] = Convert.ToDouble(strFAI);
            //}

            //菲林片校正时间
            string strCalcTime = CommonFunc.Read(ini_Section_TestInfo, ini_Key_CalcTime, "60", m_fileConfig);
            GlobalVar.gl_CalcTime = Convert.ToInt32(strCalcTime);
            //量测校准片标准范围
            string strCalibValue = CommonFunc.Read(ini_Section_TestInfo, ini_Key_CalibValue, "0.01", m_fileConfig);
            m_CalibValue = Convert.ToDouble(strCalibValue);
            //量测校准片间隔
            string strCalibSpace = CommonFunc.Read(ini_Section_TestInfo, ini_Key_CalibSpace, "12", m_fileConfig);
            GlobalVar.m_CalibSpace = Convert.ToDouble(strCalibSpace);
            //上次校准时间
            string strLastTime = CommonFunc.Read(ini_Section_TestInfo, ini_Key_Last9Point, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), m_fileConfig);
            GlobalVar.m_timeLast9Point = Convert.ToDateTime(strLastTime);
            //LotNo
            string strLotNo = CommonFunc.Read(GlobalVar.gl_iniSection_WorkInfo, GlobalVar.gl_inikey_LotNo, "", m_fileConfig);
            textBox_LotNo.Text = strLotNo;
            //条码枪串口
            string scanPort = CommonFunc.Read(GlobalVar.gl_iniSection_SPScan, GlobalVar.gl_inikey_SerialPort, "COM1", m_fileConfig);
            GlobalVar.gl_sp_Scan.PortName = scanPort;
            //条码扫描
            string strNeedScan = CommonFunc.Read(GlobalVar.gl_iniSection_SPScan, GlobalVar.gl_inikey_NeedScan, "True", m_fileConfig);
            GlobalVar.gl_bNeedScanBarcode = Convert.ToBoolean(strNeedScan);
            //扫描方式
            string strScanMode = CommonFunc.Read(GlobalVar.gl_iniSection_SPScan, GlobalVar.gl_inikey_ScanMode, "1", m_fileConfig);
            GlobalVar.gl_nScanMode = Convert.ToInt32(strScanMode);
            if (GlobalVar.gl_bNeedScanBarcode && GlobalVar.gl_nScanMode == 1)
                OpenScanPort();
            //测试次数
            string strTestCount = CommonFunc.Read(ini_Section_TestInfo, ini_Key_TestCount, "0", m_fileConfig);
            m_TestCount = Convert.ToInt32(strTestCount);
            //去掉最大最小值
            string strTestSub = CommonFunc.Read(ini_Section_TestInfo, ini_Key_TestSub, "0", m_fileConfig);
            m_TestSub = Convert.ToInt32(strTestSub);
            if (m_TestCount >= 3)
            {
                if (m_TestSub >= m_TestCount)
                {
                    m_TestSub = 0;
                }
            }
            InitArrayFAI();
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_TestCount, m_TestCount.ToString(), m_fileConfig);
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_TestSub, m_TestSub.ToString(), m_fileConfig);
            //相机类型
            string strCameraType = CommonFunc.Read(ini_Section_TestInfo, ini_Key_CameraType, "0", m_fileConfig);
            GlobalVar.m_nCameraType = Convert.ToInt32(strCameraType);
            SetCameraType();
        }

        private void WriteINI()
        {
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_CalibValue, m_CalibValue.ToString(), m_fileConfig);
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_CalibSpace, GlobalVar.m_CalibSpace.ToString(), m_fileConfig);
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_TestCount, m_TestCount.ToString(), m_fileConfig);
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_TestSub, m_TestSub.ToString(), m_fileConfig);
            CommonFunc.Write(ini_Section_Offset + "_" + m_strProductModel, ini_Key_UseOffset, GlobalVar.m_bUseOffset.ToString(), m_fileConfig);
            CommonFunc.Write(ini_Section_Offset + "_" + m_strProductModel, ini_Key_UseXLD, GlobalVar.m_bUseXLD.ToString(), m_fileConfig);
            //数据补偿
            for (int i = 0; i < m_OffsetFAI.Length; i++)
            {
                CommonFunc.Write(ini_Section_Offset + "_" + m_strProductModel, ini_Key_OffsetFAI + (i + 1), m_OffsetFAI[i].ToString(), m_fileConfig);
            }
        }

        /// <summary>
        /// 加载补偿值
        /// </summary>
        private void ReadOffsetINI()
        {
            //硬性补偿
            string m_fileOffsetConst = Application.StartupPath + "\\CONFIG\\SetConst.ini";
            for (int i = 0; i < m_OffsetFAIConst.Length; i++)
            {
                string strOffsetFAIConst = CommonFunc.Read(ini_Section_Offset + "_" + m_strProductModel, ini_Key_OffsetFAI + (i + 1), "0", m_fileOffsetConst);
                m_OffsetFAIConst[i] = Convert.ToDouble(strOffsetFAIConst);
            }
            //数据补偿
            for (int i = 0; i < m_OffsetFAI.Length; i++)
            {
                string strOffsetFAI = CommonFunc.Read(ini_Section_Offset + "_" + m_strProductModel, ini_Key_OffsetFAI + (i + 1), "0", m_fileConfig);
                m_OffsetFAI[i] = Convert.ToDouble(strOffsetFAI);
            }
            //角度补偿
            string strOffsetAngle = CommonFunc.Read(ini_Section_Offset + "_" + m_strProductModel, ini_Key_OffsetAngle, "0.0", m_fileConfig);
            m_OffsetAngle = Convert.ToDouble(strOffsetAngle);
            //可重复量测校准片测试
            string strReCalibCount = CommonFunc.Read(ini_Section_TestInfo, ini_Key_ReCalibCount, "3", m_fileConfig);
            CONST_ReCalibCount = Convert.ToInt32(strReCalibCount);
            //使用补偿值
            string strUseOffset = CommonFunc.Read(ini_Section_Offset + "_" + m_strProductModel, ini_Key_UseOffset, "True", m_fileConfig);
            GlobalVar.m_bUseOffset = Convert.ToBoolean(strUseOffset);
            //使用XLD
            string strUseXLD = CommonFunc.Read(ini_Section_Offset + "_" + m_strProductModel, ini_Key_UseXLD, "False", m_fileConfig);
            GlobalVar.m_bUseXLD = Convert.ToBoolean(strUseXLD);
        }

        private void DispScanMode()
        {
            if (!GlobalVar.gl_bNeedScanBarcode)
            {
                tssl_scan.Text = "无需扫描条码";
            }
            else 
            {
                tssl_scan.Text = GlobalVar.gl_nScanMode == 0 ? "扫码：手持条码枪" : "扫码：黄色条码枪";
            }
            double[] dd = GlobalVar.m_bInCalibMode ? m_FAICADStandard : m_FAIStandard;
            string str = "";
            for (int i = 0; i < dd.Length; i++)
            {
                str += dd[i] + ", ";
            }
            lbl_Standard.Text = str.Trim().TrimEnd(',');
            tssl_xld.Text = GlobalVar.m_bUseXLD ? "XLD" : "None";
        }

        //初始化相机
        private void InitCCD()
        {
            myCCDHelp1.InitCCD();
            myCCDHelp2.InitCCD();
            myCCDHelp3.InitCCD();
            myCCDHelp4.InitCCD();
        }

        #region 日志
        private void event_DisplayLog(string str, bool isError = false)
        {
            try
            {
                this.richTextBox_log.Invoke(new EventHandler(delegate
                {
                    if (richTextBox_log.Lines.Length > 2000) richTextBox_log.Clear();
                    int nSelectStart = richTextBox_log.TextLength;
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff ");
                    richTextBox_log.AppendText("\n" + time + str);
                    int nLength = richTextBox_log.TextLength - 1;
                    richTextBox_log.Select(nSelectStart, nLength);
                    richTextBox_log.SelectionColor = isError ? Color.Red : Color.Lime;
                    //设置滚动条位置
                    richTextBox_log.ScrollToCaret();
                    richTextBox_log.Invalidate();
                }));
                CommonFunc.writeLog(str);
            }
            catch { }
        }
        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox_log.Clear();
        }
        #endregion

        #region 流程处理
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case GlobalVar.WM_StartScan:
                    event_DisplayLog("====START====");
                    CloseStatusForm();
                    Thread thdScanBarcode = new Thread(new ThreadStart(() =>
                    {
                        bool bl = StartScanBarcode();
                        if (GlobalVar.m_bManualStart && bl)
                        {
                            if (!m_bAllHandleOver)
                            {
                                ShowStatusForm("上一次正在处理中", Color.Red);
                                return;
                            }
                            CommonFunc.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_ReadyForTest, (IntPtr)0, (IntPtr)0); //开始拍照
                        }
                    }));
                    thdScanBarcode.Name = "ScanBarcode";
                    thdScanBarcode.IsBackground = true;
                    thdScanBarcode.Start();
                    break;
                case GlobalVar.WM_ReadyForTest:
                    if (GlobalVar.gl_bAllowWork)
                    {
                        Thread thdReadyForTest = new Thread(new ThreadStart
                            (() =>
                            {
                                event_DisplayLog("开始拍照");
                                Thread.Sleep(2000); //500ms稳定后再拍照
                                StartForTest();  //开始拍照
                            }
                            ));
                        thdReadyForTest.Name = "ReadyForTest";
                        thdReadyForTest.IsBackground = true;
                        thdReadyForTest.Start();
                    }
                    else
                    {
                        event_DisplayLog("还未允许作业", true);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #region 条码枪
        private void OpenScanPort()
        {
            try
            {
                if (GlobalVar.gl_sp_Scan.IsOpen)
                {
                    GlobalVar.gl_sp_Scan.Close();
                }
                GlobalVar.gl_sp_Scan.DtrEnable = true; //更换条码枪需要拉高两个信号
                GlobalVar.gl_sp_Scan.RtsEnable = true;
                GlobalVar.gl_sp_Scan.Open();
            }
            catch (Exception ex)
            {
                //ShowStatusForm("条码枪串口" + GlobalVar.gl_sp_Master.PortName + "打开失败", Color.Red);
                event_DisplayLog("条码枪串口" + GlobalVar.gl_sp_Scan.PortName + "打开失败", true);
            }
        }

        //启动前检查
        private bool CheckNeedMode()
        {
            //if (!m_bNeedMode && m_testCount >= CONST_NeedMode)
            //{
            //    ShowStatusForm("测试100次了\r需要测试校准片,来检查机台的准确性", Color.Red);
            //    sph.Send("B2", false, true);
            //    return false;
            //}
            //if (m_bNeedMode)
            //{
            //    ShowStatusForm("注意：\r现在处于校准片量测中", Color.Red);
            //    event_DisplayLog("校准片校准中");
            //    sph.Send("B1", false, true); //无需扫描条码，制品进入测试
            //    return true;
            //}
            //if ((!GlobalVar.gl_bNeedScanBarcode || m_bIn9Point))
            //{
            //    sph.Send("B1", false, true); //无需扫描条码，制品进入测试
            //    return true;
            //}
            return true;
        }
        private bool StartScanBarcode()
        {
            m_barcode = "";
            if (!GlobalVar.gl_bAllowWork)
            {
                ShowStatusForm("还未允许作业", Color.Red);
                sph.Send("B2", false, true);
                return false;
            }
            if (m_bIn9Point)
            {
                sph.Send("B1", false, true); //无需扫描条码，九点标定
                return true;
            }
            if (GlobalVar.m_bNeedReCalc && m_nCalibCount >= CONST_ReCalibCount) //可允许重复量测校准片数据5次
            {
                ShowStatusForm("量测校准片数据差异 >"+ 0.01 +"mm\r需要重新标定相机", Color.Red, "", true);
                sph.Send("B2", false, true);
                return false;
            }
            //if (m_testCount >= CONST_NeedMode && !m_bNeedMode)
            //{
            //    ShowStatusForm("测试100次了\r需要测试校准片,来检查机台的准确性", Color.Red);
            //    sph.Send("B2", false, true);
            //    m_bNeedMode = true;
            //    return;
            //}
            TimeSpan ts = DateTime.Now - GlobalVar.m_timeLast9Point;
            if (!GlobalVar.m_bInCalibMode && ts.TotalMinutes > GlobalVar.m_CalibSpace * 60)
            {
                speaker.AddSpeech("已经测试" + (GlobalVar.m_CalibSpace) + "小时了，需要量测校准片");
                ShowStatusForm("已经测试" + (GlobalVar.m_CalibSpace) + "小时了\r需要测试校准片的数据,来检查机台的准确性\r在【文件】--【校准片量测】--【开启】校准片量测流程", Color.Red);
                sph.Send("B2", false, true);
                return false;
            }
            //校准片标准数据
            for (int i = 0; i < m_FAICADStandard.Length; i++)
            {
                if (m_FAICADStandard[i] == 0.0)
                {
                    ShowStatusForm("校准片标准数据为空\r需要重新标定相机", Color.Red);
                    sph.Send("B2", false, true);
                    return false; 
                }
            }
            if (GlobalVar.m_bInCalibMode)
            {
                ShowStatusForm("注意：现在处于【校准片】量测中\r确认放入的是校准片", Color.Red);
                if (MessageBox.Show("现在处于【校准片】量测中\r确认放入的是校准片","注意",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    sph.Send("B1", false, true); //无需扫描条码，校准片量测
                    return true;
                }
                else
                    return false;
            }
            if (!GlobalVar.gl_bNeedScanBarcode)
            {
                sph.Send("B1", false, true); //无需扫描条码
                return true;
            }
            return ScanBarcode();
        }

        /// <summary>
        /// 扫描条码
        /// </summary>
        private bool ScanBarcode()
        {
            try
            {
                event_DisplayLog("开始扫条码");
                if (GlobalVar.gl_nScanMode == 0) //手持条码枪
                {
                    m_barcode = txtbox_barcode.Text.ToString().Trim().ToUpper();
                }
                if (GlobalVar.gl_nScanMode == 1) //黄色条码枪
                {
                    m_bShtMasterScanOver = false;
                    byte[] byteArry_startScan = new byte[3];
                    byteArry_startScan[0] = 0x16;
                    byteArry_startScan[1] = 0x54;
                    byteArry_startScan[2] = 0x0D;
                    if (GlobalVar.gl_sp_Scan.IsOpen)
                    {
                        GlobalVar.gl_sp_Scan.DiscardInBuffer();
                        GlobalVar.gl_sp_Scan.DiscardOutBuffer();
                        GlobalVar.gl_sp_Scan.Write(byteArry_startScan, 0, 3);
                        for (int k = 0; k < 100; k++)
                        {
                            Thread.Sleep(10);
                            if (m_bShtMasterScanOver)
                                break;
                        }
                        CloseScanBarcode();
                    }
                    else
                    {
                        ShowStatusForm("条码枪串口未打开", Color.Red);
                        sph.Send("B2", false, true);
                        return false;
                    }
                }
                if (m_barcode == "")
                {
                    sph.Send("B2", false, true); //条码扫描失败，制品不进入，重新按启动
                    event_DisplayLog("扫条码失败");
                    this.Invoke(new Action(() =>
                    {
                        txtbox_barcode.Focus();
                        txtbox_barcode.SelectAll();
                    }));
                    ShowStatusForm("条码扫描失败，请重新测试", Color.Red);
                    return false;
                }
                else
                {
                    if (GlobalVar.gl_nScanMode != 0 && !GlobalVar.gl_lsShtBarLen.Contains(m_barcode.Length))
                    {
                        ShowStatusForm("条码位数不符，请重新测试", Color.Red);
                        sph.Send("B2", false, true);
                        return false;
                    }
                    this.Invoke(new Action(() =>
                    {
                        txtbox_barcode.Text = m_barcode = m_barcode.ToUpper();
                    }));
                    event_DisplayLog("扫条码成功：" + m_barcode);
                    sph.Send("B1", false, true); //条码扫描成功，回复B，制品进入测试
                }
                return true;
            }
            catch (Exception ex)
            {
                sph.Send("B2", false, true); //条码扫描失败，制品不进入，重新按启动
                ShowStatusForm("扫描条码异常，请重新测试", Color.Red);
                return false;
            }
        }
        private void CloseScanBarcode()
        {
            byte[] byteArry_startScan = new byte[3];
            byteArry_startScan[0] = 0x16;
            byteArry_startScan[1] = 0x55;
            byteArry_startScan[2] = 0x0D;
            try
            {
                GlobalVar.gl_sp_Scan.Write(byteArry_startScan, 0, 3);
            }
            catch { }
        }
        #endregion

        private void StartForTest()
        {
            if (!m_bAllHandleOver)
            {
                ShowStatusForm("上一次正在处理中", Color.Red);
                return;
            }
            //for (int i = 0; i < CCDHelp.Length; i++)
            //{
            //    CCDHelp[i].ClearDispWindow();
            //}
            event_DisplayLog("===START===");
            if (!bFortest)
            {
                for (int i = 0; i < CCDHelp.Length; i++)
                {
                    CCDHelp[i].TakeOnePic();
                }
            }
            else
            {
                for (int i = 0; i < CCDHelp.Length; i++)
                {
                    string strpic = Application.StartupPath + "\\pic\\" + CCDHelp[i].CCDName + ".jpg";
                    HObject ho_Image;
                    HOperatorSet.GenEmptyObj(out ho_Image);
                    ho_Image.Dispose(); 
                    HImage himage;
                    HOperatorSet.ReadImage(out ho_Image, strpic);
                    //if (i == 0)
                    //{
                    //    HOperatorSet.RotateImage(ho_Image, out ho_Image1, 1, "constant");
                    //    himage = new HImage(ho_Image1);
                    //}
                    //else
                        himage = new HImage(ho_Image);
                    CCDHelp[i].LoadLocalPic(himage);
                    ho_Image.Dispose();
                }
            }
            bool bl = ThreadPool.QueueUserWorkItem(ThreadWaitAllHandleOver);
        }

        private void ThreadWaitAllHandleOver(object obj)
        {
            for (; ; )
            {
                Thread.Sleep(10);
                m_bAllHandleOver = true;
                for (int i = 0; i < CCDHelp.Length; i++)
                {
                    m_bAllHandleOver &= !CCDHelp[i].m_bInHandleImage;
                }
                if (m_bAllHandleOver) //所有处理完毕
                {
                    event_DisplayLog("ALL图片处理完成");
                    break;
                }
            }
            if (!m_bIn9Point)
            {
                //非标定流程
                GetAllResult();
            }
            else
            {
                //标定流程
                Get9PointResult();
            }
            //sph.Send("W01", false, false);
        }

        private List<double> m_ListFAI1 = new List<double>(); //记录平均值
        private List<double> m_ListFAI2 = new List<double>();
        private List<double> m_ListFAI3 = new List<double>();
        private List<double> m_ListFAI4 = new List<double>();
        int testcount = 1;
        private void GetAllResult()
        {
            try
            {
                //m_Count++;
                double[] Standard = GlobalVar.m_bInCalibMode ? m_FAICADStandard : m_FAIStandard; //标准值，区分菲林片和制品

                #region 检查结果
                for (int i = 0; i < CCDHelp.Length; i++)
                {
                    if (CCDHelp[i].MarkAxis.X == 0.0 || CCDHelp[i].MarkAxis.Y == 0.0)
                    {
                        if (!m_bLocalTest) //非重复测试，发送载板退出信号
                            sph.Send("W01", false, false);
                        ShowStatusForm("未找到Mark点，请重新测试", Color.Red);
                        TestEnd();
                        return;
                    }
                }
                if (m_strOperator == "" || m_strLotNo == "" || m_strProduct == "" || m_strProductModel == "")
                {
                    ShowStatusForm("检查到作业信息为空，请先确认，再重新测试", Color.Red);
                    TestEnd();
                    return;
                }
                if (!(!GlobalVar.gl_bNeedScanBarcode || GlobalVar.m_bInCalibMode || m_bLocalTest))
                {
                    if (GlobalVar.gl_nScanMode != 0 && !GlobalVar.gl_lsShtBarLen.Contains(m_barcode.Length))
                    {
                        ShowStatusForm("条码位数不符，请重新测试", Color.Red);
                        TestEnd();
                        return;
                    }
                }
                #endregion

                double[] CalcFAI = new double[4]; //胀缩测量值
                double[] CalcFAIAveger = new double[4];   //平均角度算法
                double[] CalcFAIRotate = new double[4];   //14Mark点
                double[] CalcFAIRotate23 = new double[4]; //14，23Mark点
                #region CCD
                //CCD1-2
                double Len12X = CCDHelp[1].MarkAxis.X - CCDHelp[0].MarkAxis.X;
                double Len12Y = CCDHelp[1].MarkAxis.Y - CCDHelp[0].MarkAxis.Y;
                double Len12 = Math.Sqrt(Math.Pow(Len12X, 2) + Math.Pow(Len12Y, 2));
                double Angle12 = Math.Atan(Len12Y / Len12X);
                double CADLen12X = CCDHelp[1].CADCenterData.X - CCDHelp[0].CADCenterData.X;
                double CADLen12Y = CCDHelp[1].CADCenterData.Y - CCDHelp[0].CADCenterData.Y;
                double CADAngle12 = Math.Atan(CADLen12Y / CADLen12X);
                double DAngle12 = Angle12 - CADAngle12;
                //CCD1-3
                double Len13X = CCDHelp[2].MarkAxis.X - CCDHelp[0].MarkAxis.X;
                double Len13Y = CCDHelp[2].MarkAxis.Y - CCDHelp[0].MarkAxis.Y;
                double Len13 = Math.Sqrt(Math.Pow(Len13X, 2) + Math.Pow(Len13Y, 2));
                double Angle13 = Math.Atan(Len13Y / Len13X);
                double CADLen13X = CCDHelp[2].CADCenterData.X - CCDHelp[0].CADCenterData.X;
                double CADLen13Y = CCDHelp[2].CADCenterData.Y - CCDHelp[0].CADCenterData.Y;
                double CADAngle13 = Math.Atan(CADLen13Y / CADLen13X);
                double DAngle13 = Angle13 - CADAngle13;
                //CCD1-4
                double Len14X = CCDHelp[3].MarkAxis.X - CCDHelp[0].MarkAxis.X;
                double Len14Y = CCDHelp[3].MarkAxis.Y - CCDHelp[0].MarkAxis.Y;
                double Len14 = Math.Sqrt(Math.Pow(Len14X, 2) + Math.Pow(Len14Y, 2));
                double Angle14 = Math.Atan(Len14Y / Len14X);
                double CADLen14X = CCDHelp[3].CADCenterData.X - CCDHelp[0].CADCenterData.X;
                double CADLen14Y = CCDHelp[3].CADCenterData.Y - CCDHelp[0].CADCenterData.Y;
                double CADAngle14 = Math.Atan(CADLen14Y / CADLen14X);
                double DAngle14 = Angle14 - CADAngle14;
                #endregion

                #region 带角度计算
                double DAngle = (DAngle12 + DAngle13 + DAngle14) / 3 + IFlexZXAngle; //以CCD1为圆心，3个点偏移角度取平均值
                double newLen12X = Len12X * Math.Cos(DAngle) + Len12Y * Math.Sin(DAngle);
                double newLen12Y = Len12Y * Math.Cos(DAngle) - Len12X * Math.Sin(DAngle);
                double newLen13X = Len13X * Math.Cos(DAngle) + Len13Y * Math.Sin(DAngle);
                double newLen13Y = Len13Y * Math.Cos(DAngle) - Len13X * Math.Sin(DAngle);
                double newLen14X = Len14X * Math.Cos(DAngle) + Len14Y * Math.Sin(DAngle);
                double newLen14Y = Len14Y * Math.Cos(DAngle) - Len14X * Math.Sin(DAngle);

                if (m_strProductModel.IndexOf("A85") >= 0)
                {
                    CalcFAIAveger = new double[4] { newLen12Y, (newLen13X - newLen12X), (newLen13Y - newLen14Y), newLen14X };
                }
                if (m_strProductModel.IndexOf("A86") >= 0)
                {
                    CalcFAIAveger = new double[4] { Math.Sqrt(Math.Pow(Len12X, 2) + Math.Pow(Len12Y, 2)),
                                                    Math.Sqrt(Math.Pow(Len12X - Len13X, 2) + Math.Pow(Len12Y - Len13Y, 2)),
                                                    Math.Sqrt(Math.Pow(Len13X - Len14X, 2) + Math.Pow(Len13Y - Len14Y, 2)),
                                                    Math.Sqrt(Math.Pow(Len14X, 2) + Math.Pow(Len14Y, 2)) };
                }
                #endregion

                #region 以14点建坐标系计算
                PointH[] newPointH14 = new PointH[4] { new PointH(0, 0),
                                                       new PointH(CCDHelp[1].MarkAxis.X - CCDHelp[0].MarkAxis.X, CCDHelp[1].MarkAxis.Y - CCDHelp[0].MarkAxis.Y),
                                                       new PointH(CCDHelp[2].MarkAxis.X - CCDHelp[0].MarkAxis.X, CCDHelp[2].MarkAxis.Y - CCDHelp[0].MarkAxis.Y),
                                                       new PointH(CCDHelp[3].MarkAxis.X - CCDHelp[0].MarkAxis.X, CCDHelp[3].MarkAxis.Y - CCDHelp[0].MarkAxis.Y) };

                PointH[] newPointH23 = new PointH[4] { new PointH(CCDHelp[0].MarkAxis.X - CCDHelp[2].MarkAxis.X, CCDHelp[0].MarkAxis.Y - CCDHelp[2].MarkAxis.Y),
                                                       new PointH(CCDHelp[1].MarkAxis.X - CCDHelp[2].MarkAxis.X, CCDHelp[1].MarkAxis.Y - CCDHelp[2].MarkAxis.Y), 
                                                       new PointH(0, 0),                                                       
                                                       new PointH(CCDHelp[3].MarkAxis.X - CCDHelp[2].MarkAxis.X, CCDHelp[3].MarkAxis.Y - CCDHelp[2].MarkAxis.Y) };
                //======================================旋转角度至制品水平，计算FAI
                double _angle14 =  Math.Atan(newPointH14[3].Y / newPointH14[3].X);
                double angle_1 = _angle14 * 180 / Math.PI;
                double angle14 = IFlex14Angle + _angle14;
                PointH[] newPointHRotate14 = new PointH[4] { new PointH(0, 0),
                                                             new PointH(newPointH14[1].X * Math.Cos(angle14) + newPointH14[1].Y * Math.Sin(angle14),  //逆时针
                                                                        newPointH14[1].Y * Math.Cos(angle14) - newPointH14[1].X * Math.Sin(angle14)),
                                                             new PointH(newPointH14[2].X * Math.Cos(angle14) + newPointH14[2].Y * Math.Sin(angle14), 
                                                                        newPointH14[2].Y * Math.Cos(angle14) - newPointH14[2].X * Math.Sin(angle14)),
                                                             new PointH(newPointH14[3].X * Math.Cos(angle14) + newPointH14[3].Y * Math.Sin(angle14), 
                                                                        newPointH14[3].Y * Math.Cos(angle14) - newPointH14[3].X * Math.Sin(angle14))
                                                           };
                double _angle23 = Math.Atan(newPointH23[1].Y / newPointH23[1].X);
                double angle_23 = _angle23 * 180 / Math.PI;
                double angle23 = IFlex14Angle + _angle23;
                PointH[] newPointHRotate23 = new PointH[4] { new PointH(newPointH23[0].X * Math.Cos(angle23) + newPointH23[0].Y * Math.Sin(angle23), 
                                                                        newPointH23[0].Y * Math.Cos(angle23) - newPointH23[0].X * Math.Sin(angle23)),
                                                             new PointH(newPointH23[1].X * Math.Cos(angle23) + newPointH23[1].Y * Math.Sin(angle23),  //逆时针
                                                                        newPointH23[1].Y * Math.Cos(angle23) - newPointH23[1].X * Math.Sin(angle23)),
                                                             new PointH(0, 0),
                                                             new PointH(newPointH23[3].X * Math.Cos(angle23) + newPointH23[3].Y * Math.Sin(angle23), 
                                                                        newPointH23[3].Y * Math.Cos(angle23) - newPointH23[3].X * Math.Sin(angle23))
                                                           };
                
                if (m_strProductModel.IndexOf("A85") >= 0)
                {
                    CalcFAIRotate = new double[4] { newPointHRotate14[1].Y,
                                                    newPointHRotate14[2].X - newPointHRotate14[1].X,
                                                    newPointHRotate14[2].Y - newPointHRotate14[3].Y,
                                                    newPointHRotate14[3].X };
                    CalcFAIRotate23 = new double[4] { newPointHRotate14[1].Y,
                                                     -newPointHRotate23[1].X,
                                                     -newPointHRotate23[3].Y,
                                                      newPointHRotate14[3].X };
                }
                if(m_strProductModel.IndexOf("A86") >= 0)
                {
                    CalcFAIRotate = new double[4] { Math.Sqrt(Math.Pow(newPointH14[1].X, 2) + Math.Pow(newPointH14[1].Y, 2)),
                                                    Math.Sqrt(Math.Pow(newPointH14[2].X - newPointH14[1].X, 2) + Math.Pow(newPointH14[2].Y - newPointH14[1].Y, 2)),
                                                    Math.Sqrt(Math.Pow(newPointH14[2].X - newPointH14[3].X, 2) + Math.Pow(newPointH14[2].Y - newPointH14[3].Y, 2)),
                                                    Math.Sqrt(Math.Pow(newPointH14[3].X, 2) + Math.Pow(newPointH14[3].Y, 2)) };
                    CalcFAIRotate23 = CalcFAIRotate;
                }
                #endregion

                switch (m_nCalcFunc)
                {
                    case 1:
                        CalcFAI = CalcFAIAveger;
                        break;
                    case 2:
                        CalcFAI = CalcFAIRotate;
                        break;
                    case 3:
                        CalcFAI = CalcFAIRotate23;
                        break;
                    default:
                        CalcFAI = CalcFAIRotate23;
                        break;
                }

                #region 取平均值计算
                if (m_TestCount > 1 && testcount <= m_TestCount)
                {
                    m_arrayFAI1[testcount - 1] = CalcFAI[0];
                    m_arrayFAI2[testcount - 1] = CalcFAI[1];
                    m_arrayFAI3[testcount - 1] = CalcFAI[2];
                    m_arrayFAI4[testcount - 1] = CalcFAI[3];
                    testcount++;
                    //测满m_TestCount次数
                    if (testcount <= m_TestCount)
                    {
                        StartForTest();
                        return;
                    }
                    m_ListFAI1 = m_arrayFAI1.ToList();
                    m_ListFAI2 = m_arrayFAI2.ToList();
                    m_ListFAI3 = m_arrayFAI3.ToList();
                    m_ListFAI4 = m_arrayFAI4.ToList();
                    if (m_TestCount >= 3 && m_TestSub < m_TestCount)
                    {
                        for (int k = 0; k < m_TestSub / 2; k++)
                        {
                            m_ListFAI1 = AverageSubMaxMin(m_ListFAI1);
                            m_ListFAI2 = AverageSubMaxMin(m_ListFAI2);
                            m_ListFAI3 = AverageSubMaxMin(m_ListFAI3);
                            m_ListFAI4 = AverageSubMaxMin(m_ListFAI4);
                        }
                    }
                    CalcFAI[0] = Math.Round(m_ListFAI1.Average(), 5);
                    CalcFAI[1] = Math.Round(m_ListFAI2.Average(), 5);
                    CalcFAI[2] = Math.Round(m_ListFAI3.Average(), 5);
                    CalcFAI[3] = Math.Round(m_ListFAI4.Average(), 5);
                }
                #endregion

                #region 补偿
                double[] CalcFAIOffset = new double[4];      //补偿值
                double[] CalcFAIAngle = new double[4];       //角度
                double[] CalcFAIOffsetAngle = new double[4]; //补偿值+角度
                if (GlobalVar.m_bUseOffset && !GlobalVar.m_bInCalibMode)
                {
                    //补偿值
                    for (int i = 0; i < CalcFAI.Length; i++)
                    {
                        CalcFAIOffset[i] = CalcFAI[i] + m_OffsetFAIConst[i] + m_OffsetFAI[i];
                    }
                    //角度
                    double OffsetAngle = DAngle + m_OffsetAngle * Math.PI / 180;
                    double newLen12XAngle = Len12X * Math.Cos(OffsetAngle) + Len12Y * Math.Sin(OffsetAngle);
                    double newLen12YAngle = Len12Y * Math.Cos(OffsetAngle) - Len12X * Math.Sin(OffsetAngle);
                    double newLen13XAngle = Len13X * Math.Cos(OffsetAngle) + Len13Y * Math.Sin(OffsetAngle);
                    double newLen13YAngle = Len13Y * Math.Cos(OffsetAngle) - Len13X * Math.Sin(OffsetAngle);
                    double newLen14XAngle = Len14X * Math.Cos(OffsetAngle) + Len14Y * Math.Sin(OffsetAngle);
                    double newLen14YAngle = Len14Y * Math.Cos(OffsetAngle) - Len14X * Math.Sin(OffsetAngle);
                    CalcFAIAngle = new double[4]{ Math.Round(newLen12YAngle, 5), 
                                                             Math.Round((newLen13XAngle - newLen12XAngle), 5), 
                                                             Math.Round((newLen13YAngle - newLen14YAngle), 5), 
                                                             Math.Round(newLen14XAngle, 5) };
                    //补偿值+角度
                    for (int i = 0; i < CalcFAIAngle.Length; i++)
                    {
                        CalcFAIOffsetAngle[i] = CalcFAIAngle[i] + m_OffsetFAIConst[i] + m_OffsetFAI[i];
                    }
                    CalcFAI = CalcFAIOffset;
                }
                #endregion

                for (int i = 0; i < CalcFAI.Length; i++)
                {
                    CalcFAI[i] = Math.Round(CalcFAI[i], 5);
                }

                #region 等级
                if (m_strProductModel.IndexOf("A86") >= 0)
                {
                    double[] change = new double[4];
                    change[0] = CalcFAI[1];
                    change[1] = CalcFAI[2];
                    change[2] = CalcFAI[3];
                    change[3] = CalcFAI[0];
                    CalcFAI = change;
                }
                int[] nLevel;      //四组测试等级
                string[] strLevel; //四组测试等级
                double[] DDFAI;    //测试值-标准值
                Color color = Color.Green;
                CalcLevel(CalcFAI, Standard, out nLevel, out strLevel, out DDFAI, out color);
                #endregion

                #region 显示结果
                if (!GlobalVar.m_bInCalibMode)
                {
                    ShowStatusForm(strLevel[0] + strLevel[1] + strLevel[2] + strLevel[3], color, m_barcode == "" ? "Test" : m_barcode , true);
                }
                this.BeginInvoke(new EventHandler(delegate
                {
                    int rowcount = dataGridViewEx_result.Rows.Count;
                    if (rowcount >= 20)
                    {
                        dataGridViewEx_result.Rows.RemoveAt(4);
                        dataGridViewEx_result.Rows.RemoveAt(3);
                        dataGridViewEx_result.Rows.RemoveAt(2);
                        dataGridViewEx_result.Rows.RemoveAt(1);
                        dataGridViewEx_result.Rows.RemoveAt(0);
                    }
                    int index = 0;
                    index = dataGridViewEx_result.Rows.Add("TEST_" + m_Count, m_barcode.ToString());
                    DataGridViewGroupCell cellParent = dataGridViewEx_result.Rows[index].Cells[0] as DataGridViewGroupCell;
                    //if (m_nCalcFunc == 1)
                    {
                        index = dataGridViewEx_result.Rows.Add("FAI1", strLevel[0] + ";" + CalcFAI[0] + "," + DDFAI[0]);
                        DataGridViewGroupCell cellFAI1 = dataGridViewEx_result.Rows[index].Cells[0] as DataGridViewGroupCell;
                        index = dataGridViewEx_result.Rows.Add("FAI2", strLevel[1] + ";" + CalcFAI[1] + "," + DDFAI[1]);
                        DataGridViewGroupCell cellFAI2 = dataGridViewEx_result.Rows[index].Cells[0] as DataGridViewGroupCell;
                        index = dataGridViewEx_result.Rows.Add("FAI3", strLevel[2] + ";" + CalcFAI[2] + "," + DDFAI[2]);
                        DataGridViewGroupCell cellFAI3 = dataGridViewEx_result.Rows[index].Cells[0] as DataGridViewGroupCell;
                        index = dataGridViewEx_result.Rows.Add("FAI4", strLevel[3] + ";" + CalcFAI[3] + "," + DDFAI[3]);
                        DataGridViewGroupCell cellFAI4 = dataGridViewEx_result.Rows[index].Cells[0] as DataGridViewGroupCell;
                        cellParent.AddChildCellRange(cellParent, new DataGridViewGroupCell[] { cellFAI1, cellFAI2, cellFAI3, cellFAI4 });
                    }
                    dataGridViewEx_result.CurrentCell = dataGridViewEx_result.Rows[index].Cells[0];
                }));
                #endregion

                #region CSV
                DateTime timenow = DateTime.Now;
                string filecsv = m_pathResult + "\\result_" + timenow.ToString("yyyy-MM-dd") + ".csv"; //简报
                string filecsvJC = m_pathResult + "\\JC\\JC_" + timenow.ToString("yyyy-MM-dd") + ".csv"; //祥报
                #region 简报
                bool bWriteHead = false;
                if (!File.Exists(filecsv))
                    bWriteHead = true;
                using (FileStream FS = new FileStream(filecsv, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter SW = new StreamWriter(FS, Encoding.Default);
                    if (bWriteHead)
                    {
                        SW.WriteLine("TestCount,Operator,WorkTime,DateTime,LotNo,Product,ProductMode,MachineNum,Barcode," + 

                                     "FAI 1,FAI 2,FAI 3,FAI 4," +  //测试值
                                     "D_FAI 1,D_FAI 2,D_FAI 3,D_FAI 4," +  //测试值-标准值
                                     "L_FAI 1,L_FAI 2,L_FAI 3,L_FAI 4,"  //测试等级
                                    );
                        SW.WriteLine("制品标准值," + m_FAIStandard[0] + "," + m_FAIStandard[1] + "," + m_FAIStandard[2] + "," + m_FAIStandard[3] + "," + 
                                     "校准片标准值," + m_FAICADStandard[0] + "," + m_FAICADStandard[1] + "," + m_FAICADStandard[2] + "," + m_FAICADStandard[3]
                                    );
                    }
                    SW.WriteLine(m_Count + "," + m_strOperator + "," + m_strWorkTime + "," + timenow + "," + m_strLotNo + "," +  //作业信息
                                 m_strProduct + "," + m_strProductModel + "," + GlobalVar.gl_PcName + "," +  (GlobalVar.m_bInCalibMode ? "Calib" : m_barcode) + "," +

                                 CalcFAI[0] + "," + CalcFAI[1] + "," + CalcFAI[2] + "," + CalcFAI[3] + "," +  //测试值23
                                 DDFAI[0] + "," + DDFAI[1] + "," + DDFAI[2] + "," + DDFAI[3] + "," +  //测试值-标准值23
                                 strLevel[0] + "," + strLevel[1] + "," + strLevel[2] + "," + strLevel[3] + ","   //测试等级23

                                );
                    SW.Close();
                    SW.Dispose();
                }
                #endregion
                #region 祥报
                bWriteHead = false;
                if (!Directory.Exists(m_pathResult + "\\JC\\"))
                    Directory.CreateDirectory(m_pathResult + "\\JC\\");
                if (!File.Exists(filecsvJC))
                    bWriteHead = true;
                using (FileStream FS = new FileStream(filecsvJC, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter SW = new StreamWriter(FS, Encoding.Default);
                    if (bWriteHead)
                    {
                        SW.WriteLine("TestCount,Operator,WorkTime,DateTime,LotNo,Product,ProductMode,MachineNum,Barcode," +
                                     "PixelX1,PixelY1,PixelX2,PixelY2,PixelX3,PixelY3,PixelX4,PixelY4," + //四个点像素坐标
                                     "AxisX1,AxisY1,AxisX2,AxisY2,AxisX3,AxisY3,AxisX4,AxisY4," + //四个点实际坐标
                                     "CADA12,CADA13,CADA14,A12,A13,A14," + //偏移角度
                                     "DA12,DA13,DA14,旋转弧度,," + //测量偏移角度-CAD偏移角度

                                     "平均角度FAI1,平均角度FAI2,平均角度FAI3,平均角度FAI4,," +  //平均角度测试值
                                     //"D_FAI1,D_FAI2,D_FAI3,D_FAI4,L_FAI1,L_FAI2,L_FAI3,L_FAI4,," +
                                     "14Mark点FAI1,14Mark点FAI2,14Mark点FAI3,14Mark点FAI4,," +  //14Mark点测试值
                                     //"D_FAI1,D_FAI2,D_FAI3,D_FAI4,L_FAI1,L_FAI2,L_FAI3,L_FAI4,," +
                                     "14_23点FAI1,14_23点FAI2,14_23点FAI3,14_23点FAI4,," +  //14，23Mark测试值
                                     //"D_FAI1,D_FAI2,D_FAI3,D_FAI4,L_FAI1,L_FAI2,L_FAI3,L_FAI4,," +
                                     
                                     "Offset 1,Offset 2,Offset 3, Offset 4,OffsetAngle," + //补偿值,补偿角度
                                     "OffsetFAI 1,OffsetFAI 2,OffsetFAI 3,OffsetFAI 4," +  //+补偿值
                                     "AngleFAI 1,AngleFAI 2,AngleFAI 3,AngleFAI 4," +  //+补偿角度
                                     "OffsetAngleFAI 1,OffsetAngleFAI 2,OffsetAngleFAI 3,OffsetAngleFAI 4,,"  //补偿值+补偿角度
                                    );
                        SW.WriteLine("制品标准值," + m_FAIStandard[0] + "," + m_FAIStandard[1] + "," + m_FAIStandard[2] + "," + m_FAIStandard[3] + "," +
                                     "校准片标准值," + m_FAICADStandard[0] + "," + m_FAICADStandard[1] + "," + m_FAICADStandard[2] + "," + m_FAICADStandard[3]
                                    );
                    }
                    SW.WriteLine(m_Count + "," + m_strOperator + "," + m_strWorkTime + "," + timenow + "," + m_strLotNo + "," + //作业信息
                                 m_strProduct + "," + m_strProductModel + "," + GlobalVar.gl_PcName + "," + (GlobalVar.m_bInCalibMode ? "Calib" : m_barcode) + "," +
                                 CCDHelp[0].MarkCenter.X + "," + CCDHelp[0].MarkCenter.Y + "," + CCDHelp[1].MarkCenter.X + "," + CCDHelp[1].MarkCenter.Y + "," + //四个点像素坐标
                                 CCDHelp[2].MarkCenter.X + "," + CCDHelp[2].MarkCenter.Y + "," + CCDHelp[3].MarkCenter.X + "," + CCDHelp[3].MarkCenter.Y + "," +
                                 CCDHelp[0].MarkAxis.X + "," + CCDHelp[0].MarkAxis.Y + "," + CCDHelp[1].MarkAxis.X + "," + CCDHelp[1].MarkAxis.Y + "," + //四个点实际坐标
                                 CCDHelp[2].MarkAxis.X + "," + CCDHelp[2].MarkAxis.Y + "," + CCDHelp[3].MarkAxis.X + "," + CCDHelp[3].MarkAxis.Y + "," +
                                 CADAngle12 * 180 / Math.PI + "," + CADAngle13 * 180 / Math.PI + "," + CADAngle14 * 180 / Math.PI + "," + //CAD偏移角度
                                 Angle12 * 180 / Math.PI + "," + Angle13 * 180 / Math.PI + "," + Angle14 * 180 / Math.PI + "," + //测量偏移角度
                                 DAngle12 + "," + DAngle13 + "," + DAngle14 + "," + DAngle + ",," +  //测量偏移角度-CAD偏移角度

                                 CalcFAIAveger[0] + "," + CalcFAIAveger[1] + "," + CalcFAIAveger[2] + "," + CalcFAIAveger[3] + ",," +  //平均角度测试值
                                 CalcFAIRotate[0] + "," + CalcFAIRotate[1] + "," + CalcFAIRotate[2] + "," + CalcFAIRotate[3] + ",," +  //14Mark点测试值
                                 CalcFAIRotate23[0] + "," + CalcFAIRotate23[1] + "," + CalcFAIRotate23[2] + "," + CalcFAIRotate23[3] + ",," +  //14,23Mark点测试值

                                 m_OffsetFAI[0] + "," + m_OffsetFAI[1] + "," + m_OffsetFAI[2] + "," + m_OffsetFAI[3] + "," + m_OffsetAngle + "," + //补偿值,补偿角度
                                 CalcFAIOffset[0] + "," + CalcFAIOffset[1] + "," + CalcFAIOffset[2] + "," + CalcFAIOffset[3] + "," +   //+补偿值
                                 CalcFAIAngle[0] + "," + CalcFAIAngle[1] + "," + CalcFAIAngle[2] + "," + CalcFAIAngle[3] + "," +  //+补偿角度
                                 CalcFAIOffsetAngle[0] + "," + CalcFAIOffsetAngle[1] + "," + CalcFAIOffsetAngle[2] + "," + CalcFAIOffsetAngle[3] + ",,"   //补偿值+补偿角度
                                 
                                );
                    SW.Close();
                    SW.Dispose();
                }
                #endregion
                #endregion

                #region 量测校准片数据
                if (GlobalVar.m_bInCalibMode)
                {
                    m_nCalibCount++;
                    GlobalVar.m_bNeedReCalc = false;
                    for (int i = 0; i < CalcFAI.Length; i++)
                    {
                        if (Math.Abs(CalcFAI[i] - m_FAICADStandard[i]) > m_CalibValue)
                        {
                            GlobalVar.m_bNeedReCalc = true;
                            ShowStatusForm("量测校准片数据差异 >" + 0.01 + "mm\r需要重新标定相机", Color.Red, "", true);
                            TestEnd();
                            return;
                        }
                    }
                    ShowStatusForm("量测校准片数据差异 <" + 0.01 + "mm\r可以继续作业", Color.Green, "", true);
                    GlobalVar.m_timeLast9Point = DateTime.Now; //量测校准片数据合格，继续作业
                    CommonFunc.Write(ini_Section_TestInfo, ini_Key_Last9Point, GlobalVar.m_timeLast9Point.ToString("yyyy-MM-dd HH:mm:ss.fff"), m_fileConfig);
                }
                #endregion

                TestEnd();
            }
            catch (Exception ex)
            {
                ShowStatusForm("计算胀缩异常，请重新测试", Color.Red);
                CommonFunc.writeLog("计算胀缩异常: " + ex.ToString());
                TestEnd();
            }
            finally
            {

            }
        }

        private List<double> AverageSubMaxMin(List<double> array)
        {
            List<double> newArray = new List<double> { };
            double average = 0;
            double max = array.Max();
            double min = array.Min();
            double sum = array.Sum();
            average = (sum - max - min) / (array.Count - 2);
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] == max || array[i] == min)
                    continue;
                newArray.Add(array[i]);
            }
            return newArray;
        }

        private void TestEnd()
        {
            m_Count++;
            this.Invoke(new EventHandler(delegate
            {
                m_barcode = "";
                txtbox_barcode.Text = "";
            }));
            event_DisplayLog("===END===完成");
            if (!m_bLocalTest && !GlobalVar.m_bManualStart) //非重复测试，发送载板退出信号
                sph.Send("W01", false, false);
            else
                m_bLocalTest = false;
            testcount = 1;
            if (GlobalVar.m_bManualStart)
                GlobalVar.m_bManualStart = false;
        }

        /// <summary>
        /// 计算产品等级
        /// </summary>
        /// <param name="_FAI"></param>
        /// <param name="Standard"></param>
        /// <param name="nLevel"></param>
        /// <param name="strLevel"></param>
        /// <param name="DDFAI"></param>
        /// <param name="color"></param>
        private void CalcLevel(double[] _FAI, double[] Standard, out int[] nLevel, out string[] strLevel, out double[] DDFAI, out Color color)
        {
            nLevel = new int[4];         //四组测试等级
            strLevel = new string[4]; //四组测试等级
            DDFAI = new double[4];    //测试值-标准值
            for (int i = 0; i < _FAI.Length; i++)
            {
                DDFAI[i] = Math.Round(_FAI[i] - Standard[i], 5);
                if (DDFAI[i] < -0.15)
                {
                    nLevel[i] = -1;
                    strLevel[i] = "D";
                }
                else if (DDFAI[i] >= -0.15 && DDFAI[i] <= -0.075)
                {
                    nLevel[i] = 1;
                    strLevel[i] = "C";
                }
                else if (DDFAI[i] > -0.075 && DDFAI[i] <= 0.075)
                {
                    nLevel[i] = 3;
                    strLevel[i] = "A";
                }
                else if (DDFAI[i] > 0.075 && DDFAI[i] <= 0.15)
                {
                    nLevel[i] = 2;
                    strLevel[i] = "B";
                }
                else if (DDFAI[i] > 0.15)
                {
                    nLevel[i] = 0;
                    strLevel[i] = "D";
                }
            }
            //制品等级
            int nProductLevel = nLevel.Min();
            string strProductLevel = strLevel.Max();
            color = Color.Green;
            switch (nProductLevel)
            {
                case 3:
                    strProductLevel = "A";
                    color = Color.Green;
                    break;
                case 2:
                    strProductLevel = "B";
                    color = Color.GreenYellow;
                    break;
                case 1:
                    strProductLevel = "C";
                    color = Color.Orange;
                    break;
                case 0:
                    strProductLevel = "不合格";
                    color = Color.Red;
                    break;
                case -1:
                    strProductLevel = "不合格";
                    color = Color.Red;
                    break;
            }
        }

        private void Get9PointResult()
        {
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                if (CCDHelp[i].MarkCenter.X == 0.0 || CCDHelp[i].MarkCenter.Y == 0.0)
                {
                    if (!m_bLocalTest) //非重复测试，发送载板退出信号
                        sph.Send("W01", false, false);
                    ShowStatusForm("未找到Mark点，请重新标定", Color.Red);
                    return;
                }
                else if (!CCDHelp[i].m_bHomMat2DOK)
                {
                    if (!m_bLocalTest) //非重复测试，发送载板退出信号
                        sph.Send("W01", false, false);
                    ShowStatusForm("标定点过少，请重新标定", Color.Red);
                    return;
                }
            }

            if (GlobalVar.m_bNeedReCalc)
            {
                GlobalVar.m_bNeedReCalc = false;
                m_nCalibCount = 0;
            }
            ShowStatusForm("标定相机完毕\r可以开始作业", Color.Green);
            GlobalVar.m_timeLast9Point = DateTime.Now; //标定完成
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_Last9Point, GlobalVar.m_timeLast9Point.ToString("yyyy-MM-dd HH:mm:ss.fff"), m_fileConfig);
            Point9Out();
        }

        //弹框
        private void ShowStatusForm(string str, Color color, string barcode = "", bool speak = false)
        {
            if (speak) speaker.AddSpeech(str);
            this.Invoke((EventHandler)delegate
            {
                if (GlobalVar.SWBreakForm == null)
                    GlobalVar.SWBreakForm = new SwitchBreakForm();
                GlobalVar.SWBreakForm.Visible = true;
                GlobalVar.SWBreakForm.Focus();
                GlobalVar.SWBreakForm.ShowText(str, color, barcode);
            });
            string _str = str.Replace('\r', ',');
            event_DisplayLog(_str, color == Color.Red ? true : false);
        }
        private void CloseStatusForm()
        {
            this.Invoke((EventHandler)delegate
            {
                if (GlobalVar.SWBreakForm != null)
                {
                    GlobalVar.SWBreakForm.Close();
                    GlobalVar.SWBreakForm = null;
                }
            });
        }
        #endregion

        #region 界面按钮
        //监控键盘按键
        string keybord = "";
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                keybord = "";
                return false;
            }
            if (keyData == Keys.Space || keyData == Keys.F1) //启动
            {
                GlobalVar.m_bManualStart = true;
                CommonFunc.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_StartScan, (IntPtr)0, (IntPtr)0); //开始扫条码
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

        private void btn_allowWork_Click(object sender, EventArgs e)
        {
            GlobalVar.gl_bAllowWork = false;
            DispScanMode();
            if (!CheckBeforeTest()) return;
            ReadOffsetINI();
            sph.AllowWork();
            event_DisplayLog("允许作业");
            GlobalVar.gl_bAllowWork = true;
        }

        private bool CheckBeforeTest()
        {
            //作业信息核查
            if (m_strOperator == "" || m_strLotNo == "" || m_strProduct == "" || m_strProductModel == "")
            {
                ShowStatusForm("作业信息为空", Color.Red);
                return false;
            }
            //相机初始化核查
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                if (!CCDHelp[i].m_bInitCCD)
                {
                    ShowStatusForm(CCDHelp[i].CCDName + "初始化失败", Color.Red);
                    return false;
                }
                if (!m_bIn9Point)
                {
                    if (!CCDHelp[i].m_bHomMat2DOK)
                    {
                        CCDHelp[i].GetHomMat2D(); //重新加载一次
                        if (!CCDHelp[i].m_bHomMat2DOK)
                        {
                            ShowStatusForm(CCDHelp[i].CCDName + "未找到标定文件\r需要重新标定相机", Color.Red);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void 电机配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_strProductModel == "")
            {
                ShowStatusForm("作业信息为空", Color.Red);
                return;
            }

            Thread thd = new Thread(new ThreadStart(delegate {
                this.Invoke(new Action(() =>
                {
                    try
                    {
                        sph.CalibValue = m_CalibValue;
                        sph.CalibSpace = GlobalVar.m_CalibSpace;
                        sph.TestCount = m_TestCount;
                        sph.TestSub = m_TestSub;
                        sph.OffsetFAI1 = m_OffsetFAI[0];
                        sph.OffsetFAI2 = m_OffsetFAI[1];
                        sph.OffsetFAI3 = m_OffsetFAI[2];
                        sph.OffsetFAI4 = m_OffsetFAI[3];
                        sph.ShowDialog();
                        m_CalibValue = sph.CalibValue;
                        GlobalVar.m_CalibSpace = sph.CalibSpace;
                        m_TestCount = sph.TestCount;
                        m_TestSub = sph.TestSub;
                        m_OffsetFAI[0] = sph.OffsetFAI1;
                        m_OffsetFAI[1] = sph.OffsetFAI2;
                        m_OffsetFAI[2] = sph.OffsetFAI3;
                        m_OffsetFAI[3] = sph.OffsetFAI4;
                        if (m_TestCount >= 3)
                        {
                            if (m_TestSub >= m_TestCount)
                            {
                                MessageBox.Show("筛选个数必须小于测试次数");
                                m_TestSub = 0;
                                return;
                            }
                        }
                        InitArrayFAI();
                        WriteINI();
                        DispScanMode();
                    }
                    catch { }
                }));
            }));
            thd.IsBackground = true;
            thd.Start();
        }

        private void InitArrayFAI()
        {
            m_arrayFAI1 = new double[m_TestCount];
            m_arrayFAI2 = new double[m_TestCount];
            m_arrayFAI3 = new double[m_TestCount];
            m_arrayFAI4 = new double[m_TestCount];
        }

        private void timer_sp_Tick(object sender, EventArgs e)
        {
            tssl_spconn.Text = sph.axSerialPort1.IsOpen ? "串口已连接" : "串口未连接";
            tssl_spconn.BackColor = sph.axSerialPort1.IsOpen ? Color.LawnGreen : Color.OrangeRed;
        }

        private void 管理登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsmi_adminlogin.Text == "管理登录")
            {
                logonIn lg = new logonIn();
                if (lg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tsmi_adminlogin.Text = "管理登出";
                    SetControlEnable(true);
                }
            }
            else
            {
                tsmi_adminlogin.Text = "管理登录";
                for (int i = 0; i < CCDHelp.Length; i++)
                {
                    CCDHelp[i].Set9Point(false);
                }
                m_bIn9Point = false;
                btn_9point.Text = "标定相机";
                GlobalVar.gl_bAdmin = GlobalVar.AdminMode.Nomal;
                SetControlEnable(false);
            }
        }
        private void SetControlEnable(bool bl)
        {
            btn_localtest.Visible = bl;
            btn_9point.Visible = bl;
            btn_9point.Enabled = btn_localtest.Enabled = bl;
            numericUpDown_testcount.Visible = bl;
            lbl_testcount.Visible = bl;
            if (!bl) m_bLocalTest = false;
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].Enabled = bl;
            }
            if (GlobalVar.gl_bAdmin == GlobalVar.AdminMode.SupperAdmin)
            {
                checkBox_Fortest.Visible = true;
                tESTToolStripMenuItem.Visible = true;
                计算方法ToolStripMenuItem.Visible = true;
            }
            else
            {
                checkBox_Fortest.Visible = false;
                tESTToolStripMenuItem.Visible = false;
                计算方法ToolStripMenuItem.Visible = false;
            }
        }

        /// <summary>
        /// 获取CAD数据中的Mark点中心坐标
        /// </summary>
        /// <returns></returns>
        private bool GetCAD4CenterPoint()
        {
            m_fileCADMatrix = m_pathConfig + "\\" + m_strProductModel.ToUpper() + "_CADDATA.csv";
            if (!File.Exists(m_fileCADMatrix))
            {
                ShowStatusForm("未找到【" + m_fileCADMatrix + "】文件，请检查！", Color.Red);
                return false;
            }
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].m_pathConfigTup = m_pathConfig + "\\" + m_strProductModel.ToUpper() + "_TUP";
                bool bl = CCDHelp[i].GetCADData(m_fileCADMatrix);
                if (!bl)
                {
                    ShowStatusForm("获取【" + m_fileCADMatrix + "】文件数据异常，请检查！", Color.Red);
                    return false;
                }

                if (CCDHelp[i].CADAllData[0, 0].X == 0.0)
                {
                    ShowStatusForm("获取CAD中心点坐标异常，请检查【" + m_fileCADMatrix + "】文件！", Color.Red);
                    return false;
                }
                CCDHelp[i].GetHomMat2D();
            }
            //2.5度
            PointH[] LenXY = new PointH[4];
            LenXY[0].X = CCDHelp[1].CADCenterData.X - CCDHelp[0].CADCenterData.X;
            LenXY[0].Y = CCDHelp[1].CADCenterData.Y - CCDHelp[0].CADCenterData.Y;
            LenXY[1].X = CCDHelp[2].CADCenterData.X - CCDHelp[1].CADCenterData.X;
            LenXY[1].Y = CCDHelp[2].CADCenterData.Y - CCDHelp[1].CADCenterData.Y;
            LenXY[2].X = CCDHelp[3].CADCenterData.X - CCDHelp[2].CADCenterData.X;
            LenXY[2].Y = CCDHelp[3].CADCenterData.Y - CCDHelp[2].CADCenterData.Y;
            LenXY[3].X = CCDHelp[3].CADCenterData.X - CCDHelp[0].CADCenterData.X;
            LenXY[3].Y = CCDHelp[3].CADCenterData.Y - CCDHelp[0].CADCenterData.Y; 
            m_FAICADStandard = new double[4]{ LenXY[0].Y * Math.Cos(IFlexZXAngle) - LenXY[0].X * Math.Sin(IFlexZXAngle),
                                              LenXY[1].X * Math.Cos(IFlexZXAngle) + LenXY[1].Y * Math.Sin(IFlexZXAngle),
                                              LenXY[2].Y * Math.Cos(IFlexZXAngle) - LenXY[2].X * Math.Sin(IFlexZXAngle),
                                              LenXY[3].X * Math.Cos(IFlexZXAngle) + LenXY[3].Y * Math.Sin(IFlexZXAngle)
                                            };            
            PointH[] newPointH14 = new PointH[4] { new PointH(0, 0),
                                                   new PointH(CCDHelp[1].CADCenterData.X - CCDHelp[0].CADCenterData.X, CCDHelp[1].CADCenterData.Y - CCDHelp[0].CADCenterData.Y),
                                                   new PointH(CCDHelp[2].CADCenterData.X - CCDHelp[0].CADCenterData.X, CCDHelp[2].CADCenterData.Y - CCDHelp[0].CADCenterData.Y),
                                                   new PointH(CCDHelp[3].CADCenterData.X - CCDHelp[0].CADCenterData.X, CCDHelp[3].CADCenterData.Y - CCDHelp[0].CADCenterData.Y) };
            //17.8度
            double _angle14 = Math.Atan(newPointH14[3].Y / newPointH14[3].X);
            double angle_1 = _angle14 * 180 / Math.PI;
            double angle14 = IFlex14Angle + _angle14;
            PointH[] newPointHRotate14 = new PointH[4] { new PointH(0, 0),
                                                         new PointH(newPointH14[1].X * Math.Cos(angle14) + newPointH14[1].Y * Math.Sin(angle14),  //逆时针
                                                                    newPointH14[1].Y * Math.Cos(angle14) - newPointH14[1].X * Math.Sin(angle14)),
                                                         new PointH(newPointH14[2].X * Math.Cos(angle14) + newPointH14[2].Y * Math.Sin(angle14), 
                                                                    newPointH14[2].Y * Math.Cos(angle14) - newPointH14[2].X * Math.Sin(angle14)),
                                                         new PointH(newPointH14[3].X * Math.Cos(angle14) + newPointH14[3].Y * Math.Sin(angle14), 
                                                                    newPointH14[3].Y * Math.Cos(angle14) - newPointH14[3].X * Math.Sin(angle14)) };
            if (m_strProductModel.IndexOf("A85") >= 0)
            {
                m_FAICADStandard = new double[4] { Math.Round(newPointHRotate14[1].Y, 5),
                                                   Math.Round(newPointHRotate14[2].X - newPointHRotate14[1].X, 5),
                                                   Math.Round(newPointHRotate14[2].Y - newPointHRotate14[3].Y, 5),
                                                   Math.Round(newPointHRotate14[3].X, 5) };
            }
            if (m_strProductModel.IndexOf("A86") >= 0)
            {
                m_FAICADStandard = new double[4] { Math.Sqrt(Math.Pow(newPointH14[1].X, 2) + Math.Pow(newPointH14[1].Y, 2)),
                                                   Math.Sqrt(Math.Pow(newPointH14[2].X - newPointH14[1].X, 2) + Math.Pow(newPointH14[2].Y - newPointH14[1].Y, 2)),
                                                   Math.Sqrt(Math.Pow(newPointH14[2].X - newPointH14[3].X, 2) + Math.Pow(newPointH14[2].Y - newPointH14[3].Y, 2)),
                                                   Math.Sqrt(Math.Pow(newPointH14[3].X, 2) + Math.Pow(newPointH14[3].Y, 2)) };
                double[] change = new double[4];
                change[0] = m_FAICADStandard[1];
                change[1] = m_FAICADStandard[2];
                change[2] = m_FAICADStandard[3];
                change[3] = m_FAICADStandard[0];
                m_FAICADStandard = change;
            }
            for (int i = 0; i < m_FAICADStandard.Length; i++)
            {
                m_FAICADStandard[i] = Math.Abs(Math.Round(m_FAICADStandard[i], 5));
            }
            return true;
        }

        private void btn_9point_Click(object sender, EventArgs e)
        {
            if (btn_9point.Text == "标定相机")
            {
                //作业信息核查
                if (m_strOperator == "" || m_strLotNo == "" || m_strProduct == "" || m_strProductModel == "")
                {
                    ShowStatusForm("作业信息为空", Color.Red);
                    return;
                }
                //相机初始化核查
                if (!bFortest)
                {
                    for (int i = 0; i < CCDHelp.Length; i++)
                    {
                        if (!CCDHelp[i].m_bInitCCD)
                        {
                            ShowStatusForm(CCDHelp[i].CCDName + "初始化失败", Color.Red);
                            return;
                        }
                    }
                }
                Point9In();
            }
            else
            {
                Point9Out();
            }
        }

        private void Point9In()
        {
            if (!File.Exists(m_fileCADMatrix))
            {
                MessageBox.Show("未找到用于校正的CAD坐标文件，请检查！");
            }
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                if (CCDHelp[i].CADAllData[0, 0].X == 0.0)
                {
                    bool bl = CCDHelp[i].GetCADData(m_fileCADMatrix);
                    CCDHelp[i].Set9Point(bl);
                }
                else
                    CCDHelp[i].Set9Point(true);
                if (!CCDHelp[i].m_bIn9Point)
                {
                    CCDHelp[0].Set9Point(false);
                    CCDHelp[1].Set9Point(false);
                    CCDHelp[2].Set9Point(false);
                    CCDHelp[3].Set9Point(false);
                    return;
                }
                CCDHelp[i].SetMarkOrPorductArea(0);
            }
            m_bIn9Point = true;
            btn_9point.Text = "退出标定";
            tssl_testType.Text = "相机标定中";
        }

        private void Point9Out()
        {
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].Set9Point(false);
                CCDHelp[i].SetMarkOrPorductArea(1);
            }
            m_bIn9Point = false;
            this.Invoke(new EventHandler(delegate
            {
                btn_9point.Text = "标定相机";
                tssl_testType.Text = GlobalVar.m_bInCalibMode ? "校准片量测中" : "制品量测中";
            }));
        }

        bool bstop = false;
        private void btn_localtest_Click(object sender, EventArgs e)
        {
            //作业信息核查
            if (m_strOperator == "" || m_strLotNo == "" || m_strProduct == "" || m_strProductModel == "")
            {
                ShowStatusForm("作业信息为空", Color.Red);
                return;
            }
            ReadOffsetINI();
            DispScanMode();

            if (!bFortest)
            {
                if (!CheckBeforeTest()) return;
            }
            int testcount = (int)numericUpDown_testcount.Value;
            bstop = false;
            Thread thd = new Thread(new ThreadStart(delegate {
                for (int i = 0; i < testcount; i++)
                {
                    if (bstop) break;
                    while (true)
                    {
                        Thread.Sleep(500);
                        if (!m_bLocalTest) break;
                    }
                    if (!m_bIn9Point) m_bLocalTest = true;
                    CloseStatusForm();
                    m_barcode = txtbox_barcode.Text.ToString().Trim();
                    StartForTest();
                    if (m_bIn9Point) break;
                    Thread.Sleep(1000);
                }
            }));
            thd.IsBackground = true;
            thd.Start();
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVar.SoftWareShutDown = true;
            myCCDHelp1.Close();
            myCCDHelp2.Close();
            myCCDHelp3.Close();
            myCCDHelp4.Close();
            Application.ExitThread();
        }

        //panel边框绘制
        private void panel_main_Paint(object sender, PaintEventArgs e)
        {
            MyGDI.DrawPanelBorder(e.Graphics, sender,Color.BlueViolet, 3);
        }
        //DataGridView行号
        private void dataGridViewEx_result_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //MyGDI.DataGridViewRowPostPaint(sender, e);
        }

        //结果全部展开
        private void btn_allExpand_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgr in dataGridViewEx_result.Rows)
            {
                DataGridViewGroupCell cell = dgr.Cells[0] as DataGridViewGroupCell;
                if (cell.Collapsed)
                {
                    cell.Expand();
                }
            }
        }
        //结果全部折叠
        private void btn_allCollapsed_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgr in dataGridViewEx_result.Rows)
            {
                DataGridViewGroupCell cell = dgr.Cells[0] as DataGridViewGroupCell;
                if (!cell.Collapsed)
                {
                    cell.Collapse();
                }
            }
        }

        private void either1_Event_BtnClick(Either.LeftRightSide lr)
        {
            if (lr == Either.LeftRightSide.Left)
            {
                sph.Send("M1", true, true); //手动模式打开
            }
            else
            {
                sph.Send("M0", true, true); //手动模式关闭
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridViewEx_result.Rows.Clear();
            m_Count = 1;
        }

        //作业信息
        private void button_accept_Click(object sender, EventArgs e)
        {
            ClearWorkInfo();
            try
            {
                m_strOperator = textBox_OperaterID.Text.ToString().Trim();
                m_strLotNo = textBox_LotNo.Text.ToString().Trim();
                if (m_strOperator == "" || m_strLotNo == "")
                {
                    ShowStatusForm("先输入工号和LotNo", Color.Red);
                    return;
                } 
                if (m_strLotNo.Length < 11)
                {
                    ShowStatusForm("LotNo格式不正确", Color.Red);
                    return;
                }
                GetWorkInfo();
                if (m_strProduct == "")
                {
                    ShowStatusForm("品目为空，确认网络是否正常，LotNo是否正确", Color.Red);
                    return;
                }
                if (m_strProductModel == "")
                {
                    ShowStatusForm("机种为空，确认网络是否正常，LotNo是否正确", Color.Red);
                    return;
                }
                GetProductINI();
                ReadOffsetINI();
                m_strWorkTime = radioButton_dayshift.Checked ? "白班" : "夜班";
                SetWorkInfoEnable(false);
                CommonFunc.Write(GlobalVar.gl_iniSection_WorkInfo, GlobalVar.gl_inikey_LotNo, m_strLotNo, m_fileConfig);

                if (!GetCAD4CenterPoint())
                {
                    ClearWorkInfo();
                    SetWorkInfoEnable(true);
                    return;
                }
                m_FAIStandard = m_strProductModel.ToUpper().IndexOf("A85") >= 0 ? CONST_DFAIA85 : CONST_DFAIA86;
                string str = "";
                for (int i = 0; i < m_FAIStandard.Length; i++)
                {
                     str += m_FAIStandard[i] + ", ";
                }
                lbl_Standard.Text = str.Trim(',');
                m_pathResult = Application.StartupPath + "\\Result\\" + m_strProductModel + "\\" + m_strLotNo;
                m_pathResultJC = Application.StartupPath + "\\JC\\" + m_strProductModel + "\\" + m_strLotNo;
                if (!Directory.Exists(m_pathResult)) Directory.CreateDirectory(m_pathResult);
                if (!Directory.Exists(m_pathResultJC)) Directory.CreateDirectory(m_pathResultJC);
                DispScanMode();
            }
            catch
            {
                ClearWorkInfo();
                SetWorkInfoEnable(true);
            }
        }

        private void GetProductINI()
        {
            //旋转补正角度,以14点为X轴
            string strRotateAngle = CommonFunc.Read(ini_Section_RotateAngle, m_strProductModel + "_14", "0", m_fileConfig);
            IFlex14Angle = Convert.ToDouble(strRotateAngle) * Math.PI / 180;
            //旋转至制品水平角度
            string strZXAngle = CommonFunc.Read(ini_Section_RotateAngle, m_strProductModel + "_ZX", "0", m_fileConfig);
            IFlexZXAngle = Convert.ToDouble(strZXAngle) * Math.PI / 180;

        }

        private void ClearWorkInfo()
        {
            m_strOperator = "";
            m_strLotNo = "";
            m_strProduct = "";
            m_strProductModel = "";
            m_strWorkTime = "";
            textBox_pinmu.Text = "";
            lbl_productModel.Text = "机种";
        }

        private void SetWorkInfoEnable(bool bl)
        {
            textBox_OperaterID.Enabled = textBox_LotNo.Enabled = groupBox_workTime.Enabled = bl;
            button_accept.Enabled = bl;
            button_cancel.Enabled = !bl;
        }

        /// <summary>
        /// 获取Lot信息
        /// </summary>
        private void GetWorkInfo()
        {
            if (m_strLotNo == "99999999985")
            {
                m_strProduct = "TEST";
                HalconCCD.myFunction.gl_strProductModel = GlobalVar.gl_strProductModel = m_strProductModel = "A85IFLEX";
                textBox_pinmu.Text = m_strProduct;
                lbl_productModel.Text = m_strProductModel;
                return;
            }
            if (m_strLotNo == "99999999986")
            {
                m_strProduct = "TEST";
                HalconCCD.myFunction.gl_strProductModel = GlobalVar.gl_strProductModel = m_strProductModel = "A86IFLEX";
                textBox_pinmu.Text = m_strProduct;
                lbl_productModel.Text = m_strProductModel;
                return;
            }
            DBQuery db = new DBQuery();
            m_strProduct = db.checkLotByS400(m_strLotNo); //品目
            HalconCCD.myFunction.gl_strProductModel = GlobalVar.gl_strProductModel = m_strProductModel = db.GetProductModel(m_strProduct);
            textBox_pinmu.Text = m_strProduct;
            lbl_productModel.Text = m_strProductModel;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            ClearWorkInfo();
            SetWorkInfoEnable(true);
        }
        
        private void btn_openResult_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", m_pathResult);
        }

        private void tsmi_ModeOpen_Click(object sender, EventArgs e)
        {
            GlobalVar.m_bInCalibMode = true;
            SingleCheck(sender);

            tssl_testType.Text = GlobalVar.m_bInCalibMode ? "校准片量测中" : "制品量测中";
            DispScanMode();
            ShowStatusForm("现在是【校准片】量测中，确认放入的是校准片", Color.Red, "", true);

            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].SetMarkOrPorductArea(0);
            }
        }
        private void SingleCheck(object sender)   //自定义函数   
        {
            tsmi_ModeOpen.Checked = false;
            tsmi_ModeClose.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
        }
        private void tsmi_ModeClose_Click(object sender, EventArgs e)
        {
            GlobalVar.m_bInCalibMode = false;
            SingleCheck(sender);
            tssl_testType.Text = GlobalVar.m_bInCalibMode ? "校准片量测中" : "制品量测中";
            DispScanMode();
            ShowStatusForm("现在是【制品】量测中，确认放入的是制品", Color.Red, "", true);
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].SetMarkOrPorductArea(1);
            }
        }

        private void 开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.m_bChangeForm = true;
            tsmi_changeOpen.Checked = false;
            tsmi_changeClose.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].btn_config.Text = "交换";
                CCDHelp[i].btn_config.BackColor = Color.DarkGray;
            }
        }
        private void 关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.m_bChangeForm = false;
            tsmi_changeOpen.Checked = false;
            tsmi_changeClose.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].changeForm = false;
                CCDHelp[i].BackColor = Color.DarkGray;
                CCDHelp[i].btn_config.Text = "CCD配置";
                CCDHelp[i].btn_config.BackColor = Color.PaleGreen;
            }
        }

        private void tsmi_basler_Click(object sender, EventArgs e)
        {
            tsmi_basler.Checked = false;
            tsmi_avt.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
            GlobalVar.m_nCameraType = 0;
            SetCameraType();
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_CameraType, GlobalVar.m_nCameraType.ToString(), m_fileConfig);
            MessageBox.Show("更改相机类型，必须重启软件");
            Thread.Sleep(1000);
            Application.ExitThread();
        }
        private void tsmi_avt_Click(object sender, EventArgs e)
        {
            tsmi_basler.Checked = false;
            tsmi_avt.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
            GlobalVar.m_nCameraType = 1;
            SetCameraType();
            CommonFunc.Write(ini_Section_TestInfo, ini_Key_CameraType, GlobalVar.m_nCameraType.ToString(), m_fileConfig);
            MessageBox.Show("更改相机类型，必须重启软件");
            Thread.Sleep(1000);
            Application.ExitThread();
        }
        private void SetCameraType()
        {
            tssl_cameraType.Text = GlobalVar.m_nCameraType == 0 ? "Basler相机" : "AVT相机";
            tsmi_avt.Checked = false;
            tsmi_basler.Checked = false;
            if (GlobalVar.m_nCameraType == 0) tsmi_basler.Checked = true;
            if (GlobalVar.m_nCameraType == 1) tsmi_avt.Checked = true;
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                CCDHelp[i].CameraType = GlobalVar.m_nCameraType;
            }
        }

        private void MainForm_event_ChangeForm(int lastIndex)
        {
            if(!GlobalVar.m_bChangeForm) return;
            for (int i = 0; i < CCDHelp.Length; i++)
            {
                if (CCDHelp[i].changeForm && i != lastIndex)
                {
                    Point loca = CCDHelp[i].Location;
                    CCDHelp[i].Location1 = CCDHelp[i].Location = CCDHelp[lastIndex].Location;
                    CCDHelp[lastIndex].Location1 = CCDHelp[lastIndex].Location = loca;
                    CCDHelp[i].changeForm = false;
                    CCDHelp[i].BackColor = Color.DarkGray;
                    CCDHelp[i].btn_config.BackColor = Color.DarkGray;
                    CCDHelp[lastIndex].changeForm = false;
                    CCDHelp[lastIndex].BackColor = Color.DarkGray;
                    CCDHelp[lastIndex].btn_config.BackColor = Color.DarkGray;
                }
            }
        }
        
        private void tsmi_calc1_Click(object sender, EventArgs e)
        {
            m_nCalcFunc = 1;
            tsmi_calc1.Checked = true;
            tsmi_calc2.Checked = false;
            tsmi_calc3.Checked = false;
            tssl_calcfunc.Text = tsmi_calc1.Text.ToString();
        }

        private void tsmi_calc2_Click(object sender, EventArgs e)
        {
            m_nCalcFunc = 2;
            tsmi_calc1.Checked = false;
            tsmi_calc2.Checked = true;
            tsmi_calc3.Checked = false;
            tssl_calcfunc.Text = tsmi_calc2.Text.ToString();
        }

        private void tsmi_calc3_Click(object sender, EventArgs e)
        {
            m_nCalcFunc = 3;
            tsmi_calc1.Checked = false;
            tsmi_calc2.Checked = false;
            tsmi_calc3.Checked = true;
            tssl_calcfunc.Text = tsmi_calc3.Text.ToString();
        }

        private void textBox_OperaterID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
                button_accept.PerformClick();
        }

        private void textBox_LotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                button_accept.PerformClick();
        }

        #endregion

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //m_timeLast9Point = DateTime.Now;
                //return;
                //CommonFunc.Write(ini_Section_DATA, ini_Key_Last9Point, m_timeLast9Point.ToString("yyyy-MM-dd HH:mm:ss.fff"), m_fileConfig);

                //string str = CommonFunc.Read(ini_Section_DATA, ini_Key_Last9Point, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), m_fileConfig);
                //m_timeLast9Point = Convert.ToDateTime(str);

                //StartScanBarcode();

                //GetAllResult();

            }
            catch { }
            finally { }
        }

        private void lbl_testcount_Click(object sender, EventArgs e)
        {
            bstop = true;
        }

        bool bFortest = false;
        private void checkBox_Fortest_CheckedChanged(object sender, EventArgs e)
        {
            bFortest = checkBox_Fortest.Checked;
        }




    }
}
