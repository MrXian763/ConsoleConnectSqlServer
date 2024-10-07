using System;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleConnectSqlServer
{
    public class SqlHelper
    {
        /// <summary>
        /// 执行修改语句
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>受影响行数</returns>
        public static int EditData(string sql)
        {
            // 连接数据库
            SqlConnection connection = new SqlConnection();
            // 连接信息
            connection.ConnectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true;";
            connection.Open(); // 打开连接
            SqlCommand command = new SqlCommand(sql, connection);
            int count = 0;
            try
            {
                count = command.ExecuteNonQuery(); // 执行新增语句，返回受影响行数
            }
            catch (Exception ex)
            {
                count = -1;
                Console.WriteLine(ex);
            }
            connection.Close(); // 关闭连接
            return count;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns>查询出来的数据</returns>
        public static DataTable SelectData(string sql)
        {
            // 连接数据库
            SqlConnection connection = new SqlConnection();
            // 连接信息
            connection.ConnectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true;";
            connection.Open(); // 打开连接
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = sql; // 查询语句

            // 提交sql语句
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            // 得到返回结果
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable table = ds.Tables[0];
            return table;
        }
    }
}
