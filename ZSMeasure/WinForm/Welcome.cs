using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using HalconDotNet;

namespace ZSMeasure
{
    public partial class Welcome : Form
    {
        MainForm mainform = null;
        List<Panel> m_panelList = new List<Panel>();
        Thread thread_countadd = null;
        public Welcome()
        {
            //DirectShowLib.DsDevice[] ds = CamControl.GetDeviceList();
            //检查更新
            //UpdateClass update = new UpdateClass();
            //update.GetVersion(); //fortest

            //EOSCapture cap = new EOSCapture();
            InitializeComponent();
            //Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
            
            m_panelList.Add(this.panel1);
            m_panelList.Add(this.panel2);
            m_panelList.Add(this.panel3);
            m_panelList.Add(this.panel4);
            m_panelList.Add(this.panel5);
            m_panelList.Add(this.panel6);
            m_panelList.Add(this.panel7);
            m_panelList.Add(this.panel8);
            m_panelList.Add(this.panel9);
            m_panelList.Add(this.panel10);
            m_panelList.Add(this.panel11);
            m_panelList.Add(this.panel12);
            m_panelList.Add(this.panel13);
            m_panelList.Add(this.panel14);
            m_panelList.Add(this.panel15);

            panel_init.Visible = true;
            thread_countadd = new Thread(thread_addcount);
            thread_countadd.Start();
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //OpeneVision初始化
                try
                {
                    //MatrixDecode m_decode = new MatrixDecode();
                    //EMatrixCodeReader EMatrixCodeReader1 = new EMatrixCodeReader(); //条码解析初始化
                    //EMatcher EMatch1 = new EMatcher(); //形状匹配初始化
                    //EPatternFinder EPatternFinder1 = new EPatternFinder(); //形状查找初始化
                }
                catch(Exception ex)
                {
                    MessageBox.Show("OpeneVision初始化失败：" + ex.Message);
                }
                #region //HALCON初始化
                
                try
                {
                    HObject ho_Image, ho_SymbolXLDs;
                    HOperatorSet.GenEmptyObj(out ho_Image);
                    HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
                    HTuple hv_DataCodeHandle, hv_ResultHandles, hv_DecodedDataStrings;
                    HTuple train = "train";
                    HTuple all = "all";
                    HOperatorSet.CreateDataCode2dModel("Data Matrix ECC 200", new HTuple(), new HTuple(), out hv_DataCodeHandle);
                    HOperatorSet.FindDataCode2d(ho_Image, out ho_SymbolXLDs, hv_DataCodeHandle, train, all, out hv_ResultHandles, out hv_DecodedDataStrings);
                    HalconDotNet.HWindowControl hWindowControl_Player = new HWindowControl();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("HALCON 初始化失败：" + ex.Message);
                }
                
                #endregion
                this.Invoke((EventHandler)delegate
                {
                    mainform = new MainForm();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.Visible = false;
                thread_countadd.Abort();
                this.Invoke((EventHandler)delegate
                {
                    if (mainform == null)
                        mainform = new MainForm();
                    mainform.ShowDialog();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void thread_addcount()
        {
            int nFirst = 0, nSnd = 1, nThird = 2;
            bool m_bOrient = true;
            for (; ; )
            {
                if (m_bOrient)
                {
                    nFirst++;
                    nSnd++;
                    nThird++;
                }
                else
                {
                    nFirst--;
                    nSnd--;
                    nThird--;
                }
                if ((nFirst >= m_panelList.Count)
                    || (nSnd >= m_panelList.Count)
                    || (nThird >= m_panelList.Count)
                    || (nFirst == 0)
                    || (nFirst == 0)
                    || (nFirst == 0))
                {
                    m_bOrient = !m_bOrient;
                }
                for (int i = 0; i < m_panelList.Count; i++)
                {
                    if (nFirst == i || (nSnd == i) || (nThird == i))
                    {
                        (m_panelList[i] as Panel).BackColor = Color.Lime;
                    }
                    else
                    {
                        (m_panelList[i] as Panel).BackColor = Color.White;
                    }
                }
                Thread.Sleep(70);
            }
        }

    }
}