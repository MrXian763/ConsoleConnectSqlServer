using System;
using System.Collections.Generic;
using System.Data;

namespace ConsoleConnectSqlServer
{
    public class UserTOperation
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>当前登录用户信息</returns>
        public static UserTModel Login(string userName, string password)
        {
            // 将用户的输入与数据库中的数据比对
            string selectSql = $"select * from UserT where UserName = '{userName}' and Password = '{password}'";
            DataTable table = SqlHelper.SelectData(selectSql);
            if (table.Rows.Count < 1)
                return null;
            return DataRowToUserTModel(table.Rows[0]);
        }

        /// <summary>
        /// 更新用户性别
        /// </summary>
        /// <param name="sex">性别 1-男；2-女</param>
        /// <param name="loginUser">当前登录用户</param>
        /// <returns>更新结果</returns>
        public static int UpdateSex(string sex, UserTModel loginUser)
        {
            string updateSql = $"update UserT set Sex = '{sex}' where UserName = '{loginUser.UserName}';";
            return SqlHelper.EditData(updateSql);
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns>用户集合</returns>
        public static List<UserTModel> QueryAllUser()
        {
            string selectAllUser = "select * from UserT;";
            DataTable allUserDataTable = SqlHelper.SelectData(selectAllUser);
            // 将查询出的用户对象添加到集合
            List<UserTModel> users = new List<UserTModel>();
            for (int i = 0; i < allUserDataTable.Rows.Count; i++)
            {
                UserTModel model = DataRowToUserTModel(allUserDataTable.Rows[i]);
                users.Add(model);
            }
            return users;
        }

        public static UserTModel DataRowToUserTModel(DataRow row)
        {
            UserTModel userTModel = new UserTModel();
            userTModel.UserName = row["UserName"].ToString();
            userTModel.NickName = row["NickName"].ToString();
            userTModel.Password = row["Password"].ToString();
            userTModel.Sex = row["Sex"].ToString();
            return userTModel;
        }
    }
}
