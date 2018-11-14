using HtmlAgilityPack;
using LogManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Spider.ZhiHu
{
    class HTMLParserZhihu
    {
        /// <summary>
        ///  获取知乎热点数据
        /// </summary>
        /// <returns></returns>
        public static List<HotRepo> GetHotData()
        {
            ATLog.Info("获取知乎热点数据");
            string html = GetHTMLContent();
            return GetHotList(html);
        }

        /// <summary>
        /// 获取HTML内容
        /// </summary>
        private static string GetHTMLContent()
        {
            ATLog.Info("获取知乎热点HTML内容");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string url = "https://www.zhihu.com/hot";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            request.Headers.Add("Cookie", "_zap=ffcb80ae-726b-4a59-a615-410e7eb27c0a; d_c0=\"AHBks-FZGQ6PTglKpzz049qTZPR5e0aOGBQ=|1534999452\"; _xsrf=EDNW11FHbkn920HvNkAg70GjVV6oC1uP; q_c1=c069cf71863f4712957982039d03e8db|1539078018000|1534999452000; tst=h; _ga=GA1.2.97907782.1536630653; __utmz=155987696.1541512098.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); capsion_ticket=\"2|1:0|10:1541658653|14:capsion_ticket|44:NmZkOTkwZDU2MmIxNDZkODk4ZWQxMmVkZWJiZGMwYWE=|b74c9551d614e6e38d07ff820a5dcdf5013ccb9da1f3fe755431f3835c6bcb54\"; z_c0=\"2|1:0|10:1541658655|4:z_c0|92:Mi4xSS1HS0JRQUFBQUFBY0dTejRWa1pEaVlBQUFCZ0FsVk5IeWJSWEFBb1hCN3EwclVSd0VqZGkydkhERm0yUExGdkRB|d89fe04f809cd2110fa8969101cc21fc0576cd48ee6021516fefbafa8b309687\"; tgw_l7_route=3072ae0b421aa02514eac064fb2d64b5");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Cache-Control", "max-age=0");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.Method = "GET";
            request.Referer = "https://www.zhihu.com/";
            request.Headers.Add("Accept-Encoding", " gzip, deflate");
            request.KeepAlive = true;//启用长连接

            string Content = String.Empty;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    if (response.ContentEncoding.ToLower().Contains("gzip"))//解压
                    {
                        using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                Content = reader.ReadToEnd();
                            }
                        }
                    }
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))//解压
                    {
                        using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                Content = reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader reader = new StreamReader(dataStream, Encoding.UTF8))
                        {
                            Content = reader.ReadToEnd();
                        }
                    }
                }
            }
            request.Abort();
            watch.Stop();
            ATLog.Info(string.Format("请求网页用了{0}毫秒", watch.ElapsedMilliseconds.ToString()));
            Console.WriteLine("请求网页用了{0}毫秒", watch.ElapsedMilliseconds.ToString());

            return Content;
        }

        /// <summary>
        /// 解析HTML内容，获取关键数据
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private static List<HotRepo> GetHotList(string htmlContent)
        {
            ATLog.Info("解析知乎HTML内容");
            List<HotRepo> hotRepos = new List<HotRepo>();//存入数据的数据类型
            List<HotData> hotList = new List<HotData>();//分析的完整数据类型

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            HtmlNode node = doc.GetElementbyId("js-initialData");
            StringBuilder jsonContent = new StringBuilder(node.InnerText);
            JObject jsonObj = JObject.Parse(jsonContent.ToString());
            JToken tokens = jsonObj.SelectToken("initialState.topstory.hotList",true);

            if (tokens.Type == JTokenType.Array)
            {
                foreach (var token in tokens)
                {
                    HotData item = JsonConvert.DeserializeObject<HotData>(token.ToString());
                    hotList.Add(item);
                }
            }

            hotRepos = HotData.Convert(hotList);
            return hotRepos;
        }

        private static string GetUrl(string keyword)
        {
            return "https://www.zhihu.com/" + keyword;
        }
    }
}
