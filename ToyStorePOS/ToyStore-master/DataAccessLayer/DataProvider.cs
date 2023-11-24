﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public class DataProvider
    {
        public static SqlConnection Openconnect()
        {
            //string sChuoiKetNoi = @"Data Source=LAPTOP-AFOP5TPF\SQLSERVER;Initial Catalog=CH_NOITHAT_DB;Integrated Security=True";

            string sChuoiKetNoi = @"Data Source=MYNAMEISKHOA\MINHKHOA666;Initial Catalog=CH_NOITHAT_DB;Integrated Security=True";

            //nghia
           // string sChuoiKetNoi = @"Data Source=DESKTOP-UOI8PTB;Initial Catalog=CH_NOITHAT_DB;Integrated Security=True";

            SqlConnection con = new SqlConnection(sChuoiKetNoi);
            Console.WriteLine(con);
            con.Open();
            if (con != null)
            {
                Console.WriteLine("connect duoc roi do bro");
                return con;
            }
            else
            {
                Console.WriteLine("Khong co connect duoc");
                return null;
            }
            
        }
        public static void Disconnect(SqlConnection con)
        {
            con.Close();
        }
        public static int JustExcuteNoParameter(string sql)
        {
            SqlConnection con = Openconnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            int rows = cmd.ExecuteNonQuery();
            Disconnect(con);
            if (rows > 0)
            {
                return rows;
            }
            else
            {
                return -1;
            }
        }
        public static DataTable GetTable(string sql)
        {
            SqlConnection con = Openconnect();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Disconnect(con);
            return dt;
        }
    }
}
