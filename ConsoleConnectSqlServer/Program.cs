using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleConnectSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {

            // 选择语言
            Console.WriteLine("请选择系统语言：1-中文 2-English");
            string inputLang = Console.ReadLine();

            if (inputLang == "2")
            {
                InfoHelper.Sex1 = "Male";
                InfoHelper.Sex2 = "Female"; 
                InfoHelper.WelcomeInfo = "Welcome to the login system!!!";
                InfoHelper.InputTipInfo = "Please enter your username and password below";
                InfoHelper.InputUserNameInfo = "Please enter username:";
                InfoHelper.InputPasswordInfo = "Please input a password:";
                InfoHelper.WrongUserNameOrPasswordInfo = "Username or password error, please try again!";
                InfoHelper.WelcomeUseInfo = "Welcome to use: @NickName";
                InfoHelper.InputSexInfo = "Dear @NickName, please enter your gender information";
                InfoHelper.SexSelectInfo = "Please select gender: 1- Male; 2- Female";
                InfoHelper.InputWrongInfo = "Input error, please try again";
                InfoHelper.UpdateSuccessInfo = "Update successful!";
                InfoHelper.UpdateErrInfo = "Update failed!";
                InfoHelper.SelectAllUserInfo = "Query all user information";
                InfoHelper.UserOutputInfo = "Username: @UserName; Nickname: @NickName; Gender: @Sex";
            }
            else
            {
                InfoHelper.Sex1 = "男";
                InfoHelper.Sex2 = "女";
                InfoHelper.WelcomeInfo = "欢迎访问登录系统!!!";
                InfoHelper.InputTipInfo = "请在下方输入您的用户名和密码";
                InfoHelper.InputUserNameInfo = "请输入用户名：";
                InfoHelper.InputPasswordInfo = "请输入密码：";
                InfoHelper.WrongUserNameOrPasswordInfo = "用户名或密码错误，请重试!";
                InfoHelper.WelcomeUseInfo = "欢迎使用：@NickName";
                InfoHelper.InputSexInfo = "尊敬的@NickName，请录入性别信息";
                InfoHelper.SexSelectInfo = "请选择性别：1-男；2-女";
                InfoHelper.InputWrongInfo = "输入有误，请重试";
                InfoHelper.UpdateSuccessInfo = "更新成功！";
                InfoHelper.UpdateErrInfo = "更新失败！";
                InfoHelper.SelectAllUserInfo = "查询所有用户信息";
                InfoHelper.UserOutputInfo = "用户名：@UserName; 昵称：@NickName; 性别：@Sex";
            }

            #region 登录
            // 打印系统基本信息
            Console.WriteLine(InfoHelper.WelcomeInfo);
            Console.WriteLine(InfoHelper.InputTipInfo);
            DataTable dt;
            // 验证用户名和密码
            while (true)
            {
                Console.Write(InfoHelper.InputUserNameInfo);
                string inputUserName = Console.ReadLine();
                Console.Write(InfoHelper.InputPasswordInfo);
                string inputPassword = Console.ReadLine();

                // 将用户的输入与数据库中的数据比对
                string selectSql = $"select * from UserT where UserName = '{inputUserName}' and Password = '{inputPassword}'";
                dt = SelectData(selectSql);

                int count = dt.Rows.Count;
                if (count <= 0)
                    Console.WriteLine(InfoHelper.WrongUserNameOrPasswordInfo);
                else
                    break;
            }
            string str = InfoHelper.WelcomeUseInfo.Replace("@NickName", dt.Rows[0]["NickName"].ToString());
            Console.WriteLine(str);
            // 读取性别，如果性别为空强制用户录入
            string sex = dt.Rows[0]["Sex"].ToString();
            if (string.IsNullOrEmpty(sex))
            {
                str = InfoHelper.InputSexInfo.Replace("@NickName", dt.Rows[0]["NickName"].ToString());
                Console.WriteLine(str);
                while (true)
                {
                    Console.WriteLine(InfoHelper.SexSelectInfo);
                    string selectSex = Console.ReadLine();
                    if (selectSex != "1" && selectSex != "2")
                    {
                        Console.WriteLine(InfoHelper.InputWrongInfo);
                        continue;
                    }

                    // 更新性别到数据库
                    string updateSql = $"update UserT set Sex = '{selectSex}' where UserName = '{dt.Rows[0]["UserName"]}';";
                    int count = EditData(updateSql);
                    if (count > 0)
                        Console.WriteLine(InfoHelper.UpdateSuccessInfo);
                    else
                        Console.WriteLine(InfoHelper.UpdateErrInfo);
                    break;
                }
                
            }

            // 查询展示所有用户列表   
            string selectAllUser = "select * from UserT;";
            dt = SelectData(selectAllUser);
            Console.WriteLine(InfoHelper.SelectAllUserInfo);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sex1 = dt.Rows[i]["Sex"].ToString();
                if (sex1 == "1")
                    sex1 = InfoHelper.Sex1;
                else if (sex1 == "2")
                    sex1 = InfoHelper.Sex2;

                str = InfoHelper.UserOutputInfo;
                str = str.Replace("@NickName", dt.Rows[i]["NickName"].ToString());
                str = str.Replace("@Sex", dt.Rows[i]["Sex"].ToString());
                str = str.Replace("@UserName", dt.Rows[i]["UserName"].ToString());
                Console.WriteLine(str);
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

        /// <summary>
        /// 增删改查演示
        /// </summary>
        private static void ShowCRUD()
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
        }
    }
}
