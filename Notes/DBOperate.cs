using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Notes
{
    public class DBOperate
    {
        private static string StrConnectionString= "Server=cloud2.wxcsxy.top;Port=33060;UserId=root;Password=equipment@SSD;Database=exam";
        public DBOperate() { 

        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="strSql"></param>
        public static void ExecuteSql(string strSql) {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(StrConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = strSql;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
            }
            catch
            {
                throw;
                
            }
        }

        /// <summary>
        /// 根据SQL获取数据表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable GetSqlDataTable(string strSql)
        {
            DataTable dtResult = new DataTable();
            using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(StrConnectionString))
            {
                conn.Open();
                DataSet ds=new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter(strSql,conn);
                adapter.Fill(ds);
                dtResult=ds.Tables[0];
                
               conn.Close();
            }
            return dtResult;
        }

    }

  
}
