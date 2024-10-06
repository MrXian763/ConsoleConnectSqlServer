using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleConnectSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 新增
            string userName = "1111";
            string password = "222";
            string nickName = "333";
            string insertSql = $"insert into UserT values('{userName}', '{password}', '{nickName}');";
            int count = EditData(insertSql);
            if (count > 0)
                Console.WriteLine($"新增成功，{count}行受影响");
            else
                Console.WriteLine($"新增失败，{count}行受影响");
            #endregion

            #region 修改
            count = 0;
            string updateSql = $"update UserT set UserName = '更改后的用户名' where UserName =  '{userName}'";
            count = EditData(updateSql);
            if (count > 0)
                Console.WriteLine($"修改成功，{count}行受影响");
            else
                Console.WriteLine($"修改成功，{count}行受影响");
            #endregion

            #region 删除
            count = 0;
            userName = "1111";
            string deleteSql = $"delete from UserT where UserName = '{userName}'";
            count = EditData(deleteSql);
            if (count > 0)
                Console.WriteLine($"删除成功，{count}行受影响");
            else
                Console.WriteLine($"删除成功，{count}行受影响");
            #endregion

            #region 查询
            Console.WriteLine("查询数据：");
            string selectSql = "select * from UserT";
            DataTable table = SelectData(selectSql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Console.WriteLine($"{table.Rows[i]["UserName"]}  {table.Rows[i]["Password"]}  {table.Rows[i]["NickName"]}");
            }
            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// 执行修改语句
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>受影响行数</returns>
        private static int EditData(string sql)
        {
            // 连接数据库
            SqlConnection connection = new SqlConnection();
            // 连接信息
            connection.ConnectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true;";
            connection.Open(); // 打开连接
            SqlCommand command = new SqlCommand(sql, connection);
            int count = 0 ;
            try
            {
                count = command.ExecuteNonQuery(); // 执行新增语句，返回受影响行数
            }
            catch (Exception ex)
            {
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
        private static DataTable SelectData(string sql)
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
