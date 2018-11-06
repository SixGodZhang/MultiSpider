using System;
using System.Collections.Generic;
using System.IO;

namespace GithubSpider
{
    class SpiderMain
    {
        private static int noticeHour;
        private static int noticeMin;

        private static Configure configure;
        private static bool isDebug = false;
        private static bool isDebugSend = false;
        private static MailContentType mailType = MailContentType.TEXT;
        private static NoticeRate noticeRate = NoticeRate.WEEK;

        static void Main(string[] args)
        {
            ReadSetting();

            while (true)
            {
                if ((isDebug && !isDebugSend) || UpdateNoticeCondition(noticeRate))
                {
                    string mailContens = "";
                    //trending
                    foreach (string key in MailManager.DomainDict.Keys)
                    {
                        if (MailManager.DomainDict[key].Count != 0)
                        {//该语言有关注的人
                            mailContens = GetFllowContents(key, mailType);
                            Console.WriteLine("send language mail...");
                            //File.WriteAllText(System.Environment.CurrentDirectory + "/C.html", mailContens);
                            MailManager.SendMail(SubscriptionSubject.Trending, mailContens, MailManager.DomainDict[key]);
                        }
                    }

                    //语言
                    foreach (string key in MailManager.ThemeDict.Keys)
                    {
                        if (MailManager.ThemeDict[key].Count != 0)
                        {
                            mailContens = GetThemeContents(key, mailType);
                            Console.WriteLine("send topic mail...");
                            MailManager.SendMail(SubscriptionSubject.Topics, mailContens, MailManager.ThemeDict[key]);
                        }
                    }

                    if (isDebug) isDebugSend = true;
                    SendStatus.SetSendSatus(noticeRate);
                }

                if(!isDebug) CheckSendStatusForReset(noticeRate);

            }

            //var FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Trending.json");
            //File.WriteAllText(FilePath, Newtonsoft.Json.JsonConvert.SerializeObject(Repos.OrderByDescending(x => x.StarsToday)));
        }

        /// <summary>
        /// 检查发送状态，判断是否到了重置时间点
        /// </summary>
        /// <param name="rate"></param>
        private static void CheckSendStatusForReset(NoticeRate rate)
        {
            switch (rate)
            {
                case NoticeRate.DAILY:
                    if(SendStatus.DailySend && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                        SendStatus.ResetSendStatus(rate);
                    break;
                case NoticeRate.WEEK:
                    if(SendStatus.WeekSend && DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                        SendStatus.ResetSendStatus(rate);
                    break;
                case NoticeRate.MONTH:
                    if(SendStatus.MonthSend && DateTime.Now.Day == 1 && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                        SendStatus.ResetSendStatus(rate);
                    break;
                case NoticeRate.YEAR:
                    if(SendStatus.YearSend && DateTime.Now.DayOfYear == 1 && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                        SendStatus.ResetSendStatus(rate);
                    break;
                case NoticeRate.SPECIAL:
                    //if(SendStatus.SpecialSend)
                   // SendStatus.ResetSpecialSend();
                    break;
            }
        }

        /// <summary>
        /// 刷新提醒的条件
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        private static bool UpdateNoticeCondition(NoticeRate rate)
        {
            bool conditionNoticeByNoticeRate = false;

            switch (rate)
            {   
                case NoticeRate.DAILY:
                    conditionNoticeByNoticeRate = (!SendStatus.DailySend && DateTime.Now.Hour == noticeHour && DateTime.Now.Minute == noticeMin);
                    break;
                case NoticeRate.WEEK:
                    conditionNoticeByNoticeRate = (DayOfWeek.Saturday == DateTime.Now.DayOfWeek && !SendStatus.WeekSend && DateTime.Now.Hour == noticeHour && DateTime.Now.Minute == noticeMin);
                    break;
                case NoticeRate.MONTH:
                    conditionNoticeByNoticeRate = (1 == DateTime.Now.Day && !SendStatus.MonthSend && DateTime.Now.Hour == noticeHour && DateTime.Now.Minute == noticeMin);
                    break;
                case NoticeRate.YEAR:
                    conditionNoticeByNoticeRate = (1 == DateTime.Now.DayOfYear && !SendStatus.YearSend && DateTime.Now.Hour == noticeHour && DateTime.Now.Minute == noticeMin);
                    break;
            }

            return conditionNoticeByNoticeRate;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private static void ReadSetting()
        {
            string settingContent = File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "setting.ini"));
            configure = Newtonsoft.Json.JsonConvert.DeserializeObject<Configure>(settingContent);

            //initial noticerate
            noticeRate = (NoticeRate)Enum.Parse(typeof(NoticeRate), configure.noticerate.ToUpper());

            //initial mailtype
            mailType = (MailContentType)Enum.Parse(typeof(MailContentType), configure.mailcontenttype.ToUpper());

            //initial debug mode
            isDebug = configure.debug;

            //initial notice time
            string[] noticeTimeData = configure.noticetime.Split('-');
            int.TryParse(noticeTimeData[0], out noticeHour);
            int.TryParse(noticeTimeData[1], out noticeMin);

            //initial follow<--->mail
            foreach (Receiver item in configure.receivers)
            {
                //语言
                foreach (string followPlace in item.follow)
                {
                    MailManager.DomainDict[followPlace].Add(item.mail);
                }

                //领域
                foreach (string theme in item.themes)
                {
                    if (MailManager.ThemeDict.ContainsKey(theme))
                    {
                        if (MailManager.ThemeDict[theme] == null)
                        {
                            List<string> mail = new List<string>();
                            mail.Add(item.mail);
                            MailManager.ThemeDict.Add(theme, mail);
                        }
                        else
                        {
                            MailManager.ThemeDict[theme].Add(item.mail);
                        }
                    }
                    else
                    {
                        List<string> mail = new List<string>();
                        mail.Add(item.mail);
                        MailManager.ThemeDict.Add(theme, mail);
                    }
                }
            }
        }

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(GetFllowContents(FllowLanguage.Java));
        //}

        public static string GetFllowContents(string fllowLanguage, MailContentType type)
        {
            List<TrendingRepo> repos = HTMLParserGitHub.Trending("daily",fllowLanguage).Result;

            string content = "";
            switch (type)
            {
                case MailContentType.TEXT:
                    content = MailTextTemplate.CreateMailByLanguageTemplate(repos);
                    break;
                case MailContentType.HTML:
                    content = MailHTMLTemplate.GetHTMLContentByLanguage(repos);
                    break;

            }
            //File.WriteAllText(Path.Combine(System.Environment.CurrentDirectory, "A.html"),content);
            return content;
        }

        public static string GetThemeContents(string theme,MailContentType type)
        {
            List<ThemeRepo> repos = APIGithub.GetThemeRepos(theme);

            string content = "";
            switch (type)
            {
                case MailContentType.TEXT:
                    content = MailTextTemplate.CreateMailByThemeTemplate(repos);
                    break;
                case MailContentType.HTML:
                    content = MailHTMLTemplate.GetHTMLContentByTheme(repos);
                    break;

            }
            File.WriteAllText(Path.Combine(System.Environment.CurrentDirectory, "B.html"), content);
            return content;
        }

    }
}
