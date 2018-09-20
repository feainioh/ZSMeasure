
namespace ZSMeasure
{
    partial class AxSerialPort
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.combox_databit = new System.Windows.Forms.ComboBox();
            this.label_databit = new System.Windows.Forms.Label();
            this.combox_stopbit = new System.Windows.Forms.ComboBox();
            this.label_stopBit = new System.Windows.Forms.Label();
            this.combox_parity = new System.Windows.Forms.ComboBox();
            this.label_priority = new System.Windows.Forms.Label();
            this.combox_boudrate = new System.Windows.Forms.ComboBox();
            this.label_baudrate = new System.Windows.Forms.Label();
            this.label_ports = new System.Windows.Forms.Label();
            this.combox_serialPorts = new System.Windows.Forms.ComboBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lab_RtsState = new System.Windows.Forms.Label();
            this.lab_CtsState = new System.Windows.Forms.Label();
            this.lab_DsrState = new System.Windows.Forms.Label();
            this.btn_OpenSp = new System.Windows.Forms.Button();
            this.lab_CD = new System.Windows.Forms.Label();
            this.chkboxRts = new System.Windows.Forms.CheckBox();
            this.chkboxDtr = new System.Windows.Forms.CheckBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabpgSPSet = new System.Windows.Forms.TabPage();
            this.tabpgCMLog = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtboxCMLog = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabpgSPSet.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // combox_databit
            // 
            this.combox_databit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.combox_databit.AutoCompleteCustomSource.AddRange(new string[] {
            "5",
            "6",
            "7",
            "8"});
            this.combox_databit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_databit.FormattingEnabled = true;
            this.combox_databit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.combox_databit.Location = new System.Drawing.Point(79, 143);
            this.combox_databit.Name = "combox_databit";
            this.combox_databit.Size = new System.Drawing.Size(55, 20);
            this.combox_databit.TabIndex = 31;
            this.combox_databit.SelectedIndexChanged += new System.EventHandler(this.comboBox_databit_SelectedIndexChanged);
            // 
            // label_databit
            // 
            this.label_databit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_databit.AutoSize = true;
            this.label_databit.Location = new System.Drawing.Point(20, 147);
            this.label_databit.Name = "label_databit";
            this.label_databit.Size = new System.Drawing.Size(53, 12);
            this.label_databit.TabIndex = 30;
            this.label_databit.Text = "数据位：";
            // 
            // combox_stopbit
            // 
            this.combox_stopbit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.combox_stopbit.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "1.5",
            "2"});
            this.combox_stopbit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_stopbit.FormattingEnabled = true;
            this.combox_stopbit.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.combox_stopbit.Location = new System.Drawing.Point(79, 116);
            this.combox_stopbit.Name = "combox_stopbit";
            this.combox_stopbit.Size = new System.Drawing.Size(55, 20);
            this.combox_stopbit.TabIndex = 29;
            this.combox_stopbit.SelectedIndexChanged += new System.EventHandler(this.comboBox_stopbit_SelectedIndexChanged);
            // 
            // label_stopBit
            // 
            this.label_stopBit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_stopBit.AutoSize = true;
            this.label_stopBit.Location = new System.Drawing.Point(20, 120);
            this.label_stopBit.Name = "label_stopBit";
            this.label_stopBit.Size = new System.Drawing.Size(53, 12);
            this.label_stopBit.TabIndex = 28;
            this.label_stopBit.Text = "停止位：";
            // 
            // combox_parity
            // 
            this.combox_parity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.combox_parity.AutoCompleteCustomSource.AddRange(new string[] {
            "NONE",
            "ODD",
            "EVEN",
            "MARK",
            "SPACE"});
            this.combox_parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_parity.FormattingEnabled = true;
            this.combox_parity.Items.AddRange(new object[] {
            "NONE",
            "ODD",
            "EVEN",
            "MARK",
            "SPACE"});
            this.combox_parity.Location = new System.Drawing.Point(79, 89);
            this.combox_parity.Name = "combox_parity";
            this.combox_parity.Size = new System.Drawing.Size(55, 20);
            this.combox_parity.TabIndex = 27;
            this.combox_parity.SelectedIndexChanged += new System.EventHandler(this.comboBox_parity_SelectedIndexChanged);
            // 
            // label_priority
            // 
            this.label_priority.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_priority.AutoSize = true;
            this.label_priority.Location = new System.Drawing.Point(20, 93);
            this.label_priority.Name = "label_priority";
            this.label_priority.Size = new System.Drawing.Size(53, 12);
            this.label_priority.TabIndex = 26;
            this.label_priority.Text = "校验位：";
            // 
            // combox_boudrate
            // 
            this.combox_boudrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.combox_boudrate.AutoCompleteCustomSource.AddRange(new string[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "115200"});
            this.combox_boudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_boudrate.FormattingEnabled = true;
            this.combox_boudrate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "115200"});
            this.combox_boudrate.Location = new System.Drawing.Point(79, 62);
            this.combox_boudrate.Name = "combox_boudrate";
            this.combox_boudrate.Size = new System.Drawing.Size(55, 20);
            this.combox_boudrate.TabIndex = 25;
            this.combox_boudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_boudrate_SelectedIndexChanged);
            // 
            // label_baudrate
            // 
            this.label_baudrate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_baudrate.AutoSize = true;
            this.label_baudrate.Location = new System.Drawing.Point(20, 66);
            this.label_baudrate.Name = "label_baudrate";
            this.label_baudrate.Size = new System.Drawing.Size(53, 12);
            this.label_baudrate.TabIndex = 24;
            this.label_baudrate.Text = "波特率：";
            // 
            // label_ports
            // 
            this.label_ports.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_ports.AutoSize = true;
            this.label_ports.Location = new System.Drawing.Point(8, 39);
            this.label_ports.Name = "label_ports";
            this.label_ports.Size = new System.Drawing.Size(65, 12);
            this.label_ports.TabIndex = 22;
            this.label_ports.Text = "串口选择：";
            // 
            // combox_serialPorts
            // 
            this.combox_serialPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.combox_serialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_serialPorts.FormattingEnabled = true;
            this.combox_serialPorts.Location = new System.Drawing.Point(79, 35);
            this.combox_serialPorts.Name = "combox_serialPorts";
            this.combox_serialPorts.Size = new System.Drawing.Size(55, 20);
            this.combox_serialPorts.TabIndex = 23;
            this.combox_serialPorts.SelectedIndexChanged += new System.EventHandler(this.comboBox_serialPorts_SelectedIndexChanged);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Refresh.Location = new System.Drawing.Point(79, 8);
            this.btn_Refresh.MinimumSize = new System.Drawing.Size(61, 21);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(61, 21);
            this.btn_Refresh.TabIndex = 32;
            this.btn_Refresh.Text = "刷新串口";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.83957F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.41788F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.74255F));
            this.tableLayoutPanel1.Controls.Add(this.combox_serialPorts, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_databit, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.combox_databit, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.label_stopBit, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_Refresh, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_priority, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.combox_boudrate, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_baudrate, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.combox_stopbit, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_ports, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.combox_parity, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.lab_RtsState, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.lab_CtsState, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lab_DsrState, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_OpenSp, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lab_CD, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.chkboxRts, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkboxDtr, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(188, 173);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // lab_RtsState
            // 
            this.lab_RtsState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_RtsState.AutoSize = true;
            this.lab_RtsState.Location = new System.Drawing.Point(140, 120);
            this.lab_RtsState.Name = "lab_RtsState";
            this.lab_RtsState.Size = new System.Drawing.Size(45, 12);
            this.lab_RtsState.TabIndex = 36;
            this.lab_RtsState.Text = "RTS: 0";
            this.lab_RtsState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_CtsState
            // 
            this.lab_CtsState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_CtsState.AutoSize = true;
            this.lab_CtsState.Location = new System.Drawing.Point(140, 93);
            this.lab_CtsState.Name = "lab_CtsState";
            this.lab_CtsState.Size = new System.Drawing.Size(45, 12);
            this.lab_CtsState.TabIndex = 35;
            this.lab_CtsState.Text = "CTS: 0";
            this.lab_CtsState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_DsrState
            // 
            this.lab_DsrState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_DsrState.AutoSize = true;
            this.lab_DsrState.Location = new System.Drawing.Point(140, 66);
            this.lab_DsrState.Name = "lab_DsrState";
            this.lab_DsrState.Size = new System.Drawing.Size(45, 12);
            this.lab_DsrState.TabIndex = 34;
            this.lab_DsrState.Text = "DSR: 0";
            this.lab_DsrState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_OpenSp
            // 
            this.btn_OpenSp.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_OpenSp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_OpenSp.Location = new System.Drawing.Point(8, 8);
            this.btn_OpenSp.Name = "btn_OpenSp";
            this.btn_OpenSp.Size = new System.Drawing.Size(65, 21);
            this.btn_OpenSp.TabIndex = 37;
            this.btn_OpenSp.Text = "打开串口";
            this.btn_OpenSp.UseVisualStyleBackColor = false;
            this.btn_OpenSp.Click += new System.EventHandler(this.btn_OpenSp_Click);
            // 
            // lab_CD
            // 
            this.lab_CD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_CD.AutoSize = true;
            this.lab_CD.Location = new System.Drawing.Point(140, 147);
            this.lab_CD.Name = "lab_CD";
            this.lab_CD.Size = new System.Drawing.Size(45, 12);
            this.lab_CD.TabIndex = 38;
            this.lab_CD.Text = "CD:0";
            this.lab_CD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkboxRts
            // 
            this.chkboxRts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkboxRts.AutoSize = true;
            this.chkboxRts.Location = new System.Drawing.Point(140, 37);
            this.chkboxRts.Name = "chkboxRts";
            this.chkboxRts.Size = new System.Drawing.Size(45, 16);
            this.chkboxRts.TabIndex = 39;
            this.chkboxRts.Text = "Rts";
            this.chkboxRts.UseVisualStyleBackColor = true;
            this.chkboxRts.CheckedChanged += new System.EventHandler(this.chkboxRts_CheckedChanged);
            // 
            // chkboxDtr
            // 
            this.chkboxDtr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkboxDtr.AutoSize = true;
            this.chkboxDtr.Location = new System.Drawing.Point(140, 10);
            this.chkboxDtr.Name = "chkboxDtr";
            this.chkboxDtr.Size = new System.Drawing.Size(45, 16);
            this.chkboxDtr.TabIndex = 40;
            this.chkboxDtr.Text = "Dtr";
            this.chkboxDtr.UseVisualStyleBackColor = true;
            this.chkboxDtr.CheckedChanged += new System.EventHandler(this.chkboxDtr_CheckedChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPort1_PinChanged);
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabpgSPSet);
            this.tabControl1.Controls.Add(this.tabpgCMLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(202, 205);
            this.tabControl1.TabIndex = 34;
            // 
            // tabpgSPSet
            // 
            this.tabpgSPSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabpgSPSet.Controls.Add(this.tableLayoutPanel1);
            this.tabpgSPSet.Location = new System.Drawing.Point(4, 22);
            this.tabpgSPSet.Name = "tabpgSPSet";
            this.tabpgSPSet.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgSPSet.Size = new System.Drawing.Size(194, 179);
            this.tabpgSPSet.TabIndex = 0;
            this.tabpgSPSet.Text = "串口设置";
            // 
            // tabpgCMLog
            // 
            this.tabpgCMLog.Location = new System.Drawing.Point(4, 22);
            this.tabpgCMLog.Name = "tabpgCMLog";
            this.tabpgCMLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgCMLog.Size = new System.Drawing.Size(194, 179);
            this.tabpgCMLog.TabIndex = 1;
            this.tabpgCMLog.Text = "通信记录";
            this.tabpgCMLog.UseVisualStyleBackColor = true;
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
            // rtboxCMLog
            // 
            this.rtboxCMLog.BackColor = System.Drawing.Color.Black;
            this.rtboxCMLog.ContextMenuStrip = this.contextMenuStrip1;
            this.rtboxCMLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtboxCMLog.Location = new System.Drawing.Point(202, 0);
            this.rtboxCMLog.Name = "rtboxCMLog";
            this.rtboxCMLog.Size = new System.Drawing.Size(202, 205);
            this.rtboxCMLog.TabIndex = 35;
            this.rtboxCMLog.Text = "";
            // 
            // AxSerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rtboxCMLog);
            this.Controls.Add(this.tabControl1);
            this.Name = "AxSerialPort";
            this.Size = new System.Drawing.Size(404, 205);
            this.Load += new System.EventHandler(this.SerialPortPara_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabpgSPSet.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_databit;
        private System.Windows.Forms.Label label_stopBit;
        private System.Windows.Forms.Label label_priority;
        private System.Windows.Forms.Label label_baudrate;
        private System.Windows.Forms.Label label_ports;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lab_DsrState;
        private System.Windows.Forms.Label lab_CtsState;
        private System.Windows.Forms.Label lab_RtsState;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btn_OpenSp;
        private System.Windows.Forms.Label lab_CD;
        private System.Windows.Forms.CheckBox chkboxRts;
        private System.Windows.Forms.CheckBox chkboxDtr;
        private System.Windows.Forms.ComboBox combox_databit;
        private System.Windows.Forms.ComboBox combox_stopbit;
        private System.Windows.Forms.ComboBox combox_parity;
        private System.Windows.Forms.ComboBox combox_boudrate;
        private System.Windows.Forms.ComboBox combox_serialPorts;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabpgSPSet;
        private System.Windows.Forms.TabPage tabpgCMLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtboxCMLog;
    }
}


