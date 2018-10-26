using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesPlatform.Utility.SearchUtility
{
    public class SearchToken
    {
        public String tag { get; set; }
        public String soundex { get; set; }

        public static List<SearchToken> getTokens(String input)
        {
            List<SearchToken> searchTokens = new List<SearchToken>();

            String[] tokens = Tokenizer.getTokens(input);

            List<String> stems = Normalizer.normalize(tokens);

            foreach (String stem in stems)
            {
                String soundexText = Soundex.Generate(stem);
                SearchToken searchToken = new SearchToken()
                {
                    tag = stem ,
                    soundex = soundexText
                };
                searchTokens.Add(searchToken);
            }
            return searchTokens;
        }
    }
}