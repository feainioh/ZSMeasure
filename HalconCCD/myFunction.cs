using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;

namespace HalconCCD
{
    public class myFunction
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(
                            IntPtr hwnd,
                            int wMsg,
                            IntPtr wParam,
                            IntPtr lParam);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        public static string gl_strProductModel = "";
        #region 配置文件
        //配置文件的路径
        public static string GetConfigIniPath()
        {
            string dllpath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            dllpath = dllpath.Substring(8, dllpath.Length - 8);    // 8是 file:// 的长度
            char sep = System.IO.Path.DirectorySeparatorChar;
            return System.IO.Path.GetDirectoryName(dllpath) + sep + "Config" + sep + "Config.ini";
        }

        //配置文件的读取
        public static bool GetIniString(string key, out string value)
        {
            string iniPath = GetConfigIniPath();
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString("IPInfo", key, "", sb, 1024, iniPath);
            value = sb.ToString();
            if (value.Length > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="section">Section范围</param>
        /// <param name="key">Key关键字</param>
        /// <param name="value">值</param>
        public static void WriteIniString(string section, string key, string value)
        {
            string iniPath = GetConfigIniPath();
            WritePrivateProfileString(section, key, value, iniPath);
        }

        //配置文件的读取
        public static bool GetIniString(string section, string key, out string value)
        {
            string iniPath = GetConfigIniPath();
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", sb, 1024, iniPath);
            value = sb.ToString();
            if (value.Length > 0)
                return true;
            else
                return false;
        }

        //判斷IP是否合格
        public bool checkIPStringIsLegal(string str)
        {
            try
            {
                Regex reg = new Regex(@"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}");
                if (!reg.IsMatch(str))
                {
                    return false;
                }
                string[] array_str = str.Split('.');
                for (int i = 0; i < array_str.Length; i++)
                {
                    if (Convert.ToInt32(array_str[i]) > 255) { return false; }
                }
                return true;
            }
            catch
            { return false; }
        }

        #endregion
        
        /// <summary>
        /// 获得本机IP
        /// </summary>
        /// <returns></returns>
        public IPAddress getHostIP()
        {
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork") //排除IPV6
                {
                    return _IPAddress;
                }
            }
            return IPAddress.Parse("127.0.0.1");
        }

        //保存设置到TBS.INI
        public void WriteGlobalInfoToTBS()
        {
            try
            {
                string iniFilePath = Application.StartupPath + "\\";// +GlobalVar.gl_iniTBS_FileName;
            }
            catch { }
        }

        //Logs log = Logs.LogsT(); //fortest
        public myFunction()
        { 

        }   
       
        #region CRC校验
        /// <summary>
        /// 字节数组CRC16位校验
        /// </summary>
        /// <param name="str"> </param>
        /// <returns></returns>
        public static byte[] CRC16(byte[] data)
        {
            byte CRC16Lo;
            byte CRC16Hi;   //CRC寄存器 
            byte CL; byte CH;       //多项式码&HA001 
            byte SaveHi; byte SaveLo;
            byte[] tmpData;
            int I;
            int Flag;
            CRC16Lo = 0xFF;
            CRC16Hi = 0xFF;
            CL = 0x01;
            CH = 0xA0;
            tmpData = data;
            for (int i = 0; i < tmpData.Length; i++)
            {
                CRC16Lo = (byte)(CRC16Lo ^ tmpData[i]); //每一个数据与CRC寄存器进行异或 
                for (Flag = 0; Flag <= 7; Flag++)
                {
                    SaveHi = CRC16Hi;
                    SaveLo = CRC16Lo;
                    CRC16Hi = (byte)(CRC16Hi >> 1);      //高位右移一位 
                    CRC16Lo = (byte)(CRC16Lo >> 1);      //低位右移一位 
                    if ((SaveHi & 0x01) == 0x01) //如果高位字节最后一位为1 
                    {
                        CRC16Lo = (byte)(CRC16Lo | 0x80);   //则低位字节右移后前面补1 
                    }             //否则自动补0 
                    if ((SaveLo & 0x01) == 0x01) //如果LSB为1，则与多项式码进行异或 
                    {
                        CRC16Hi = (byte)(CRC16Hi ^ CH);
                        CRC16Lo = (byte)(CRC16Lo ^ CL);
                    }
                }
            }
            byte[] ReturnData = new byte[2];
            //ReturnData[0] = CRC16Hi;       //CRC高位 
            //ReturnData[1] = CRC16Lo;       //CRC低位 
            ReturnData[0] = CRC16Lo;       //CRC高位 
            ReturnData[1] = CRC16Hi;       //CRC低位
            return ReturnData;
        }

        /// <summary>
        /// 字符串CRC16位校验
        /// </summary>
        /// <param name="str"> </param>
        /// <returns></returns>
        public string CRC16(string str)
        { return ""; }

        //判断收到的消息（字符串）的CRC16校验码  是否正确，待完善，确定是否可用
        public bool CRC16_IsTrue(string data)
        { 
            bool isTrue = false;
            string temp_NoCRC = data.Substring(0,data.Length-4);
            string data_CRC16 = data.Substring(data.Length-4);
            byte[] temp_byte = strToToHexByte(temp_NoCRC);
            byte[] res = CRC16(temp_byte);
            string resToStr = null;
            for (int i = 0; i < res.Length; i++)
            {
                resToStr += Convert.ToString(res[i],16);
            }
            if (data_CRC16 == resToStr)
            {
                isTrue = true;
            }
            return isTrue;
        }

        /// <summary>
        /// 字符串CRC8位校验
        /// </summary>
        /// <param name="str"> </param>
        /// <returns></returns>
        public string CRC8(string str)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(str);
            short crc = 0;
            for (int j = 0; j < buffer.Length; j++)
            {
                crc ^= (Int16)(buffer[j] << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) > 0)
                    {
                        crc = (Int16)((crc << 1) ^ 0x1021);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }
            return string.Format(Convert.ToString(crc, 16).ToUpper().PadLeft(4, '0'), "0000");
        }

