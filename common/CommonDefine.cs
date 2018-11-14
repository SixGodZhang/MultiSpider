using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    enum WebType
    {
        github      = 0,
        zhihu       = 1,
    }

    /// <summary>
    /// 发送给订阅者的邮件内容类型
    /// </summary>
    public enum MailContentType
    {
        TEXT = 0,
        HTML = 1
    }

    /// <summary>
    /// 发送邮件的周期
    /// </summary>
    public enum NoticeRate
    {
        DAILY = 0,
        WEEK = 1,
        MONTH = 2,
        YEAR = 3,
        SPECIAL = 4,
    }

    /// <summary>
    /// 关注的语言
    /// </summary>
    public struct FllowLanguage
    {
        public static string C { get { return "c"; } }
        public static string CPlusPlus { get { return "c++"; } }
        public static string CSharp { get { return "c#"; } }
        public static string Java { get { return "java"; } }
        public static string Python { get { return "python"; } }
        public static string UnKnown { get { return "unknown"; } }
    }

    /// <summary>
    /// 订阅邮件标题
    /// </summary>
    struct SubscriptionSubject
    {
        public static string Trending { get { return "Github 趋势专题(近期热点)"; } }
        public static string Topics { get { return "Github 热门话题"; } }
    }

    /// <summary>
    /// 发送状态
    /// </summary>
    struct SendStatus
    {
        static SendStatus()
        {
            DailySend = false;
            WeekSend = false;
            MonthSend = false;
            YearSend = false;
            SpecialSend = false;
        }

        public static bool DailySend { get; set; }
        public static bool WeekSend { get; set; }
        public static bool MonthSend { get; set; }
        public static bool YearSend { get; set; }
        public static bool SpecialSend { get; set; }

        public static void ResetSendStatus(NoticeRate rate)
        {
            switch (rate)
            {
                case NoticeRate.DAILY:
                    DailySend = false;
                    break;
                case NoticeRate.WEEK:
                    WeekSend = false;
                    break;
                case NoticeRate.MONTH:
                    MonthSend = false;
                    break;
                case NoticeRate.YEAR:
                    YearSend = false;
                    break;
                case NoticeRate.SPECIAL:
                    SpecialSend = false;
                    break;
            }
        }

        public static void SetSendSatus(NoticeRate rate)
        {
            switch (rate)
            {
                case NoticeRate.DAILY:
                    DailySend = true;
                    break;
                case NoticeRate.WEEK:
                    WeekSend = true;
                    break;
                case NoticeRate.MONTH:
                    MonthSend = true;
                    break;
                case NoticeRate.YEAR:
                    YearSend = true;
                    break;
                case NoticeRate.SPECIAL:
                    SpecialSend = true;
                    break;
            }
        }

    }

    /// <summary>
    /// 查找参数
    /// </summary>
    enum SearchKey
    {
        l = 0,//语言(可选)
        q = 1,//查询关键字(必须)
        type = 2,//类型
        sort = 3,//分类
    }

    /// <summary>
    /// 查找类型
    /// </summary>
    enum SearchType
    {
        Repositories = 0,//仓库
        Code = 1,//代码
        Commits = 2,//提交
        Issues = 3,//问题
        Marketplace = 4,//市场
        Topics = 5,//话题
        Users = 7,//用户
        Lables = 8, //标签
    }

    /// <summary>
    /// 查询的语言
    /// </summary>
    struct SearchLanguage
    {
        public string C { get { return "C"; } }
        public string PHP { get { return "PHP"; } }
        public string Java { get { return "Java"; } }
        public string HTML { get { return "HTML"; } }
        public string JavaScripts { get { return "JavaScripts"; } }
        public string CPlusPlus { get { return "C++"; } }
        public string Ruby { get { return "Ruby"; } }
        public string Text { get { return "Text"; } }
        public string CSharp { get { return "C#"; } }
        public string XML { get { return "XML"; } }
    }

    /// <summary>
    /// 从api.github.com获取相关的信息需要用到
    /// </summary>
    public partial class CommonDefine
    {

    }
}
