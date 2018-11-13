using Database;
using HtmlAgilityPack;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    public class HTMLParserGitHub
    {
        public static async Task<List<TrendingRepo>> Trending(String Period = "daily", String Language = "")
        {
            List<TrendingRepo> Repos = new List<TrendingRepo>();
            //解决远程身份验证问题
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            using (var web = new HttpClient())
            {
                var result = await web.GetAsync(GetUrl(Period, Language));

                if (result.IsSuccessStatusCode)
                {
                    var htmlData = await result.Content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlData);

                    var baseRepos = doc.DocumentNode.Descendants().Where(x => x.Name == "ol").Select(x => x.ChildNodes).First().Where(x => x.Name == "li").Select(x => x);

                    foreach (HtmlNode repo in baseRepos)
                    {
                        var tempTrendingRepo = new TrendingRepo();
                        var childDivs = repo.ChildNodes.Where(x => x.Name == "div").Where(x => x.HasAttributes).Where(x => x.GetAttributeValue("class", "") != "float-right");


                        var repoOwnerData = childDivs.ElementAt(0).ChildNodes.Where(x => x.Name == "h3").First().ChildNodes.Where(x => x.Name == "a").First().ChildNodes;

                        var repoURLData = childDivs.ElementAt(0).ChildNodes.Where(x => x.Name == "h3").First().ChildNodes.Where(x => x.Name == "a");

                        tempTrendingRepo.RepoOwner = GetRepoOwner(repoOwnerData);
                        tempTrendingRepo.RepoTitle = GetRepoTitle(repoOwnerData);
                        tempTrendingRepo.Url = GetRepoURL(repoURLData);

                        tempTrendingRepo.RepoDescription = GetRepoDescription(childDivs.ElementAt(1));

                        tempTrendingRepo.Language = GetRepoLanguage(childDivs.ElementAt(2));

                        tempTrendingRepo.Stars = GetRepoStars(childDivs.ElementAt(2));
                        tempTrendingRepo.Forks = GetRepoForks(childDivs.ElementAt(2));
                        tempTrendingRepo.StarsToday = GetRepoStarsToday(childDivs.ElementAt(2));
                        Repos.Add(tempTrendingRepo);
                    }
                }
            }

            if (Repos.Count != 0)
                GithubOp.Instance.SaveRangeData(Repos);

            return Repos;
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        static int GetRepoStarsToday(HtmlNode Parent)
        {
            try
            {
                return int.Parse(Parent.Descendants()
                    .Where(x => x.Name == "span")
                    .Where(x => x.HasChildNodes)
                    .Where(x => x.HasAttributes)
                    .Where(x => x.GetAttributeValue("class", "") == "d-inline-block float-sm-right")
                    .Select(x => x)
                    .First()
                    .ChildNodes
                    .Where(x => x.Name == "#text")
                    .Where(x => x.InnerHtml.Trim() != "")
                    .Select(x => x.InnerHtml.Replace("stars today", "").Replace(",", "").Trim())
                    .First());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        static int GetRepoForks(HtmlNode Parent)
        {
            try
            {
                return int.Parse(Parent.Descendants().Where(x => x.Name == "a").Where(x => x.HasAttributes).Where(x => x.GetAttributeValue("href", "").Contains("network")).Select(x => x).First().ChildNodes.Where(x => x.Name == "#text").Where(x => x.InnerHtml.Trim() != "").Select(x => x.InnerHtml.Trim().Replace(",", "")).First());
            }
            catch (Exception)
            {
                return 0;
            }

        }

        static int GetRepoStars(HtmlNode Parent)
        {
            try
            {
                return int.Parse(Parent.Descendants().Where(x => x.Name == "a").Where(x => x.HasAttributes).Where(x => x.GetAttributeValue("href", "").Contains("stargazers")).Select(x => x).First().ChildNodes.Where(x => x.Name == "#text").Where(x => x.InnerHtml.Trim() != "").Select(x => x.InnerHtml.Trim().Replace(",", "")).First());
            }
            catch (Exception)
            {
                return 0;
            }

        }

        static String GetRepoLanguage(HtmlNode Parent)
        {
            try
            {
                return Parent.Descendants().Where(x => x.Name == "span").Where(x => x.HasAttributes).Where(x => x.GetAttributeValue("itemprop", "") == "programmingLanguage").Select(x => x.InnerHtml.Trim()).First();
            }
            catch (Exception)
            {
                return String.Empty;
            }

        }

        static String GetRepoDescription(HtmlNode Parent)
        {
            try
            {
                return Parent.ChildNodes.Where(x => x.Name == "p").Select(x => x.InnerText.Trim()).First();
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        static String GetRepoTitle(HtmlNodeCollection Divs)
        {
            return Divs.Where(x => x.Name == "#text").Where(x => x.InnerHtml.Trim() != "").Select(x => x.InnerText.Trim()).First();
        }

        static String GetRepoURL(IEnumerable<HtmlNode> nodes)
        {
            HtmlNode node = nodes.First<HtmlNode>();
            return "https://github.com/" + node.InnerText.Trim().Replace(" ", "");
        }

        static String GetRepoOwner(HtmlNodeCollection Divs)
        {
            return Divs.Where(x => x.Name == "span").Select(x => x.InnerText.Replace("/", "").Trim()).First();
        }

        static String GetUrl(String Period, String Language)
        {
            //Console.WriteLine($"https://github.com/trending/{WebUtility.UrlEncode(Language)}?since={Period}");

            //return $"https://github.com/trending/{WebUtility.UrlEncode(Language)}?since={Period}";
            string url = string.IsNullOrEmpty(Language) ? $"https://github.com/trending?since={Period}" : $"https://github.com/trending/{WebUtility.UrlEncode(Language)}?since={Period}";

            //Console.WriteLine(url);

            return url;
        }
    }
}
