using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;

namespace ZSMeasure
{
    public partial class myCCDHelp : UserControl
    {
        //=================================Flag=================================
        private int m_nCameraType = 0;
        public int CameraType //相机类型
        {
            get { return m_nCameraType; }
            set
            {
                m_nCameraType = value;
                this.halconCCD1.m_nCameraType = m_nCameraType; 
            }
        }
        //=================================Floder=================================
        private string m_pathConfig = Application.StartupPath + "\\CONFIG";
        private string m_pathLog = Application.StartupPath + "\\LOG";
        private string m_pathImage = Application.StartupPath + "\\Image";
        public string m_pathConfigTup = Application.StartupPath + "\\CONFIG";
        //=================================INI=================================
        private string m_fileINI = "";
        private string ini_key_MarkArea = "MarkArea";
        private string ini_key_PointArea = "PointArea";
        private string ini_key_ProductArea = "ProductArea";
        private string ini_key_ExposureP = "ExposureP";
        private string ini_key_ExposureM = "ExposureM";
        private string ini_key_UmPixel = "UmPixel";
        private string ini_key_Location = "Location";
        private string ini_key_AVTDevice = "AVTDevice";
        //=================================图片处理=================================
        HTuple hv_DispWindown;
        private HalconHelp halconHelp = new HalconHelp();
        private HObject hoImage = null; //待处理图片
        public bool m_bIn9Point = false;      //九点校正模式
        public bool m_bInHandleImage = false; //图片处理中
        public bool m_bHomMat2DOK = false;    //校正矩阵加载完毕
        private PointH m_MarkCenter = new PointH(); //Mark点中心
        private PointH m_MarkAxis = new PointH();  //Mark点中心，实际坐标
        private int[] AreaMark = new int[2] { 100, 100 };    //校准片大Mark点面积区间
        private int[] AreaPoint = new int[2] { 100, 100 };   //校准片小Mark点面积区间
        private int[] AreaProduct = new int[2] { 100, 100 }; //制品Mark点面积区间
        public PointH point1 = new PointH(); //ROI左上角
        public PointH point2 = new PointH(); //ROI右下角

        public int m_ExposureModel = 0;   //相机曝光值-校准片
        public int m_ExposureProduct = 0; //相机曝光值-制品
        public double m_UmPixel = 0.0;    //预估像素点尺寸，用于筛选校正点
        private Point m_Location = new Point(); //位置
        public Point Location1
        {
            get { return m_Location; }
            set 
            { 
                m_Location = value;
                CommonFunc.Write(m_CCDName, ini_key_Location, (m_Location.X + "," + m_Location.Y), m_fileINI);
            }
        }
        //=========================菲林片11*11矩阵=========================
        public PointH[,] CADAllData = new PointH[11, 11];
        private PointH cadCenterData = new PointH(); //CAD 大Mark点中心坐标
        //点之间的间距
        private double[] DDX = new double[11] { 1.75, 1.4, 1.05, 0.7, 0.35, 0, -0.35, -0.7, -1.05, -1.4, -1.75 };
        private double[] DDY = new double[11] { 1.75, 1.4, 1.05, 0.7, 0.35, 0, -0.35, -0.7, -1.05, -1.4, -1.75 };

        public int sequence = -1;
        #region 属性
        public bool m_bInitCCD
        {
            get { return halconCCD1.m_bInitOK; }
        }
        private string m_AVTName = "";
        public string AVTName
        {
            get { return m_AVTName; }
            set
            {
                m_AVTName = value;
                halconCCD1.m_avtName = m_AVTName;
            }
        }
        private string m_CCDName = "";
        /// <summary>
        /// 相机的位置【名称】
        /// </summary>
        [Category("自定义"), Browsable(true), Description("相机")]
        public string CCDName
        {
            get { return m_CCDName; }
            set
            {
                this.m_CCDName = value;
                this.halconCCD1.CCDName = this.m_CCDName;
            }
        }
        public PointH CADCenterData
        {
            get { return cadCenterData; }
            set { cadCenterData = value; }
        }
        /// <summary>
        /// Mark点像素坐标
        /// </summary>
        public PointH MarkCenter
        {
            get { return m_MarkCenter; }
            set { m_MarkCenter = value; }
        }
        /// <summary>
        /// Mark点实际坐标
        /// </summary>
        public PointH MarkAxis
        {
            get { return m_MarkAxis; }
            set { m_MarkAxis = value; }
        }