        //判断收到的消息（字符串）的CRC8校验码  
        public bool CRC8_IsTrue(string data)
        {
            bool isTrue = false;
            try
            {
                //data = data.Substring(data.IndexOf("@")).Replace("\r", "").Replace("\n", "").Trim();
                data = data.Replace("\r", "").Replace("\n", "").Trim();
            }
            catch
            {
                //log.AddERRORLOG("CRC8校验出错"); //fortest
            }            
            string temp_NoCRC = data.Substring(0, data.Length - 4);
            string data_CRC16 = data.Substring(data.Length - 4);
            string resToStr = CRC8(temp_NoCRC);
            if (data_CRC16 == resToStr)
            {
                isTrue = true;
            }
            return isTrue;
        }
        #endregion

        //字符串加上CRC16校验码
        public string StrAdd_CRC16(string str)
        {
            byte[] temp = Encoding.Default.GetBytes(str);
            byte[] res = CRC16(temp);
            for (int i = 0; i < res.Length; i++)
            {
                str += Convert.ToString(res[i],16);
            }
            return str;
        }
                
        //字符串变16进制字节数组
        private byte[] strToToHexByte(string hexString)
        { 
            hexString = hexString.Replace(" ", ""); 
            if ((hexString.Length % 2) != 0)     
                hexString += " "; 
            byte[] returnBytes = new byte[hexString.Length / 2]; 
            for (int i = 0; i < returnBytes.Length; i++)         
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16); 
            return returnBytes; 
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 获得接收消息 标志字符【截取开始位5，长度3】  500\502\504....
        /// </summary>
        /// <param name="str">需要带前缀?或@或/</param>
        /// <returns></returns>
        public string getTagString(string str)
        {
            return str.Substring(5, 3);
        }            
  
