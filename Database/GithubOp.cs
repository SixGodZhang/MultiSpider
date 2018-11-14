using Spider.MultiDbContext;
using Repos;
using System.Collections.Generic;

namespace Spider.Database
{
    //public class GithubOpTest
    //{
    //    static void Main(string[] args)
    //    {
    //        TrendingRepo trending = new TrendingRepo();
    //        trending.Url = "111";
    //        trending.Forks = 22;
    //        trending.Language = "csharp";
    //        trending.RepoTitle = "Test";
    //        trending.RepoOwner = "zhansan";
    //        trending.Stars = 999;
    //        trending.RepoDescription = "desc";
    //        trending.StarsToday = 333;

    //        TrendingRepo trending1 = new TrendingRepo();
    //        trending1.Url = "111";
    //        trending1.Forks = 22;
    //        trending1.Language = "csharp";
    //        trending1.RepoTitle = "Test";
    //        trending1.RepoOwner = "zhansan";
    //        trending1.Stars = 999;
    //        trending1.RepoDescription = "desc";
    //        trending1.StarsToday = 333;

    //        //GithubOp.Instance.SaveData(trending1);
    //        GithubOp.Instance.SaveRangeData(new List<TrendingRepo>() { trending1, trending });
    //        Console.WriteLine("SaveRangeData end...");

    //        ThemeRepo themeRepo = new ThemeRepo();
    //        GithubOp.Instance.SaveData(themeRepo);
    //    }

    internal class GithubOp : IDatabaseOp
    {
        private static GithubOp _instance;

        public static GithubOp Instance
        {
            get
            {
                if (_instance == null) _instance = new GithubOp();
                return _instance;
            }
        }

        /// <summary>
        /// 保存一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void SaveData<T>(T data) where T : IRepo
        {
            using (GithubContext github = new GithubContext())
            {
                if (typeof(T) == typeof(TrendingRepo))
                    github.TrendingEntities.Add(data as TrendingRepo);
                else if (typeof(T) == typeof(ThemeRepo))
                    github.ThemeEntities.Add(data as ThemeRepo);
                github.SaveChanges();
            }
        }

        /// <summary>
        /// 保存一组数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        public void SaveRangeData<T>(IEnumerable<T> datas) where T : IRepo
        {
            using (GithubContext github = new GithubContext())
            {
                if (typeof(T) == typeof(TrendingRepo))
                    github.TrendingEntities.AddRange(datas as IEnumerable<TrendingRepo>);
                else if (typeof(T) == typeof(ThemeRepo))
                    github.ThemeEntities.AddRange(datas as IEnumerable<ThemeRepo>);
                github.SaveChanges();
            }
        }
    }
}