        public int[] MarkArea1
        {
            get { return AreaMark; }
            set { AreaMark = value; }
        }
        public int[] PointArea1
        {
            get { return AreaPoint; }
            set { AreaPoint = value; }
        }
        public int[] AreaProduct1
        {
            get { return AreaProduct; }
            set { AreaProduct = value; }
        }
        #endregion
        #region 委托
        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="str"></param>
        public delegate void dele_StatusText(string str, bool isError = false);
        private dele_StatusText m_eveInstagram_StatusText;
        public event dele_StatusText event_StatusText
        {
            add
            {
                if (m_eveInstagram_StatusText == null)
                {
                    m_eveInstagram_StatusText += value;
                }
            }
            remove
            {
                m_eveInstagram_StatusText -= value;
            }
        }
        //显示鼠标位置坐标
        public delegate void dele_event_displayMouse(PointF point);
        private dele_event_displayMouse m_event_displayMouse;
        public event dele_event_displayMouse event_displayMouse
        {
            add
            {
                if (m_event_displayMouse == null)
                {
                    m_event_displayMouse += value;
                }
            }
            remove
            {
                m_event_displayMouse -= value;
            }
        }
        //交换窗体
        public delegate void dele_ChangeForm(int lastIndex);
        private dele_ChangeForm m_eventChangeForm;
        public event dele_ChangeForm event_ChangeForm
        {
            add 
            {
                if (m_eventChangeForm == null)
                    m_eventChangeForm += value;
            }
            remove 
            {
                m_eventChangeForm -= value;
            }
        }
        #endregion
        
        public myCCDHelp()
        {
            InitializeComponent();
            halconCCD1.event_StatusText += new HalconCCD.HalconCCD.dele_StatusText(halconCCD1_event_StatusText); 
            halconCCD1.event_ShowHimage += new HalconCCD.HalconCCD.dele_ShowHimage(halconCCD1_event_ShowHimage);
            halconCCD1.event_displayMouse += new HalconCCD.HalconCCD.dele_event_displayMouse(halconCCD1_event_displayMouse);
        }

        private void myCCDHelp_Load(object sender, EventArgs e)
        {
            hv_DispWindown = this.halconCCD1.hWindowControl_Player.HalconWindow;
            halconHelp.SetDispWindow(hv_DispWindown);
            //GetHomMat2D();
        }

        /// <summary>
        /// 加载相机标定文件
        /// </summary>
        public void GetHomMat2D()
        {
            if (!Directory.Exists(m_pathConfigTup)) Directory.CreateDirectory(m_pathConfigTup);
            m_bHomMat2DOK = halconHelp.SetHomMat2D(m_pathConfigTup, CCDName);
            DispMMPixel();
        }

        public void InitCCD()
        {
            if (m_nCameraType == 1) //AVT相机
            {
                if (m_AVTName == "")
                {
                    halconCCD1_event_StatusText("未设置AVT相机编号，初始化失败", true);
                    ShowStatusForm(this.CCDName + "-" + "未设置AVT相机编号，初始化失败", Color.Red);
                    return;
                }
            }
            halconCCD1.InitCCD();
            halconCCD1.SetCCDExposure(m_ExposureProduct); //初始化相机，设置曝光值
            point1 = new PointH(halconCCD1.SearchRec_x1, halconCCD1.SearchRec_y1);
            point2 = new PointH(halconCCD1.SearchRec_x2, halconCCD1.SearchRec_y2);
        }

