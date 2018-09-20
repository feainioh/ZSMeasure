using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Net;
using System.Threading;

namespace ZSMeasure
{
    public class GlobalVar
    {
        public enum AdminMode : int
        {
            Nomal = 0,
            Admin = 1,
            SupperAdmin = 2,
            ZhengZhaolei = 3
        }
        //==========================Windows全局消息==========================
        public static IntPtr gl_IntPtr_MainWindow;      //主窗体句柄
        public const int WM_ReadyForTest = 0x0400 + 1;  //通知PC拍照
        public const int WM_StartScan = 0x0400 + 2; //通知扫描条码
        public const int WM_AddNewSlave = 0x0400 + 3;   //SocetServer通知主窗体有新的辅机連接
        public const int WM_DeleteSlave = 0x0400 + 4;   //SocetServer通知主窗体删除指定辅机連接
        //==========================Flag==========================
        public static bool gl_bAllowWork = false;  //允许作业
        public static AdminMode gl_bAdmin = AdminMode.Nomal;     //管理登录
        public static bool m_bNeedReCalc = false;  //标定相机
        public static DateTime m_timeLast9Point;   //记录标定的时间，2个小时需要标定一次
        public static double m_CalibSpace = 12;    //1小时量测校准片
        public static bool m_bInCalibMode = false; //量测校准片中
        public static bool m_bManualStart = false; //手动启动
        public static bool m_bUseOffset = true;    //使用补偿值
        public static bool m_bUseXLD = false;       //亚像素处理
        public static bool m_bChangeForm = false;  //交换窗体
        public static int m_nCameraType = 0;       //相机类型 0:Basler 1:AVT
        public static ManualResetEventSlim gl_ManualReset = new ManualResetEventSlim(false);
        public static bool SoftWareShutDown = false;
        //==========================条码枪串口==========================
        //ini--scanPort
        public const string gl_iniSection_SPScan = "SPScan";
        public const string gl_inikey_NeedScan = "NeedScanBarcode";
        public const string gl_inikey_ScanMode = "ScanMode";
        public const string gl_inikey_SerialPort = "SerialPort"; //用于通讯用的串口，固定COM1
        //ini--workinfo
        public const string gl_iniSection_WorkInfo = "WorkInfo";
        public const string gl_inikey_LotNo = "LotNo";
        public static string gl_PcName = Dns.GetHostName().ToString();
        public static List<int> gl_lsShtBarLen = new List<int>(); //厂内Sheet条码长度,目前手动配置(以后放在数据库)
        public static int gl_CalcTime = 60; //大于60分钟要校正菲林片
        //串口
        public static bool gl_bNeedScanBarcode = true; //是否需要扫描条码
        public static int gl_nScanMode = 1; //扫描条码方式 0：手持条码枪  1：黄色条码枪
        public static SerialPort gl_sp_Scan = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One); //条码枪串口

        public static string gl_strDefaultODBC = "EBSFLIB"; //默认ODBC连接
        public static SwitchBreakForm SWBreakForm = null;         //弹框
        public static string gl_strProductModel = ""; //机种


    }
}
