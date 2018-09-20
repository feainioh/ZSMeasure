using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ZSMeasure
{
    class DBQuery
    {
        public string m_str_SheetSNMaster = "";
        public string m_str_SheetSNSlave = "";
        public string m_str_LotNo = "";
        public string m_str_PinMu = "";
        public string m_str_OPID = "";
        public string m_str_SHIFT = "";
        public string m_str_MapNum = "";

        //suzsql08: 192.168.208.221
        SqlConnection m_conn = null;
        string str_as400 = "Provider=IBMDA400.DataSource.1;Password=MMCSUSR;Persist Security Info=True;User ID=MMCSUSR;Data Source=mmcsas1;Protection Level=None;"+
                            "Initial Catalog=;Transport Product=Client Access;SSL=DEFAULT;Force Translate=65535;Default Collection=EBSFLIB;Convert Date Time To Char=TRUE;"+
                            "Catalog Library List=;Cursor Sensitivity=3";        
        const string str_SuzsqlV01_BARDATA = "Data Source=SUZSQLV01;database=BARDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
        const string str_SuzsqlV01_BaseData = "Data Source=SUZSQLV01;database=BASEDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
        const string str_Suzsql07_MekData = "Data Source=SUZSQL07;database=MekData;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
        const string str_SuzsqlV01_EM = "Data Source=SUZSQLV01;database=EM;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
        //const String str2 = "Provider=SQLOLEDB;pwd=MMCSUSR;Persist Security Info=True;uid=MMCSUSR;Data Source=MEKFLIB;Connect Timeout=1";
        //const String str_sql08_BARDATA = "Data Source=192.168.208.221;database=BARDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=1";
        //const string str_Sql08_BaseData = "Data Source=suzsql08;database=BASEDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=5";

        public DBQuery()
        {
            m_conn = new SqlConnection(str_SuzsqlV01_BARDATA);
        }

        //lot号查询出对应的品目
        public String checkLotByS400(String lot)
        {
            OleDbConnection con = getConnection3();
            OleDbDataReader reader = null;
            String sql = "select ZHSZNO,ZHLTNO,ZHHMCD,ZHREDO from MZSODRP where ZHSZNO='" + lot.Substring(0, 8) +
           "'" + " and ZHLTNO ='" + lot.Substring(8, 3) + "'";
            String result = "";
            try
            {
                con.Open();
                OleDbCommand comm = new OleDbCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    result = reader.GetValue(2).ToString();
                    string bonusLot = reader.GetValue(3).ToString();
                    //if (bonusLot == "")
                    //    GlobalVar.glBonusLot = 1; //此lot号不是BonusLot
                    //else
                    //    GlobalVar.glBonusLot = 0; //此lot号是BonusLot，不做限定
                }
                reader.Close();
                con.Close();
            }
            catch //(Exception en)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return result;

            }
            return result;
        }
        //根据品目号查找机种
        public string GetProductModel(String strProduct)
        {
            //GlobalVar.gl_str_productModel = "";
            string strRet = "";
            SqlConnection con = new SqlConnection(str_SuzsqlV01_BaseData);
            SqlDataReader reader = null;
            String sql = "select ProductModel from ProjectBasic Where Product='" + strProduct + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader.GetValue(0).ToString();
                }
                reader.Close();
                con.Close();
                if (strRet.Length <= 0)
                {
                    return "";
                }
                string strconn = GetDBConnStrByProductModel(strRet);
                m_connProM = new SqlConnection(strconn);
                return strRet;
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return "";
            }
        }
        private OleDbConnection getConnection3()   //AS400
        {
            try
            {
                str_as400 = "Provider=IBMDA400.DataSource.1;Password=MMCSUSR;Persist Security Info=True;User ID=MMCSUSR;Data Source=mmcsas1;" +
                            "Protection Level=None;Initial Catalog=;Transport Product=Client Access;SSL=DEFAULT;Force Translate=65535; "+
                            "Default Collection=" + GlobalVar.gl_strDefaultODBC + ";Convert Date Time To Char=TRUE;Catalog Library List=;Cursor Sensitivity=3";
                OleDbConnection _ocon = new OleDbConnection();
                _ocon = new OleDbConnection(str_as400);
                return _ocon;
            }
            catch { return new OleDbConnection(); }
        }

        public bool is_datatable_empty(DataTable dt)
        {
            try
            {
                if (dt == null) return true;
                if (dt.Rows.Count <= 0) return true;
                if (dt.Rows[0][0] == DBNull.Value) return true;
                if (dt.Rows[0][0].ToString() == null) return true;
                if (dt.Rows[0][0].ToString().Length <= 0) return true;
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public DataTable get_database_cmd(string theSQL)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(theSQL, m_conn);
                DataSet theDataSet = new DataSet();
                adapter.Fill(theDataSet);
                return theDataSet.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public DataTable GetLineListFromDB()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        m_conn.Open();
        //        string sql = "SELECT [LineName],[ReferenceName] FROM [BARDATA].[dbo].[LineList] WHERE [LineName]='" + GlobalVar.gl_LineName + "'";
        //        SqlDataAdapter da = new SqlDataAdapter(sql, m_conn);
        //        da.Fill(dt);
        //    }
        //    catch { }
        //    finally { m_conn.Close(); }
        //    return dt;
        //}


        //执行sql语句
        public int ExecuteSQL(string strSql)
        {
            int nRet;
            try
            {
                m_conn.Open();
                using (SqlCommand cmd = new SqlCommand(strSql, m_conn))
                {
                    nRet = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                //myfunction.appendNewLogMessage("执行SQL语句【" + strSql + "】异常：" + ex.ToString(), 2);
                MessageBox.Show(ex.Message);
                nRet = -1;
            }
            m_conn.Close();
            return nRet;
        }

        //2017.03 打孔前检查有无EC电测
        public bool CheckBeforeTest(string shtbarcode)
        {
            string strSql = string.Format("SELECT TOP 1 ShtBarcode FROM SUZSQLV01.EM.DBO.ECTEST WHERE ShtBarcode = '{0}'", shtbarcode);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                cmd.Connection = new SqlConnection(str_SuzsqlV01_EM);
                cmd.CommandText = strSql;
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //myfunction.appendNewLogMessage("检查前工程电测异常：" + ex.ToString(), 2);
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                cmd.Dispose();
                if (reader != null) reader.Dispose();
            }
        }

        //根据LOT号查找整LOT数量，会先从LOT分割表(LotSeparation)进行查找，若未找到，则从
        //LotLink表中根据后工程LOT号获取对应前工程LOT号，再通过PreLotBasic表获取数量
        public static int GetQtyByLot(string strLot)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                int qty = 0;
                cmd.CommandText = string.Format("SELECT Qty FROM [BARDATA].[dbo].[LotSeparation] WHERE LotNo = '{0}' and ISNULL(DeletedFlag,'') <> '1'",
                    strLot);
                cmd.Connection = new SqlConnection(str_SuzsqlV01_BARDATA);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return int.Parse(reader.GetValue(0).ToString());
                }
                else
                {
                    reader.Close();
                    cmd.Connection.Close();
                    //cmd.CommandText = string.Format("SELECT Qty FROM [BARDATA].[dbo].[PreLotBasic] WHERE PLotNo = '{0}' and ISNULL(DeletedFlag,'') <> '1'",
                    //GetPreLotByLot(strLot));
                    cmd.CommandText = string.Format("SELECT Qty FROM [BARDATA].[dbo].[PreLotBasic] WHERE PLotNo in ({0}) and ISNULL(DeletedFlag,'') <> '1'",
                    GetPreLotByLot(strLot));
                    cmd.Connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        qty += int.Parse(reader.GetValue(0).ToString());
                    }
                    if (qty > 0) return qty;
                    else
                    {
                        MessageBox.Show("未获取LOT数量信息!");
                        throw new Exception("未获取LOT数量信息!");
                    }
                }
            }
        }

        //根据后工程LOT获取前工程LOT
        public static string GetPreLotByLot(string strLot)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string strsPlotNo = "";
                List<string> listPlotNo = new List<string>(); //2016.07.26 一个后工程lot可能对应多个前工程lot号
                cmd.Connection = new SqlConnection(str_SuzsqlV01_BARDATA);
                cmd.CommandText = string.Format("SELECT PLotNo FROM [BARDATA].dbo.LotLink WHERE LotNo = '{0}' AND ISNULL(DeletedFlag,'')<>'1'", strLot);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string str = reader.GetValue(0).ToString();
                    listPlotNo.Add(reader.GetValue(0).ToString());
                }
                if (listPlotNo != null && listPlotNo.Count > 0)
                {
                    for (int i = 0; i < listPlotNo.Count; i++)
                    {
                        strsPlotNo += "'" + listPlotNo[i] + "',";
                    }
                    return strsPlotNo.TrimEnd(',');
                }
                else
                {
                    MessageBox.Show("获取前工程LOT失败!");
                    throw new Exception("获取前工程LOT失败!");
                }
            }
        }

        public static bool IsSpecialLot(string strLot)
        {
            string strSql = string.Format("SELECT COUNT(*) FROM BARDATA.dbo.SpecialLot WHERE LotNo = '{0}'", strLot);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                cmd.Connection = new SqlConnection(str_SuzsqlV01_BARDATA);
                cmd.CommandText = strSql;
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Convert.ToInt32(reader.GetValue(0)) > 0;
                }
                return false;
            }
            catch (Exception ex)            {
                
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                cmd.Dispose();
                if (reader != null) reader.Dispose();
            }
        }

        public static bool CheckLotInfo(string strLotNo, string strSheet)
        {
            if (IsSpecialLot(strLotNo)) return true;    //特权Lot不需要检查
            //if (GlobalVar.glBonusLot == 0) return true; //BonusLot不需要检查
            //if (strSheet.Substring(0, 1).ToUpper() == "R") return true; //重工sheet条码不检查
            string strSql = string.Format("select LotNo from LotLink where PLotNo in (select PLotNo from Lot_Sht_Info where ShtBarcode='{0}' and ISNULL(DeletedFlag,'') <> '1')", strSheet);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                cmd.Connection = new SqlConnection(str_SuzsqlV01_BARDATA);
                cmd.CommandText = strSql;
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();
                List<string> lsLotNo = new List<string> { };
                while (reader.Read())
                {
                    lsLotNo.Add(reader.GetValue(0).ToString().Trim());
                }
                return lsLotNo.Exists(a => (a.ToUpper() == strLotNo.ToUpper().Trim()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                cmd.Dispose();
                if (reader != null) reader.Dispose();
            }
        }

        //检查是否是PCS管理版本
        public static bool CheckPcsManage(string strProductModel)
        {
            string strRet = "";
            SqlConnection con = new SqlConnection(str_SuzsqlV01_BaseData);
            SqlDataReader reader = null;
            String sql = "select Flag1 from ModelList Where ProductModel='" + strProductModel + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader.GetValue(0).ToString().Trim();
                }
                reader.Close();
                con.Close();
                if (strRet.Length <= 0)
                {
                    return false;
                }
                return strRet == "1";
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return false;
            }
        }

        private SqlConnection m_connProM = null;
        /// <summary>
        /// 根据机种设置数据库连接字符串
        /// </summary>
        /// <param name="strProductModel"></param>
        /// <returns></returns>
        private string GetDBConnStrByProductModel(String strProductModel)
        {
            string strRetConn = "";
            string m_strDBNAME = "", m_strServerName = "", m_strAccountName = "", m_strPassword = "";
            //SqlConnection con = new SqlConnection(str_Sql08_BaseData);
            SqlConnection con = new SqlConnection(str_SuzsqlV01_BaseData);
            SqlDataReader reader = null;
            String sql = "select DBNAME,ServerName,AccountName,Password from ModelList Where ProductModel='" + strProductModel.Trim(' ') + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    m_strDBNAME = reader.GetValue(0).ToString();
                    m_strServerName = reader.GetValue(1).ToString();
                    m_strAccountName = reader.GetValue(2).ToString();
                    m_strPassword = reader.GetValue(3).ToString();
                    if ((m_strDBNAME == "") || (m_strServerName == "") ||
                        (m_strAccountName == "") || (m_strPassword == ""))
                    {
                        return strRetConn;
                    }
                    else
                    {
                        strRetConn = "Data Source=" + m_strServerName + ";database=" + m_strDBNAME + ";uid=" + m_strAccountName + ";pwd=" + m_strPassword + ";Connect Timeout=5";
                    }
                }
                reader.Close();
                con.Close();
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
            }
            return strRetConn;
        }

        /// <summary>
        /// 检查前工程来料电测数据是否完整（条件为台湾SHT条码，可根据MMCS SHT条码去各机种的BarcodeLink表中获取）
        /// </summary>
        /// <param name="shtBar"></param>
        public void CheckTWElecTest(string shtBar, int count)
        {
            string sqlTWsht = "";
            using(SqlCommand comm = new SqlCommand())
            {
                m_connProM.Open();
                comm.Connection = m_connProM;
                comm.CommandText = string.Format("SELECT TOP 1 Barcode  FROM [BarcodeLink] WHERE ShtBarcode = '{0}' AND FlowId = 5 AND ISNULL(DeletedFlag,'')<>'1' " +
                                                 "ORDER BY CreateDate DESC", 
                                                 shtBar);
                using (SqlDataReader reader = comm.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sqlTWsht = reader.GetValue(0).ToString();
                    }
                }
            }
            if (sqlTWsht == "")
            {
                MessageBox.Show(string.Format("该sht条码【{0}】未关联台湾sht条码", shtBar));
                return;
            }
            using (SqlConnection conn = new SqlConnection(str_Suzsql07_MekData))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = string.Format("SELECT COUNT(0) FROM [BefTest_OnLine] WHERE SheetNO = '{0}'",
                                                     sqlTWsht);
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string str = reader.GetValue(0).ToString();
                            if (count != Convert.ToInt32(str))
                            {
                                MessageBox.Show("前工程来料台湾电测数据缺失");
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 检查MMCS电测数据是否完整(MMCS SHT条码)
        /// </summary>
        /// <param name="sthBar"></param>
        /// <param name="count"></param>
        public void CheckMMCSElecTest(string sthBar, int count)
        {
            using (SqlConnection conn = new SqlConnection(str_SuzsqlV01_EM))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = string.Format("SELECT COUNT(0) FROM [BefTest] WHERE SheetNO = '{0}'",
                                                     sthBar);
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string str = reader.GetValue(0).ToString();
                            if (count != Convert.ToInt32(str))
                            {
                                MessageBox.Show("MMCS电测数据缺失");
                                return;
                            }
                        }
                    }
                }
            }
        }


    }
}