        public void ReadINI()
        {
            m_fileINI = m_pathConfig + "\\" + "Config.ini"; 
            string str = "";
            //相机曝光值-制品
            str = CommonFunc.Read(m_CCDName, ini_key_ExposureP, "10000", m_fileINI);
            m_ExposureProduct = Convert.ToInt32(str);
            //相机曝光值-校准片
            str = CommonFunc.Read(m_CCDName, ini_key_ExposureM, "10000", m_fileINI);
            m_ExposureModel = Convert.ToInt32(str);
            //制品Mark点面积区间
            str = CommonFunc.Read(m_CCDName, ini_key_ProductArea, "100,100", m_fileINI);
            string[] _strPro = str.Split(',');
            if (_strPro.Length == 2)
            {
                AreaProduct[0] = Convert.ToInt32(_strPro[0]);
                AreaProduct[1] = Convert.ToInt32(_strPro[1]);
            }
            else
            { AreaProduct = new int[2] { 100, 100 }; }
            halconHelp.SetMarkArea(AreaProduct); //默认制品Mark点
            //菲林片大Mark点面积区间
            str = CommonFunc.Read(m_CCDName, ini_key_MarkArea, "100,100", m_fileINI);
            string[] _strM = str.Split(',');
            if (_strM.Length == 2)
            {
                AreaMark[0] = Convert.ToInt32(_strM[0]);
                AreaMark[1] = Convert.ToInt32(_strM[1]);
            }
            else
            { AreaMark = new int[2] { 100, 100 }; }
            //菲林片小Mark点面积区间
            str = CommonFunc.Read(m_CCDName, ini_key_PointArea, "100,100", m_fileINI);
            string[] _strP = str.Split(',');
            if (_strP.Length == 2)
            {
                AreaPoint[0] = Convert.ToInt32(_strP[0]);
                AreaPoint[1] = Convert.ToInt32(_strP[1]);
            }
            else
            { AreaPoint = new int[2] { 100, 100 }; }
            halconHelp.SetPointArea(AreaPoint);
            //像素点尺寸
            str = CommonFunc.Read(m_CCDName, ini_key_UmPixel, "2.224", m_fileINI);
            m_UmPixel = Convert.ToDouble(str);
            //位置
            str = CommonFunc.Read(m_CCDName, ini_key_Location, "0,0", m_fileINI);
            Location1 = new Point(Convert.ToInt32(str.Split(',')[0]), Convert.ToInt32(str.Split(',')[1]));
            //AVT相机编号
            AVTName = CommonFunc.Read(m_CCDName, ini_key_AVTDevice, "", m_fileINI);            
        }
        private void WriteINI()
        {
            //制品Mark点面积区间
            string str = AreaProduct[0] + "," + AreaProduct[1];
            CommonFunc.Write(m_CCDName, ini_key_ProductArea, str, m_fileINI);
            //校准片Mark点面积区间
            str = AreaMark[0] + "," + AreaMark[1];
            CommonFunc.Write(m_CCDName, ini_key_MarkArea, str, m_fileINI);
            //校准片小Mark点面积区间
            str = AreaPoint[0] + "," + AreaPoint[1];
            CommonFunc.Write(m_CCDName, ini_key_PointArea, str, m_fileINI);
            //相机曝光值-制品
            CommonFunc.Write(m_CCDName, ini_key_ExposureP, m_ExposureProduct.ToString(), m_fileINI);
            //相机曝光值-校准片
            CommonFunc.Write(m_CCDName, ini_key_ExposureM, m_ExposureModel.ToString(), m_fileINI);
            //像素点尺寸
            CommonFunc.Write(m_CCDName, ini_key_UmPixel, m_UmPixel.ToString(), m_fileINI);
            //ROI
            halconCCD1.WriteRoiIni();
            halconCCD1.InitRoiConfig();
            //AVT相机编号
            CommonFunc.Write(m_CCDName, ini_key_AVTDevice, m_AVTName, m_fileINI);
        }

        private void halconCCD1_event_StatusText(string str, bool isError = false)
        {
            if (m_eveInstagram_StatusText != null)
                m_eveInstagram_StatusText(this.CCDName + "-" + str, isError);
        }

        private void halconCCD1_event_displayMouse(PointF point)
        {
            if (m_event_displayMouse != null)
                m_event_displayMouse(point);
        }

