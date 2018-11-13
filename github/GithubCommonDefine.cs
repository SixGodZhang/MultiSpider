using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSpider
{
    public partial class CommonDefine
    {
        public static Dictionary<string, string> addressRoot = new Dictionary<string, string>
        {
            { "Repositories","https://api.github.com/search/repositories"},
            { "Commits","https://api.github.com/search/commits"},
            { "Code","https://api.github.com/search/code"},
            { "Issues","https://api.github.com/search/issues"},
            { "Users","https://api.github.com/search/users"},
            { "Topics","https://api.github.com/search/topics"},
            { "Labels","https://api.github.com/search/labels"},
        };

        //public static Dictionary<string, List<string>> ThemeDict = new Dictionary<string, List<string>>();

        ////语言和关注者对应,一对多关系
        //public static Dictionary<string, List<string>> DomainDict = new Dictionary<string, List<string>>
        //{
         //   { FllowLanguage.C,new List<string>()},
        //    { FllowLanguage.CPlusPlus,new List<string>()},
        //    { FllowLanguage.CSharp,new List<string>()},
        //    { FllowLanguage.Java,new List<string>()},
        //    { FllowLanguage.Python,new List<string>()},
        //    { FllowLanguage.UnKnown,new List<string>()}
        //};
    }
}
