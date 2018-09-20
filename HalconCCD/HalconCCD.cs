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
using System.Threading;

namespace HalconCCD
{
    public partial class HalconCCD : UserControl
    {
        public int m_nCameraType = 0;     //相机类型
        public bool m_bInitOK = false;    //相机初始化
        private string ccdName = "";      //CCD名称
        public string CCDDeviceID = "";   //CCD编号
        public Size CCDSize = new Size(2448, 2048); //CCD分辨率
        public int CCDExposure = 0;       //CCD曝光值
        public string m_avtName = "";

        // Local iconic variables 
        private HObject ho_Image = null;
        private HImage CurrentImg;

        // Local control variables 
        public HTuple hv_AcqHandle = null;

        public HWindow mHoDisplay;
        public HWndCtrl mView;
        public PointF m_currentPoint = new PointF();

        /// <summary>
        /// 是否实时播放
        /// </summary>
        private ManualResetEventSlim Player = new ManualResetEventSlim(false);
        /// <summary>
        /// 是否开启实时播放
        /// </summary>
        internal bool Run { get { return Player.IsSet; } }
        #region 属性
        /// <summary>
        /// CCD名称
        /// </summary>
        public string CCDName
        {
            get { return ccdName; }
            set
            {
                ccdName = value;
                this.label_Title.Text = this.ccdName.ToString();
            }
        }
        #endregion
        #region 委托
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
        //显示图片
        public delegate void dele_ShowHimage(HImage himage);
        private dele_ShowHimage m_eveShowHimage;
        public event dele_ShowHimage event_ShowHimage
        {
            add
            {
                if (m_eveShowHimage == null)
                {
                    m_eveShowHimage += value;
                }
            }
            remove
            {
                m_eveShowHimage -= value;
            }
        }
        //适应窗口
        public delegate void dele_event_resetWindow();
        private dele_event_resetWindow m_event_resetWindow;
        public event dele_event_resetWindow event_resetWindow
        {
            add
            {
                if (m_event_resetWindow == null)
                {
                    m_event_resetWindow += value;
                }
            }
            remove
            {
                m_event_resetWindow -= value;
            }
        }
        #endregion
        public HalconCCD()
        {
            InitializeComponent();
        }

        private void HalconCCD_Load(object sender, EventArgs e)
        {
            mView = new HWndCtrl(this.hWindowControl_Player);
            mHoDisplay = this.hWindowControl_Player.HalconWindow;
            mView.event_disCurrentPoint += new HWndCtrl.dele_event_disCurrentPoint(mView_event_disCurrentPoint);
        }
        
        private void mView_event_disCurrentPoint(PointF point)
        {
            if (m_event_displayMouse != null)
                m_event_displayMouse(point);
        }

        /* Image coordinates, which describe the image part that is displayed  
           in the HALCON window */
        private double ImgRow1, ImgCol1, ImgRow2, ImgCol2;
        private double zoomWndFactor;
        public void resetWindow(HObject obj, double imageHeight = 512, double imageWidth = 512)
        {
            string s = string.Empty;
            int h, w;
            //((HImage)obj).GetImagePointer1(out s, out w, out h);
            CurrentImg.GetImagePointer1(out s, out w, out h);

            ImgRow1 = 0;
            ImgCol1 = 0;
            ImgRow2 = imageHeight = h;
            ImgCol2 = imageWidth = w;

            zoomWndFactor = (double)imageWidth / hWindowControl_Player.Width;

            System.Drawing.Rectangle rect = hWindowControl_Player.ImagePart;
            rect.X = (int)ImgCol1;
            rect.Y = (int)ImgRow1;
            rect.Width = (int)imageWidth;
            rect.Height = (int)imageHeight;
            hWindowControl_Player.ImagePart = rect;
        }