        //弹框
        private void ShowStatusForm(string str, Color color)
        {
            this.Invoke((EventHandler)delegate
            {
                if (GlobalVar.SWBreakForm == null)
                    GlobalVar.SWBreakForm = new SwitchBreakForm();
                GlobalVar.SWBreakForm.Visible = true;
                GlobalVar.SWBreakForm.Focus();
                GlobalVar.SWBreakForm.ShowText(str, color);
            });
        }

        internal void ClearDispWindow()
        {
            HOperatorSet.ClearWindow(halconCCD1.hWindowControl_Player.HalconWindow);
        }

        /// <summary>
        /// 校准片/制品 0/1
        /// </summary>
        /// <param name="MarkOrPorduct"></param>
        public void SetMarkOrPorductArea(int MarkOrPorduct)
        {
            if (MarkOrPorduct == 0)
            {
                halconHelp.SetMarkArea(AreaMark);
                halconCCD1.SetCCDExposure(m_ExposureModel);
            }
            if (MarkOrPorduct == 1)
            {
                if (GlobalVar.m_bInCalibMode)
                {
                    halconHelp.SetMarkArea(AreaMark);
                    halconCCD1.SetCCDExposure(m_ExposureModel); 
                }
                else
                {
                    halconHelp.SetMarkArea(AreaProduct);
                    halconCCD1.SetCCDExposure(m_ExposureProduct);
                }
            }
        }

        /// <summary>
        /// 启动拍照
        /// </summary>
        internal void TakeOnePic()
        {
            m_bInHandleImage = true;
            halconCCD1.PlayerOne();
        }

        public void LoadLocalPic(HImage himage)
        {
            halconCCD1.ShowHimage(himage);
        }

        //显示图片,处理
        private void halconCCD1_event_ShowHimage(HImage himage)
        {
            try
            {
                //hoImage = himage.Clone();
                StartHanle(himage);
            }
            catch (Exception ex)
            {
                halconCCD1_event_StatusText("图片处理异常：" + ex.ToString(), true);
            }
            finally
            {
                if (hoImage != null)
                    hoImage.Dispose();
            }
        }

        public void Set9Point(bool bl)
        {
            this.m_bIn9Point = bl;
            halconCCD1.m_bIn9Point = bl;
        }

