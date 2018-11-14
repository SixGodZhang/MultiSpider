using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repos
{
    [Table("Theme")]
    public class ThemeRepo:IRepo
    {
        public ThemeRepo() { }
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

        [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        public String Addrress { get; set; }
        public String Desc { get; set; }
        public int Stars { get; set; }
        public String Language { get; set; }
        public float Score { get; set; }
        public String OwnerName { get; set; }
    }
}

