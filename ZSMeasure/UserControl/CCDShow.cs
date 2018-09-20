using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HalconDotNet;
using MatchingModule;
using System.IO;

namespace ZSMeasure
{
    public partial class CCDShow : UserControl
    {
        /// <summary>Window control that manages visualization</summary>
        /// <summary>ROI control that manages ROI objects</summary>
        public HWndCtrl mView;
        public ROIController roiController;

        private HImage CurrentImg;
        private HRegion ModelRegion;
        private HXLD ModelContour;
        private HXLD DetectionContour;
        private MatchingAssistant mAssistant;
        private MatchingParam parameterSet;
        private MatchingOptSpeed speedOptHandler;
        private MatchingOptStatistics inspectOptHandler;
        private Color createModelWindowMode;
        private Color trainModelWindowMode;
        public bool locked;

        public string m_configFile = ""; //配置文件
        public string m_markFile = ""; //Mark点匹配模版
        public string m_xizuiMarkFile = ""; //吸嘴匹配模版
        public string m_susMarkFile = "";   //sus匹配模版
        public string m_iniCCDMatchSection = ""; //模版匹配参数配置
        public bool m_bCaptureOK = false;  //是否拍照完成
        public MatchingResult MatchResult 
        { 
            get 
            {
                return mAssistant.MatchResult; 
            }
        }

        #region 属性窗口
        private string m_CameraLocation = "";
        /// <summary>
        /// 相机的位置【名称】
        /// </summary>
        [Category("自定义"),Browsable (true),Description("相机")]
        public string CameraLocation
        {
            get { return m_CameraLocation; }
            set 
            {
                this.m_CameraLocation = value;
                this.label_Title.Text = this.m_CameraLocation.ToString();
            }
        }
        
        #region #############  minScore  ################
        private double _MinScore = 70 / 100.0;
        public double m_MinScore
        {
            get { return _MinScore; }
            set
            {
                _MinScore = value;
                mAssistant.setMinScore((double)value / 100.0);
            }
        }
        #endregion

        #region #############  numMatches  ################
        private int _MatchesNum = 1;
        public int m_MatchesNum
        {
            get { return _MatchesNum; }
            set
            {
                _MatchesNum = value;
                mAssistant.setNumMatches(value);
            }
        }
        #endregion

        #region #############  Greediness  ################
        private double _Greediness = 75 / 100.0;
        public double m_Greediness
        {
            get { return _Greediness; }
            set
            {
                _Greediness = value;
                mAssistant.setGreediness((double)value / 100.0);
            }
        }
        #endregion

        #region #############  maxOverlap  ################
        //private int _maxOverlap ;
        private double _maxOverlap = 50 / 100.0;
        public double m_maxOverlap
        {
            get { return _maxOverlap; }
            set
            {
                _maxOverlap = value;
                mAssistant.setMaxOverlap((double)value / 100.0);
            }
        }
        #endregion

        #region #############  subPixel Setting  ################
        public enum subPixel
        {
            none = 0,
            interpolation = 1,
            least_squares = 2,
            least_squares_high = 3,
            least_squares_very_high = 4
        }

        private subPixel _subPixel = subPixel.least_squares;
        //private subPixel _subPixel;
        public subPixel m_subPixel
        {
            get { return _subPixel; }
            set
            {
                _subPixel = value;
                mAssistant.setSubPixel(value.ToString());
            }
        }
        #endregion

        #region #############  lastPyramidLevel  ################
        private int _lastPyramidLevel = 1;
        public int m_lastPyramidLevel
        {
            get { return _lastPyramidLevel; }
            set
            {
                _maxOverlap = value;
                mAssistant.setLastPyramLevel(value);
            }
        }
        #endregion
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
        #endregion

        //MatchingForm MatchForm; //fortest
        /// <summary>
        /// 是否实时播放
        /// </summary>
        private ManualResetEventSlim Player = new ManualResetEventSlim(false);
        /// <summary>
        /// 是否开启实时播放
        /// </summary>
        internal bool Run { get { return Player.IsSet; } }

        // Local iconic variables 
        public HObject ho_Image = null;

        // Local control variables 
        public HTuple hv_AcqHandle = null;