        private string ini_Section_TestInfo = "TestInfo";
        private string ini_Key_Last9Point = "Last9PointTime";
        public void StartHanle(HImage himage)
        {
            //m_bInHandleImage = true; //2018.05.25 改为多线程，在拍照前置为True
            m_MarkCenter = new PointH();
            m_MarkAxis = new PointH();
            try
            {
                HImage handleImage = himage;

                //if (GlobalVar.gl_strProductModel.ToUpper().IndexOf("A85IFLEX") >= 0)
                if (!GlobalVar.m_bUseXLD)
                    m_MarkCenter = halconHelp.GetMarkCenter(handleImage);
                else
                    m_MarkCenter = halconHelp.GetMarkCenterXLD(handleImage);
                if (m_MarkCenter.X == 0.0 || m_MarkCenter.Y == 0.0)
                    m_MarkAxis = new PointH();
                else
                    m_MarkAxis = halconHelp.GetMarkAxis(m_MarkCenter);
                if (m_bIn9Point)
                {
                    #region 九点标定处理
                    m_bHomMat2DOK = false;
                    try
                    {
                        if (m_MarkCenter.X == 0.0 || m_MarkCenter.Y == 0.0)
                        {
                            m_bHomMat2DOK = false;
                            return;
                        }
                        HTuple hv_Row, hv_Column, hv_AxisX, hv_AxisY, hv_Height, hv_Width, hv_newRow, hv_newColumn;
                        halconHelp.Get9Points(handleImage, out hv_Row, out hv_Column, out hv_Height, out hv_Width);                        
                        GetAxisData(hv_Row, hv_Column, hv_Height, hv_Width, out hv_newRow, out hv_newColumn, out hv_AxisY, out hv_AxisX);
                        //int length = hv_newRow.Length;
                        //hv_newRow[length] = m_MarkCenter.Y;
                        //hv_newColumn[length] = m_MarkCenter.X;
                        //hv_AxisY[length] = cadCenterData.Y;
                        //hv_AxisX[length] = cadCenterData.X;
                        for (int i = 0; i < hv_newRow.Length; i++)
                        {
                            halconHelp.disp_message(hv_DispWindown, (i + 1), "image", hv_newRow[i].D, hv_newColumn[i].D, "red", "true");
                        }
                        int countMY = hv_newRow.Length, countMX = hv_newColumn.Length, countAY = hv_AxisY.Length, countAX = hv_AxisX.Length;
                        if (countMY < 9 || countMX < 9 || countAY < 9 || countAX < 9 || countAX != countAY || countAX != countMX || countMX != countMY)
                        {
                            m_bHomMat2DOK = false;
                            halconCCD1_event_StatusText("标定相机异常: 标定点过少或个数不匹配", true);
                            GlobalVar.m_bNeedReCalc = true; //需要重新标定相机
                            GlobalVar.m_timeLast9Point = GlobalVar.m_timeLast9Point.AddHours((GlobalVar.m_CalibSpace + 12) * -1);
                            string strfile = Application.StartupPath + "\\CONFIG\\Config.ini";
                            CommonFunc.Write(ini_Section_TestInfo, ini_Key_Last9Point, GlobalVar.m_timeLast9Point.ToString("yyyy-MM-dd HH:mm:ss.fff"), strfile);
                            return;
                        }
                        HOperatorSet.WriteTuple(hv_newRow, m_pathConfigTup + "\\" + CCDName + "MarkY.tup");
                        HOperatorSet.WriteTuple(hv_newColumn, m_pathConfigTup + "\\" + CCDName + "MarkX.tup");
                        HOperatorSet.WriteTuple(hv_AxisY, m_pathConfigTup + "\\" + CCDName + "AxisY.tup");
                        HOperatorSet.WriteTuple(hv_AxisX, m_pathConfigTup + "\\" + CCDName + "AxisX.tup");
                        GetHomMat2D();
                    }
                    catch (Exception ex)
                    {
                        m_bHomMat2DOK = false;
                        halconCCD1_event_StatusText("标定相机异常: " + ex.ToString(), true);
                        ShowStatusForm("标定相机异常", Color.Red);
                    }
                    #endregion
                }
            }
            catch(Exception ex)
            {
                halconCCD1_event_StatusText("图片处理异常: " + ex.ToString(), true);
            }
            finally
            {
                m_bInHandleImage = false;
                DispCenterPoint();
            }
        }

        private void DispCenterPoint()
        {
            halconHelp.set_display_font(hv_DispWindown, 11, "sans", "false", "true");            
            halconHelp.disp_message(hv_DispWindown, "X:" + (m_MarkCenter.X == 0.0 ? "------" : m_MarkCenter.X.ToString("0.0000")) + " Y:" +
                                    (m_MarkCenter.Y == 0.0 ? "------" : m_MarkCenter.Y.ToString("0.0000")), "image", 10, 200, "red", "true");
            //if (m_bManualTest)
            //{
            //    m_bManualTest = false;
            //    DateTime timenow = DateTime.Now;
            //    string filecsv = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Test_" + timenow.ToString("yyyy-MM-dd") + ".csv"; //简报
            //    try
            //    {
            //        using (FileStream FS = new FileStream(filecsv, FileMode.Append, FileAccess.Write))
            //        {
            //            StreamWriter SW = new StreamWriter(FS, Encoding.Default);
            //            SW.WriteLine(m_MarkCenter.X + "," + m_MarkCenter.Y + "," + m_MarkAxis.X + "," + m_MarkAxis.Y + ",");
            //            SW.Close();
            //            SW.Dispose();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        halconCCD1_event_StatusText("手动处理异常：" + ex.ToString(), true);
            //    }
            //}
        }

        public void Close()
        {
            CloseCamera();
        }
        /// <summary>
        /// 关闭相机
        /// </summary>
        private void CloseCamera()
        {
            halconCCD1.CloseCCD();
        }

