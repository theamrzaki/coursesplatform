using NHunspell;
using System;
using System.Collections.Generic;
using System.IO;
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


        public static List<NormalizeToken> normalize(string[] input)
        {
            stopWords = stopwordsArray.ToList<String>();
            List<NormalizeToken> ret = new List<NormalizeToken>();
            
            foreach(string str in input)
            {
                String norm = str;
                if(norm == "")
                {
                    continue;
                }
                try
                {
                    norm = norm.Normalize(NormalizationForm.FormD);
                }catch(Exception ex)
                {

                }

                if (!stopWords.Contains(norm))
                {
                    norm = TrimPunctuation(norm);
                    norm = removePeriods(norm);
                    if (norm == "")
                    {
                        continue;
                    }

                    string en_us_aff_virtual_path = "Utility\\SearchUtility\\res\\en_us.aff";
                    string en_us_aff_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, en_us_aff_virtual_path);

                    string en_us_dic_virtual_path = "Utility\\SearchUtility\\res\\en_us.dic";
                    string en_us_dic_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, en_us_dic_virtual_path);
                    
                    Hunspell hunspell = new Hunspell(en_us_aff_path, en_us_dic_path);
                    
                    List<string> stems = hunspell.Stem(norm);
                    
                    if (stems.Count == 0)
                    {
                        NormalizeToken normalizeToken = new NormalizeToken();
                        normalizeToken.source = str;
                        normalizeToken.stem = norm;
                        ret.Add(normalizeToken);
                    }
                    else
                    {
                        foreach (string stem in stems)
                        {
                            NormalizeToken normalizeToken = new NormalizeToken();
                            normalizeToken.source = str;
                            normalizeToken.stem = stem;
                            ret.Add(normalizeToken);
                        }
                    }
                }              
            }
            return ret;
        }
        
    }

    public class NormalizeToken
    {
        public String source { get; set; }
        public String stem { get; set; }
    }
}