        /// <summary>
        /// 設置picturebox圖片
        /// </summary>
        /// <param name="bmp"></param>
        public void ShowHimage(HImage himage)
        {
            try
            {
                HObject hoimage = himage;
                CurrentImg = new HImage(hoimage);
                mView.addIconicVar(himage);
                resetWindow(himage);
                repaint(mHoDisplay, himage);
                if (m_eveShowHimage != null)
                    m_eveShowHimage(himage);
            }
            catch { }
        }

        /// <summary>
        /// Repaints the HALCON window 'window'
        /// </summary>
        public void repaint(HalconDotNet.HWindow window, HObject obj)
        {
            HSystem.SetSystem("flush_graphic", "false");
            window.ClearWindow();

            window.DispObj(obj);

            HSystem.SetSystem("flush_graphic", "true");

            window.SetColor("black");
            window.DispLine(-100.0, -100.0, -101.0, -101.0);
        }

        #region --------------外部调用--------------
        public void SetStatusText(string str, bool bl = false)
        {
            if (m_eveInstagram_StatusText != null)
                m_eveInstagram_StatusText(str, bl);
        }
        
        /// <summary>
        /// 获取当前显示图片
        /// </summary>
        /// <returns></returns>
        public HImage GetCurrentImage()
        {
            return CurrentImg;
        }

