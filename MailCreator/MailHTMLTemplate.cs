using Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Spider.Mail
{
    public class MailHTMLTemplate 
    {
        public static string GetHTMLContentByTheme(IEnumerable<ThemeRepo> repos)
        {
            string headPath = Path.Combine(System.Environment.CurrentDirectory, "MailHead.html");
            string tailPath = Path.Combine(System.Environment.CurrentDirectory, "MailTail.html");
            string liPath = Path.Combine(System.Environment.CurrentDirectory, "MailLi.html");

            StringBuilder html = new StringBuilder();
            html.Append(File.ReadAllText(headPath));
            foreach (ThemeRepo item in repos)
            {
                html.Append(GetNewLiElement(liPath, item.Name, item.Desc, item.Stars + "", item.Language,item.Addrress));
            }
            html.Append(File.ReadAllText(tailPath));

            return html.ToString();
        }

        public static string GetHTMLContentByLanguage(IEnumerable<TrendingRepo> repos)
        {
            string headPath = Path.Combine(System.Environment.CurrentDirectory, "MailHead.html");
            string tailPath = Path.Combine(System.Environment.CurrentDirectory, "MailTail.html");
            string liPath = Path.Combine(System.Environment.CurrentDirectory, "MailLi.html");

            StringBuilder html = new StringBuilder();
            html.Append(File.ReadAllText(headPath));
            foreach (TrendingRepo item in repos)
            {
                html.Append(GetNewLiElement(liPath, item.RepoTitle, item.RepoDescription, item.Stars + "", item.Language,item.Url));
            }
            html.Append(File.ReadAllText(tailPath));

            return html.ToString();
        }

        public static string GetNewLiElement(string liTemplatePath, string author, string desc, string stars, string language,string link)
        {
            ReadLiElement(liTemplatePath);
            AddNewLiElement(author, desc, stars, language,link);

            StringBuilder liContent = new StringBuilder();

            string[] contentArr = htlmElement.Values.ToArray();
            for (int i = 0; i < contentArr.Length; i++)
            {
                liContent.Append(contentArr[i] + "\n");
            }

            return liContent.ToString();
        }

        public static Dictionary<string, string> htlmElement = new Dictionary<string, string>();

        public static void ReadLiElement(string path)
        {
            htlmElement = new Dictionary<string, string>();
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("class"))
                {
                    int start = line.IndexOf("class=");
                    int length = line.IndexOf("\" ") - start - 7;
                    string key = line.Substring(start + 7, length);
                    htlmElement.Add(key, line);
                }
            }

            fs.Close();
            sr.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="author">项目名称</param>
        /// <param name="desc">描述</param>
        /// <param name="stars">星</param>
        /// <param name="language">语言</param>
        public static void AddNewLiElement(string author, string desc, string stars, string language,string link)
        {
            htlmElement["author"] = GetProjectName(author);
            htlmElement["desc"] = GetDesc(desc);
            htlmElement["stars"] = GetStars(stars);
            htlmElement["language"] = GetLanguage(language);
            htlmElement["title-text"] = GetProjectLink(link);
        }

        private static string GetProjectLink(string plink)
        {
            return "<a class=\"title - text\" style=\"\" href=\"" + plink + "\" target=\"_blank\">";
        }

        public static string GetProjectName(string author)
        {
            return "<span class=\"author\">" + author + "</span></a></h2></div>";
        }

        public static string GetDesc(string desc)
        {
            return "<div class=\"desc\" title=\"" + desc + "\">" + desc + "</div></div>";
        }

        public static string GetStars(string stars)
        {
            return "<i style=\" display: inline - block; font - style:normal; \">★</i>" + stars + "</span>";
        }

        public static string GetLanguage(string language)
        {
            return "<span class=\"language\" >" + language + "</span></span></div></div></div></div></li>";
        }

    }
}
