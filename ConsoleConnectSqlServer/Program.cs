using ConsoleConnectSqlServer.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ConsoleConnectSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // 选择语言
            InfoHelper.initLanguage();

            #region 登录
            // 打印系统基本信息
            Console.WriteLine(InfoHelper.WelcomeInfo);
            Console.WriteLine(InfoHelper.InputTipInfo);
            //UserTModel loginUser;
            UserTModelForEF loginUser;
            // 验证用户名和密码
            while (true)
            {
                Console.Write(InfoHelper.InputUserNameInfo);
                string inputUserName = Console.ReadLine();
                Console.Write(InfoHelper.InputPasswordInfo);
                string inputPassword = Console.ReadLine();

                //loginUser = UserTOperation.Login(inputUserName, inputPassword);
                using (MyDBContext context = new MyDBContext())
                {
                    loginUser = context.UserT.FirstOrDefault(e => e.UserName == inputUserName && e.Password == inputPassword);
                }
                if (loginUser == null)
                {
                    Console.WriteLine(InfoHelper.WrongUserNameOrPasswordInfo);
                    continue;
                }
                break;
            }
            string str = InfoHelper.WelcomeUseInfo.Replace("@NickName", loginUser.NickName);
            Console.WriteLine(str);
            #endregion

            #region 性别录入
            // 读取性别，如果性别为空强制用户录入
            string sex = loginUser.Sex;
            if (string.IsNullOrEmpty(sex))
            {
                str = InfoHelper.InputSexInfo.Replace("@NickName", loginUser.NickName);
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
                    //int count = UserTOperation.UpdateSex(selectSex, loginUser);
                    int count;
                    using (MyDBContext context = new MyDBContext())
                    {
                        // 查询当前登录用户
                        //UserTModelForEF currentUser = context.UserT.FirstOrDefault(e => e.UserName == loginUser.UserName);
                        //currentUser.Sex = selectSex;
                        context.UserT.Attach(loginUser);
                        context.Entry(loginUser).State = System.Data.Entity.EntityState.Modified;
                        loginUser.Sex = selectSex;
                        count = context.SaveChanges();
                    }

                    if (count > 0)
                        Console.WriteLine(InfoHelper.UpdateSuccessInfo);
                    else
                        Console.WriteLine(InfoHelper.UpdateErrInfo);
                    break;
                }
            }
            #endregion

            #region 查询用户数据输出

            // 使用Query语句方式查询数据
            using (MyDBContext myDB = new MyDBContext())
            {
                var query = from usert in myDB.UserT
                            join userscorest in myDB.UserScoresT
                            on usert.UserName equals userscorest.UserName
                            into utus
                            from userscorest in utus.OrderByDescending(e => e.RecordTime).Take(1).DefaultIfEmpty()
                            select new UserTAndUserScoresTModelForEF
                            {
                                UserName = usert.UserName,
                                NickName = usert.NickName,
                                Sex = usert.Sex,
                                Password = usert.Password,
                                Chinese = userscorest.Chinese,
                                English = userscorest.English,
                                Math = userscorest.Math,
                                RecordTime = userscorest.RecordTime,
                            };
                List<UserTAndUserScoresTModelForEF> list = query.ToList();
            }

            using (MyDBContext myDB = new MyDBContext())
            {
                // 查询所有UserT数据
                List<UserTModelForEF> allUsers = myDB.UserT.ToList();

                for (int i = 0; i < allUsers.Count; i++)
                {
                    string userSex = allUsers[i].Sex;
                    if (userSex == "1")
                        userSex = InfoHelper.Sex1;
                    else if (userSex == "2")
                        userSex = InfoHelper.Sex2;
                    // 查询所有UserScoresT数据
                    string cutrrentUserName = allUsers[i].UserName;
                    string str1 = InfoHelper.UserOutputInfo;
                    str1 = str1.Replace("@NickName", allUsers[i].NickName);
                    str1 = str1.Replace("@Sex", userSex);
                    str1 = str1.Replace("@UserName", allUsers[i].UserName);

                    // 只查询用户最新的一条成绩数据
                    List<UserScoresTModelForEF> latestScores = myDB.UserScoresT.OrderByDescending(s => s.RecordTime).Where(s => s.UserName == cutrrentUserName).ToList();
                    if (latestScores.Count < 1)
                    {
                        Console.WriteLine($"{str1} 0 0 0 0");
                        continue;
                    }
                    UserScoresTModelForEF scores = latestScores[0];
                    string str2 = InfoHelper.UserScoresOutputInfo;
                    str2 = str2.Replace("@ChineseScores", scores.Chinese.ToString());
                    str2 = str2.Replace("@MathScores", scores.Math.ToString());
                    str2 = str2.Replace("@EnglishScores", scores.English.ToString());
                    str2 = str2.Replace("@RecordTime", scores.RecordTime.ToString());
                    Console.WriteLine($"{str1} {str2}");

                    // 查询用户所有成绩数据
                    //List<UserScoresTModelForEF> allUserScores = myDB.UserScoresT.Where(s => s.UserName == cutrrentUserName).ToList();
                    //if (allUserScores.Count < 1)
                    //{
                    //    Console.WriteLine($"{str1} 0 0 0 0");
                    //    continue;
                    //}
                    //foreach (UserScoresTModelForEF score in allUserScores)
                    //{
                    //    string str2 = InfoHelper.UserScoresOutputInfo;
                    //    str2 = str2.Replace("@ChineseScores", score.Chinese.ToString());
                    //    str2 = str2.Replace("@MathScores", score.Math.ToString());
                    //    str2 = str2.Replace("@EnglishScores", score.English.ToString());
                    //    str2 = str2.Replace("@RecordTime", score.RecordTime.ToString());
                    //    Console.WriteLine($"{str1} {str2}");
                    //}
                }
            }

            //List<UserTAndUserScoresTModel> dataList = UserTAndUserScoresTOperation.QueryLatestData();
            //foreach (UserTAndUserScoresTModel userData in dataList)
            //{
            //    string userSex = userData.Sex;
            //    if (userSex == "1")
            //        userSex = InfoHelper.Sex1;
            //    else if (userSex == "2")
            //        userSex = InfoHelper.Sex2;
            //    str = InfoHelper.UserOutputInfo;
            //    str = str.Replace("@NickName", userData.NickName);
            //    str = str.Replace("@Sex", userSex);
            //    str = str.Replace("@UserName", userData.UserName);
            //    InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@ChineseScores", userData.Chinese.ToString());
            //    InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@MathScores", userData.Math.ToString());
            //    InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@EnglishScores", userData.English.ToString());
            //    InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@RecordTime", userData.RecordTime.ToString());
            //    Console.WriteLine($"{str} {InfoHelper.UserScoresOutputInfo}");
            //}
            #endregion

            Console.ReadKey();
        }

    }
}
