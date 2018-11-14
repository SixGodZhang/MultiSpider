using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Github
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
    }
}
