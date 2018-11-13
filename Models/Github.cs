namespace Models
{
    using Repos;
    using System;
    using System.Data.Entity;
    using System.Linq;

    //public class GithubDatabaseTest
    //{
    //    static void Main(string[] args)
    //    {
    //        using (Github github = new Github())
    //        {
    //            TrendingRepo trending = new TrendingRepo();
    //            trending.Url = "111";
    //            trending.Forks = 22;
    //            trending.Language = "csharp";
    //            trending.RepoTitle = "Test";
    //            trending.RepoOwner = "zhansan";
    //            trending.Stars = 999;
    //            trending.RepoDescription = "desc";
    //            trending.StarsToday = 333;

    //            github.TrendingEntities.Add(trending);
    //            github.SaveChanges();
    //        }
    //        Console.WriteLine("end");
    //    }
    //}


    public class Github : DbContext
    {
        public Github()
            : base("name=Github")
        {
            //Database.SetInitializer<Github>(null);
        }

        public virtual DbSet<TrendingRepo> TrendingEntities { get; set; }
        public virtual DbSet<ThemeRepo> ThemeEntities { get; set; }
    }
}