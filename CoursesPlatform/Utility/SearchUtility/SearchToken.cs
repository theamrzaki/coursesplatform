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
        public String source { get; set; }

        public static List<SearchToken> getTokens(String input)
        {
            List<SearchToken> searchTokens = new List<SearchToken>();

            String[] tokens = Tokenizer.getTokens(input);

            List<NormalizeToken> normalizeTokensList = Normalizer.normalize(tokens);

            foreach (NormalizeToken normalizeToken in normalizeTokensList)
            {
                String soundexText = Soundex.Generate(normalizeToken.stem);
                SearchToken searchToken = new SearchToken()
                {
                    tag = normalizeToken.stem,
                    soundex = soundexText,
                    source = normalizeToken.source
                };
                searchTokens.Add(searchToken);
            }
            return searchTokens;
        }
    }
}