        /// <summary>
        /// 实时播放时 执行匹配的线程
        /// </summary>
        private Thread thd;

        public CCDShow()
        {
            InitializeComponent();
            m_configFile = Application.StartupPath + "\\CONFIG\\config.ini";            
            #region 初始化halcon模块
            mView = new HWndCtrl(this.hWindowControl_Player);

            createModelWindowMode = Color.RoyalBlue;
            trainModelWindowMode = Color.Chartreuse;

            roiController = new ROIController();
            mView.useROIController(roiController);

            roiController.setROISign(ROIController.MODE_ROI_POS);

            mView.NotifyIconObserver = new IconicDelegate(UpdateViewData);
            roiController.NotifyRCObserver = new IconicDelegate(UpdateViewData);

            mView.setViewState(HWndCtrl.MODE_VIEW_NONE);
            locked = true;
            parameterSet = new MatchingParam();
            Init(parameterSet);
            locked = false;

            mAssistant = new MatchingAssistant(parameterSet);
            mAssistant.NotifyIconObserver = new MatchingDelegate(UpdateMatching);
            //mAssistant.NotifyParamObserver = new AutoParamDelegate(UpdateButton);

            speedOptHandler = new MatchingOptSpeed(mAssistant, parameterSet);
            speedOptHandler.NotifyStatisticsObserver = new StatisticsDelegate(UpdateStatisticsData);

            inspectOptHandler = new MatchingOptStatistics(mAssistant, parameterSet);
            inspectOptHandler.NotifyStatisticsObserver = new StatisticsDelegate(UpdateStatisticsData);
            #endregion
        }

        private void CCDShow_Load(object sender, EventArgs e)
        {            
            //listView_Result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //tsb_loadImage.l
        }

        public void ReadINI()
        {
            this.Invoke(new Action(() =>
            {
                m_iniCCDMatchSection = this.CameraLocation.ToString() + "MATCH";
                string str = CommonFunc.Read(m_iniCCDMatchSection, "minScore", "0", m_configFile);
                //MinScoreUpDown.Value = Convert.ToInt32(str == "" ? "0" : str);

                str = CommonFunc.Read(m_iniCCDMatchSection, "numMatches", "1", m_configFile);
                //NumMatchesUpDown.Value = Convert.ToInt32(str == "" ? "1" : str);

                str = CommonFunc.Read(m_iniCCDMatchSection, "greediness", "0", m_configFile);
                //GreedinessUpDown.Value = Convert.ToInt32(str == "" ? "0" : str);

                str = CommonFunc.Read(m_iniCCDMatchSection, "maxOverlap", "0", m_configFile);
                //MaxOverlapUpDown.Value = Convert.ToInt32(str == "" ? "0" : str);

                str = CommonFunc.Read(m_iniCCDMatchSection, "subpixelBox", "least_squares", m_configFile);
                //SubPixelBox.SelectedIndex = SubPixelBox.Items.IndexOf(str == "" ? "least_squares" : str);

                str = CommonFunc.Read(m_iniCCDMatchSection, "lastPyramidLevel", "1", m_configFile);
                //LastPyrLevUpDown.Value = Convert.ToInt32(str == "" ? "1" : str);
            }));
        }
        
        #region --------------匹配参数设置--------------
        ///********************************************************************/
        ///*                       minScore最低得分                           */
        ///********************************************************************/
        //private void MinScoreUpDown_ValueChanged(object sender, System.EventArgs e)
        //{
        //    int val = (int)MinScoreUpDown.Value;
        //    m_MinScore = (double)val;
        //    CommonFunc.Write(m_iniCCDMatchSection, "minScore", val.ToString(), m_configFile);
        //}

        ///********************************************************************/
        ///*                       numMatches匹配个数                         */
        ///********************************************************************/
        //private void NumMatchesUpDown_ValueChanged(object sender, System.EventArgs e)
        //{
        //    int val = (int)NumMatchesUpDown.Value;
        //    m_MatchesNum = val;
        //    CommonFunc.Write(m_iniCCDMatchSection, "numMatches", val.ToString(), m_configFile);
        //}