        #region 获取软件版本号
        public  string GetVersion()
        {
            string NowVersion = "V1.0";
            object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute), false);
            if (attributes.Length > 0)
            {
                if (attributes.Length > 0)
                {
                    NowVersion = ((System.Reflection.AssemblyFileVersionAttribute)attributes[0]).Version;
                }
            }
            return NowVersion;
        }
        #endregion

        /// <summary>
        /// 获取最近的IP4地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public string GetIP4()
        {
            string ipAddr = string.Empty;
            System.Net.IPAddress[] arrIP = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in arrIP)
            {
                if (System.Net.Sockets.AddressFamily.InterNetwork.Equals(ip.AddressFamily))
                {
                    ipAddr = ip.ToString();
                    break;
                }
                //else if (System.Net.Sockets.AddressFamily.InterNetworkV6.Equals(ip.AddressFamily))
                //{
                //    ipAddr = ip.ToString();
                //}
            }
            return ipAddr;
        }

        /// <summary>
        /// PLC异常代码
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public string E(string EIndex)
        {
            string ErrorStr = string.Empty;
            switch (EIndex)
            {
                case "E0":
                    ErrorStr = "软件件编号异常";
                    break;
                case "E1":
                    ErrorStr = "命令异常";
                    break;
                case "E2":
                    ErrorStr = "程序未登陆";
                    break;
                case "E4":
                    ErrorStr = "禁止写入";
                    break;
                case "E5":
                    ErrorStr = "单元错误";
                    break;
                case "E6":
                    ErrorStr = "无注释";
                    break;
                default:
                    ErrorStr = "本程序未考虑此异常";
                    break;
            }
            ErrorStr = "PLC异常：" + ErrorStr + "\r\n查看PDF（8-36）";
            return ErrorStr;
        }

        /// <summary>
        /// Output输出字符串  
        /// Console.Writeline(时间：str)
        /// </summary>
        /// <param name="str"></param>
        public static void Output(string str)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + "\t" + str);
        }

        public static void WriteText(string str)
        { 
            try
            {
                const string FolderName = "TempLog";
                string filename = DateTime.Now.ToString("yyyyMMddHH");//文件名称
                string dirName = Application.StartupPath + string.Format(@"\{0}\",FolderName);
                if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);

                string _logfile = dirName + filename + ".Txt";
                FileStream FS = new FileStream(_logfile, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter SW = new StreamWriter(FS, Encoding.Default);
                string writestr = string.Format("{0}\t{1}\r\n\r\n", DateTime.Now.ToString("HH:mm:ss:fff"),str);
                SW.Write(writestr);
                SW.Close();
                SW.Dispose();
            }
            catch(Exception ex)
            {
                //MessageBox.Show("CommLog Error:"+ex.Message+"\r\n"+ex.StackTrace);
                Console.WriteLine("\r\n！！！Logs Error\r\n"+ex.Message);
            }
        }

        public static void WriteCSV(double LaserAngle,Axis LeftMark,Axis RightMark)
        {
            try
            {
                const string FolderName = "Axis_Angle";
                string filename = DateTime.Now.ToString("yyyyMMddHH");//文件名称
                string dirName = Application.StartupPath + string.Format(@"\LOG\\{0}\", FolderName);

                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }

                bool CsvCreate = false;    
                string _logfile = dirName + filename + ".csv";
                CsvCreate = File.Exists(_logfile);

                FileStream FS = new FileStream(_logfile, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter SW = new StreamWriter(FS, Encoding.Default);

                if(!CsvCreate)
                    SW.Write(string.Format("时间,镭射角度,左边Mark  X,左边Mark  Y,右边Mark  X,右边Mark  Y,两点间距离\r\n"));

                string writestr = string.Format("{0},{1},{2},{3},{4},{5},{6}\r\n", 
                    DateTime.Now.ToString("HH:mm:ss:fff"),
                    LaserAngle,
                    LeftMark.X.ToString("#0.000"),
                    LeftMark.Y.ToString("#0.000"),
                    RightMark.X.ToString("#0.000"),
                    RightMark.Y.ToString("#0.000"),
                    Math.Sqrt(Math.Pow((RightMark.X - LeftMark.X), 2) + Math.Pow((RightMark.Y - LeftMark.Y), 2)).ToString("#0.000")
                    );
                SW.Write(writestr);
                SW.Close();
                SW.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("CommLog Error:"+ex.Message+"\r\n"+ex.StackTrace);
                Console.WriteLine("\r\n！！！Logs Error\r\n" + ex.Message);
            }
        }

        #region 重启软件
        public void ReStart(String AppName, String FFileName)
        {
            try
            {
                if (File.Exists("update.bat"))
                {
                    File.Delete("update.bat");
                }
            }
            catch { }
            string str = @"C:\Windows\System32\taskkill /F /IM " + AppName + ".exe ";
            str += "\r\n" + @"C:\Windows\System32\taskkill /F /IM " + AppName + ".vshost.exe ";
            //str += "\r\n" + "@ping 127.0.0.1 -n 2 > nul";
            //str += "\r\n" + "del " + AppName;
            //str += "\r\n" + "@ping 127.0.0.1 -n 1 > nul";
            //str += "\r\n" + "ren " + FFileName + " " + AppName;
            str += "\r\n" + AppName + ".exe "; ;
            str += "\r\n" + "del update.bat";

            File.WriteAllText("update.bat", str, ASCIIEncoding.Default);
            System.Diagnostics.Process.Start("update.bat");
        }
        #endregion        
    }

}
