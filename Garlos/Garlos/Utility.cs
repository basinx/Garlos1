using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class Utility
    {
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        public static bool NotBlank(string strcheck)
        {
            if ((strcheck == "") || strcheck == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool AnyMatch(string userinput, string strcheck, bool pickedyet)
        {
            
            userinput = userinput.ToLower();
            strcheck = strcheck.ToLower();
            if (strcheck.Contains(userinput))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool WordMatch(string userinput, string strcheck, bool pickedyet)
        {
            int strlen;
            //strcheck = strcheck.ToLower();
            if (userinput.IndexOf(" ") == -1)
            {
                
                strlen = userinput.Length;
                if (((strcheck.Substring(0, Clamp(strlen, 0, strcheck.Length)) == userinput.ToLower()) && pickedyet == false) && NotBlank(userinput))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string firstword = userinput.Substring(0, userinput.IndexOf(" "));
                strlen = firstword.Length;
                if (((strcheck.Substring(0, Clamp(strlen, 0, strcheck.Length)) == firstword.ToLower()) && pickedyet == false) && NotBlank(userinput))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void Colorize(string tocolor)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string substr = tocolor;

            int pound = substr.IndexOf("#");

            if (pound != -1)
            {
                
                while (pound != -1)
                {
                    Console.Write(substr.Substring(0, pound));
                    char color = substr[pound + 1];
                    if (color == 'r')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (color == 'b')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    if (color == 'w')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (color == 'c')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    if (color == 'y')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    substr = substr.Substring(pound + 2, substr.Length - (pound + 2));
                    pound = substr.IndexOf("#");
                    

                }

                Console.Write(substr + "\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                
                Console.WriteLine(tocolor);
            }
            
            
        }
        public static bool KeyWord(string userinput, string strcheck)
        {
            strcheck = strcheck.ToLower();
            if (userinput.IndexOf(" ") == -1)
            {
                return false;
            }
            else
            {

                string secondword = userinput.Substring(userinput.IndexOf(" ") + 1, userinput.Length - userinput.IndexOf(" ") - 1);
                int strlen = secondword.Length;
                //Console.WriteLine("second word =" + secondword + " and length =" + strlen);
                if (((strcheck.Substring(0, Clamp(strlen, 0, strcheck.Length)) == secondword.ToLower())) && NotBlank(userinput))
                {
                    return true;
                }
                else
                {
                    string failedword = strcheck.Substring(0, Clamp(strlen, 0, strcheck.Length));
                    int failchar = failedword.Length;
                    //Console.WriteLine(failedword + " has " + failchar + " characters");
                    return false;
                }
            }
        }

        public static string GetKeyWord(string userinput)
        {

            if (userinput.IndexOf(" ") == -1)
            {
                return "";
            }
            else
            {

                return userinput.Substring(userinput.IndexOf(" ") + 1, userinput.Length - userinput.IndexOf(" ") - 1).Trim();

            }
        }

        public static string GetFirstWord(string userinput)
        {
            if (userinput.IndexOf(" ") == -1)
            {
                return userinput;
            }
            else
            {
                return userinput.Substring(0, userinput.IndexOf(" "));
            }
        }
    }
}
