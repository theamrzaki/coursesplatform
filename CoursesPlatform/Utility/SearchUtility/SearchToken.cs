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
        public bool isEn { get; set; }

        public static List<SearchToken> getTokens(String input)
        {
            List<SearchToken> searchTokens = new List<SearchToken>();

            String[] tokens = Tokenizer.getTokens(input);

            List<NormalizeToken> normalizeTokensList = Normalizer.normalize(tokens);

            foreach (NormalizeToken normalizeToken in normalizeTokensList)
            {
                if (normalizeToken.isEn)
                {
                    String soundexText = EnglishSoundex.Generate(normalizeToken.stem);
                    SearchToken searchToken = new SearchToken()
                    {
                        tag = normalizeToken.stem,
                        soundex = soundexText,
                        source = normalizeToken.source,
                        isEn = true
                        
                    };
                    searchTokens.Add(searchToken);
                }else
                {
                    List<String> soundexList = ArabicSoundex.Generate(normalizeToken.stem);
                    foreach(String s in soundexList)
                    {
                        SearchToken searchToken = new SearchToken()
                        {
                            tag = normalizeToken.stem,
                            soundex = s,
                            source = normalizeToken.source,
                            isEn = false
                        };
                        searchTokens.Add(searchToken);
                    }
                }
            }
            return searchTokens;
        }
    }
}