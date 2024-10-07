using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
            UserTModel loginUser;
            // 验证用户名和密码
            while (true)
            {
                Console.Write(InfoHelper.InputUserNameInfo);
                string inputUserName = Console.ReadLine();
                Console.Write(InfoHelper.InputPasswordInfo);
                string inputPassword = Console.ReadLine();
                
                loginUser = UserTOperation.Login(inputUserName, inputPassword);
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
                    int count = UserTOperation.UpdateSex(selectSex, loginUser);

                    if (count > 0)
                        Console.WriteLine(InfoHelper.UpdateSuccessInfo);
                    else
                        Console.WriteLine(InfoHelper.UpdateErrInfo);
                    break;
                }
            }
            #endregion

            #region 查询用户数据输出
            List<UserTAndUserScoresTModel> dataList = UserTAndUserScoresTOperation.QueryLatestData();
            foreach (UserTAndUserScoresTModel userData in dataList)
            {
                string userSex = userData.Sex;
                if (userSex == "1")
                    userSex = InfoHelper.Sex1;
                else if (userSex == "2")
                    userSex = InfoHelper.Sex2;
                str = InfoHelper.UserOutputInfo;
                str = str.Replace("@NickName", userData.NickName);
                str = str.Replace("@Sex", userSex);
                str = str.Replace("@UserName", userData.UserName);
                InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@ChineseScores", userData.Chinese.ToString());
                InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@MathScores", userData.Math.ToString());
                InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@EnglishScores", userData.English.ToString());
                InfoHelper.UserScoresOutputInfo = InfoHelper.UserScoresOutputInfo.Replace("@RecordTime", userData.RecordTime.ToString());
                Console.WriteLine($"{str} {InfoHelper.UserScoresOutputInfo}");
            }
            #endregion

            Console.ReadKey();
        }

    }
}
