using NHunspell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoursesPlatform.Utility.SearchUtility
{
    class Normalizer
    {
        private static string en_us_aff_virtual_path = "Utility\\SearchUtility\\res\\en_us.aff";
        private static string en_us_aff_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, en_us_aff_virtual_path);

        private static string en_us_dic_virtual_path = "Utility\\SearchUtility\\res\\en_us.dic";
        private static string en_us_dic_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, en_us_dic_virtual_path);

        private static string ar_aff_virtual_path = "Utility\\SearchUtility\\res\\ar.aff";
        private static string ar_aff_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ar_aff_virtual_path);

        private static string ar_dic_virtual_path = "Utility\\SearchUtility\\res\\ar.dic";
        private static string ar_dic_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ar_dic_virtual_path);

        #region Arabic Stop Words
        private static String ASWA = @"بيد
                    سوى
                    غير
                    لاسيما
                    متى
                    أنّى
                    أيّ
                    ّأيّان
                    أين
                    بكم
                    بما
                    بماذا
                    بمن
                    كم
                    كيف
                    ما
                    ماذا
                    متى
                    مما
                    ممن
                    من
                    أنّى
                    أيّ
                    ّأيّان
                    أين
                    أينما
                    حيثما
                    كيفما
                    ما
                    متى
                    من
                    مهما
                    أولئك
                    أولئكم
                    أولاء
                    أولالك
                    تانِ
                    تانِك
                    تلك
                    تلكم
                    تلكما
                    تِه
                    تِي
                    تَيْنِ
                    تينك
                    ثمّ
                    ثمّة
                    ذا
                    ذاك
                    ذانِ
                    ذانك
                    ذلك
                    ذلكم
                    ذلكما
                    ذلكن
                    ذِه
                    ذوا
                    ذواتا
                    ذواتي
                    ذِي
                    ذَيْنِ
                    ذينك
                    كذلك
                    هَؤلاء
                    هَاتانِ
                    هَاتِه
                    هَاتِي
                    هَاتَيْنِ
                    هاهنا
                    هَذا
                    هَذانِ
                    هَذِه
                    هَذِي
                    هَذَيْنِ
                    هكذا
                    هنا
                    هناك
                    هنالك
                    أي
                    إذ
                    إذا
                    بعض
                    تجاه
                    تلقاء
                    جميع
                    حسب
                    حيث
                    سبحان
                    سوى
                    شبه
                    كل
                    لعمر
                    لما
                    مثل
                    مع
                    معاذ
                    نحو
                    أقل
                    أكثر
                    إذاً
                    آهاً
                    بسّ
                    حاي
                    صهْ
                    صهٍ
                    طاق
                    طَق
                    عَدَسْ
                    كِخ
                    نَخْ
                    هَجْ
                    وا
                    واهاً
                    وَيْ
                    آمينَ
                    آه
                    أُفٍّ
                    أُفٍّ
                    أمامك
                    أمامكَ
                    أوّهْ
                    إلَيْكَ
                    إلَيْكَ
                    إليكَ
                    إليكم
                    إليكم
                    إليكما
                    إليكنّ
                    إيهٍ
                    بخٍ
                    بَسْ
                    بطآن
                    بَلْهَ
                    حَذارِ
                    حيَّ
                    حيَّ
                    دونك
                    رويدك
                    سرعان
                    شَتَّانَ
                    عليك
                    مكانَك
                    مكانَك
                    مكانَك
                    مكانكم
                    مكانكما
                    مكانكنّ
                    مه
                    ها
                    هاؤم
                    هاكَ
                    هلمَّ
                    هيّا
                    هيت
                    هَيْهَاتَ
                    وا
                    وراءَك
                    وُشْكَانََ
                    ويكأنّ
                    الألاء
                    الألى
                    التي
                    الذي
                    الذين
                    اللائي
                    اللاتي
                    اللتان
                    اللتيا
                    اللتين
                    اللذان
                    اللذين
                    اللواتي
                    أََيُّ
                    ذا
                    ذات
                    ما
                    مَنْ
                    أب
                    أخ
                    حم
                    ذو
                    فو
                    لن
                    لو
                    لولا
                    لوما
                    نعم
                    بِئْسَ
                    حَبَّذَا
                    سَاءَ
                    سَاءَمَا
                    نِعْمَ
                    نِعِمّا
                    إن
                    لات
                    ما
                    لا
                    أَنَّ
                    إِنَّ
                    علًّ
                    كَأَنَّ
                    لَعَلَّ
                    لَكِنَّ
                    لَيْتَ
                    آي
                    كي
                    أجمع
                    جميع
                    عامة
                    عين
                    كل
                    كلا
                    كلاهما
                    كلتا
                    كليكما
                    كليهما
                    نفس
                    ء
                    ؤ
                    ئ
                    آ
                    أ
                    أ
                    ب
                    ت
                    ة
                    ث
                    ج
                    ح
                    خ
                    د
                    ذ
                    ر
                    ز
                    س
                    ش
                    ص
                    ض
                    ط
                    ظ
                    ع
                    غ
                    ف
                    ق
                    ك
                    ل
                    م
                    ن
                    ه
                    و
                    ى
                    ي
                    إلّا
                    حاشا
                    خلا
                    عدا
                    لكن
                    فيم
                    فيما
                    هل
                    سوف
                    هلّا
                    قد
                    إمّا
                    كأنّما
                    كما
                    لكي
                    لكيلا
                    إلى
                    رُبَّ
                    على
                    عن
                    في
                    مِن
                    عَمَّا
                    حَتَّى
                    منذ
                    مذ
                    لم
                    لمّا
                    أجل
                    إذن
                    إي
                    بلى
                    جلل
                    جير
                    كلَّا
                    إذما
                    لئن
                    أمّا
                    ألا
                    أما
                    أم
                    أو
                    بل
                    ثُمَّ
                    أيا
                    هيا
                    أن
                    بك
                    بكم
                    بكما
                    بكن
                    بنا
                    به
                    بها
                    بي
                    لك
                    لكم
                    لكما
                    لكن
                    لنا
                    له
                    لها
                    لي
                    أنا
                    أنت
                    أنتِ
                    أنتم
                    أنتما
                    أنتن
                    نحن
                    هم
                    هما
                    هن
                    هو
                    هي
                    إياك
                    إياك
                    إياكم
                    إياكما
                    إياكن
                    إيانا
                    إياه
                    إياها
                    إياهم
                    إياهما
                    إياهن
                    إياي
                    دون
                    ريث
                    عند
                    عوض
                    قبل
                    قطّ
                    كلّما
                    لدن
                    لدى
                    لمّا
                    الآن
                    آناء
                    آنفا
                    أبدا
                    أصلا
                    أمد
                    أمس
                    أول
                    أيّان
                    إذ
                    بعد
                    تارة
                    حين
                    صباح
                    ضحوة
                    غدًا
                    غداة
                    مرّة
                    مساء
                    يومئذ
                    خلال
                    أمام
                    أين
                    إزاء
                    بين
                    تحت
                    ثمّ
                    خلف
                    شمال
                    ضمن
                    فوق
                    يمين
                    حوالى
                    حول
                    طالما
                    قلما
                    ابتدأ
                    اخلولق
                    انبرى
                    أخذ
                    أقبل
                    أنشأ
                    أوشك
                    جعل
                    حرى
                    شرع
                    طفق
                    عسى
                    علق
                    قام
                    كاد
                    كرب
                    هبّ
                    إنّما
                    لكنَّما
                    ارتدّ
                    استحال
                    انقلب
                    آض
                    أصبح
                    أضحى
                    أمسى
                    بات
                    تبدّل
                    تحوّل
                    حار
                    راح
                    رجع
                    صار
                    ظلّ
                    عاد
                    غدا
                    كان
                    ماانفك
                    مابرح
                    مادام
                    مازال
                    مافتئ
                    لَيْسَ
                    لَسْتُ
                    لَِسْنَا
                    لَسْتَ
                    لَسْتِ
                    لَسْتُمَا
                    لَسْتُم
                    لَسْتُنَّ
                    لَيْسَتْ
                    لَيْسَا
                    لَيْسَتَا
                    لَيْسُوا
                    لَسْنَ
                    بضع
                    ذيت
                    فلان
                    كأيّ
                    كأين
                    كأيّن
                    كذا
                    كم
                    كيت";
        #endregion

        private static String arabicStopWordsStr = Regex.Replace(ASWA, @"\s+", " ");

        public static string[] arabicStopWordArray = arabicStopWordsStr.Split(' ');

        public static string[] englishStopwordsArray = {
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

        public static bool isEnglish(string text)
        {
            bool isEn = true;


            isEn = Regex.IsMatch(text, "^[a-zA-Z0-9. -_?]*$");

            if (!isEn)
            {
                bool isAr = Regex.IsMatch(text, "[\u0600-\u06ff\\s0-9]*");

                if (!isAr)
                {
                    isEn = true;
                }
            }



            return isEn;
        }



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

                norm = TrimPunctuation(norm);
                norm = removePeriods(norm);

                if (norm == "")
                {
                    continue;
                }
                List<NormalizeToken> normsList = new List<NormalizeToken>();

                bool isEnbool = isEnglish(norm);
                if (isEnbool)
                {
                    normsList = normalizeEnglish(norm);

                }
                else
                {
                    normsList = normalizeArabic(norm);
                }

                if (!checkIfStrInNormalizeTokenOrNot(norm,normsList))
                {
                    NormalizeToken normalizeToken = new NormalizeToken()
                    {
                        source = norm,
                        stem = norm,
                        isEn = isEnbool
                    };
                    normsList.Add(normalizeToken);
                }

                foreach (NormalizeToken ns in normsList)
                {
                    ret.Add(ns);
                }
            }
            return ret;
        }

        private static bool checkIfStrInNormalizeTokenOrNot(String theSource , List<NormalizeToken> normalizeTokens)
        {
            bool isFound = false;

            foreach(NormalizeToken norm in normalizeTokens)
            {
                if(norm.stem == theSource)
                {
                    isFound = true;
                    break;
                }
            }

            return isFound;
        }

        private static List<NormalizeToken> normalizeEnglish(String input)
        {
            List<String> stopWords = englishStopwordsArray.ToList<String>();

            List<NormalizeToken> list = new List<NormalizeToken>();


            using (Hunspell hunspell = new Hunspell(en_us_aff_path, en_us_dic_path))
            {
                if (!stopWords.Contains(input))
                {

                    List<string> stems = hunspell.Stem(input);
                    if (stems.Count == 0)
                    {
                        NormalizeToken normalizeToken = new NormalizeToken()
                        {
                            source = input,
                            stem = input,
                            isEn = true
                        };
                        list.Add(normalizeToken);
                    }
                    else
                    {
                        foreach (string sstem in stems)
                        {
                            NormalizeToken normalizeToken = new NormalizeToken()
                            {
                                source = input,
                                stem = sstem,
                                isEn = true
                            };
                            list.Add(normalizeToken);
                        }
                    }

                }
            }
            return list;
        }

        private static List<NormalizeToken> normalizeArabic(String input)
        {
            List<String> stopWords = arabicStopWordArray.ToList<String>();

            List<NormalizeToken> list = new List<NormalizeToken>();


            using (Hunspell hunspell = new Hunspell(ar_aff_path, ar_dic_path))
            {
                if (!stopWords.Contains(input))
                {

                    List<string> stems = hunspell.Stem(input);
                    if (stems.Count == 0)
                    {
                        NormalizeToken normalizeToken = new NormalizeToken()
                        {
                            source = input,
                            stem = input,
                            isEn = false
                        };
                        list.Add(normalizeToken);
                    }
                    else
                    {
                        foreach (string sstem in stems)
                        {
                            NormalizeToken normalizeToken = new NormalizeToken()
                            {
                                source = input,
                                stem = sstem,
                                isEn = false
                            };
                            list.Add(normalizeToken);
                        }
                    }

                }
            }
            return list;
        }

    }

    public class NormalizeToken
    {
        public String source { get; set; }
        public String stem { get; set; }
        public bool isEn { get; set; }
    }
}
