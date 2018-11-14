using Newtonsoft.Json;
using Repos;
using Spider.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Spider.Database;
using LogManager;

namespace Spider.Github
{
    class JsonParserGithub
    {
        /// <summary>
        /// 获取Github趋势邮件内容
        /// </summary>
        /// <param name="fllowLanguage">关注何种语言趋势</param>
        /// <param name="type">邮件内容格式</param>
        /// <returns></returns>
        public static string GetFollowContents(string fllowLanguage, MailContentType type)
        {
            ATLog.Info("获取Github每日趋势");
            List<TrendingRepo> repos = HTMLParserGitHub.Trending("daily", fllowLanguage).Result;

            string content = "";
            switch (type)
            {
                case MailContentType.TEXT:
                    ATLog.Info("创建Text格式的邮件模板");
                    content = MailTextTemplate.CreateMailTemplate(repos);
                    break;

                case MailContentType.HTML:
                    ATLog.Info("创建HTML格式的邮件模板");
                    content = MailHTMLTemplate.GetHTMLContentByLanguage(repos);
                    break;
            }
            //File.WriteAllText(Path.Combine(System.Environment.CurrentDirectory, "A.html"),content);
            return content;
        }

        /// <summary>
        /// 获取Github主题邮件内容
        /// </summary>
        /// <param name="theme">关注的Github主题</param>
        /// <param name="type">邮件内容格式</param>
        /// <returns></returns>
        public static string GetThemeContents(string theme, MailContentType type)
        {
            ATLog.Info("获取Github关注的话题");
            List<ThemeRepo> repos = JsonParserGithub.GetThemeRepos(theme);

            string content = "";
            switch (type)
            {
                case MailContentType.TEXT:
                    content = MailTextTemplate.CreateMailByThemeTemplate(repos);
                    break;

                case MailContentType.HTML:
                    content = MailHTMLTemplate.GetHTMLContentByTheme(repos);
                    break;
            }
            //File.WriteAllText(Path.Combine(System.Environment.CurrentDirectory, "B.html"), content);
            return content;
        }

        /// <summary>
        /// 获取话题数据
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<ThemeRepo> GetThemeRepos(string theme, SearchType type = SearchType.Repositories)
        {
            ATLog.Info("获取Github上关注的话题");
            List<ThemeRepo> repos = new List<ThemeRepo>();

            StringBuilder conditions = new StringBuilder();
            conditions.Append("?");
            conditions.Append(Conditions.SetQueryKeyWord(theme));

            string requestURL = Conditions.SetRequestRootURL(type.ToString()) + conditions.ToString();
            Console.WriteLine(requestURL);
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(requestURL);
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            //httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                //Console.WriteLine("状态返回值: success");
                Stream stream = (Stream)httpWebResponse.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string resultJson = sr.ReadToEnd();
                resultJson = resultJson.Replace("private", "_private");
                RepositoriesEntity repositoriesResult = JsonConvert.DeserializeObject<RepositoriesEntity>(resultJson);

                RepositoriesResultItem[] items = repositoriesResult.items;
                for (int i = 0; i < items.Length; i++)
                {
                    ThemeRepo themeRepo = new ThemeRepo(items[i].full_name, items[i].html_url, items[i].description, items[i].stargazers_count, items[i].language, items[i].score, items[i].owner.login);
                    repos.Add(themeRepo);
                }
                //Console.WriteLine(repositoriesResult.total_count);
            }

            if (repos.Count != 0)
                GithubOp.Instance.SaveRangeData(repos);

            return repos;
        }
    }
}
