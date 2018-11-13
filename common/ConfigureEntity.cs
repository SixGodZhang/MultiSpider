using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    /// <summary>
    /// 此类的实例在启动程序时从配置文件XXX_setting.ini中读取
    /// </summary>
    public class Configure<T> where T:Receiver
    {
        public string sender;//邮件发送者
        public T[] receivers;
        public string license;//秘钥
        public string noticetime;//提醒时间
        public bool debug;//是否是调试模式
        public string mailcontenttype;//邮件内容类型
        public string noticerate;//邮件提醒频率
    }

    public abstract class Receiver
    {
        public int id;
        public string name;
        public string mail;
    }

    public class GithubReceiver : Receiver
    {
        //关注语言:c、c++、java、c#、python、unknow
        public string[] follows;
        //关注主题自定义关键字
        public string[] themes;
    }

    public class ZhihuReceiver : Receiver
    {
        //关注热点:知乎热点
        public string[] hots;
    }

}