        ///********************************************************************/
        ///*                       greediness贪婪系数                         */
        ///********************************************************************/
        //private void GreedinessUpDown_ValueChanged(object sender, System.EventArgs e)
        //{
        //    int val = (int)GreedinessUpDown.Value;
        //    m_Greediness = (double)val;
        //    CommonFunc.Write(m_iniCCDMatchSection, "greediness", val.ToString(), m_configFile);
        //}

        ///********************************************************************/
        ///*       maxOverlap最大重叠区域，我们基本上不会有，不用改动         */
        ///********************************************************************/
        //private void MaxOverlapUpDown_ValueChanged(object sender, System.EventArgs e)
        //{
        //    int val = (int)MaxOverlapUpDown.Value;
        //    m_maxOverlap = (double)val;
        //    CommonFunc.Write(m_iniCCDMatchSection, "maxOverlap", val.ToString(), m_configFile);
        //}

        ///********************************************************************/
        ///*     subpixelBox亚像素级别,越高越精确，时间越长，建议选高         */
        ///********************************************************************/
        //private void SubPixelBox_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    string strSubPixel = SubPixelBox.Text.ToString();
        //    m_subPixel = (subPixel)Enum.Parse(typeof(subPixel), strSubPixel);
        //    CommonFunc.Write(m_iniCCDMatchSection, "subpixelBox", strSubPixel, m_configFile);
        //}

        ///********************************************************************/
        ///*                            lastPyramidLevel                      */
        ///********************************************************************/
        //private void LastPyrLevUpDown_ValueChanged(object sender, System.EventArgs e)
        //{
        //    int val = (int)LastPyrLevUpDown.Value;
        //    m_lastPyramidLevel = val;
        //    CommonFunc.Write(m_iniCCDMatchSection, "lastPyramidLevel", val.ToString(), m_configFile);
        //}
        #endregion

