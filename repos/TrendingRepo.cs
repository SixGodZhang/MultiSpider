using System;
using System.ComponentModel.DataAnnotations;

namespace Repos
{
    public class TrendingRepo:IRepo
    {
        [Key]
        public int ID { get; set; }
        public String RepoOwner { get; set; }
        public String RepoTitle { get; set; }
        public String Url { get; set; }
        public String RepoDescription { get; set; }
        public String Language { get; set; }
        public int StarsToday { get; set; }
        public int Stars { get; set; }
        public int Forks { get; set; }
    }
}