        /// <summary>
        /// 九点校正的CAD坐标
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool GetCADData(string strPath)
        {
            CADAllData = new PointH[11, 11];
            cadCenterData = new PointH();
            try
            {
                using (FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8")))
                    {
                        string strReadline;
                        while ((strReadline = sr.ReadLine()) != null)
                        {
                            if (strReadline.IndexOf(CCDName) >= 0)
                            {
                                string camindex = strReadline.Substring(3, 1);
                                for (int i = 0; i < 11; i++)
                                {
                                    strReadline = sr.ReadLine();
                                    string[] strx = strReadline.Split(',');
                                    strReadline = sr.ReadLine();
                                    string[] stry = strReadline.Split(',');
                                    if (strx.Length != stry.Length || strx.Length != 11)
                                    {
                                        MessageBox.Show(CCDName + "读取CAD坐标失败");
                                        halconCCD1_event_StatusText("读取CAD坐标失败", true);
                                        return false;
                                    }
                                    for (int j = 0; j < strx.Length; j++)
                                    {
                                        double xx = strx[j] == "" ? 0.0 : Convert.ToDouble(strx[j]);
                                        double yy = stry[j] == "" ? 0.0 : Convert.ToDouble(stry[j]);
                                        PointH pointh = new PointH(xx, yy);
                                        CADAllData[j, i] = pointh;
                                    }
                                }
                            }
                        }
                    }
                }
                cadCenterData = CADAllData[5, 5];
                return true;
            }
            catch (Exception ex)
            {
                halconCCD1_event_StatusText("获取CAD坐标异常：" + ex.ToString());
                ShowStatusForm("获取CAD坐标异常", Color.Red);
                CADAllData = new PointH[11, 11];
                cadCenterData = new PointH();
                return false;
            }
        }

        /// <summary>
        /// 获得视野内点的实际CAD坐标
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        /// <param name="AxisX"></param>
        /// <param name="AxisY"></param>
        private void GetAxisData(HTuple Row, HTuple Column, HTuple Height, HTuple Width, out HTuple newRow, out HTuple newColumn, out HTuple AxisY, out HTuple AxisX)
        {
            newRow = new HTuple();
            newColumn = new HTuple();
            AxisX = new HTuple();
            AxisY = new HTuple();
            if (m_MarkCenter.X == 0 || m_MarkCenter.Y == 0)
                return;
            //相差15个像素，认为是一行或一列
            int Distance = 15;
            int index = 0;
            for (int i = 0; i < Column.Length; i++)
            {
                double dd = Width[i].D - Height[i].D;
                if (Math.Abs(dd) >= 3) //高度宽度小于3个像素，判定超出视野，舍弃
                    continue;
                newRow[index] = Row[i];
                newColumn[index] = Column[i];
                index++;
            }
            //所有点左上到右下排序
            for (int i = 0; i < newColumn.Length - 1; i++)
            {
                for (int j = i + 1; j < newColumn.Length; j++)
                {
                    if (Math.Abs(newRow[i].D - newRow[j].D) < Distance)
                    {
                        if (newColumn[i].D > newColumn[j].D)
                        {
                            double temp = newRow[i].D;
                            newRow[i].D = newRow[j].D;
                            newRow[j].D = temp;
                            temp = newColumn[i].D;
                            newColumn[i].D = newColumn[j].D;
                            newColumn[j].D = temp;
                        }
                    }
                }
            }
            //所有点对应的实际坐标
            double DDAxis = 0.06; //距离中心点间距差小于0.06，确认点
            List<int> XXIndex = new List<int>();
            List<int> YYIndex = new List<int>();
            for (int i = 0; i < newRow.Length; i++)
            {
                double lenx1 = (m_MarkCenter.X - newColumn[i].D) * m_UmPixel * 0.001;
                double leny1 = (m_MarkCenter.Y - newRow[i].D) * m_UmPixel * 0.001;
                for (int j = 0; j < DDX.Length; j++)
                {
                    for (int k = 0; k < DDY.Length; k++)
                    {
                        double _xx = Math.Abs(lenx1 - DDX[j]);
                        double _yy = Math.Abs(leny1 - DDY[k]);
                        if (_xx < DDAxis && _yy < DDAxis)
                        {
                            XXIndex.Add(j);
                            YYIndex.Add(k);
                        }
                    }
                }
            }
            //所有点的实际坐标
            for (int i = 0; i < XXIndex.Count; i++)
            {
                AxisX[i] = CADAllData[XXIndex[i], YYIndex[i]].X;
                AxisY[i] = CADAllData[XXIndex[i], YYIndex[i]].Y;
            }
        }

