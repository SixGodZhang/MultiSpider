using System;

/// <summary>
/// 此文件保存数据清洗后的对象类型
/// </summary>
namespace GithubSpider
{
    public class TrendingRepo
    {
        public String RepoOwner { get; set; }
        public String RepoTitle { get; set; }
        public String Url { get; set; }

        public String RepoDescription { get; set; }

        public String Language { get; set; }

        public int StarsToday { get; set; }
        public int Stars { get; set; }
        public int Forks { get; set; }
    }

    public class ThemeRepo
    {
        public ThemeRepo(string name, string addrress, string desc, int stars, string language, float score, string ownerName)
        {
            Name = name;
            Addrress = addrress;
            Desc = desc;
            Stars = stars;
            Language = language;
            Score = score;
            OwnerName = ownerName;
        }

        public String Name { get; set; }
        public String Addrress { get; set; }
        public String Desc { get; set; }
        public int Stars { get; set; }
        public String Language { get; set; }
        public float Score { get; set; }
        public String OwnerName { get; set; }
    }

}
