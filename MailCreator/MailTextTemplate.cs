using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Mail
{
    class MailTextTemplate
    {
        /// <summary>
        /// 创建邮件的模板(无布局)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repos"></param>
        /// <returns></returns>
        public static string CreateMailTemplate<T>(IEnumerable<T> repos) where T:IRepo
        {
            StringBuilder content = new StringBuilder();
            foreach (T item in repos)
            {
                Type type = item.GetType();
                PropertyInfo[] piArr = type.GetProperties();
                foreach (var pi in piArr)
                {
                    content.Append(pi.Name + ":" + pi.GetValue(item) + "\n");
                }
                content.Append("\n");
            }

            return content.ToString();
        }

        /// <summary>
        /// 创建关于语言邮件模板
        /// </summary>
        /// <param name="repos"></param>
        /// <returns></returns>
        public static string CreateMailByLanguageTemplate(IEnumerable<TrendingRepo> repos)
        {
            StringBuilder content = new StringBuilder();

            foreach (TrendingRepo item in repos)
            {
                content.Append("项目名称: " + item.RepoTitle + "\n")
                       .Append("简介: " + item.RepoDescription + "\n")
                       .Append("项目地址: " + item.Url + "\n")
                       .Append("作者: " + item.RepoOwner + "\n")
                       .Append("今日获得星: " + item.StarsToday + "\n")
                       .Append("语言: " + item.Language + "\n\n");
            }

            return content.ToString();
        }

        /// <summary>
        /// 创建关注主题邮件模板
        /// </summary>
        /// <param name="repos"></param>
        /// <returns></returns>
        public static string CreateMailByThemeTemplate(IEnumerable<ThemeRepo> repos)
        {
            StringBuilder content = new StringBuilder();

            foreach (ThemeRepo item in repos)
            {
                content.Append("项目名称: " + item.Name + "\n")
                       .Append("简介: " + item.Desc + "\n")
                       .Append("项目评分: " + item.Score + "\n")
                       .Append("项目地址: " + item.Addrress + "\n")
                       .Append("作者: " + item.OwnerName + "\n")
                       .Append("星总数: " + item.Stars + "\n")
                       .Append("语言: " + item.Language + "\n\n");
            }

            return content.ToString();
        }
    }
}
