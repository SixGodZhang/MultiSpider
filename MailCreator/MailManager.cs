using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Spider.Mail
{
    public class MailManager
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="mails">关注者</param>
        /// <param name="mailContentType">邮件内容是否采用HTML</param>
        public static void SendMail<T>(SpiderWebConfig<T> webConfig, string subject, string body, List<string> mails, bool mailContentType = true) where T : Receiver
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = mailContentType;
            mailMessage.BodyEncoding = Encoding.GetEncoding(936);
            mailMessage.From = new MailAddress(webConfig.sender);

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
            client.Credentials = new NetworkCredential(webConfig.sender, webConfig.license);

            client.Send(mailMessage);
        }
    }
}