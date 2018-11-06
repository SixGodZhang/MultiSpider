using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    public class MailReceiver
    {
        private string m_mailAddress;
        private string m_followLanguage;

        public MailReceiver(string m_mailAddress, string m_followLanguage)
        {
            this.m_mailAddress = m_mailAddress;
            this.m_followLanguage = m_followLanguage;
        }

        public string MailAddress { get;}
        public string FollowLanguage { get; }
    }
}