        #region 界面按钮
        private void DispMMPixel()
        {
            try
            {
                this.Invoke(new EventHandler(delegate {
                    lbl_X.Text = "X:0.0000 um/pixel";
                    lbl_Y.Text = "Y:0.0000 um/pixel";
                    PointH pm = halconHelp.GetPixelMM();
                    lbl_X.Text = "X:" + (pm.X * 1000).ToString("0.0000") + " um/pixel";
                    lbl_Y.Text = "Y:" + (pm.Y * 1000).ToString("0.0000") + " um/pixel";
                }));                
            }
            catch(Exception ex)
            {

            }
        }

        private void btn_config_Click(object sender, EventArgs e)
        {
            if (GlobalVar.m_bChangeForm)
            {
                if (this.changeForm)
                {
                    this.changeForm = false;
                    this.BackColor = Color.DarkGray;
                    btn_config.BackColor = Color.DarkGray;
                    return;
                }
                this.changeForm = true;
                this.BackColor = Color.Green;
                btn_config.BackColor = Color.Green;
                if (m_eventChangeForm != null)
                    m_eventChangeForm(sequence);
                return;
            }

            CCDParaConfig paraconfig = new CCDParaConfig(this);
            if (paraconfig.ShowDialog() == DialogResult.OK)
            {
                m_AVTName = paraconfig.AVTName;
                AreaMark[0] = paraconfig.MarkMinArea;
                AreaMark[1] = paraconfig.MarkMaxArea;
                AreaPoint[0] = paraconfig.PointMinArea;
                AreaPoint[1] = paraconfig.PointMaxArea;
                AreaProduct[0] = paraconfig.ProductMinArea;
                AreaProduct[1] = paraconfig.ProductMaxArea;
                m_ExposureProduct = paraconfig.m_ExposureProduct;
                m_ExposureModel = paraconfig.m_ExposureModel;
                m_UmPixel = paraconfig.m_UmPixel;
                point1 = new PointH(paraconfig.m_roiX1, paraconfig.m_roiY1);
                point2 = new PointH(paraconfig.m_roiX2, paraconfig.m_roiY2);
                halconCCD1.SearchRec_x1 = paraconfig.m_roiX1;
                halconCCD1.SearchRec_y1 = paraconfig.m_roiY1;
                halconCCD1.SearchRec_x2 = paraconfig.m_roiX2;
                halconCCD1.SearchRec_y2 = paraconfig.m_roiY2;
                WriteINI();
                if (m_bIn9Point)
                {
                    halconHelp.SetMarkArea(AreaMark);
                    halconHelp.SetPointArea(AreaPoint);
                    halconCCD1.SetCCDExposure(m_ExposureModel); //修改曝光值-校准片
                }
                else
                {
                    if (GlobalVar.m_bInCalibMode)
                    {
                        halconHelp.SetMarkArea(AreaMark);
                        halconHelp.SetPointArea(AreaPoint);
                        halconCCD1.SetCCDExposure(m_ExposureModel); //修改曝光值-校准片
                    }
                    else
                    {
                        halconHelp.SetMarkArea(AreaProduct);
                        halconHelp.SetPointArea(AreaPoint);
                        halconCCD1.SetCCDExposure(m_ExposureProduct); //修改曝光值-制品
                    }
                }
            }
        }

        public bool changeForm = false;
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //m_bIn9Point = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.m_bChangeForm) return;
            this.changeForm = true;
            this.BackColor = Color.Green;
            if (m_eventChangeForm != null)
                m_eventChangeForm(sequence);

            return;
            PointH p = new PointH(599.9981256, 798.0322981);
            PointH pp = halconHelp.GetMarkAxis(p);

            HImage himage = halconCCD1.GetCurrentImage();
            if (himage == null)
                return;
            StartHanle(himage);
        }


    }
}
