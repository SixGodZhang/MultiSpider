using LogManager;
using System;
using System.Collections.Generic;
using System.IO;

namespace Spider
{
    /// <summary>
    /// 此类为代码中调用配置需要使用的类,
    /// 与Configure<T> 的区别在于,Configure<T>
    /// 是从文件中读取的理论数据模型，而SpiderWebConfig
    /// 是实际数据
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public abstract class SpiderWebConfig<T1> where T1 : Receiver
    {
        public string sender;
        public T1[] receivers;
        public string license;
        public TimeSpan noticeTime;
        public bool isDebug = false;
        public bool isDebugSend = false;//辅助字段
        public MailContentType mailType = MailContentType.TEXT;
        public NoticeRate noticeRate = NoticeRate.WEEK;
        public bool database = false;

        /// <summary>
        /// 用于转换数据类型(实际使用的)
        /// </summary>
        /// <param name="configure"></param>
        public virtual void Convert(Configure<T1> configure)
        {
            this.sender = configure.sender;
            this.receivers = configure.receivers;
            this.license = configure.license;
            this.database = configure.database;
            string[] noticeTimeData = configure.noticetime.Split('-');
            try
            {
                this.noticeTime = new TimeSpan(int.Parse(noticeTimeData[0]), int.Parse(noticeTimeData[1]), 0);
            }
            catch (Exception ex)
            {
                ATLog.Error(ex.Message + "\n" + ex.StackTrace);
            }
            this.isDebug = configure.debug;
            this.isDebugSend = false;//初始化为false
            this.noticeRate = (NoticeRate)Enum.Parse(typeof(NoticeRate), configure.noticerate.ToUpper());
            this.mailType = (MailContentType)Enum.Parse(typeof(MailContentType), configure.mailcontenttype.ToUpper());
        }
    }

    /// <summary>
    /// Github网站的相关配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GithubWebConfig<T> : SpiderWebConfig<T> where T : GithubReceiver
    {
        public Dictionary<string, List<string>> ThemesDict = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> LanguagesDict = new Dictionary<string, List<string>>();

        public override void Convert(Configure<T> configure)
        {
            base.Convert(configure);
            foreach (var receiver in configure.receivers)
            {
                foreach (var theme in receiver.themes)
                {
                    //遍历主题
                    if (ThemesDict.ContainsKey(theme))
                    {
                        List<string> mails = ThemesDict[theme];
                        if (mails == null)
                            mails = new List<string>() { receiver.mail };
                        else
                            mails.Add(receiver.mail);
                    }
                    else
                        ThemesDict.Add(theme, new List<string>() { receiver.mail });
                }
                ////遍历语言
                foreach (var language in receiver.follows)
                {
                    if (LanguagesDict.ContainsKey(language))
                    {
                        List<string> mails = LanguagesDict[language];
                        if (mails == null)
                            mails = new List<string> { receiver.mail };
                        else
                            mails.Add(receiver.mail);
                    }
                    else
                        LanguagesDict.Add(language, new List<string>() { receiver.mail });
                }
            }
        }
    }

    public class ZhihuWebConfig<T> : SpiderWebConfig<T> where T : ZhihuReceiver
    {
        //是否订阅热点
        public Dictionary<string, bool> SubscribeHot = new Dictionary<string, bool>();
        //关注话题
        public Dictionary<string, List<string>> TopicsDict = new Dictionary<string, List<string>>();
        public override void Convert(Configure<T> configure)
        {
            base.Convert(configure);
            foreach (var receiver in configure.receivers)
            {
                SubscribeHot.Add(receiver.mail, receiver.hot);

                foreach (var topic in receiver.topics)
                {
                    if (TopicsDict.ContainsKey(topic))
                    {
                        List<string> mails = TopicsDict[topic];
                        if (mails == null)
                            mails = new List<string>() { receiver.mail };
                        else
                            mails.Add(receiver.mail);
                    }
                    else
                        TopicsDict.Add(topic, new List<string>() { receiver.mail });
                }   
            }
        }
    }

    internal class FunctionSetting
    {
        private static string zhihuConfPath = Path.Combine(System.Environment.CurrentDirectory, "setting/zhihuer_setting.ini");
        private static string githubConfPath = Path.Combine(System.Environment.CurrentDirectory, "setting/github_setting.ini");

        /// <summary>
        /// 读取知乎的配置
        /// </summary>
        public static ZhihuWebConfig<ZhihuReceiver> ReadSettingForZhihu()
        {
            ZhihuWebConfig<ZhihuReceiver> config = new ZhihuWebConfig<ZhihuReceiver>();

            string settingContent = File.ReadAllText(zhihuConfPath);
            Configure<ZhihuReceiver> zhihuConfigure = Newtonsoft.Json.JsonConvert.DeserializeObject<Configure<ZhihuReceiver>>(settingContent);
            config.Convert(zhihuConfigure);

            return config;
        }

        /// <summary>
        /// 读取Github的配置
        /// </summary>
        public static GithubWebConfig<GithubReceiver> ReadSettingForGithub()
        {
            GithubWebConfig<GithubReceiver> config = new GithubWebConfig<GithubReceiver>();

            string settingContent = File.ReadAllText(githubConfPath);
            Configure<GithubReceiver> githubConfigure = Newtonsoft.Json.JsonConvert.DeserializeObject<Configure<GithubReceiver>>(settingContent);
            config.Convert(githubConfigure);
            return config;
        }
    }
}