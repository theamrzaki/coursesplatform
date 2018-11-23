using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CoursesPlatform.Utility.SearchUtility
{
    public class ArabicSoundex
    {
        public static List<string> Generate(String word)
        {
            // Value to return
            string value = "";
            int length = 4;

            //switch (word[0])
            //{
            //    case 'ا':
            //    case 'أ':
            //    case 'إ':
            //    case 'آ':
            //        {
            //            word = word.Substring(1, word.Length - 1);
            //        }
            //        break;

            //}

            // Size of the word to process
            int size = word.Length;
            // Make sure the word is at least two characters in length
            List<string> allSoundex = new List<string>();

            if (size > 1)
            {

                // Convert the word to character array for faster processing
                char[] chars = word.ToCharArray();
                // Buffer to build up with character codes

                List<char> firstChars = getFirstChar(chars);

                foreach (char c in firstChars)
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.Length = 0;
                    // The current and previous character codes
                    int prevCode = 0;
                    int currCode = 0;

                    buffer.Append(c);

                    // Loop through all the characters and convert them to the proper character code
                    for (int i = 1; i < size; i++)
                    {
                        switch (chars[i])
                        {
                            case 'ا':
                            case 'أ':
                            case 'إ':
                            case 'آ':
                            case 'ح':
                            case 'خ':
                            case 'ه':
                            case 'ع':
                            case 'غ':
                            case 'ش':
                            case 'و':
                            case 'ي':
                                currCode = 0;
                                break;
                            case 'ف':
                            case 'ب':
                                currCode = 1;
                                break;

                            case 'ج':
                            case 'ز':
                            case 'س':
                            case 'ص':
                            case 'ظ':
                            case 'ق':
                            case 'ك':
                                currCode = 2;
                                break;
                            case 'ت':
                            case 'ث':
                            case 'د':
                            case 'ذ':
                            case 'ض':
                            case 'ط':
                                currCode = 3;
                                break;
                            case 'ل':
                                currCode = 4;
                                break;
                            case 'م':
                            case 'ن':
                                currCode = 5;
                                break;
                            case 'ر':
                                currCode = 6;
                                break;
                        }

                        // Check to see if the current code is the same as the last one
                        if (currCode != prevCode)
                        {
                            // Check to see if the current code is 0 (a vowel); do not process vowels
                            if (currCode != 0)
                                buffer.Append(currCode);
                        }
                        // Set the new previous character code
                        prevCode = currCode;
                        // If the buffer size meets the length limit, then exit the loop
                        if (buffer.Length == length)
                            break;
                    }
                    // Pad the buffer, if required
                    int bufferSize = buffer.Length;
                    if (bufferSize < length)
                        buffer.Append('0', (length - bufferSize));
                    // Set the value to return
                    value = buffer.ToString();
                    allSoundex.Add(value);
                }

            }
            // Return the value
            return allSoundex;
        }

        private static List<char> getFirstChar(char[] chars)
        {
            List<char> charList = new List<char>();

            switch (chars[0])
            {
                case 'ا':
                case 'أ':
                case 'إ':
                case 'آ':
                    if (chars[1] == 'ي' || chars[1] == 'ى')
                    {
                        charList.Add('I');

                    }
                    else
                    {
                        charList.Add('A');
                    }
                    charList.Add('E');
                    break;
                case 'ع':
                    charList.Add('A');
                    charList.Add('O');
                    break;
                case 'ب':
                    charList.Add('B');
                    charList.Add('P');
                    break;
                case 'ت':
                case 'ث':
                case 'ط':
                    charList.Add('T');
                    break;
                case 'ج': //could be G or J
                case 'چ':
                    charList.Add('G');
                    charList.Add('J');
                    break;
                case 'غ':
                    charList.Add('G');
                    break;
                case 'ح':
                case 'ه':
                    charList.Add('H');
                    break;
                case 'خ':
                    charList.Add('K');
                    break;
                case 'ك': //could be K or C
                    charList.Add('K');
                    charList.Add('C');
                    break;
                case 'د':
                case 'ض':
                case 'ظ':
                    charList.Add('D');
                    break;
                case 'ذ': // byo2ol an l 7rf dh bywazy DH 
                case 'ز':
                    charList.Add('Z');
                    break;
                case 'ر':
                    charList.Add('R');
                    break;
                case 'س':
                    if (chars[1] == 'ي' || chars[1] == 'ى')
                    {
                        charList.Add('C');
                    }
                    else
                    {
                        charList.Add('S');
                    }
                    break;
                case 'ش':
                case 'ص':
                    charList.Add('S');
                    break;
                case 'ف':
                case 'ڤ':
                    charList.Add('F');
                    charList.Add('V');
                    break;
                case 'ق':
                    charList.Add('Q');
                    charList.Add('K');
                    break;
                case 'ل':
                    charList.Add('L');
                    break;
                case 'م':
                    charList.Add('M');
                    break;
                case 'ن':
                    charList.Add('N');
                    break;
                case 'و':
                    charList.Add('W');
                    break;
                case 'ى':
                case 'ي':
                    charList.Add('Y');
                    break;
                default:
                    charList.Add('X');
                    break;
            }
            return charList;
        }
    }
}