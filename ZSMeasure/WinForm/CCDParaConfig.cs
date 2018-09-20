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
    public partial class CCDParaConfig : Form
    {
        private string m_avtName = "";
        private string m_ccdName = "";

        #region 属性
        public string AVTName
        {
            get
            {
                m_avtName = txtbox_AVTDeviceNum.Text.ToString().Trim();
                return m_avtName;
            }
            set
            {
                m_avtName = value;
                txtbox_AVTDeviceNum.Text = m_avtName;
            }
        }
        public int MarkMinArea
        {
            get { return (int)numericUpDown_markMinArea.Value; }
            set { numericUpDown_markMinArea.Value = value; }
        }
        public int MarkMaxArea
        {
            get { return (int)numericUpDown_markMaxArea.Value; }
            set { numericUpDown_markMaxArea.Value = value; }
        }
        public int PointMinArea
        {
            get { return (int)numericUpDown_pointMinArea.Value; }
            set { numericUpDown_pointMinArea.Value = value; }
        }
        public int PointMaxArea
        {
            get { return (int)numericUpDown_pointMaxArea.Value; }
            set { numericUpDown_pointMaxArea.Value = value; }
        }
        public int ProductMinArea
        {
            get { return (int)numericUpDown_productMin.Value; }
            set { numericUpDown_productMin.Value = value; }
        }
        public int ProductMaxArea
        {
            get { return (int)numericUpDown_productMax.Value; }
            set { numericUpDown_productMax.Value = value; }
        }
        public int m_ExposureProduct
        {
            get { return (int)numericUpDown_exposureP.Value; }
            set { numericUpDown_exposureP.Value = value; }
        }
        public int m_ExposureModel
        {
            get { return (int)numericUpDown_exposureM.Value; }
            set { numericUpDown_exposureM.Value = value; }
        }
        public double m_UmPixel
        {
            get { return (double)numericUpDown_umPixel.Value; }
            set { numericUpDown_umPixel.Value = Convert.ToDecimal(value); }
        }
        public double m_roiX1
        {
            get { return (double)numericUpDown_roiX1.Value; }
            set
            {
                try
                {
                    numericUpDown_roiX1.Value = Convert.ToDecimal(value);
                }
                catch { numericUpDown_roiX1.Value = 100; }
            }
        }
        public double m_roiY1
        {
            get { return (double)numericUpDown_roiY1.Value; }
            set
            {
                try
                {
                    numericUpDown_roiY1.Value = Convert.ToDecimal(value);
                }
                catch { numericUpDown_roiY1.Value = 100; }
            }
        }
        public double m_roiX2
        {
            get { return (double)numericUpDown_roiX2.Value; }
            set
            {
                try
                {
                    numericUpDown_roiX2.Value = Convert.ToDecimal(value);
                }
                catch { numericUpDown_roiX2.Value = 500; }
            }
        }
        public double m_roiY2
        {
            get { return (double)numericUpDown_roiY2.Value; }
            set
            {
                try
                {
                    numericUpDown_roiY2.Value = Convert.ToDecimal(value);
                }
                catch { numericUpDown_roiY2.Value = 500; }
            }
        }
        #endregion

        private myCCDHelp myccdhelp;
        public CCDParaConfig(myCCDHelp ccdhelp)
        {
            InitializeComponent();
            myccdhelp = ccdhelp;
        }

        private void ParaConfig_Load(object sender, EventArgs e)
        {
            m_ccdName = myccdhelp.CCDName;
            this.Text = "参数配置-" + m_ccdName;
            AVTName = myccdhelp.AVTName;
            MarkMinArea = myccdhelp.MarkArea1[0];
            MarkMaxArea = myccdhelp.MarkArea1[1];
            PointMinArea = myccdhelp.PointArea1[0];
            PointMaxArea = myccdhelp.PointArea1[1];
            ProductMinArea = myccdhelp.AreaProduct1[0];
            ProductMaxArea = myccdhelp.AreaProduct1[1];
            m_ExposureProduct = myccdhelp.m_ExposureProduct;
            m_ExposureModel = myccdhelp.m_ExposureModel;
            m_UmPixel = myccdhelp.m_UmPixel;
            m_roiX1 = myccdhelp.point1.X;
            m_roiY1 = myccdhelp.point1.Y;
            m_roiX2 = myccdhelp.point2.X;
            m_roiY2 = myccdhelp.point2.Y;
            groupBox_avt.Visible = true;
            if (GlobalVar.gl_bAdmin != GlobalVar.AdminMode.ZhengZhaolei)
            {
                groupBox_avt.Visible = false;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }




    }
}
