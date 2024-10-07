using System;

namespace ConsoleConnectSqlServer
{
    class InfoHelper
    {
        public static string Sex1 { get; set; }
        public static string Sex2 { get; set; }
        public static string WelcomeInfo { get; set; }
        public static string InputTipInfo { get; set; }
        public static string InputUserNameInfo { get; set; }
        public static string InputPasswordInfo { get; set; }
        public static string WrongUserNameOrPasswordInfo { get; set; }
        public static string WelcomeUseInfo { get; set; }
        public static string InputSexInfo { get; set; }
        public static string SexSelectInfo { get; set; }
        public static string InputWrongInfo { get; set; }
        public static string UpdateSuccessInfo { get; set; }
        public static string UpdateErrInfo { get; set; }
        public static string SelectAllUserInfo { get; set; }
        public static string UserOutputInfo { get; set; }
        public static string UserScoresOutputInfo { get; set; }

        /// <summary>
        /// 系统语言初始化
        /// </summary>
        public static void initLanguage()
        {
            Console.WriteLine("请选择系统语言：1-中文 2-English");
            string inputLang = Console.ReadLine();

            if (inputLang == "2")
            {
                Sex1 = "Male";
                Sex2 = "Female";
                WelcomeInfo = "Welcome to the login system!!!";
                InputTipInfo = "Please enter your username and password below";
                InputUserNameInfo = "Please enter username:";
                InputPasswordInfo = "Please input a password:";
                WrongUserNameOrPasswordInfo = "Username or password error, please try again!";
                WelcomeUseInfo = "Welcome to use: @NickName";
                InputSexInfo = "Dear @NickName, please enter your gender information";
                SexSelectInfo = "Please select gender: 1- Male; 2- Female";
                InputWrongInfo = "Input error, please try again";
                UpdateSuccessInfo = "Update successful!";
                UpdateErrInfo = "Update failed!";
                SelectAllUserInfo = "Query all user information";
                UserOutputInfo = "Username: @UserName; Nickname: @NickName; Gender: @Sex";
                UserScoresOutputInfo = "Chinese Score: @ChineseScore Math Score: @MathScores English Score: @EnglishScores Record Date: @RecordTime";
            }
            else
            {
                Sex1 = "男";
                Sex2 = "女";
                WelcomeInfo = "欢迎访问登录系统!!!";
                InputTipInfo = "请在下方输入您的用户名和密码";
                InputUserNameInfo = "请输入用户名：";
                InputPasswordInfo = "请输入密码：";
                WrongUserNameOrPasswordInfo = "用户名或密码错误，请重试!";
                WelcomeUseInfo = "欢迎使用：@NickName";
                InputSexInfo = "尊敬的@NickName，请录入性别信息";
                SexSelectInfo = "请选择性别：1-男；2-女";
                InputWrongInfo = "输入有误，请重试";
                UpdateSuccessInfo = "更新成功！";
                UpdateErrInfo = "更新失败！";
                SelectAllUserInfo = "查询所有用户信息";
                UserOutputInfo = "用户名：@UserName; 昵称：@NickName; 性别：@Sex";
                UserScoresOutputInfo = "语文成绩：@ChineseScores 数学成绩：@MathScores 英语成绩：@EnglishScores 记录日期：@RecordTime";
            }
        }
    }
}
