using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data.MySqlClient;

namespace l4dhelper.Data.MySqlClient
{
    public class MySqlQuery
    {
        internal static MySqlClient db;
        
        internal static string host { get; set; }
        internal static string port { get; set; }
        internal static string username { get; set; }
        internal static string password { get; set; }
        internal static string database { get; set; }

        public MySqlQuery(MySqlClient mysqlClient)
        {
            db = mysqlClient;
        }

        public static bool select_bool(string query)
        {
            var result = db.Select(query);
            return result;
        }

        public static DataTable select_dt(string query)
        {
            var result = db.DataTable(query);
            return result;
        }

        public static Int64 select_int64(string query)
        {
            var result = db.Select_ReturnInt(query);
            return result;
        }

        public static bool execute(string query)
        {
            try
            {
                var result = db.Execute(query);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class MySqlClient
    {
        MySqlConnection mysqlCon;

        public static Int64 Id { get; set; }

        public static string ConnectionString { get; set; }

        public MySqlClient(string host, string port, string username, string password, string database)
        {
            ConnectionString = "Server=" + host + "; Port=" + port + "; Uid=" + username + "; Password=" + password + "; Database=" + database + ";";
        }

        public static Int64 Count(DataTable dt)
        {
            Int64 c = dt != null ? dt.Rows.Count : 0;
            return c;
        }

        public bool Test_Connection()
        {
            try
            {
                var con = Connect();
                con.Open();
                if (IsOpen)
                {
                    this.Close();
                    return true;
                }
            }
            catch (Exception ex)
            { }
            return false;
        }

        private MySqlConnection Connect()
        {
            try
            {
                mysqlCon = new MySqlConnection(ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mysqlCon;
        }

        public bool IsOpen
        {
            get
            {
                bool open = mysqlCon.State == ConnectionState.Open ? true : false;
                return open;
            }
        }

        public bool Close()
        {
            if (mysqlCon.State == ConnectionState.Open)
            {
                mysqlCon.Dispose();
                mysqlCon.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Select(string query)
        {

            using (var mysqlCon = Connect())
            {
                using (MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon))
                {
                    try
                    {
                        if (!IsOpen) { mysqlCon.Open(); }

                        MySqlDataReader mysqlDr = mysqlCom.ExecuteReader();
                        if (mysqlDr.Read())
                        {
                            Id = Convert.ToInt64(mysqlDr[0]);
                            return true;
                        }
                        mysqlDr.Dispose();
                        mysqlDr.Close();
                        Close();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public int Select_ReturnInt32(string query)
        {
            using (var mysqlCon = Connect())
            {
                using (MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon))
                {
                    try
                    {
                        if (!IsOpen) { mysqlCon.Open(); }

                        int count = -99;
                        MySqlDataReader mysqlDr = mysqlCom.ExecuteReader();
                        if (mysqlDr.Read())
                        {
                            count = Convert.ToInt32(mysqlDr[0]);
                        }
                        mysqlDr.Dispose();
                        mysqlDr.Close();
                        Close();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public Int64 Select_ReturnInt(string query)
        {

            using (var mysqlCon = Connect())
            {
                using (MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon))
                {
                    try
                    {
                        if (!IsOpen) { mysqlCon.Open(); }

                        MySqlDataReader mysqlDr = mysqlCom.ExecuteReader();
                        if (mysqlDr.Read())
                        {
                            Int64 count = Convert.ToInt64(mysqlDr[0]);
                            return count;
                        }
                        mysqlDr.Dispose();
                        mysqlDr.Close();
                        Close();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public decimal Select_ReturnDecimal(string query)
        {

            using (var mysqlCon = Connect())
            {
                using (MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon))
                {
                    try
                    {
                        if (!IsOpen) { mysqlCon.Open(); }

                        MySqlDataReader mysqlDr = mysqlCom.ExecuteReader();
                        if (mysqlDr.Read())
                        {
                            decimal count = Convert.ToDecimal(mysqlDr[0]);
                            return count;
                        }

                        mysqlDr.Dispose();
                        mysqlDr.Close();
                        Close();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public DataTable DataTable(string query)
        {
            DataTable dTable = new DataTable("DataTable");
            
            using (var mysqlCon = Connect())
            {
                using (MySqlDataAdapter mysqlDa = new MySqlDataAdapter(query, mysqlCon))
                {
                    try
                    {
                        if (!IsOpen) { mysqlCon.Open(); }

                        mysqlDa.Fill(dTable);
                        dTable = (dTable.Rows.Count > 0) ? dTable : null;
                        mysqlDa.Dispose();
                        Close();
                        return dTable;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public bool Execute(string query)
        {
            using (var mysqlCon = Connect())
            {
                using (MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon))
                {
                    try
                    {
                        if (!IsOpen) { mysqlCon.Open(); }

                        mysqlCom.ExecuteNonQuery();
                        Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public bool ExecuteWithParams(string query, string[] contents)
        {
            using (var mysqlCon = Connect())
            {
                try
                {
                    MySqlCommand mysqlCom = new MySqlCommand(query, mysqlCon);
                    mysqlCom.CommandType = CommandType.Text;
                    mysqlCom.Connection = mysqlCon;

                    for (int i = 0; i < contents.Length; i++)
                    {
                        string[] contentSplits = contents[i].Split(',');
                        mysqlCom.Parameters.AddWithValue(contentSplits[0], contentSplits[1]);
                    }
                    mysqlCon.Open();
                    mysqlCom.ExecuteNonQuery();
                    Close();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
