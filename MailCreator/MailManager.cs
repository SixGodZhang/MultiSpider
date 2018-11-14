using LogManager;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
            ATLog.Info("正在发送邮件,详细信息:\n" +
                "发件人: " + webConfig.sender + "\n" +
                "邮件主题: " + subject + "\n" +
                "收件人: " + mails.ToArray().ToString() +"\n");
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

            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;

            client.Send(mailMessage);
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}