using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    class HTMLParserZhihu
    {
        public static void Main(string[] args)
        {
            //List<HotRepo> hotRepos = GetHots("hot").Result;
            ValidateIdentification();

        }

        public static void ValidateIdentification()
        {
            string url = "https://www.zhihu.com/hot";
            HttpWebRequest httpWebRequest = HttpWebRequest.CreateHttp(url);
            httpWebRequest.Method = "Get";
            String username = "17621047312";
            String password = "zh110119120";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

            using (WebResponse webResponse = httpWebRequest.GetResponse())
            {
                string reader = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                Console.WriteLine(reader);
            }
            
            

        }

        /// <summary>
        /// 获取热点信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static async Task<List<HotRepo>> GetHots(string keyword)
        {
            List<HotRepo> hotRepos = new List<HotRepo>();
            if (ServicePointManager.ServerCertificateValidationCallback == null)
            {//解决远程验证问题
                ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            }

            using (var web = new HttpClient())
            {
                Console.WriteLine(GetUrl(keyword));
                var result = await web.GetAsync(GetUrl(keyword));


                if (result.IsSuccessStatusCode)
                {
                    var htmlData = await result.Content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlData);
                    Console.WriteLine(htmlData); ;
                    //Topstory-hot HotList
                    IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants().Where(x => x.HasAttributes);

                    foreach (HtmlNode item in nodes)
                    {
                        Console.WriteLine(item.Name);
                    }
                    //Console.WriteLine();
                }
            }

            return hotRepos;
        }

        private static string GetUrl(string keyword)
        {
            return "https://www.zhihu.com/" + keyword;
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
