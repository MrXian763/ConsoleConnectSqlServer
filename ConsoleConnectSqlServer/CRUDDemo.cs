using System;
using System.Data;

namespace ConsoleConnectSqlServer
{
    public class CRUDDemo
    {
        /// <summary>
        /// 增删改查演示
        /// </summary>
        public static void ShowCRUD()
        {
            #region 新增
            string userName = "1111";
            string password = "222";
            string nickName = "333";
            string insertSql = $"insert into UserT values('{userName}', '{password}', '{nickName}');";
            int count = SqlHelper.EditData(insertSql);
            if (count > 0)
                Console.WriteLine($"新增成功，{count}行受影响");
            else
                Console.WriteLine($"新增失败，{count}行受影响");
            #endregion

            #region 修改
            count = 0;
            string updateSql = $"update UserT set UserName = '更改后的用户名' where UserName =  '{userName}'";
            count = SqlHelper.EditData(updateSql);
            if (count > 0)
                Console.WriteLine($"修改成功，{count}行受影响");
            else
                Console.WriteLine($"修改成功，{count}行受影响");
            #endregion

            #region 删除
            count = 0;
            userName = "1111";
            string deleteSql = $"delete from UserT where UserName = '{userName}'";
            count = SqlHelper.EditData(deleteSql);
            if (count > 0)
                Console.WriteLine($"删除成功，{count}行受影响");
            else
                Console.WriteLine($"删除成功，{count}行受影响");
            #endregion

            #region 查询
            Console.WriteLine("查询数据：");
            string selectSql = "select * from UserT";
            DataTable table = SqlHelper.SelectData(selectSql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Console.WriteLine($"{table.Rows[i]["UserName"]}  {table.Rows[i]["Password"]}  {table.Rows[i]["NickName"]}");
            }
            #endregion
        }
    }
}
