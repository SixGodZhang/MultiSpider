using Repos;
using System;
using System.Collections.Generic;

namespace Spider.ZhiHu
{
    public class HotData:IZhiHuData
    {
        public string styleType { get; set; }
        public FeedSpecific feedSpecific { get; set; }
        public Target target { get; set; }
        public string cardId { get; set; }
        public string attachedInfo { get; set; }
        public string type { get; set; }
        public string id { get; set; }

        /// <summary>
        /// 转换成数据库类型(实例方法)
        /// </summary>
        /// <returns></returns>
        public HotRepo Convert()
        {
            HotRepo hotRepo = new HotRepo();
            hotRepo.Title = target.titleArea.text;
            hotRepo.MetricsArea = target.metricsArea.text;
            hotRepo.AnswerCount = feedSpecific.answerCount;
            hotRepo.Link = target.link.url;
            hotRepo.Score = feedSpecific.score;
            hotRepo.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            return hotRepo;
        }

        /// <summary>
        /// 批量装换为数据库类型(静态方法)
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static List<HotRepo> Convert(List<HotData> datas)
        {
            List<HotRepo> repos = new List<HotRepo>();
            foreach (var item in datas)
            {
                HotRepo repo = item.Convert();
                repos.Add(repo);
            }

            return repos;
        }
    }

    public class FeedSpecific
    {
        public int trend { get; set; }
        public int score { get; set; }
        public bool debut { get; set; }
        public int answerCount { get; set; }
    }

    public class Target
    {
        public LabelArea labelArea { get; set; }
        public MetricsArea metricsArea { get; set; }
        public TitleArea titleArea { get; set; }
        public ExcerptArea excerptArea { get; set; }
        public ImageArea imageArea { get; set; }
        public Link link { get; set; }
    }

    public class LabelArea
    {
        public int trend { get; set; }
        public string type { get; set; }
        public string nightColor { get; set; }
        public string normalColor { get; set; }
    }

    public class MetricsArea
    {
        public string text { get; set; }
    }

    public class TitleArea
    {
        public string text { get; set; }
    }

    public class ExcerptArea
    {
        public string text { get; set; }
    }

    public class ImageArea
    {
        public string url { get; set; }
    }

    public class Link
    {
        public string url { get; set; }
    }
}
