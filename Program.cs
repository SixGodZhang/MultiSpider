using System;
using System.Collections.Generic;
using System.IO;

namespace GithubSpider
{
    internal class SpiderMain
    {
        public static GithubWebConfig<GithubReceiver> githubConf;//github配置
        public static ZhihuWebConfig<ZhihuReceiver> zhihuConf;//zhihu配置

        public static Action<IEnumerable<WebType>> ReadSettingAction;

        static SpiderMain()
        {
            ReadSettingAction +=(types)=>
            {
                foreach (var item in types)
                {
                    switch (item)
                    {
                        case WebType.github:
                            githubConf = FunctionSetting.ReadSettingForGithub();
                            break;
                        case WebType.zhihu:
                            zhihuConf = FunctionSetting.ReadSettingForZhihu();
                            break;
                    }
                }
            };
        }

        private static void Main(string[] args)
        {
            //读取配置
            List<WebType> types = new List<WebType>() { WebType.github, WebType.zhihu };
            ReadSettingAction?.Invoke(types);

            while (true)
            {
                GithubAction();
                ZhihuAction();
            }
        }

        /// <summary>
        /// 与知乎数据相关的行为
        /// </summary>
        private static void ZhihuAction()
        {
            
        }

        /// <summary>
        /// 与Github数据相关的行为
        /// </summary>
        private static void GithubAction()
        {
            if ((githubConf.isDebug && !githubConf.isDebugSend) || UpdateNoticeCondition(githubConf.noticeRate))
            {
                string mailContens = "";
                //
                foreach (string key in githubConf.LanguagesDict.Keys)
                {
                    if (githubConf.LanguagesDict[key].Count != 0)
                    {//该语言有关注的人
                        mailContens = JsonParserGithub.GetFollowContents(key, githubConf.mailType);
                        Console.WriteLine("send language mail...");
                        //File.WriteAllText(System.Environment.CurrentDirectory + "/C.html", mailContens);
                        MailManager.SendMail(githubConf,SubscriptionSubject.Trending, mailContens, githubConf.LanguagesDict[key]);
                    }
                }

                //
                foreach (string key in githubConf.ThemesDict.Keys)
                {
                    if (githubConf.ThemesDict[key].Count != 0)
                    {
                        mailContens = JsonParserGithub.GetThemeContents(key, githubConf.mailType);
                        Console.WriteLine("send topic mail...");
                        MailManager.SendMail(zhihuConf,SubscriptionSubject.Topics, mailContens, githubConf.ThemesDict[key]);
                    }
                }

                if (githubConf.isDebug) githubConf.isDebugSend = true;
                SendStatus.SetSendSatus(githubConf.noticeRate);
            }

            if (!githubConf.isDebug) CheckSendStatusForReset(githubConf.noticeRate);
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
                    if (SendStatus.DailySend && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                        SendStatus.ResetSendStatus(rate);
                    break;

                case NoticeRate.WEEK:
                    if (SendStatus.WeekSend && DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                        SendStatus.ResetSendStatus(rate);
                    break;

                case NoticeRate.MONTH:
                    if (SendStatus.MonthSend && DateTime.Now.Day == 1 && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                        SendStatus.ResetSendStatus(rate);
                    break;

                case NoticeRate.YEAR:
                    if (SendStatus.YearSend && DateTime.Now.DayOfYear == 1 && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
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
                    conditionNoticeByNoticeRate = (!SendStatus.DailySend && DateTime.Now.Hour == githubConf.noticeTime.Hours && DateTime.Now.Minute == githubConf.noticeTime.Minutes);
                    break;

                case NoticeRate.WEEK:
                    conditionNoticeByNoticeRate = (DayOfWeek.Saturday == DateTime.Now.DayOfWeek && !SendStatus.WeekSend && DateTime.Now.Hour == githubConf.noticeTime.Hours && DateTime.Now.Minute == githubConf.noticeTime.Minutes);
                    break;

                case NoticeRate.MONTH:
                    conditionNoticeByNoticeRate = (1 == DateTime.Now.Day && !SendStatus.MonthSend && DateTime.Now.Hour == githubConf.noticeTime.Hours && DateTime.Now.Minute == githubConf.noticeTime.Minutes);
                    break;

                case NoticeRate.YEAR:
                    conditionNoticeByNoticeRate = (1 == DateTime.Now.DayOfYear && !SendStatus.YearSend && DateTime.Now.Hour == githubConf.noticeTime.Hours && DateTime.Now.Minute == githubConf.noticeTime.Minutes);
                    break;
            }

            return conditionNoticeByNoticeRate;
        }



        //static void Main(string[] args)
        //{
        //    Console.WriteLine(GetFllowContents(FllowLanguage.Java));
        //}
    }
}