        #region --------------CCD相机控制--------------
        public void SetCCDButtonEnable(bool bl)
        {
            this.Invoke(new Action(() => {
                this.tsb_oneShot.Enabled = this.tsb_continuousShot.Enabled = this.tsb_stop.Enabled = bl;
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

        private void tsb_init_Click(object sender, EventArgs e)
        {
           bool bl = InitCCD(this.CameraLocation.ToString());
           SetCCDButtonEnable(bl);
        }

        private void tsb_save_Click(object sender, EventArgs e)
        {
            SaveImage(CurrentImg);
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
        
        private void tsb_loadmodel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\";
            openFileDialog1.Filter = "模版文件 (*.shm)|*.shm";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_markFile = openFileDialog1.FileName;
                if (!LoadModel(m_markFile)) //手动导入
                { MessageBox.Show("模版导入失败"); }
            }
        }

        private void tsb_match_Click(object sender, EventArgs e)
        {
            if (this.m_ccdName.ToUpper().Contains("DOWN"))
            {
                m_MatchesNum = 1; //需要几个匹配结果
                m_Greediness = 75;  //贪婪系数
                m_maxOverlap = 50;  //最大重叠区域，我们基本上不会有，不用改动
                m_subPixel = subPixel.least_squares;  //亚像素级别,越高越精确，时间越长，建议选高
                m_MinScore = 45;  //最小SCORE
            }
            //m_markFile = Application.StartupPath + "\\MCH\\UP.shm";
            //LoadModel(m_markFile); //初始化加载
            //string resultFile = Application.StartupPath + "\\MCH\\image\\UPCCD2.jpg";
            //loadImage(resultFile);
            MatchMode();
            PaintMatchResult("blue");
            MatchingResult result = MatchResult; //取结果
            RefreshListView(result); //显示匹配结果 fortest
        }

        #endregion

        #region --------------Halcon匹配模块----------------
        /// <summary> 
        /// Updates all model parameters to the initial settings of the GUI 
        /// components.  
        /// </summary>
        /// <param name="parSet">holds all necessary parameter values</param>
        private void Init(MatchingParam parSet)
        {
            parSet.mMinScore = _MinScore;
            parSet.mNumMatches = _MatchesNum;
            parSet.mGreediness = _Greediness;
            parSet.mMaxOverlap = _maxOverlap;
            parSet.mSubpixel = _subPixel.ToString();
            //parSet.mSubpixel		= (string)SubPixelBox.Items[SubPixelBox.SelectedIndex];  //debugging test ""least_squares""
            parSet.mLastPyramidLevel = _lastPyramidLevel;

            parSet.mRecogSpeedMode = MatchingParam.RECOGM_MANUALSELECT;

            string imPathValue = Environment.GetEnvironmentVariable(
                                 "HALCONIMAGES");
            //openFileDialog1.InitialDirectory = imPathValue;

            //openFileDialog2.InitialDirectory = imPathValue;

            //openFileDialog3.InitialDirectory =
            //Environment.GetFolderPath(
            //System.Environment.SpecialFolder.Personal);

            //saveFileDialog1.InitialDirectory =
            //Environment.GetFolderPath(
            //System.Environment.SpecialFolder.Personal);
        }

        /// <summary>
        /// This method is invoked if changes occur in the HWndCtrl instance
        /// or the ROIController. In either case, the HALCON 
        /// window needs to be updated/repainted.
        /// </summary>
        public void UpdateViewData(int val)
        {

            switch (val)
            {
                case ROIController.EVENT_CHANGED_ROI_SIGN:
                case ROIController.EVENT_DELETED_ACTROI:
                case ROIController.EVENT_DELETED_ALL_ROIS:
                case ROIController.EVENT_UPDATE_ROI:
                    ModelContour = null;
                    DetectionContour = null;
                    bool genROI = roiController.defineModelROI();
                    ModelRegion = roiController.getModelRegion();
                    mAssistant.setModelROI(ModelRegion);
                    CreateModelGraphics();
                    if (!genROI)
                        mView.repaint();

                    break;
                case HWndCtrl.ERR_READING_IMG:
                    MessageBox.Show("Problem occured while reading file! \n" + mView.exceptionText,
                        "Matching assistant",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method is invoked for any changes in the 
        /// MatchingAssistant, concerning the model creation and
        /// the model finding. Also changes in the display mode 
        /// (e.g., pyramid level) are mapped here.
        /// </summary>
        public void UpdateMatching(int val)
        {
            bool paint = false;
            switch (val)
            {
                case MatchingAssistant.UPDATE_XLD:
                    ModelContour = mAssistant.getModelContour();
                    CreateModelGraphics();
                    paint = true;
                    break;
                case MatchingAssistant.UPDATE_DISPLEVEL:
                    CurrentImg = mAssistant.getDispImage();
                    ModelContour = mAssistant.getModelContour();
                    CreateModelGraphics();
                    paint = true;
                    break;
                case MatchingAssistant.UPDATE_DETECTION_RESULT:
                    //DetectionContour = mAssistant.getDetectionResults();
                    //FindModelGraphics();
                    //paint = true;
                    break;
                case MatchingAssistant.UPDATE_TESTVIEW:
                    CurrentImg = mAssistant.getCurrTestImage();
                    FindModelGraphics("green");
                    break;
                case MatchingAssistant.ERR_WRITE_SHAPEMODEL:
                    MessageBox.Show("Problem occured while writing into file \n" + mAssistant.exceptionText,
                        "Matching Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                case MatchingAssistant.ERR_READ_SHAPEMODEL:
                    MessageBox.Show("Problem occured while reading from file \n" + mAssistant.exceptionText,
                        "Matching Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                case MatchingAssistant.ERR_NO_MODEL_DEFINED:
                    MessageBox.Show("Please define a Model!",
                        "Matching Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    paint = true;
                    break;
                case MatchingAssistant.ERR_NO_IMAGE:
                    MessageBox.Show("Please load an image",
                        "Matching Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;
                case MatchingAssistant.ERR_NO_TESTIMAGE:
                    MessageBox.Show("Please load a testimage",
                        "Matching Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    paint = true;
                    break;
                case MatchingAssistant.ERR_NO_VALID_FILE:
                    MessageBox.Show("Selected file is not a HALCON ShapeModel file .shm",
                        "Matching Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;
                case MatchingAssistant.ERR_READING_IMG:
                    UpdateViewData(HWndCtrl.ERR_READING_IMG);
                    break;
                default:
                    break;
            }
            if (paint)
                mView.repaint();
        }

        /// <summary>
        /// This method is invoked when the inspection tab or the 
        /// recognition tab are triggered to compute the optimized values
        /// and to forward the results to the display.
        /// </summary>
        public void UpdateStatisticsData(int mode)
        {
            switch (mode)
            {
                case MatchingOpt.UPDATE_TEST_ERR:
                    MessageBox.Show("Optimization failed! \n Please check if your model is well defined\n(e.g contains some model contours)!?",
                        "Shapebases Matching Assistant",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    break;
                case MatchingOpt.UPDATE_RECOG_ERR:
                    //labelOptStatus.Text = "Optimization failed";
                    MessageBox.Show("There was no appropriate set of parameters to match - \n" +
                        "Check if your model is well defined and the parameters\nfor model creation are set appropriately!",
                        "Shapebases Matching Assistant",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    break;
                case MatchingAssistant.ERR_NO_TESTIMAGE:
                    UpdateMatching(MatchingAssistant.ERR_NO_TESTIMAGE);
                    break;
                case MatchingOpt.RUN_SUCCESSFUL:
                    //UpdateButton(MatchingParam.BUTTON_GREEDINESS);
                    //UpdateButton(MatchingParam.BUTTON_MINSCORE);
                    break;
                case MatchingOpt.RUN_FAILED:
                    //setMinScore((int)MinScoreUpDown.Value);
                    //setGreediness((int)GreedinessUpDown.Value);
                    break;
                default:
                    break;
            }
        }

        /********************************************************************/
        /********************************************************************/
        private void CreateModelGraphics()
        {
            mView.clearList();
            mView.changeGraphicSettings(GraphicsContext.GC_LINESTYLE, new HTuple());
            mView.addIconicVar(CurrentImg);
            if (ModelRegion != null)
            {
                mView.changeGraphicSettings(GraphicsContext.GC_COLOR, "blue");
                mView.changeGraphicSettings(GraphicsContext.GC_LINEWIDTH, 3);
                mView.addIconicVar(ModelRegion);
            }
            if (ModelContour != null)
            {
                mView.changeGraphicSettings(GraphicsContext.GC_COLOR, "red");
                mView.changeGraphicSettings(GraphicsContext.GC_LINEWIDTH, 1);
                mView.addIconicVar(ModelContour);
            }
        }
        
        /********************************************************************/
        /********************************************************************/
        public void FindModelGraphics(string color)
        {            
            try
            {
                mView.clearList();
                mView.changeGraphicSettings(GraphicsContext.GC_LINESTYLE, new HTuple());
                mView.addIconicVar(CurrentImg);
                if (DetectionContour != null)
                {
                    //mView.changeGraphicSettings(GraphicsContext.GC_COLOR, "green");
                    mView.changeGraphicSettings(GraphicsContext.GC_COLOR, color);
                    mView.changeGraphicSettings(GraphicsContext.GC_LINEWIDTH, 3);
                    mView.addIconicVar(DetectionContour);
                }
            }
            catch { }
        }
        
        /// <summary>
        /// Repaints the HALCON window 'window'
        /// </summary>
        public void repaint(HalconDotNet.HWindow window, HObject obj)
        {
            //int count = HObjList.Count;
            //HObjectEntry entry;

            HSystem.SetSystem("flush_graphic", "false");
            window.ClearWindow();
            //mGC.stateOfSettings.Clear();

            //for (int i=0; i < count; i++)
            //{
            //    entry = ((HObjectEntry)HObjList[i]);
            //    mGC.applyContext(window, entry.gContext);
            window.DispObj(obj);
            //}

            //addInfoDelegate();

            //if (roiManager != null && (dispROI == MODE_INCLUDE_ROI))
            //    roiManager.paintData(window);

            HSystem.SetSystem("flush_graphic", "true");

            window.SetColor("black");
            window.DispLine(-100.0, -100.0, -101.0, -101.0);
        }

        //显示匹配结果，画在界面上
        public void PaintMatchResult(string color)
        {
            DetectionContour = mAssistant.getDetectionResults();
            FindModelGraphics(color);
            mView.repaint();
        }
        #endregion

        #region --------------外部调用--------------
        public void SetStatusText(string str, bool isError = false)
        {
            if (m_eveInstagram_StatusText != null)
                m_eveInstagram_StatusText(str, isError);
        }

        PointF m_markPoint = new PointF(); //Mark点位置
        PointF[] m_listxizuiPoint = new PointF[4]; //4个吸嘴的位置
        public void SetParm(PointF markPoint, PointF[] listxizuiPoint)
        {
            this.m_markPoint = markPoint;
            this.m_listxizuiPoint = listxizuiPoint;
        }

        public string m_ccdName = "";
        public bool m_bInitOK = false;
        /// <summary>
        /// 初始化CCD
        /// </summary>
        /// <param name="ccdname">CCD名称</param>
        /// <returns></returns>
        public bool InitCCD(string ccdname)
        {
            try
            {
                m_ccdName = ccdname;
                SetStatusText(ccdname + "初始化中。。。");
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);

                //Image Acquisition 01: Code generated by Image Acquisition 01

                HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "progressive",
                    -1, "default", -1, "false", "default", ccdname, 0, -1, out hv_AcqHandle);
                //HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
                SetStatusText(ccdname + "初始化成功");
                m_bInitOK = true;
                return true;
            }
            catch 
            {
                SetStatusText(ccdname + "初始化失败", true);
                m_bInitOK = false;
                return false;
            }
        }

        /// <summary>
        /// 关闭CCD
        /// </summary>
        public void CloseCCD()
        {
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
        }

        /// <summary>
        /// 当前图片
        /// </summary>
        /// <returns></returns>
        public HImage GetCurrentImage()
        {
            HImage _CurrentImg = CurrentImg.Clone();
            return _CurrentImg;
        }

        /// <summary>
        /// CCD拍照处理，镜像旋转
        /// </summary>
        /// <param name="himage"></param>
        /// <returns></returns>
        private HImage CCDImageRote(HImage himage)        
        {
            HImage retImage = null;
            HImage _CurrentImg = new HImage(himage);
            if (this.m_ccdName.ToUpper().Contains("UP")) //上CCD需旋转180度
            {
                retImage = _CurrentImg.RotateImage(180.0, "constant");
            }
            if (this.m_ccdName.ToUpper().Contains("DOWN")) //下CCD需左右镜像
            {
                retImage = _CurrentImg.MirrorImage("row");
            }
            return retImage;
        }

        /// <summary>
        /// 拍一张
        /// </summary>
        public void PlayerOne()
        {
            try
            {
                if (!m_bInitOK) 
                {
                    SetStatusText(m_ccdName + "未初始化", true);
                    return;
                }
                SetStatusText(m_ccdName + "开始拍照");
                Player.Reset();
                Thread.Sleep(30);
                m_bCaptureOK = false;
                lock (hv_AcqHandle)
                {
                    HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
                    ho_Image.Dispose();
                    if (CurrentImg != null)
                        CurrentImg.Dispose();
                    HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                    CurrentImg = new HImage(ho_Image);
                    mAssistant.SetTestImage(CurrentImg);
                    CreateModelGraphics();
                    repaint(this.hWindowControl_Player.HalconWindow, CurrentImg);
                };
                SetStatusText(m_ccdName + "拍照成功");
                m_bCaptureOK = true;
            }
            catch
            {
                m_bCaptureOK = false;
                SetStatusText(m_ccdName + "拍照失败");
            }
        }
        
        /// <summary>
        /// 开启播放
        /// </summary>
        public void PlayerRun()
        {
            if (!m_bInitOK)
            {
                SetStatusText(m_ccdName + "未初始化", true);
                return;
            }
            if (thd == null || (thd != null && !thd.IsAlive))
            {
                thd = new Thread(HDevelopExport);
                thd.IsBackground = true;
                thd.Name = this.m_CameraLocation.ToString() + "匹配线程";
                thd.Start();
            }
            Player.Set();
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
                    SetStatusText(m_ccdName + "连续生成图像中。。。");
                    ho_Image.Dispose();
                    HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                    ////Image Acquisition 01: Do something
                    if(CurrentImg != null)
                        CurrentImg.Dispose();
                    CurrentImg = new HImage(ho_Image);
                    mAssistant.SetTestImage(CurrentImg);
                    CreateModelGraphics();
                    repaint(this.hWindowControl_Player.HalconWindow, CurrentImg);
                    Thread.Sleep(30);
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
        /// 停止播放
        /// </summary>
        public void PlayerStop()
        {
            if (!m_bInitOK)
            {
                SetStatusText(m_ccdName + "未初始化", true);
                return;
            }
            Player.Reset();
            SetStatusText(m_ccdName + "相机已STOP");
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        public void SaveImage(HImage image)
        {
            try
            {
                string str = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); 
                    //Application.StartupPath + "\\LOG\\SaveImage\\";
                //if (!Directory.Exists(str))
                //    Directory.CreateDirectory(str);
                //str += "test"; //+文件名
                HOperatorSet.WriteImage(image, "jpeg", 0, str + "\\" + m_ccdName);
                SetStatusText("保存图片成功");
            }
            catch { SetStatusText("保存图片失败"); }
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="strPicPath"></param>
        public void loadImage(string strPicPath)
        {
            Player.Reset();
            HImage image = new HImage(strPicPath);
            mAssistant.SetTestImage(image);
            ho_Image = CurrentImg = mAssistant.getCurrTestImage();
            CreateModelGraphics();
            repaint(this.hWindowControl_Player.HalconWindow, CurrentImg);
            image.Dispose();
        }

        /// <summary>
        /// 加载匹配模版
        /// </summary>
        /// <param name="strModelPath"></param>
        /// <returns></returns>
        public bool LoadModel(string strModelPath)
        {            
            try
            {
                if (!File.Exists(strModelPath))
                {
                    SetStatusText("未找到匹配模版，请自己创建模版");
                    return false;
                }

                roiController.reset();
                DetectionContour = null;
                mAssistant.reset();
                if (mAssistant.loadShapeModel(strModelPath))
                {
                    //解析后轮廓数据
                    ModelContour = mAssistant.getLoadedModelContour();
                    //CreateModelGraphics();
                    mView.repaint();
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 匹配模版
        /// </summary>
        /// <returns></returns>
        public bool MatchMode()
        {
            try
            {
                return mAssistant.applyFindModel(); //匹配                
            }
            catch
            {
                SetStatusText("匹配失败");
                return false;
            }
        }
        #endregion
        
        //刷新DtatGridView
        public void RefreshListView(MatchingResult Results)
        {
            this.Invoke(new Action(() => {
                //dataGridView_Result.Rows.Clear();
                //for (int i = 0; i < Results.count; i++)
                //{
                //    string score = (((double)Results.mScore[i]) * 100).ToString("g3");
                //    string x = ((double)Results.mCol[i]).ToString("#0.000");
                //    string y = ((double)Results.mRow[i]).ToString("#0.000");
                //    double angle = (double)Results.mAngle[i] * (180 / Math.PI); //弧度转角度
                //    string strangle = angle.ToString("#0.000000");
                //    int nIndex = dataGridView_Result.Rows.Add(score, x, y, strangle);
                //}
            }));
        }


        //DtatGridView显示行号
        private void dataGridView_Result_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
            //                                                                 e.RowBounds.Location.Y,
            //                                                                 dataGridView_Result.RowHeadersWidth - 4,
            //                                                                 e.RowBounds.Height);
            //TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            //                      dataGridView_Result.RowHeadersDefaultCellStyle.Font,
            //                      rectangle,
            //                      dataGridView_Result.RowHeadersDefaultCellStyle.ForeColor,
            //                      TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        private void dataGridView_resultCalc_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
            //                                                                 e.RowBounds.Location.Y,
            //                                                                 dataGridView_resultCalc.RowHeadersWidth - 4,
            //                                                                 e.RowBounds.Height);
            //TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            //                      dataGridView_resultCalc.RowHeadersDefaultCellStyle.Font,
            //                      rectangle,
            //                      dataGridView_resultCalc.RowHeadersDefaultCellStyle.ForeColor,
            //                      TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        

        


    }
}
