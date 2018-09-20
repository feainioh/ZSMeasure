namespace ZSMeasure
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_adminlogin = new System.Windows.Forms.ToolStripMenuItem();
            this.校准片量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ModeOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ModeClose = new System.Windows.Forms.ToolStripMenuItem();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.电机配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_changeForm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_changeOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_changeClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_camertType = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_basler = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_avt = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算方法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_calc1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_calc2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_calc3 = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_spconn = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_testType = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_scan = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_cameraType = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_space = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_calcfunc = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_xld = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_currentPoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_var = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_sp = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_main = new System.Windows.Forms.Panel();
            this.myCCDHelp4 = new ZSMeasure.myCCDHelp();
            this.myCCDHelp1 = new ZSMeasure.myCCDHelp();
            this.myCCDHelp3 = new ZSMeasure.myCCDHelp();
            this.myCCDHelp2 = new ZSMeasure.myCCDHelp();
            this.serialPort_scan = new System.IO.Ports.SerialPort(this.components);
            this.groupBoxEx_right = new HalconCCD.GroupBoxEx();
            this.panel_result = new System.Windows.Forms.Panel();
            this.btn_openResult = new System.Windows.Forms.Button();
            this.btn_allCollapsed = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewEx_result = new ZSMeasure.DataGridViewEx();
            this.Column1 = new ZSMeasure.DataGridViewGroupColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_allExpand = new System.Windows.Forms.Button();
            this.panel_barcode = new System.Windows.Forms.Panel();
            this.lbl_Standard = new System.Windows.Forms.Label();
            this.lbl_productModel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbox_barcode = new System.Windows.Forms.TextBox();
            this.groupBoxEx_left = new HalconCCD.GroupBoxEx();
            this.checkBox_Fortest = new System.Windows.Forms.CheckBox();
            this.panel_pinmuSetting = new System.Windows.Forms.Panel();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_accept = new System.Windows.Forms.Button();
            this.textBox_OperaterID = new System.Windows.Forms.TextBox();
            this.textBox_pinmu = new System.Windows.Forms.TextBox();
            this.label_OperaterID = new System.Windows.Forms.Label();
            this.label_input = new System.Windows.Forms.Label();
            this.textBox_LotNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_workTime = new System.Windows.Forms.GroupBox();
            this.radioButton_nightShift = new System.Windows.Forms.RadioButton();
            this.radioButton_dayshift = new System.Windows.Forms.RadioButton();
            this.label_shift = new System.Windows.Forms.Label();
            this.either1 = new ZSMeasure.Either();
            this.numericUpDown_testcount = new System.Windows.Forms.NumericUpDown();
            this.btn_allowWork = new System.Windows.Forms.Button();
            this.lbl_testcount = new System.Windows.Forms.Label();
            this.btn_localtest = new System.Windows.Forms.Button();
            this.btn_9point = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.groupBoxEx_right.SuspendLayout();
            this.panel_result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx_result)).BeginInit();
            this.panel_barcode.SuspendLayout();
            this.groupBoxEx_left.SuspendLayout();
            this.panel_pinmuSetting.SuspendLayout();
            this.groupBox_workTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_testcount)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.配置ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1261, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_adminlogin,
            this.校准片量测ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // tsmi_adminlogin
            // 
            this.tsmi_adminlogin.Name = "tsmi_adminlogin";
            this.tsmi_adminlogin.Size = new System.Drawing.Size(136, 22);
            this.tsmi_adminlogin.Text = "管理登录";
            this.tsmi_adminlogin.Click += new System.EventHandler(this.管理登录ToolStripMenuItem_Click);
            // 
            // 校准片量测ToolStripMenuItem
            // 
            this.校准片量测ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ModeOpen,
            this.tsmi_ModeClose});
            this.校准片量测ToolStripMenuItem.Name = "校准片量测ToolStripMenuItem";
            this.校准片量测ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.校准片量测ToolStripMenuItem.Text = "校准片量测";
            // 
            // tsmi_ModeOpen
            // 
            this.tsmi_ModeOpen.Name = "tsmi_ModeOpen";
            this.tsmi_ModeOpen.Size = new System.Drawing.Size(100, 22);
            this.tsmi_ModeOpen.Text = "开启";
            this.tsmi_ModeOpen.Click += new System.EventHandler(this.tsmi_ModeOpen_Click);
            // 
            // tsmi_ModeClose
            // 
            this.tsmi_ModeClose.Checked = true;
            this.tsmi_ModeClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi_ModeClose.Name = "tsmi_ModeClose";
            this.tsmi_ModeClose.Size = new System.Drawing.Size(100, 22);
            this.tsmi_ModeClose.Text = "关闭";
            this.tsmi_ModeClose.Click += new System.EventHandler(this.tsmi_ModeClose_Click);
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.电机配置ToolStripMenuItem,
            this.tsmi_changeForm,
            this.tsmi_camertType});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // 电机配置ToolStripMenuItem
            // 
            this.电机配置ToolStripMenuItem.Name = "电机配置ToolStripMenuItem";
            this.电机配置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.电机配置ToolStripMenuItem.Text = "参数配置";
            this.电机配置ToolStripMenuItem.Click += new System.EventHandler(this.电机配置ToolStripMenuItem_Click);
            // 
            // tsmi_changeForm
            // 
            this.tsmi_changeForm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_changeOpen,
            this.tsmi_changeClose});
            this.tsmi_changeForm.Name = "tsmi_changeForm";
            this.tsmi_changeForm.Size = new System.Drawing.Size(148, 22);
            this.tsmi_changeForm.Text = "窗体位置交换";
            // 
            // tsmi_changeOpen
            // 
            this.tsmi_changeOpen.Name = "tsmi_changeOpen";
            this.tsmi_changeOpen.Size = new System.Drawing.Size(88, 22);
            this.tsmi_changeOpen.Text = "开";
            this.tsmi_changeOpen.Click += new System.EventHandler(this.开ToolStripMenuItem_Click);
            // 
            // tsmi_changeClose
            // 
            this.tsmi_changeClose.Checked = true;
            this.tsmi_changeClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi_changeClose.Name = "tsmi_changeClose";
            this.tsmi_changeClose.Size = new System.Drawing.Size(88, 22);
            this.tsmi_changeClose.Text = "关";
            this.tsmi_changeClose.Click += new System.EventHandler(this.关ToolStripMenuItem_Click);
            // 
            // tsmi_camertType
            // 
            this.tsmi_camertType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_basler,
            this.tsmi_avt});
            this.tsmi_camertType.Name = "tsmi_camertType";
            this.tsmi_camertType.Size = new System.Drawing.Size(148, 22);
            this.tsmi_camertType.Text = "相机类型";
            // 
            // tsmi_basler
            // 
            this.tsmi_basler.Checked = true;
            this.tsmi_basler.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi_basler.Name = "tsmi_basler";
            this.tsmi_basler.Size = new System.Drawing.Size(112, 22);
            this.tsmi_basler.Text = "Basler";
            this.tsmi_basler.Click += new System.EventHandler(this.tsmi_basler_Click);
            // 
            // tsmi_avt
            // 
            this.tsmi_avt.Name = "tsmi_avt";
            this.tsmi_avt.Size = new System.Drawing.Size(112, 22);
            this.tsmi_avt.Text = "AVT";
            this.tsmi_avt.Click += new System.EventHandler(this.tsmi_avt_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tESTToolStripMenuItem,
            this.计算方法ToolStripMenuItem});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // tESTToolStripMenuItem
            // 
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.tESTToolStripMenuItem.Text = "TEST";
            this.tESTToolStripMenuItem.Visible = false;
            this.tESTToolStripMenuItem.Click += new System.EventHandler(this.tESTToolStripMenuItem_Click);
            // 
            // 计算方法ToolStripMenuItem
            // 
            this.计算方法ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_calc1,
            this.tsmi_calc2,
            this.tsmi_calc3});
            this.计算方法ToolStripMenuItem.Name = "计算方法ToolStripMenuItem";
            this.计算方法ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.计算方法ToolStripMenuItem.Text = "计算方法";
            this.计算方法ToolStripMenuItem.Visible = false;
            // 
            // tsmi_calc1
            // 
            this.tsmi_calc1.Name = "tsmi_calc1";
            this.tsmi_calc1.Size = new System.Drawing.Size(143, 22);
            this.tsmi_calc1.Text = "平均角度";
            this.tsmi_calc1.Click += new System.EventHandler(this.tsmi_calc1_Click);
            // 
            // tsmi_calc2
            // 
            this.tsmi_calc2.Name = "tsmi_calc2";
            this.tsmi_calc2.Size = new System.Drawing.Size(143, 22);
            this.tsmi_calc2.Text = "14坐标系";
            this.tsmi_calc2.Click += new System.EventHandler(this.tsmi_calc2_Click);
            // 
            // tsmi_calc3
            // 
            this.tsmi_calc3.Checked = true;
            this.tsmi_calc3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi_calc3.Name = "tsmi_calc3";
            this.tsmi_calc3.Size = new System.Drawing.Size(143, 22);
            this.tsmi_calc3.Text = "14,23坐标系";
            this.tsmi_calc3.Click += new System.EventHandler(this.tsmi_calc3_Click);
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.BackColor = System.Drawing.Color.Black;
            this.richTextBox_log.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox_log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox_log.Location = new System.Drawing.Point(0, 625);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.Size = new System.Drawing.Size(1261, 99);
            this.richTextBox_log.TabIndex = 2;
            this.richTextBox_log.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_spconn,
            this.tssl_testType,
            this.tssl_scan,
            this.tssl_cameraType,
            this.tssl_space,
            this.tssl_calcfunc,
            this.tssl_xld,
            this.tssl_currentPoint,
            this.tssl_var});
            this.statusStrip1.Location = new System.Drawing.Point(0, 724);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1261, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_spconn
            // 
            this.tssl_spconn.BackColor = System.Drawing.Color.OrangeRed;
            this.tssl_spconn.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssl_spconn.Name = "tssl_spconn";
            this.tssl_spconn.Size = new System.Drawing.Size(72, 21);
            this.tssl_spconn.Text = "串口未连接";
            // 
            // tssl_testType
            // 
            this.tssl_testType.BackColor = System.Drawing.Color.DarkGray;
            this.tssl_testType.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssl_testType.Name = "tssl_testType";
            this.tssl_testType.Size = new System.Drawing.Size(72, 21);
            this.tssl_testType.Text = "制品量测中";
            // 
            // tssl_scan
            // 
            this.tssl_scan.BackColor = System.Drawing.Color.DarkGray;
            this.tssl_scan.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssl_scan.Name = "tssl_scan";
            this.tssl_scan.Size = new System.Drawing.Size(72, 21);
            this.tssl_scan.Text = "条码扫描：";
            // 
            // tssl_cameraType
            // 
            this.tssl_cameraType.AutoSize = false;
            this.tssl_cameraType.BackColor = System.Drawing.Color.DarkGray;
            this.tssl_cameraType.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssl_cameraType.Name = "tssl_cameraType";
            this.tssl_cameraType.Size = new System.Drawing.Size(80, 21);
            this.tssl_cameraType.Text = "相机类型";
            // 
            // tssl_space
            // 
            this.tssl_space.BackColor = System.Drawing.Color.Silver;
            this.tssl_space.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tssl_space.Name = "tssl_space";
            this.tssl_space.Size = new System.Drawing.Size(538, 21);
            this.tssl_space.Spring = true;
            // 
            // tssl_calcfunc
            // 
            this.tssl_calcfunc.AutoSize = false;
            this.tssl_calcfunc.BackColor = System.Drawing.Color.Silver;
            this.tssl_calcfunc.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssl_calcfunc.Name = "tssl_calcfunc";
            this.tssl_calcfunc.Size = new System.Drawing.Size(100, 21);
            // 
            // tssl_xld
            // 
            this.tssl_xld.AutoSize = false;
            this.tssl_xld.BackColor = System.Drawing.Color.Silver;
            this.tssl_xld.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssl_xld.Name = "tssl_xld";
            this.tssl_xld.Size = new System.Drawing.Size(100, 21);
            // 
            // tssl_currentPoint
            // 
            this.tssl_currentPoint.AutoSize = false;
            this.tssl_currentPoint.BackColor = System.Drawing.Color.DarkGray;
            this.tssl_currentPoint.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tssl_currentPoint.Name = "tssl_currentPoint";
            this.tssl_currentPoint.Size = new System.Drawing.Size(140, 21);
            this.tssl_currentPoint.Text = "            ";
            // 
            // tssl_var
            // 
            this.tssl_var.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssl_var.Name = "tssl_var";
            this.tssl_var.Size = new System.Drawing.Size(72, 21);
            this.tssl_var.Text = "软件版本：";
            // 
            // timer_sp
            // 
            this.timer_sp.Enabled = true;
            this.timer_sp.Tick += new System.EventHandler(this.timer_sp_Tick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem1.Text = "清除";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // panel_main
            // 
            this.panel_main.BackColor = System.Drawing.Color.DarkGray;
            this.panel_main.Controls.Add(this.myCCDHelp4);
            this.panel_main.Controls.Add(this.myCCDHelp1);
            this.panel_main.Controls.Add(this.myCCDHelp3);
            this.panel_main.Controls.Add(this.myCCDHelp2);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(161, 25);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(811, 600);
            this.panel_main.TabIndex = 12;
            this.panel_main.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_main_Paint);
            // 
            // myCCDHelp4
            // 
            this.myCCDHelp4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myCCDHelp4.AreaProduct1 = new int[] {
        100,
        100};
            this.myCCDHelp4.AVTName = "";
            this.myCCDHelp4.BackColor = System.Drawing.Color.DarkGray;
            this.myCCDHelp4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myCCDHelp4.CameraType = 0;
            this.myCCDHelp4.CCDName = "CCD4";
            this.myCCDHelp4.Location = new System.Drawing.Point(16, 7);
            this.myCCDHelp4.Location1 = new System.Drawing.Point(0, 0);
            this.myCCDHelp4.MarkArea1 = new int[] {
        100,
        100};
            this.myCCDHelp4.Name = "myCCDHelp4";
            this.myCCDHelp4.PointArea1 = new int[] {
        100,
        100};
            this.myCCDHelp4.Size = new System.Drawing.Size(388, 279);
            this.myCCDHelp4.TabIndex = 3;
            // 
            // myCCDHelp1
            // 
            this.myCCDHelp1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myCCDHelp1.AreaProduct1 = new int[] {
        100,
        100};
            this.myCCDHelp1.AVTName = "";
            this.myCCDHelp1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myCCDHelp1.CameraType = 0;
            this.myCCDHelp1.CCDName = "CCD1";
            this.myCCDHelp1.Location = new System.Drawing.Point(410, 7);
            this.myCCDHelp1.Location1 = new System.Drawing.Point(0, 0);
            this.myCCDHelp1.MarkArea1 = new int[] {
        100,
        100};
            this.myCCDHelp1.Name = "myCCDHelp1";
            this.myCCDHelp1.PointArea1 = new int[] {
        100,
        100};
            this.myCCDHelp1.Size = new System.Drawing.Size(388, 279);
            this.myCCDHelp1.TabIndex = 0;
            // 
            // myCCDHelp3
            // 
            this.myCCDHelp3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myCCDHelp3.AreaProduct1 = new int[] {
        100,
        100};
            this.myCCDHelp3.AVTName = "";
            this.myCCDHelp3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myCCDHelp3.CameraType = 0;
            this.myCCDHelp3.CCDName = "CCD3";
            this.myCCDHelp3.Location = new System.Drawing.Point(16, 292);
            this.myCCDHelp3.Location1 = new System.Drawing.Point(0, 0);
            this.myCCDHelp3.MarkArea1 = new int[] {
        100,
        100};
            this.myCCDHelp3.Name = "myCCDHelp3";
            this.myCCDHelp3.PointArea1 = new int[] {
        100,
        100};
            this.myCCDHelp3.Size = new System.Drawing.Size(388, 302);
            this.myCCDHelp3.TabIndex = 2;
            // 
            // myCCDHelp2
            // 
            this.myCCDHelp2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myCCDHelp2.AreaProduct1 = new int[] {
        100,
        100};
            this.myCCDHelp2.AVTName = "";
            this.myCCDHelp2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myCCDHelp2.CameraType = 0;
            this.myCCDHelp2.CCDName = "CCD2";
            this.myCCDHelp2.Location = new System.Drawing.Point(410, 292);
            this.myCCDHelp2.Location1 = new System.Drawing.Point(0, 0);
            this.myCCDHelp2.MarkArea1 = new int[] {
        100,
        100};
            this.myCCDHelp2.Name = "myCCDHelp2";
            this.myCCDHelp2.PointArea1 = new int[] {
        100,
        100};
            this.myCCDHelp2.Size = new System.Drawing.Size(388, 302);
            this.myCCDHelp2.TabIndex = 1;
            // 
            // serialPort_scan
            // 
            this.serialPort_scan.BaudRate = 115200;
            // 
            // groupBoxEx_right
            // 
            this.groupBoxEx_right.Controls.Add(this.panel_result);
            this.groupBoxEx_right.Controls.Add(this.panel_barcode);
            this.groupBoxEx_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxEx_right.Location = new System.Drawing.Point(972, 25);
            this.groupBoxEx_right.Name = "groupBoxEx_right";
            this.groupBoxEx_right.Radius = 10;
            this.groupBoxEx_right.Size = new System.Drawing.Size(289, 600);
            this.groupBoxEx_right.TabIndex = 11;
            this.groupBoxEx_right.TabStop = false;
            this.groupBoxEx_right.TitleFont = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // panel_result
            // 
            this.panel_result.Controls.Add(this.btn_openResult);
            this.panel_result.Controls.Add(this.btn_allCollapsed);
            this.panel_result.Controls.Add(this.label2);
            this.panel_result.Controls.Add(this.dataGridViewEx_result);
            this.panel_result.Controls.Add(this.btn_allExpand);
            this.panel_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_result.Location = new System.Drawing.Point(3, 132);
            this.panel_result.Name = "panel_result";
            this.panel_result.Size = new System.Drawing.Size(283, 465);
            this.panel_result.TabIndex = 7;
            // 
            // btn_openResult
            // 
            this.btn_openResult.BackColor = System.Drawing.Color.Silver;
            this.btn_openResult.Location = new System.Drawing.Point(188, 3);
            this.btn_openResult.Name = "btn_openResult";
            this.btn_openResult.Size = new System.Drawing.Size(90, 23);
            this.btn_openResult.TabIndex = 12;
            this.btn_openResult.Text = "查看测试结果";
            this.btn_openResult.UseVisualStyleBackColor = false;
            this.btn_openResult.Click += new System.EventHandler(this.btn_openResult_Click);
            // 
            // btn_allCollapsed
            // 
            this.btn_allCollapsed.BackColor = System.Drawing.Color.LightGreen;
            this.btn_allCollapsed.Location = new System.Drawing.Point(124, 3);
            this.btn_allCollapsed.Name = "btn_allCollapsed";
            this.btn_allCollapsed.Size = new System.Drawing.Size(65, 23);
            this.btn_allCollapsed.TabIndex = 1;
            this.btn_allCollapsed.Text = "全部折叠";
            this.btn_allCollapsed.UseVisualStyleBackColor = false;
            this.btn_allCollapsed.Click += new System.EventHandler(this.btn_allCollapsed_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "结果:";
            // 
            // dataGridViewEx_result
            // 
            this.dataGridViewEx_result.AllowUserToAddRows = false;
            this.dataGridViewEx_result.AllowUserToDeleteRows = false;
            this.dataGridViewEx_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEx_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx_result.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridViewEx_result.ContextMenuStrip = this.contextMenuStrip2;
            this.dataGridViewEx_result.Location = new System.Drawing.Point(3, 32);
            this.dataGridViewEx_result.Name = "dataGridViewEx_result";
            this.dataGridViewEx_result.ReadOnly = true;
            this.dataGridViewEx_result.RowTemplate.Height = 23;
            this.dataGridViewEx_result.Size = new System.Drawing.Size(277, 430);
            this.dataGridViewEx_result.TabIndex = 0;
            this.dataGridViewEx_result.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewEx_result_RowPostPaint);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Direction";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "X,Y";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // btn_allExpand
            // 
            this.btn_allExpand.BackColor = System.Drawing.Color.LightGreen;
            this.btn_allExpand.Location = new System.Drawing.Point(56, 3);
            this.btn_allExpand.Name = "btn_allExpand";
            this.btn_allExpand.Size = new System.Drawing.Size(65, 23);
            this.btn_allExpand.TabIndex = 2;
            this.btn_allExpand.Text = "全部展开";
            this.btn_allExpand.UseVisualStyleBackColor = false;
            this.btn_allExpand.Click += new System.EventHandler(this.btn_allExpand_Click);
            // 
            // panel_barcode
            // 
            this.panel_barcode.BackColor = System.Drawing.Color.Linen;
            this.panel_barcode.Controls.Add(this.lbl_Standard);
            this.panel_barcode.Controls.Add(this.lbl_productModel);
            this.panel_barcode.Controls.Add(this.label1);
            this.panel_barcode.Controls.Add(this.txtbox_barcode);
            this.panel_barcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_barcode.Location = new System.Drawing.Point(3, 17);
            this.panel_barcode.Name = "panel_barcode";
            this.panel_barcode.Size = new System.Drawing.Size(283, 115);
            this.panel_barcode.TabIndex = 5;
            // 
            // lbl_Standard
            // 
            this.lbl_Standard.Location = new System.Drawing.Point(3, 72);
            this.lbl_Standard.Name = "lbl_Standard";
            this.lbl_Standard.Size = new System.Drawing.Size(274, 37);
            this.lbl_Standard.TabIndex = 6;
            this.lbl_Standard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_productModel
            // 
            this.lbl_productModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_productModel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_productModel.Location = new System.Drawing.Point(0, 0);
            this.lbl_productModel.Name = "lbl_productModel";
            this.lbl_productModel.Size = new System.Drawing.Size(283, 42);
            this.lbl_productModel.TabIndex = 5;
            this.lbl_productModel.Text = "机种";
            this.lbl_productModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "条码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtbox_barcode
            // 
            this.txtbox_barcode.Location = new System.Drawing.Point(51, 48);
            this.txtbox_barcode.Name = "txtbox_barcode";
            this.txtbox_barcode.Size = new System.Drawing.Size(220, 21);
            this.txtbox_barcode.TabIndex = 3;
            // 
            // groupBoxEx_left
            // 
            this.groupBoxEx_left.BackColor = System.Drawing.Color.DarkGray;
            this.groupBoxEx_left.BorderColor = System.Drawing.Color.Blue;
            this.groupBoxEx_left.Controls.Add(this.checkBox_Fortest);
            this.groupBoxEx_left.Controls.Add(this.panel_pinmuSetting);
            this.groupBoxEx_left.Controls.Add(this.either1);
            this.groupBoxEx_left.Controls.Add(this.numericUpDown_testcount);
            this.groupBoxEx_left.Controls.Add(this.btn_allowWork);
            this.groupBoxEx_left.Controls.Add(this.lbl_testcount);
            this.groupBoxEx_left.Controls.Add(this.btn_localtest);
            this.groupBoxEx_left.Controls.Add(this.btn_9point);
            this.groupBoxEx_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxEx_left.Location = new System.Drawing.Point(0, 25);
            this.groupBoxEx_left.Name = "groupBoxEx_left";
            this.groupBoxEx_left.Radius = 10;
            this.groupBoxEx_left.Size = new System.Drawing.Size(161, 600);
            this.groupBoxEx_left.TabIndex = 10;
            this.groupBoxEx_left.TabStop = false;
            this.groupBoxEx_left.TitleFont = new System.Drawing.Font("宋体", 10F);
            // 
            // checkBox_Fortest
            // 
            this.checkBox_Fortest.AutoSize = true;
            this.checkBox_Fortest.Location = new System.Drawing.Point(29, 316);
            this.checkBox_Fortest.Name = "checkBox_Fortest";
            this.checkBox_Fortest.Size = new System.Drawing.Size(66, 16);
            this.checkBox_Fortest.TabIndex = 12;
            this.checkBox_Fortest.Text = "Fortest";
            this.checkBox_Fortest.UseVisualStyleBackColor = true;
            this.checkBox_Fortest.Visible = false;
            this.checkBox_Fortest.CheckedChanged += new System.EventHandler(this.checkBox_Fortest_CheckedChanged);
            // 
            // panel_pinmuSetting
            // 
            this.panel_pinmuSetting.BackColor = System.Drawing.Color.DimGray;
            this.panel_pinmuSetting.Controls.Add(this.button_cancel);
            this.panel_pinmuSetting.Controls.Add(this.button_accept);
            this.panel_pinmuSetting.Controls.Add(this.textBox_OperaterID);
            this.panel_pinmuSetting.Controls.Add(this.textBox_pinmu);
            this.panel_pinmuSetting.Controls.Add(this.label_OperaterID);
            this.panel_pinmuSetting.Controls.Add(this.label_input);
            this.panel_pinmuSetting.Controls.Add(this.textBox_LotNo);
            this.panel_pinmuSetting.Controls.Add(this.label3);
            this.panel_pinmuSetting.Controls.Add(this.groupBox_workTime);
            this.panel_pinmuSetting.Controls.Add(this.label_shift);
            this.panel_pinmuSetting.Location = new System.Drawing.Point(5, 4);
            this.panel_pinmuSetting.Name = "panel_pinmuSetting";
            this.panel_pinmuSetting.Size = new System.Drawing.Size(153, 128);
            this.panel_pinmuSetting.TabIndex = 11;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_cancel.BackColor = System.Drawing.Color.Violet;
            this.button_cancel.Location = new System.Drawing.Point(75, 99);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(58, 25);
            this.button_cancel.TabIndex = 31;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_accept
            // 
            this.button_accept.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_accept.BackColor = System.Drawing.Color.Violet;
            this.button_accept.Location = new System.Drawing.Point(11, 99);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(58, 25);
            this.button_accept.TabIndex = 30;
            this.button_accept.Text = "确定";
            this.button_accept.UseVisualStyleBackColor = false;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // textBox_OperaterID
            // 
            this.textBox_OperaterID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_OperaterID.Location = new System.Drawing.Point(42, 3);
            this.textBox_OperaterID.Name = "textBox_OperaterID";
            this.textBox_OperaterID.Size = new System.Drawing.Size(102, 21);
            this.textBox_OperaterID.TabIndex = 26;
            this.textBox_OperaterID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_OperaterID_KeyPress);
            // 
            // textBox_pinmu
            // 
            this.textBox_pinmu.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_pinmu.BackColor = System.Drawing.Color.Silver;
            this.textBox_pinmu.Enabled = false;
            this.textBox_pinmu.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox_pinmu.Location = new System.Drawing.Point(42, 49);
            this.textBox_pinmu.Name = "textBox_pinmu";
            this.textBox_pinmu.Size = new System.Drawing.Size(102, 21);
            this.textBox_pinmu.TabIndex = 22;
            // 
            // label_OperaterID
            // 
            this.label_OperaterID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_OperaterID.AutoSize = true;
            this.label_OperaterID.ForeColor = System.Drawing.Color.PaleGreen;
            this.label_OperaterID.Location = new System.Drawing.Point(5, 6);
            this.label_OperaterID.Name = "label_OperaterID";
            this.label_OperaterID.Size = new System.Drawing.Size(35, 12);
            this.label_OperaterID.TabIndex = 27;
            this.label_OperaterID.Text = "工号:";
            // 
            // label_input
            // 
            this.label_input.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_input.AutoSize = true;
            this.label_input.ForeColor = System.Drawing.Color.PaleGreen;
            this.label_input.Location = new System.Drawing.Point(6, 54);
            this.label_input.Name = "label_input";
            this.label_input.Size = new System.Drawing.Size(35, 12);
            this.label_input.TabIndex = 23;
            this.label_input.Text = "品目:";
            // 
            // textBox_LotNo
            // 
            this.textBox_LotNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_LotNo.Location = new System.Drawing.Point(42, 26);
            this.textBox_LotNo.MaxLength = 11;
            this.textBox_LotNo.Name = "textBox_LotNo";
            this.textBox_LotNo.Size = new System.Drawing.Size(102, 21);
            this.textBox_LotNo.TabIndex = 24;
            this.textBox_LotNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_LotNo_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.PaleGreen;
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "LotNo:";
            // 
            // groupBox_workTime
            // 
            this.groupBox_workTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox_workTime.Controls.Add(this.radioButton_nightShift);
            this.groupBox_workTime.Controls.Add(this.radioButton_dayshift);
            this.groupBox_workTime.Location = new System.Drawing.Point(42, 65);
            this.groupBox_workTime.Name = "groupBox_workTime";
            this.groupBox_workTime.Size = new System.Drawing.Size(103, 32);
            this.groupBox_workTime.TabIndex = 29;
            this.groupBox_workTime.TabStop = false;
            // 
            // radioButton_nightShift
            // 
            this.radioButton_nightShift.AutoSize = true;
            this.radioButton_nightShift.ForeColor = System.Drawing.Color.Violet;
            this.radioButton_nightShift.Location = new System.Drawing.Point(51, 12);
            this.radioButton_nightShift.Name = "radioButton_nightShift";
            this.radioButton_nightShift.Size = new System.Drawing.Size(47, 16);
            this.radioButton_nightShift.TabIndex = 1;
            this.radioButton_nightShift.Text = "晚班";
            this.radioButton_nightShift.UseVisualStyleBackColor = true;
            // 
            // radioButton_dayshift
            // 
            this.radioButton_dayshift.AutoSize = true;
            this.radioButton_dayshift.Checked = true;
            this.radioButton_dayshift.ForeColor = System.Drawing.Color.Violet;
            this.radioButton_dayshift.Location = new System.Drawing.Point(5, 12);
            this.radioButton_dayshift.Name = "radioButton_dayshift";
            this.radioButton_dayshift.Size = new System.Drawing.Size(47, 16);
            this.radioButton_dayshift.TabIndex = 0;
            this.radioButton_dayshift.TabStop = true;
            this.radioButton_dayshift.Text = "白班";
            this.radioButton_dayshift.UseVisualStyleBackColor = true;
            // 
            // label_shift
            // 
            this.label_shift.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_shift.AutoSize = true;
            this.label_shift.ForeColor = System.Drawing.Color.PaleGreen;
            this.label_shift.Location = new System.Drawing.Point(5, 79);
            this.label_shift.Name = "label_shift";
            this.label_shift.Size = new System.Drawing.Size(35, 12);
            this.label_shift.TabIndex = 28;
            this.label_shift.Text = "班別:";
            // 
            // either1
            // 
            this.either1.BackColor = System.Drawing.Color.Silver;
            this.either1.BtnLeftText = "开";
            this.either1.BtnRightText = "关";
            this.either1.LeftPress = false;
            this.either1.Location = new System.Drawing.Point(7, 265);
            this.either1.Name = "either1";
            this.either1.Size = new System.Drawing.Size(149, 35);
            this.either1.TabIndex = 10;
            this.either1.Title = "手动";
            this.either1.Event_BtnClick += new ZSMeasure.Either.dele_LeftRight(this.either1_Event_BtnClick);
            // 
            // numericUpDown_testcount
            // 
            this.numericUpDown_testcount.Location = new System.Drawing.Point(29, 449);
            this.numericUpDown_testcount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_testcount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_testcount.Name = "numericUpDown_testcount";
            this.numericUpDown_testcount.Size = new System.Drawing.Size(70, 21);
            this.numericUpDown_testcount.TabIndex = 8;
            this.numericUpDown_testcount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_allowWork
            // 
            this.btn_allowWork.BackColor = System.Drawing.Color.Silver;
            this.btn_allowWork.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_allowWork.Location = new System.Drawing.Point(19, 149);
            this.btn_allowWork.Name = "btn_allowWork";
            this.btn_allowWork.Size = new System.Drawing.Size(119, 95);
            this.btn_allowWork.TabIndex = 9;
            this.btn_allowWork.Text = "允许作业";
            this.btn_allowWork.UseVisualStyleBackColor = false;
            this.btn_allowWork.Click += new System.EventHandler(this.btn_allowWork_Click);
            // 
            // lbl_testcount
            // 
            this.lbl_testcount.AutoSize = true;
            this.lbl_testcount.Location = new System.Drawing.Point(105, 453);
            this.lbl_testcount.Name = "lbl_testcount";
            this.lbl_testcount.Size = new System.Drawing.Size(29, 12);
            this.lbl_testcount.TabIndex = 7;
            this.lbl_testcount.Text = "停止";
            this.lbl_testcount.Click += new System.EventHandler(this.lbl_testcount_Click);
            // 
            // btn_localtest
            // 
            this.btn_localtest.BackColor = System.Drawing.Color.Silver;
            this.btn_localtest.Enabled = false;
            this.btn_localtest.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_localtest.Location = new System.Drawing.Point(19, 355);
            this.btn_localtest.Name = "btn_localtest";
            this.btn_localtest.Size = new System.Drawing.Size(119, 95);
            this.btn_localtest.TabIndex = 5;
            this.btn_localtest.Text = "手动测试";
            this.btn_localtest.UseVisualStyleBackColor = false;
            this.btn_localtest.Click += new System.EventHandler(this.btn_localtest_Click);
            // 
            // btn_9point
            // 
            this.btn_9point.BackColor = System.Drawing.Color.Silver;
            this.btn_9point.Enabled = false;
            this.btn_9point.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_9point.Location = new System.Drawing.Point(19, 472);
            this.btn_9point.Name = "btn_9point";
            this.btn_9point.Size = new System.Drawing.Size(119, 95);
            this.btn_9point.TabIndex = 4;
            this.btn_9point.Text = "标定相机";
            this.btn_9point.UseVisualStyleBackColor = false;
            this.btn_9point.Click += new System.EventHandler(this.btn_9point_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 750);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.groupBoxEx_right);
            this.Controls.Add(this.groupBoxEx_left);
            this.Controls.Add(this.richTextBox_log);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "胀缩测量";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel_main.ResumeLayout(false);
            this.groupBoxEx_right.ResumeLayout(false);
            this.panel_result.ResumeLayout(false);
            this.panel_result.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx_result)).EndInit();
            this.panel_barcode.ResumeLayout(false);
            this.panel_barcode.PerformLayout();
            this.groupBoxEx_left.ResumeLayout(false);
            this.groupBoxEx_left.PerformLayout();
            this.panel_pinmuSetting.ResumeLayout(false);
            this.panel_pinmuSetting.PerformLayout();
            this.groupBox_workTime.ResumeLayout(false);
            this.groupBox_workTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_testcount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private myCCDHelp myCCDHelp1;
        private myCCDHelp myCCDHelp4;
        private myCCDHelp myCCDHelp3;
        private myCCDHelp myCCDHelp2;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 电机配置ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_spconn;
        private System.Windows.Forms.ToolStripStatusLabel tssl_space;
        private System.Windows.Forms.ToolStripStatusLabel tssl_var;
        private System.Windows.Forms.Timer timer_sp;
        private System.Windows.Forms.ToolStripMenuItem tsmi_adminlogin;
        private System.Windows.Forms.Button btn_9point;
        private System.Windows.Forms.Button btn_localtest;
        private System.Windows.Forms.ToolStripStatusLabel tssl_currentPoint;
        private System.Windows.Forms.Label lbl_testcount;
        private System.Windows.Forms.NumericUpDown numericUpDown_testcount;
        private System.Windows.Forms.Button btn_allowWork;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
        private HalconCCD.GroupBoxEx groupBoxEx_left;
        private HalconCCD.GroupBoxEx groupBoxEx_right;
        private System.Windows.Forms.Panel panel_main;
        private ZSMeasure.DataGridViewEx dataGridViewEx_result;
        private System.Windows.Forms.Button btn_allExpand;
        private System.Windows.Forms.Button btn_allCollapsed;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TextBox txtbox_barcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_barcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_changeForm;
        private System.IO.Ports.SerialPort serialPort_scan;
        private Either either1;
        private System.Windows.Forms.Panel panel_pinmuSetting;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.TextBox textBox_OperaterID;
        private System.Windows.Forms.TextBox textBox_pinmu;
        private System.Windows.Forms.Label label_input;
        private System.Windows.Forms.TextBox textBox_LotNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_OperaterID;
        private System.Windows.Forms.GroupBox groupBox_workTime;
        private System.Windows.Forms.RadioButton radioButton_nightShift;
        private System.Windows.Forms.RadioButton radioButton_dayshift;
        private System.Windows.Forms.Label label_shift;
        private System.Windows.Forms.Panel panel_result;
        private System.Windows.Forms.Label lbl_productModel;
        private System.Windows.Forms.Button btn_openResult;
        private System.Windows.Forms.ToolStripStatusLabel tssl_scan;
        private System.Windows.Forms.ToolStripStatusLabel tssl_testType;
        private System.Windows.Forms.Label lbl_Standard;
        private System.Windows.Forms.ToolStripMenuItem 校准片量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ModeOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ModeClose;
        private DataGridViewGroupColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.ToolStripMenuItem 计算方法ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_calc1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_calc2;
        private System.Windows.Forms.ToolStripStatusLabel tssl_calcfunc;
        private System.Windows.Forms.CheckBox checkBox_Fortest;
        private System.Windows.Forms.ToolStripMenuItem tsmi_calc3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_changeOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmi_changeClose;
        private System.Windows.Forms.ToolStripMenuItem tsmi_camertType;
        private System.Windows.Forms.ToolStripMenuItem tsmi_basler;
        private System.Windows.Forms.ToolStripMenuItem tsmi_avt;
        private System.Windows.Forms.ToolStripStatusLabel tssl_cameraType;
        private System.Windows.Forms.ToolStripStatusLabel tssl_xld;

    }
}