        /// <summary>
        /// 初始化CCD
        /// </summary>
        /// <param name="ccdname">CCD名称</param>
        /// <returns></returns>
        public bool InitCCD()
        {
            //Thread thd = new Thread(new ThreadStart(delegate {
                try
                {
                    m_bInitOK = false;
                    if (string.IsNullOrEmpty(CCDName))
                    {
                        SetStatusText("先确定要连接的相机", true);
                        return false;
                    }
                    SetStatusText("初始化中。。。");
                    if (m_nCameraType == 0)
                    {
                        // Initialize local and output iconic variables 
                        HOperatorSet.GenEmptyObj(out ho_Image);
                        //Image Acquisition 01: Code generated by Image Acquisition 01
                        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "progressive",
                            -1, "default", -1, "false", "default", CCDName, 0, -1, out hv_AcqHandle);
                    }
                    else if (m_nCameraType == 1)
                    {
                        // Initialize local and output iconic variables 
                        HOperatorSet.GenEmptyObj(out ho_Image);
                        //Image Acquisition 01: Code generated by Image Acquisition 01
                        HOperatorSet.OpenFramegrabber("1394IIDC", 1, 1, 0, 0, 0, 0, "progressive", 8,
                            "gray", -1, "false", "default", m_avtName, 0, -1, out hv_AcqHandle);

                        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "camera_type", "7:0:0");
                        if (ccdName.ToUpper() == "CCD1" || ccdName.ToUpper() == "CCD3")
                        {
                            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "packet_size", 1500);
                        } 
                        if (ccdName.ToUpper() == "CCD2" || ccdName.ToUpper() == "CCD4")
                        {
                            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "packet_size", 6000);
                        }
                        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "exposure", 200);
                    }
                    m_bInitOK = true;
                    SetStatusText("初始化成功");
                    
                    //相机参数读取
                    HTuple value;             
                }
                catch
                {
                    SetStatusText("初始化失败", true);
                    m_bInitOK = false;
                }
                SetCCDButtonEnable(m_bInitOK);
                InitRoiConfig();
                return m_bInitOK;
            //}));
            //thd.IsBackground = true;
            //thd.Start();
            
        }

        /// <summary>
        /// 关闭CCD
        /// </summary>
        public void CloseCCD()
        {
            try
            {
                Player.Reset();
                Thread.Sleep(100);
                HOperatorSet.CloseFramegrabber(hv_AcqHandle);
                if (ho_Image != null)
                    ho_Image.Dispose();
                SetStatusText("已关闭");
            }
            catch { }
        }

        /// <summary>
        /// 设置相机曝光值
        /// </summary>
        /// <param name="value"></param>
        public void SetCCDExposure(int value)
        {
            try
            {
                if (!m_bInitOK) return;
                if (m_nCameraType == 0)
                { HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTimeRaw", value); }
                if (m_nCameraType == 1)
                {
                    if (value >= 4095)
                    {
                        MessageBox.Show("曝光范围0-4095");
                        value = 4090;
                    }
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "shutter", value);
                }
            }
            catch (Exception ex)
            {
                SetStatusText("设置曝光值异常：" + ex.Message, true);
            }
        }

        /// <summary>
        /// 图片镜像旋转
        /// </summary>
        /// <param name="himage"></param>
        /// <returns></returns>
        public HImage CCDImageRotate(HImage himage)
        {
            HImage retImage = null;
            if (this.CCDName.ToUpper().Contains("UP")) //上CCD需旋转180度
            {
                retImage = himage.RotateImage(180.0, "constant");
            }
            if (this.CCDName.ToUpper().Contains("DOWN")) //下CCD需左右镜像
            {
                retImage = himage.MirrorImage("row");
            }
            return retImage;
        }

        public bool m_bCaptureOK = false;  //是否拍照完成
        /// <summary>
        /// 拍一张
        /// </summary>
        public void PlayerOne()
        {
            if (m_nCameraType == 1)
            {
                Thread thd = new Thread(new ThreadStart(delegate
                {
                    Player.Reset();
                    Thread.Sleep(10);
                    GrabImage();
                }));
                thd.IsBackground = true;
                thd.Start();
            }
            else if (m_nCameraType == 0)
            {
                Player.Reset();
                Thread.Sleep(10);
                GrabImage();
            }
        }

        private Thread thd;
        /// <summary>
        /// 开启播放
        /// </summary>
        public void PlayerRun()
        {
            if (!m_bInitOK)
            {
                SetStatusText("未初始化", true);
                return;
            }
            if (thd == null || (thd != null && !thd.IsAlive))
            {
                thd = new Thread(HDevelopExport);
                thd.IsBackground = true;
                thd.Name = CCDName.ToString() + "实时图像";
                thd.Start();
            }
            Player.Set();
            SetStatusText("连续生成图像中。。。");
        }
        private void HDevelopExport()
        {
            //// Default settings used in HDevelop 
            //HOperatorSet.SetSystem("width", 512);
            //HOperatorSet.SetSystem("height", 512);

            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            while (Player.Wait(-1))
            {
                try
                {
                    GrabImage(true);
                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            //HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            //ho_Image.Dispose();
        }
        
        /// <summary>
        /// 抓取图像 的 锁
        /// </summary>
        private object lockshow = new object();
        private bool InGrabImage = false;//是否处于抓取中
        DateTime lastgrab;
        Thread thd_Beat;
        /// <summary>
        /// 抓取图像
        /// </summary>
        /// <param name="Async">是否异步</param>
        ///  <param name="Beat">心跳只抓图，不匹配</param>
        private void GrabImage(bool Async = false, bool Beat = false)
        {
            try
            {
                InGrabImage = true;
                lock (lockshow)
                {
                    lastgrab = DateTime.Now;
                    if (!m_bInitOK)
                    {
                        SetStatusText("相机未打开", true);
                        return; 
                    };
                    //m_MarkSucc = false;
                    if(ho_Image != null) ho_Image.Dispose();

                    if (Async) HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                    else HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
                    
                    if (Beat) return;

                    //Image Acquisition 01: Do something
                    if (CurrentImg != null)
                        CurrentImg.Dispose();
                    CurrentImg = new HImage(ho_Image);

                    //A86IFlex Basler相机要上下左右镜像图片
                    if (m_nCameraType == 0 && myFunction.gl_strProductModel.ToUpper().IndexOf("A86") >= 0)
                    {
                        CurrentImg = CurrentImg.RotateImage(180.0, "bilinear");
                        CurrentImg = CurrentImg.RotateImage(0.0, "constant");
                    }

                    CurrentImg = ReduceImage(CurrentImg);//选中ROI图片
                    //mAssistant.SetTestImage(CurrentImg);
                    //CreateModelGraphics();
                    ShowHimage(CurrentImg);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Thread.Sleep(10);
                InGrabImage = false;
            }
        }

        public bool m_bIn9Point = false;
        //裁剪图片 2018.05.14
        private HImage ReduceImage(HImage image)
        {
            if (m_bIn9Point) return image;
            if (UserSearchArea)
            {
                HObject ho_Rectangle, ho_ImageReduced;
                HOperatorSet.GenEmptyObj(out ho_Rectangle);
                HOperatorSet.GenEmptyObj(out ho_ImageReduced);
                try
                {
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Rectangle, SearchRec_y1, SearchRec_x1, SearchRec_y2, SearchRec_x2);
                    ho_ImageReduced.Dispose();
                    HOperatorSet.ReduceDomain(image, ho_Rectangle, out ho_ImageReduced);

                    return new HImage(ho_ImageReduced);
                }
                catch (Exception ex)
                {
                    SetStatusText("ROI图像异常：" + ex.Message);
                    return image;
                }
                finally
                {
                    if (ho_Rectangle != null) ho_Rectangle.Dispose();
                    if (ho_ImageReduced != null) ho_ImageReduced.Dispose();
                }
            }
            else return image;
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        public void PlayerStop()
        {
            if (!m_bInitOK)
            {
                SetStatusText("未初始化");
                return;
            }
            Player.Reset();
            SetStatusText("已STOP");
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        public void SaveImage(HObject image)
        {
            try
            {
                //string str = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); 
                string str = Application.StartupPath + "\\SaveImage\\" + CCDName + "\\";
                if (!Directory.Exists(str))
                    Directory.CreateDirectory(str);
                str += DateTime.Now.ToString("yyyyMMdd-HHmmssfff"); //+文件名
                HOperatorSet.WriteImage(image, "jpeg", 0, str);
                SetStatusText("保存图片成功");
            }
            catch { SetStatusText("保存图片失败", true); }
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="strPicPath"></param>
        public void loadImage(string strPicPath)
        {
            Player.Reset();
            CurrentImg = new HImage(strPicPath);
            //CreateModelGraphics();
            ShowHimage(CurrentImg);
            resetWindow(CurrentImg);
            if (m_event_resetWindow != null)
                m_event_resetWindow();
        }
        #endregion

        #region --------------CCD相机控制--------------
        public void SetCCDButtonEnable(bool bl)
        {
            this.Invoke(new Action(() =>
            {
                this.tsb_oneShot.Enabled = 
                this.tsb_continuousShot.Enabled = 
                this.tsb_stop.Enabled = bl;
            }));
        }

        private void tsb_oneShot_Click(object sender, EventArgs e)
        {
            PlayerOne();
        }

        private void tsb_continuousShot_Click(object sender, EventArgs e)
        {
            PlayerRun();
        }

        private void tsb_stop_Click(object sender, EventArgs e)
        {
            PlayerStop();
        }

        bool bCloseCCD = false;
        private void tsb_init_Click(object sender, EventArgs e)
        {
            Thread thd = new Thread(new ThreadStart(delegate
            {
                if (!bCloseCCD)
                {
                    CloseCCD();
                    bCloseCCD = !bCloseCCD;
                }
                else
                {
                    InitCCD();
                    bCloseCCD = !bCloseCCD;
                }
            }));
            thd.IsBackground = true;
            thd.Start();
        }

        private void tsb_save_Click(object sender, EventArgs e)
        {
            if(CurrentImg != null)
                SaveImage(CurrentImg.Clone());
        }

        private void tsb_loadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string resultFile = openFileDialog1.FileName;
                loadImage(resultFile);
            }
        }
        private void tsb_restImage_Click(object sender, EventArgs e)
        {
            ////mView.addIconicVar(ho_Image);
            ////mView.repaint();

            if (CurrentImg != null)
            {
                resetWindow(CurrentImg);
                repaint(mHoDisplay, CurrentImg);
            }

            if (m_event_resetWindow != null)
                m_event_resetWindow();

        }
        #endregion

        #region ROI 2018.5.14新增
        private const string gl_inikey_ShowArea = "ShowArea";
        private const string gl_iniKey_X1 = "Rect_X1";
        private const string gl_iniKey_Y1 = "Rect_Y1";
        private const string gl_iniKey_X2 = "Rect_X2";
        private const string gl_inikey_Y2 = "Rect_Y2";
        
        /// <summary>
        /// 初始化ROI配置
        /// </summary>
        public void InitRoiConfig()
        {
            ReadRoiIni();

            int Times = 3;//临时缩小三倍显示
            //SearchRec_x1 /= Times;
            //SearchRec_y1 /= Times;
            //SearchRec_x2 /= Times;
            //SearchRec_y2 /= Times;

            DrawSearchArea(); //读取完配置后，绘制搜索框
            ReadRoiIni();//重新读取一次
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void ReadRoiIni()
        {
            ReadIniValue(ref UserSearchArea, ccdName, gl_inikey_ShowArea);
            if (this.checkBox_SerachArea.InvokeRequired)
                this.checkBox_SerachArea.Invoke(new Action(delegate { this.checkBox_SerachArea.Checked = UserSearchArea; }));
            else this.checkBox_SerachArea.Checked = UserSearchArea;
            ReadIniValue(ref SearchRec_x1, ccdName, gl_iniKey_X1);
            ReadIniValue(ref SearchRec_y1, ccdName, gl_iniKey_Y1);
            ReadIniValue(ref SearchRec_x2, ccdName, gl_iniKey_X2);
            ReadIniValue(ref SearchRec_y2, ccdName, gl_inikey_Y2);
        }

        public void WriteRoiIni()
        {
            WriteIni(ccdName, gl_iniKey_X1, SearchRec_x1.ToString());
            WriteIni(ccdName, gl_iniKey_Y1, SearchRec_y1.ToString());
            WriteIni(ccdName, gl_iniKey_X2, SearchRec_x2.ToString());
            WriteIni(ccdName, gl_inikey_Y2, SearchRec_y2.ToString());
        }

        /// <summary>
        /// 根据不同的CCD，读取相关的配置
        /// </summary>
        /// <param name="BoolStr"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        private void ReadIniValue(ref bool BoolStr, string section, string key)
        {
            string inifile = "ini File：";

            string str;
            if (myFunction.GetIniString(section, string.Format("{0}", key), out str))
            {
                bool boolstr;
                if (bool.TryParse(str, out boolstr)) BoolStr = boolstr;
                //else MessageBox.Show(inifile + key, "Convert Fail");
            }
            //else MessageBox.Show(inifile + key, "Read Fail");
        }

        private void ReadIniValue(ref double NumStr, string section, string key)
        {
            string inifile = "ini File：";

            string str;
            if (myFunction.GetIniString(section, string.Format("{0}", key), out str))
            {
                int intstr;
                double doublestr;
                if (int.TryParse(str, out intstr)) NumStr = intstr;
                else if (double.TryParse(str, out doublestr)) NumStr = doublestr;
                //else MessageBox.Show(inifile + key, "Convert Fail");
            }
            //else MessageBox.Show(inifile + key, "Read Fail");
        }

        private void ReadIniValue(ref HTuple NumStr, string section, string key)
        {
            string inifile = "ini File：";

            string str;
            if (myFunction.GetIniString(section, string.Format("{0}", key), out str))
            {
                int intstr;
                double doublestr;
                if (int.TryParse(str, out intstr)) NumStr = intstr;
                else if (double.TryParse(str, out doublestr)) NumStr = doublestr;
                //else MessageBox.Show(inifile + key, "Convert Fail");
            }
            //else MessageBox.Show(inifile + key, "Read Fail");
        }
        
        #region 线组成矩形 搜索区域
        public bool UserSearchArea = false;
        public double SearchRec_x1, SearchRec_x2, SearchRec_y1, SearchRec_y2;
        #endregion
        bool InDrawRect = false;
        private void btn_SearchArea_Click(object sender, EventArgs e)
        {
            if (!this.checkBox_SerachArea.Checked) return;//未选中绘制ROI
            if (InGrabImage) return;//抓图中，不响应选择搜索区域
            if (InDrawRect) return;//上次绘图未完成，不再进入
            try
            {
                UserSearchArea = false;
                GrabImage();//抓取一次图像，清空之前绘制的线
                HTuple width; HTuple height;
                CurrentImg.GetImageSize(out width, out height);
                UserSearchArea = this.checkBox_SerachArea.Checked;
                HTuple SearchRec_x1, SearchRec_y1, SearchRec_x2, SearchRec_y2;
                InDrawRect = true;
                HOperatorSet.DrawRectangle1(this.hWindowControl_Player.HalconWindow, out SearchRec_y1, out SearchRec_x1, out SearchRec_y2, out SearchRec_x2);
                Console.WriteLine("Top:{0}\tLeft:{1}\tBottom:{2}\tRight:{3}", SearchRec_y1, SearchRec_x1, SearchRec_y2, SearchRec_x2);
                WriteRoiIni();
                DrawSearchArea();
            }
            catch (Exception ex) {  }
            finally
            {
                InDrawRect = false;
            }
        }

        /// <summary>
        /// 根据不同的CCD，写入ini【区分CCD1和CCD2】
        /// </summary>
        /// <param name="section">区域</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        private void WriteIni(string section, string key, string value)
        {
            myFunction.WriteIniString(section, string.Format("{0}", key), value);
        }
        /// <summary>
        /// 画搜索区域
        /// </summary>
        private void DrawSearchArea()
        {
            #region 线围成矩形
            if (!UserSearchArea) return;    //不使用搜索区域

            //x1 = (rec.Width - Region_Widht) / 2;
            //x2 = (rec.Width + Region_Widht) / 2;
            //y1 = (rec.Height - Region_Height) / 2;
            //y2 = (rec.Height + Region_Height) / 2;
            HOperatorSet.ClearWindow(hWindowControl_Player.HalconID);
            HOperatorSet.DispLine(hWindowControl_Player.HalconID, SearchRec_y1, SearchRec_x1, SearchRec_y1, SearchRec_x2);
            HOperatorSet.DispLine(hWindowControl_Player.HalconID, SearchRec_y1, SearchRec_x2, SearchRec_y2, SearchRec_x2);
            HOperatorSet.DispLine(hWindowControl_Player.HalconID, SearchRec_y2, SearchRec_x2, SearchRec_y2, SearchRec_x1);
            HOperatorSet.DispLine(hWindowControl_Player.HalconID, SearchRec_y2, SearchRec_x1, SearchRec_y1, SearchRec_x1);
            #endregion
            //HOperatorSet.DispRectangle1(hWindowControl_Player.HalconID, rec.Height / 2 - 300, rec.Width / 2 - 300, rec.Height / 2 + 300, rec.Width / 2 + 300);     
        }

        private void checkBox_SerachArea_CheckedChanged(object sender, EventArgs e)
        {
            UserSearchArea = this.btn_SearchArea.Enabled = this.checkBox_SerachArea.Checked;
            WriteIni(ccdName, gl_inikey_ShowArea, UserSearchArea.ToString());
        }
        #endregion

        private void hWindowControl_Player_Paint(object sender, PaintEventArgs e)
        {
            return;
            Point p1 = new Point(0, hWindowControl_Player.Height / 2);
            Point p2 = new Point(hWindowControl_Player.Width, hWindowControl_Player.Width / 2);
            Point p3 = new Point(hWindowControl_Player.Height / 2, 0);
            Point p4 = new Point(hWindowControl_Player.Height, hWindowControl_Player.Width);
            HOperatorSet.DispLine(hWindowControl_Player.HalconWindow, p1.X, p1.Y, p2.X, p2.Y);
            HOperatorSet.DispLine(hWindowControl_Player.HalconWindow, p3.X, p3.Y, p4.X, p4.Y);
        }


    }
}
