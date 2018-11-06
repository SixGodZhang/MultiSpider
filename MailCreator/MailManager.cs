using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    public class MailManager
    {
        //语言和关注者对应,一对多关系
        public static Dictionary<string, List<string>> DomainDict = new Dictionary<string, List<string>>
        {
            { FllowLanguage.C,new List<string>()},
            { FllowLanguage.CPlusPlus,new List<string>()},
            { FllowLanguage.CSharp,new List<string>()},
            { FllowLanguage.Java,new List<string>()},
            { FllowLanguage.Python,new List<string>()},
            { FllowLanguage.UnKnown,new List<string>()}
        };

        public static Dictionary<string, List<string>> ThemeDict = new Dictionary<string, List<string>>();

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="mails">关注者</param>
        /// <param name="mailContentType">邮件内容是否采用HTML</param>
        public static void SendMail(string subject, string body, List<string> mails,bool mailContentType = true)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = mailContentType;
            mailMessage.BodyEncoding = Encoding.GetEncoding(936);
            mailMessage.From = new MailAddress(SpiderMain.configure.sender);

            for (int i = 0; i < mails.Count; i++)
            {
                mailMessage.To.Add(new MailAddress(mails[i]));
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            SmtpClient client = new SmtpClient();
            client.Host = "14.17.57.241";//"smtp.qq.com";
            client.EnableSsl = true; 
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(SpiderMain.configure.sender, SpiderMain.configure.license);

            client.Send(mailMessage);
        }
    }
}
