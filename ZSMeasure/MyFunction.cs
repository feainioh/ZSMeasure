using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Net;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace ZSMeasure
{
    public class PopMessage
    {
        public static DialogResult Message(string str)
        {
            return MessageBox.Show(str, "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Error(string str)
        {
            return MessageBox.Show(str, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Warning(string str)
        {
            return MessageBox.Show(str, "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public static DialogResult Exception(string str)
        {
            return MessageBox.Show(str, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    public static class CommonFunc
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(
                            IntPtr hwnd,
                            int wMsg,
                            IntPtr wParam,
                            IntPtr lParam);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        ///  从配置文件中读出整个Section的内容 
        /// </summary>
        ///  <param name="section">INI文件中的段落</param>
        ///  <param name="lpReturn">返回的数据数组</param>
        ///  <param name="nSize">返回数据的缓冲区长度</param>
        ///  <param name="strFileName">INI文件的完整的路径(包含文件名)</param>
        ///  <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string section, byte[] lpReturn, int nSize, string strFileName);

        /// <summary>
        /// 将一个整个Section的内容写入ini文件的指定Section中
        /// </summary>
        /// <param name="Section">INI文件中的段落名称</param>
        /// <param name="str">要写入的字符串</param>
        /// <param name="strFilePath">INI文件的完整路径(包含文件名)</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileSection(string Section, string str, string strFilePath);

        /// <summary>
        /// 将一个Key值写入Win.ini文件的指定Section中
        /// </summary>
        /// <param name="section">INI文件中的段落</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="val">INI文件中关键字的数值</param>
        /// <param name="filePath">INI文件的完整的路径(包含文件名)</param>
        public static void Write(string section, string key, string val, string filePath)
        {
            WritePrivateProfileString(section, key, val, filePath);
        }

        /// <summary>
        /// 从ini文件的某个Section取得一个key的字符串
        /// </summary>
        /// <param name="Section">INI文件中的段落名称</param>
        /// <param name="Key">INI文件中的关键字</param>
        /// <param name="path">INI文件的完整路径(包含文件名)</param>
        /// <returns></returns>      
        public static string Read(string Section, string Key, string defValue, string path)
        {
            StringBuilder temp = new StringBuilder(1000);
            int i = GetPrivateProfileString(Section, Key, "", temp, 1000, path);
            string strRes = temp.ToString().Trim() == "" ? defValue : temp.ToString().Trim();
            return strRes;
        }

        /// <summary>
        /// 从ini文件的某个Section取得所有key的集合
        /// </summary>
        /// <param name="section">INI文件中的段落名称</param>
        /// <param name="filePath">INI文件的完整路径(包含文件名)</param>
        /// <returns></returns>
        public static List<string> ReadSection(string section, string filePath)
        {
            List<string> listSection = new List<string>(); //读到的块
            NameValueCollection productnameList = new NameValueCollection();
            StringCollection items = new StringCollection();
            byte[] buffer = new byte[32768];
            int bufLen = 0;
            bufLen = GetPrivateProfileSection(section, buffer,
               buffer.GetUpperBound(0), filePath);
            if (bufLen > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bufLen; i++)
                {
                    if (buffer[i] != 0)
                    {
                        sb.Append((char)buffer[i]);
                    }
                    else
                    {
                        if (sb.Length > 0)
                        {
                            items.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                    }
                }
            }

            foreach (string key in items)
            {
                listSection.Add(ReadString(key));
            }
            return listSection;
        }

        /// <summary>
        /// 将一个{Key值,value值}的哈希表写入Win.ini文件的指定Section中
        /// </summary>
        /// <param name="section">INI文件中的段落名称</param>
        /// <param name="ht">值的哈希表</param>
        /// <param name="filePath">INI文件的完整路径(包含文件名)</param>
        /// <returns></returns>
        public static bool WriteSection(string section, Hashtable ht, string filePath)
        {
            bool flag = false;
            string lpString = "";
            try
            {
                if (section.Trim().Length <= 0 || ht.Count == 0)
                {
                    flag = false;
                }
                else
                {
                    foreach (DictionaryEntry de in ht)
                    {
                        lpString += de.Key + "=" + de.Value;
                        lpString += "\r\n";
                    }
                    if (WritePrivateProfileSection(section, lpString, filePath) == 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        
        public static bool WriteSection(string section, List<string> listValue, string filePath)
        {
            bool flag = false;
            string lpString = "";
            try
            {
                foreach (string str in listValue)
                {
                    lpString += str;
                    lpString += "\r\n";
                }
                if (WritePrivateProfileSection(section, lpString, filePath) == 0)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }


        //获得整块section内容
        public static string[] GetSection(string strSection, string strIniFilePath)
        {
            byte[] byProduct = new byte[524288];
            int nCount = GetPrivateProfileSection(strSection, byProduct, 524288, strIniFilePath);
            if (nCount <= 0) return new string[] { };
            string strTmp = Encoding.Default.GetString(byProduct, 0, nCount);
            strTmp = strTmp.Trim().TrimEnd('\0');
            return strTmp.Trim().Split('\0');
        }

        //写入整块section到ini
        public static int SetSection(string section, string str, string strFilePath)
        {
            return WritePrivateProfileSection(section, str, strFilePath);
        }

        public static string ReadString(string key)
        {
            string str = "";
            int index = key.IndexOf("=");
            if (index >= 0)
                str = key.Substring(index + 1);
            else
                str = key;
            return str.Trim();
        }

        /// <summary>
        /// 获取变量名称,似乎是有问题的？
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>return string</returns>
        private static string GetVarName<T>(System.Linq.Expressions.Expression<Func<T, T>> exp)
        {
            return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;
        }

        /// <summary>
        /// 扩展 获取变量名称(字符串)
        /// </summary>
        /// <param name="var_name"></param>
        /// <param name="exp"></param>
        /// <returns>return string</returns>
        public static string GetVarName<T>(this T var_name, System.Linq.Expressions.Expression<Func<T, T>> exp)
        {
            return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;
        }
        
        //CRC8位校验
        public static string CRC8(string str)
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

        /// <summary>
        /// 检验输入是否合法
        /// </summary>
        /// <param name="str">检测字串</param>
        /// <param name="checkType">1：数字  2：英文字符  3：数字+英文字符 4：数字+英文字符+横杠线(用于新的条码定义)</param>
        /// <returns></returns>
        public static bool checkStringIsLegal(string str, int checkType)
        {
            bool result = true;
            if (checkType == 1)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= ((c >= 48) && (c <= 57));
                }
            }
            else if (checkType == 2)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= (((c >= 65) && (c <= 90))
                        || ((c >= 97) && (c <= 122)));
                }
            }
            else if (checkType == 3)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= ((((c >= 65) && (c <= 90)) || ((c >= 97) && (c <= 122)))
                        || ((c >= 48) && (c <= 57)));
                }
            }
            else if (checkType == 4)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= ((((c >= 65) && (c <= 90)) || ((c >= 97) && (c <= 122)))
                        || ((c >= 48) && (c <= 57)) || (c == '-'));
                }
            }
            return result;
        }

        /// <summary>
        /// 获得本机IP
        /// </summary>
        /// <returns></returns>
        public static IPAddress getHostIP()
        {
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork") //排除IPV6
                {
                    //if(_IPAddress.ToString().IndexOf("192.168.3") >= 0)
                        return _IPAddress;
                }
            }
            return IPAddress.Parse("127.0.0.1");
        }
        //判斷IP是否合格
        public static bool checkIPStringIsLegal(string str)
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

        /// <summary>
        /// 写LOG
        /// </summary>
        /// <param name="str"></param>
        public static void writeLog(String str, string filePath = "")
        {
            try
            {
                String path = "";
                if (filePath == "")
                    path = System.Windows.Forms.Application.StartupPath + "\\LOG\\";
                else
                    path = filePath;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                StreamWriter writer = new StreamWriter(path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true);
                writer.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":  " + str);
                writer.Close();
                return;
            }
            catch
            {
                return;
            }
        }
        public static void deleOldLog()
        {
            try
            {
                String path = System.Windows.Forms.Application.StartupPath + "\\LOG\\";
                DirectoryInfo info = new DirectoryInfo(path);
                foreach (FileInfo fileinfo in info.GetFiles())
                {
                    TimeSpan ts = DateTime.Now.Subtract(fileinfo.CreationTime);
                    if (ts.Days > 30)
                    {
                        File.Delete(fileinfo.FullName);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 递归删除文件夹下的文件（包括子文件夹）
        /// </summary>
        /// <param name="strP"></param>
        public static void DeleteLogFunc(string strP)
        {
            DirectoryInfo d = new DirectoryInfo(strP);
            FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                if (fsinfo is DirectoryInfo)     //判断是否为文件夹  
                {
                    DeleteLogFunc(fsinfo.FullName);//递归调用  
                }
                else
                {
                    TimeSpan ts = DateTime.Now.Subtract(fsinfo.CreationTime);
                    if (ts.Days > 7)
                    {
                        File.Delete(fsinfo.FullName);
                    }
                }
            }
        }

        /// <summary>
        /// 获取程序集版本
        /// </summary>
        public static string AssemblyVersion
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        /// <summary>  
        /// 反射得到实体类的属性名称和值  
        /// var dict = GetProperties(model);  
        /// </summary>  
        /// <typeparam name="T">实体类</typeparam>  
        /// <param name="t">实例化</param>  
        /// <returns></returns>  
        public static Dictionary<object, object> GetProperties<T>(T t)
        {
            var ret = new Dictionary<object, object>();
            if (t == null) { return null; }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Add(name, value);
                    Type type = item.GetType();
                }
            }
            return ret;
        }

        /// <summary>
        /// 反射得到实体类的字段名称和值  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static FieldInfo[] GetFields<T>(T t)
        {
            var ret = new Dictionary<object, object>();
            if (t == null) { return null; }
            FieldInfo[] fields = t.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            if (fields.Length <= 0) { return null; }
            return fields;
            //foreach (FieldInfo item in fields)
            //{
            //    string name = item.Name;
            //    object value = item.GetValue(t);
            //    object type = item.FieldType.Name;
            //    //object value = item.GetValue(t, null);
            //    //if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
            //    //{
            //    //    ret.Add(name, value);
            //    //}
            //}
            //return ret;
        }

        public static T SetFields<T>(T t, Dictionary<string, string> d)
        {
            if (t == null || d == null)
            {
                return default(T);
            }
            FieldInfo[] fields = t.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);

            if (fields.Length <= 0)
            {
                return default(T);
            }
            foreach (System.Reflection.FieldInfo item in fields)
            {
                string name = item.Name;            //名称
                object value = item.GetValue(t);    //值

                if (item.FieldType.IsValueType || item.FieldType.Name.StartsWith("String"))
                {
                    string val = d.Where(c => c.Key == name).FirstOrDefault().Value;
                    if (val != null && val != value.ToString())
                    {
                        if (item.FieldType.Name.StartsWith("Nullable`1"))
                        {
                            item.SetValue(t, val);
                        }
                        else
                        {
                            item.SetValue(t, val);
                        }
                    }
                }
            }
            return t;
        }
        
        /// <summary> 
        /// 将一个object对象序列化，返回一个byte[]         
        /// </summary> 
        /// <param name="obj">能序列化的对象</param>         
        /// <returns></returns> 
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }
        /// <summary> 
        /// 将一个序列化后的byte[]数组还原         
        /// </summary>
        /// <param name="Bytes"></param>         
        /// <returns></returns> 
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 文件夹移动
        /// </summary>
        /// <param name="srcFolderPath"></param>
        /// <param name="destFolderPath"></param>
        public static void FolderMove(string srcFolderPath, string destFolderPath)
        {
            //检查目标目录是否以目标分隔符结束，如果不是则添加之
            if (destFolderPath[destFolderPath.Length - 1] != Path.DirectorySeparatorChar)
                destFolderPath += Path.DirectorySeparatorChar;
            //判断目标目录是否存在，如果不在则创建之
            if (!Directory.Exists(destFolderPath))
                Directory.CreateDirectory(destFolderPath);
            string[] fileList = Directory.GetFileSystemEntries(srcFolderPath);
            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    FolderMove(file, destFolderPath + Path.GetFileName(file));
                    //Directory.Delete(file);
                }
                else
                    File.Move(file, destFolderPath + Path.GetFileName(file));
            }
            Directory.Delete(srcFolderPath);
        }

        static string userName = "mmcs\\santec";
        static string passWord = "Mektec01!";
        static string netPath = @"\\192.168.208.237\share";
        /// <summary>
        /// 登录网络共享
        /// </summary>
        public static void LoadShare()
        {
            Process proc = new Process();
            try
            {
                string dosLine = @"net use " + netPath + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                //dosLine += "\r\n" + "del update.bat";
                //File.WriteAllText("update.bat", dosLine, ASCIIEncoding.Default);
                //System.Diagnostics.Process.Start("update.bat");

                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                //GlobalVar.gl_lastLoadNetTime = DateTime.Now;  //成功登录共享盘时间
            }
            catch (Exception ex)
            {
                //writeLog("登录共享盘异常：" + ex.ToString());
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
        }


    }
    
    public class MyGDI
    {
        public enum Direction
        {
            // 摘要:
            //     指定从左到右的渐变。
            Horizontal = 0,
            //
            // 摘要:
            //     指定从上到下的渐变。
            Vertical = 1,
            //
            // 摘要:
            //     指定从左上到右下的渐变。
            ForwardDiagonal = 2,
            //
            // 摘要:
            //     指定从右上到左下的渐变。
            BackwardDiagonal = 3,
        }
        public static void SetGradientClr(Graphics g, Rectangle rec, Color clr1, Color clr2, Direction direct)
        {
            LinearGradientBrush myBrush = new LinearGradientBrush(rec, clr1, clr2, (LinearGradientMode)direct);
            g.FillRectangle(myBrush, rec);
        }


        /// <summary>
        /// Panel边框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        /// <param name="border"></param>
        public static void DrawPanelBorder(Graphics g, object obj, Color color, int border)
        {
            Panel panel = (Panel)obj;
            ControlPaint.DrawBorder(g, panel.ClientRectangle,
                                    color, border, ButtonBorderStyle.Solid, //左边
                                    color, border, ButtonBorderStyle.Solid, //上边
                                    color, border, ButtonBorderStyle.Solid, //右边
                                    color, border, ButtonBorderStyle.Solid);//底边
        }

        /// <summary>
        /// DataGridView行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DataGridViewRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
                                                                              e.RowBounds.Location.Y,
                                                                              dg.RowHeadersWidth - 4,
                                                                              e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                  dg.RowHeadersDefaultCellStyle.Font,
                                  rectangle,
                                  dg.RowHeadersDefaultCellStyle.ForeColor,
                                  TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }


    public class OpXML
    {
        #region 常量定义
        //配置文件路径
        public const string strXML_FILE = "Config.XML";
        //xml节点名称
        public const string strNODE_ROOT = "Root";
        public const string strNODE_RUN_PARA = "RunPara";
        public const string strNODE_IMGLIST = "ImgCtrlList";
        public const string strNODE_CONFIG = "Configuration";
        public const string strNODE_MICMODEL = "MicModel";
        //Lot号
        public const string strNODE_LOTNO = "LotNO";
        //串口配置
        public const string strNODE_SERIAL_PORT = "SerialPort";
        public const string strNODE_BOUDRATE = "Boudrate";
        public const string strNODE_STOPBIT = "Stopbit";
        public const string strNODE_PARITY = "Parity";
        public const string strNODE_DATABIT = "Databit";
        //ImgCtrl配置节点
        public const string _CAM_INDEX = "CamIndex";//摄像头序号
        public const string _BAR_TYPE = "BarType"; //条码类型
        public const string _NEED_ANALYS = "NeedAnalysis"; //是否进行条码解析
        //MicModel配置节点
        public const string strNODE_LEFTMIC = "LeftMic";
        public const string strNODE_RIGHTMIC = "RightMic";
        public const string strNODE_MICBARCODE = "MicBarcode";

        #endregion
        #region Construction/Destruction
        public string m_strXMLPath = "";
        public OpXML()
        {
            try
            {
                m_strXMLPath = Application.StartupPath + "\\" + strXML_FILE;
                if (!System.IO.File.Exists(m_strXMLPath))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    xmlDoc.AppendChild(Declaration);
                    XmlNode xnRoot = xmlDoc.AppendChild(xmlDoc.CreateElement(strNODE_ROOT));
                    XmlNode xnConfig = xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_CONFIG));
                    xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_IMGLIST));
                    xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_MICMODEL));
                    xmlDoc.Save(m_strXMLPath);
                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(m_strXMLPath.Trim());
                    XmlNode root = xmlDoc.SelectSingleNode(strNODE_ROOT);
                    if (root == null || root.ToString().Trim() == "")
                    {
                        File.Delete(m_strXMLPath);
                        XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        xmlDoc.AppendChild(Declaration);
                        XmlNode xnRoot = xmlDoc.AppendChild(xmlDoc.CreateElement(strNODE_ROOT));
                        XmlNode xnConfig = xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_CONFIG));
                        xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_IMGLIST));
                        xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_MICMODEL));
                        xmlDoc.Save(m_strXMLPath);
                    }
                }
            }
            catch
            {
                File.Delete(m_strXMLPath);
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration Declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(Declaration);
                XmlNode xnRoot = xmlDoc.AppendChild(xmlDoc.CreateElement(strNODE_ROOT));
                XmlNode xnConfig = xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_CONFIG));
                xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_IMGLIST));
                xnRoot.AppendChild(xmlDoc.CreateElement(strNODE_MICMODEL));
                xmlDoc.Save(m_strXMLPath);
                return;
            }
        }
        #endregion
        /// <summary>
        /// 判断某个节点是否存在
        /// </summary>
        /// <param name="strFilePath">xml文件完整路径</param>
        /// <param name="strNodeName">节点名称</param>
        /// <returns>true表示存在，false表示不存在</returns>
        public bool NodeIsExisted(string strFilePath, string strNodeName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strFilePath.Trim());
                if (FindNode(xmlDoc.DocumentElement, strNodeName) != null)
                {
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
        /// <summary>
        /// 遍历查找指定名称的XmlNode
        /// </summary>
        /// <param name="xn">从节点xn开始查找遍历</param>
        /// <param name="strNodeName">要查找的节点名称</param>
        /// <returns>返回找到的节点</returns>
        private XmlNode FindNode(XmlNode xn, string strNodeName)
        {
            if (strNodeName == xn.Name)
            {
                return xn;
            }
            if (xn.HasChildNodes)
            {
                if (FindNode(xn.FirstChild, strNodeName) != null)
                {
                    return FindNode(xn.FirstChild, strNodeName);
                }
            }
            if (xn.NextSibling != null)
            {
                if (FindNode(xn.NextSibling, strNodeName) != null)
                {
                    return FindNode(xn.NextSibling, strNodeName);
                }
            }
            return null;
        }
        /// <summary>
        /// 查找节点,若未找到会创建该节点
        /// </summary>
        /// <param name="xmlDoc">xml文档</param>
        /// <param name="strParentNode">指定父节点进行查找，当未找到指定节点时，会在父节点下创建节点</param>
        /// <param name="strNodeName">给定的节点名称</param>
        /// <returns>返回要找的节点</returns>
        private XmlNode CustomizeSelSingleNode(XmlDocument xmlDoc, XmlNode xnParent, string strNodeName)
        {
            XmlNode xn;
            if ((xn = xnParent.SelectSingleNode(strNodeName)) != null)
            {
                return xn;
            }
            else
            {
                return xnParent.AppendChild(xmlDoc.CreateNode(XmlNodeType.Element, strNodeName, null));
            }
        }
        /// <summary>
        /// 删除指定名称的节点
        /// </summary>
        /// <param name="strFilePath">xml文档完整路径</param>
        /// <param name="strNodeName">要删除的节点名称</param>
        public void DeleteNode(string strFilePath, string strNodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilePath);
            XmlNode xn = FindNode(xmlDoc.DocumentElement, strNodeName);
            if (xn != null)
            {
                xn.RemoveAll();
            }
            xmlDoc.Save(strFilePath);
            return;
        }
        /// <summary>
        /// 对指定节点批量添加子节点
        /// </summary>
        /// <param name="strFilePath">xml文件的完整路径</param>
        /// <param name="strNodeName">指定的父节点名称</param>
        /// <param name="lsStrChildNodes">需要添加的子节点名称列表</param>
        public void AppendChildNode(string strFilePath, string strNodeName, List<string> lsStrChildNodes)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilePath);
            XmlNode xnRoot = FindNode(xmlDoc.DocumentElement, strNODE_ROOT);
            XmlNode xn = CustomizeSelSingleNode(xmlDoc, xnRoot, strNodeName);
            foreach (string str in lsStrChildNodes)
            {
                xn.AppendChild(xmlDoc.CreateElement(str));
            }
            xmlDoc.Save(strFilePath);
        }
        /// <summary>
        /// 获取某一节点的所有子节点
        /// </summary>
        /// <param name="strFilePath">xml文件的完整路径</param>
        /// <param name="strNodeName">指定父节点的名称</param>
        /// <returns>返回所有子节点名</returns>
        public List<string> GetChildNodes(string strFilePath, string strNodeName)
        {
            List<string> lsStr = new List<string> { };
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilePath);
            XmlNode xn = FindNode(xmlDoc.DocumentElement, strNodeName);
            if (xn != null)
            {
                if (xn.HasChildNodes)
                {
                    lsStr.Add(xn.FirstChild.Name);
                    xn = xn.FirstChild;
                    while ((xn = xn.NextSibling) != null)
                    {
                        lsStr.Add(xn.Name);
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return lsStr;
        }
        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="strFilePath">xml文件完整路径</param>
        /// <param name="strParentNode">需要设置的节点的父节点，用于定位指定节点</param>
        /// <param name="strNode">需要设置的节点名</param>
        /// <param name="strAttribute">需要设置的属性名称</param>
        /// <param name="strValue">需要设置的属性值</param>
        /// <returns></returns>
        public bool CustomizeSetAttribute(string strFilePath, string strParentNode, string strNode, string strAttribute, string strValue)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strFilePath);
                XmlNode xnParent = FindNode(xmlDoc.DocumentElement, strParentNode);
                XmlElement xe = (XmlElement)CustomizeSelSingleNode(xmlDoc, xnParent, strNode);
                xe.SetAttribute(strAttribute, strValue);
                xmlDoc.Save(strFilePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取节点属性值
        /// </summary>
        /// <param name="strFilePath">xml文件完整路径</param>
        /// <param name="strParentNode">制定节点的父节点名称</param>
        /// <param name="strNode">指定的节点名称</param>
        /// <param name="strAttribute">属性名</param>
        /// <returns></returns>
        public string CustomizeGetAttribute(string strFilePath, string strParentNode, string strNode, string strAttribute, string strDefaultRet)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strFilePath);
                XmlNode xnRoot = FindNode(xmlDoc.DocumentElement, strParentNode);
                //XmlElement xeParent = (XmlElement)CustomizeSelSingleNode(xmlDoc, xnRoot, strNode);
                XmlElement xeNode = (XmlElement)CustomizeSelSingleNode(xmlDoc, xnRoot, strNode);
                xmlDoc.Save(strFilePath);
                return (xeNode.GetAttribute(strAttribute) != "") ? xeNode.GetAttribute(strAttribute) : strDefaultRet;
            }
            catch (Exception ex)
            {
                return strDefaultRet;
            }
        }
        /// <summary>
        /// 重命名节点
        /// </summary>
        /// <param name="strFilePath">XML文件完整路径</param>
        /// <param name="strOldNode">之前的节点名称</param>
        /// <param name="strNewNode">新的节点名称</param>
        /// <returns>true表示重命名成功，false则是失败</returns>
        public bool RenameNode(string strFilePath, string strOldNode, string strNewNode)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strFilePath);
                XmlNode xnOldNode = FindNode(xmlDoc.DocumentElement, strOldNode);
                if (xnOldNode == null)
                {
                    throw new Exception("未找到该名称的节点！");
                }
                XmlNode xnNewNode = xnOldNode.ParentNode.AppendChild(xmlDoc.CreateNode(XmlNodeType.Element, strNewNode, null));
                while (xnOldNode.HasChildNodes)
                {
                    xnNewNode.AppendChild(xnOldNode.FirstChild);
                    //xnOldNode.RemoveChild(xnOldNode.FirstChild);
                }
                xnOldNode.ParentNode.RemoveChild(xnOldNode);
                xmlDoc.Save(strFilePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
    
    class ProgramInfo
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public static string ProductName
        {
            get
            {
                return Application.ProductName;
            }
        }
        /// <summary>
        /// 获取程序集完整名称(包含扩展名)
        /// </summary>
        public static string AssemblyFullName
        {
            get { return System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath); }
        }
        /// <summary>
        /// 获取程序集版本,界面显示
        /// </summary>
        public static string AssemblyVersion
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        /// <summary>
        /// 获取程序文件版本，用于升级
        /// </summary>
        public static string FileVersion
        {
            get
            {
                System.Diagnostics.FileVersionInfo fileVer =
                    System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);
                return fileVer.FileVersion;
            }
        }
        /// <summary>
        /// 获取当前程序进程名
        /// </summary>
        public static string CurrentProcName
        {
            get { return System.Diagnostics.Process.GetCurrentProcess().ProcessName; }
        }
        /// <summary>
        /// 检查当前是否有程序实例在运行
        /// </summary>
        /// <returns></returns>
        public static bool CheckAppOnRunning()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(CurrentProcName);
            return processes.Length > 1;
        }
    }

    
}
