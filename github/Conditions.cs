using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Github
{
    class Conditions
    {
        public static string SetRequestRootURL(string key)
        {
            return CommonDefine.addressRoot[key];
        }

        public static string SetQueryKeyWord(string keyWord)
        {
            return "q=" + keyWord;
        }

        public static string SetLanguage(string searchLanguage)
        {
            return "&l=" + searchLanguage;
        }

        public static string SetType(string type)
        {
            return "&type=" + type;
        }

        public static string SetResultPage(int count)
        {
            return "&p=" + count;
        }
    }
}
