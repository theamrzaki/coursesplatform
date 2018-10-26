using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoursesPlatform.Utility.SearchUtility
{
    class Tokenizer
    {

        

        public static string[] getTokens(String input)
        {
            input = input.ToLower();
            
            input = Regex.Replace(input, @"\s+", " ");
            char[] param =  {' '};
            string[] ret = input.Split(param);
            return ret;
        }
    }
}
