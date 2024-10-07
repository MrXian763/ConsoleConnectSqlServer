using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserTAndUserScoresTOperation
    {
        /// <summary>
        /// 加载用户以及成绩数据
        /// </summary>
        public static List<UserTAndUserScoresTModel> QueryAllData()
        {
            string sql = "select u.UserName, u.Password, u.NickName, u.Sex, s.Chinese, s.English, s.Math, s.RecordTime " +
                "from UserT as u left join UserScoresT as s on u.UserName = s.UserName;";
            DataTable allDataTable = SqlHelper.SelectData(sql);
            // 将查询出的用户对象添加到集合
            List<UserTAndUserScoresTModel> datas = new List<UserTAndUserScoresTModel>();
            for (int i = 0; i < allDataTable.Rows.Count; i++)
            {
                UserTAndUserScoresTModel model = DataRowToModel(allDataTable.Rows[i]);
                datas.Add(model);
            }
            return datas;
        }

        public static UserTAndUserScoresTModel DataRowToModel(DataRow row)
        {
            UserTAndUserScoresTModel model = new UserTAndUserScoresTModel();
            model.UserName = row["UserName"].ToString();
            model.NickName = row["NickName"].ToString();
            model.Password = row["Password"].ToString();
            model.Sex = row["Sex"].ToString();
            model.Chinese = string.IsNullOrEmpty(row["Chinese"].ToString()) ? 0 : float.Parse(row["Chinese"].ToString());
            model.English = string.IsNullOrEmpty(row["English"].ToString()) ? 0 : float.Parse(row["English"].ToString());
            model.Math = string.IsNullOrEmpty(row["Math"].ToString()) ? 0 : float.Parse(row["Math"].ToString());
            if (string.IsNullOrEmpty(row["RecordTime"].ToString()))
                model.RecordTime = null;
            else
                model.RecordTime = DateTime.Parse(row["RecordTime"].ToString());
            return model;
        }
    }
}
