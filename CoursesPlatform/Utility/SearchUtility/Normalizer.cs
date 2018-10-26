using NHunspell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesPlatform.Utility.SearchUtility
{
    class Normalizer
    {

        public static string[] stopwordsArray = {
                                        "I" ,
                                        "a"     ,
                                        "about" ,
                                        "an" ,
                                        "are" ,
                                        "as" ,
                                        "at" ,
                                        "be" ,
                                        "by" ,
                                        "com" ,
                                        "for" ,
                                        "from",
                                        "how",
                                        "in" ,
                                        "is" ,
                                        "it" ,
                                        "of" ,
                                        "on" ,
                                        "or" ,
                                        "that",
                                        "the" ,
                                        "this",
                                        "to" ,
                                        "was" ,
                                        "what" ,
                                        "when",
                                        "where",
                                        "who",
                                        "will",
                                        "with",
                                        "the",
                                         "www"
                                                 };

        private static List<String> stopWords;



        private static string removePeriods(string input)
        {
            string ret = input.Replace(".", "");
            ret = ret.Replace("-", "");
            ret = ret.Replace("$", "");
            ret = ret.Replace("%", "");
            ret = ret.Replace("@", "");
            ret = ret.Replace("^", "");

            return ret;
        }

        private static string TrimPunctuation(string value)
        {
            // Count start punctuation.
            int removeFromStart = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsPunctuation(value[i]))
                {
                    removeFromStart++;
                }
                else
                {
                    break;
                }
            }

            // Count end punctuation.
            int removeFromEnd = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (char.IsPunctuation(value[i]))
                {
                    removeFromEnd++;
                }
                else
                {
                    break;
                }
            }
            // No characters were punctuation.
            if (removeFromStart == 0 &&
                removeFromEnd == 0)
            {
                return value;
            }
            // All characters were punctuation.
            if (removeFromStart == value.Length &&
                removeFromEnd == value.Length)
            {
                return "";
            }
            // Substring.
            return value.Substring(removeFromStart,
                value.Length - removeFromEnd - removeFromStart);
        }


        public static List<String> normalize(string[] input)
        {
            stopWords = stopwordsArray.ToList<String>();
            List<String> ret = new List<String>();
            string norm;
            foreach(string i in input)
            {
                if(i == "")
                {
                    continue;
                }
                try
                {
                    norm = i.Normalize(NormalizationForm.FormD);
                }catch(Exception ex)
                {
                    norm = i;
                }

                if (!stopWords.Contains(norm))
                {
                    norm = TrimPunctuation(norm);
                    norm = removePeriods(norm);
                    if (norm == "")
                    {
                        continue;
                    }
                    Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic");
                    
                   List<string> stems = hunspell.Stem(norm);
                    
                    if (stems.Count == 0)
                    {
                        ret.Add(norm);
                    }
                    else
                    {
                        foreach (string stem in stems)
                        {
                            ret.Add(stem);
                        }
                    }
                }              
            }
            return ret;
        }
    }
}
