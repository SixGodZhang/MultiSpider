using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    /// <summary>
    /// 此类的实例在启动程序时从配置文件setting.ini中读取
    /// </summary>
    public class Configure
    {
        public string sender;//邮件发送者
        public Receiver[] receivers;
        public string license;//秘钥
        public string noticetime;//提醒时间
        public bool debug;//是否是调试模式
        public string mailcontenttype;//邮件内容类型
        public string noticerate;//邮件提醒频率
    }

    public class Receiver
    {
        public int id;
        public string name;
        public string mail;
        public string[] follow;
        public string[] themes;
    }

}
