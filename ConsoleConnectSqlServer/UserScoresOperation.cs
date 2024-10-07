using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    public class UserScoresOperation
    {

        /// <summary>
        /// 根据用户名获取成绩集合
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户成绩集合</returns>
        public static List<UserScoresTModel> GetUserScoresByUserName(string userName)
        {
            string sql = $"select * from UserScoresT where UserName = '{userName}';";
            DataTable userScoresTable =  SqlHelper.SelectData(sql);
            List<UserScoresTModel> userScores = new List<UserScoresTModel>();
            for (int i = 0; i < userScoresTable.Rows.Count; i++)
            {
                userScores.Add(DataRowToUserScoresModel(userScoresTable.Rows[i]));
            }
            return userScores;
        }

        /// <summary>
        /// 根据用户名获取最新的成绩记录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户最新的成绩</returns>
        public static UserScoresTModel GetTopOneUserScoresByUserNameOrderByDate(string userName)
        {
            string sql = $"select top 1 * from UserScoresT where UserName = '{userName}' order by RecordTime desc;";
            DataTable userTable = SqlHelper.SelectData(sql);
            if (userTable.Rows.Count < 1)
                return null;
            UserScoresTModel userScore = DataRowToUserScoresModel(userTable.Rows[0]);
            return userScore;
        }

        /// <summary>
        /// 对象转换
        /// </summary>
        public static UserScoresTModel DataRowToUserScoresModel(DataRow data)
        {
            UserScoresTModel userScores = new UserScoresTModel();
            userScores.UserName = data["UserName"].ToString();
            userScores.Chinese = float.Parse(data["Chinese"].ToString());
            userScores.English = float.Parse(data["English"].ToString());
            userScores.Math = float.Parse(data["Math"].ToString());
            userScores.RecordTime = DateTime.Parse(data["RecordTime"].ToString());
            return userScores;
        }
    }
}
