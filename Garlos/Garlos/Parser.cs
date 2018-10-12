using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garlos
{
    public class Parser
    {
        public string left;
        public string right;
        private string pass1;
        private string pass2;
        public int splitsize;


        public Parser()
        {
            left = "";
            right = "";
            splitsize = 30;
        }

        public void AddCol1(string line)
        {
            left += line + "\n";
        }
        public void AddCol2(string line)
        {
            right += line + "\n";
        }

        public void Clear()
        {
            left = "";
            right = "";
        }

        public void Display()
        {
            int counter;
            int chop;
            int chop2;
            string col1 = left;
            string col2 = right;
            int nextnewline;
            int nextnewline2;
            int lastspace;
            int lastspace2;
            chop = (col1.Length >= splitsize) ? splitsize : col1.Length;  //either chop the line at # of splitsize or the length of the line
            nextnewline = col1.IndexOf("\n");//where is the first new line symbol in the block
            if ((nextnewline < chop) && (nextnewline != -1))
            {
                pass1 = col1.Substring(0, col1.IndexOf("\n"));
                col1 = col1.Substring(col1.IndexOf("\n") + 1, col1.Length - col1.IndexOf("\n") - 1);
            }
            else
            {
                

                pass1 = col1.Substring(0, chop);
                lastspace = pass1.LastIndexOf(" ");
                if((lastspace < chop) && (lastspace != -1))
                {
                    pass1 = pass1.Substring(0, lastspace);
                    chop = lastspace;
                }
                col1 = col1.Substring(chop + 1, col1.Length - chop - 1);
            }
            if(Utility.NotBlank(right))//add on the right side
            {
                while(pass1.Length < splitsize)
                {
                    pass1 += " ";
                }
                //count colorizes and add spaces in their place because they are 2 characters that do not display on screen
                counter = pass1.Where(x => x == '#').Count();
                for(int CharLength = 0; CharLength < counter; CharLength++)
                {
                    pass1 += "  ";
                }
                chop2 = (col2.Length >= splitsize) ? splitsize : col2.Length;
                nextnewline2 = col2.IndexOf("\n");
                if ((nextnewline2 < chop2) && (nextnewline2 != -1))
                {
                    pass2 = col2.Substring(0, col2.IndexOf("\n"));
                    col2 = col2.Substring(col2.IndexOf("\n") + 1, col2.Length - col2.IndexOf("\n") - 1);
                }
                else
                {


                    pass2 = col2.Substring(0, chop2);
                    lastspace2 = pass2.LastIndexOf(" ");
                    if ((lastspace2 < chop2) && (lastspace2 != -1))
                    {
                        pass2 = pass2.Substring(0, lastspace2);
                        chop2 = lastspace2;
                    }
                    col2 = col2.Substring(chop2 + 1, col2.Length - chop2 - 1);
                }
            }

            while(Utility.NotBlank(pass1) && Utility.NotBlank(col1))
            {
                Utility.Colorize(pass1 + "#w" + pass2);
                chop = (col1.Length >= splitsize) ? splitsize : col1.Length;
                nextnewline = col1.IndexOf("\n");
                if ((nextnewline < chop) && (nextnewline != -1))
                {
                    pass1 = col1.Substring(0, col1.IndexOf("\n"));
                    col1 = col1.Substring(col1.IndexOf("\n") + 1, col1.Length - col1.IndexOf("\n") - 1);
                }
                else
                {

                    pass1 = col1.Substring(0, chop);
                    lastspace = pass1.LastIndexOf(" ");
                    if ((lastspace < chop) && (lastspace != -1))
                    {
                        pass1 = pass1.Substring(0, lastspace);
                        chop = lastspace;
                    }
                    col1 = col1.Substring(chop + 1, col1.Length - chop - 1);
                    
                }
                if (Utility.NotBlank(right) && Utility.NotBlank(pass2) && Utility.NotBlank(col2))//add on the right side
                {
                    while (pass1.Length < splitsize)
                    {
                        pass1 += " ";
                    }
                    counter = pass1.Where(x => x == '#').Count();
                    for (int CharLength = 0; CharLength < counter; CharLength++)
                    {
                        pass1 += "  ";
                    }
                    chop2 = (col2.Length >= splitsize) ? splitsize : col2.Length;
                    nextnewline2 = col2.IndexOf("\n");
                    if ((nextnewline2 < chop2) && (nextnewline2 != -1))
                    {
                        pass2 = col2.Substring(0, col2.IndexOf("\n"));
                        col2 = col2.Substring(col2.IndexOf("\n") + 1, col2.Length - col2.IndexOf("\n") - 1);
                    }
                    else
                    {


                        pass2 = col2.Substring(0, chop2);
                        lastspace2 = pass2.LastIndexOf(" ");
                        if ((lastspace2 < chop2) && (lastspace2 != -1))
                        {
                            pass2 = pass2.Substring(0, lastspace2);
                            chop2 = lastspace2;
                        }
                        col2 = col2.Substring(chop2 + 1, col2.Length - chop2 - 1);
                    }

                }
                else
                {
                    pass2 = "";
                }

            }
            Utility.Colorize(pass1 + pass2);
            pass1 = "";
            //finish writing if there is more in the right column
            while (Utility.NotBlank(right) && Utility.NotBlank(pass2) && Utility.NotBlank(col2))//add on the right side
            {
                
                    while (pass1.Length < splitsize)
                    {
                        pass1 += " ";
                    }

                    chop2 = (col2.Length >= splitsize) ? splitsize : col2.Length;
                    nextnewline2 = col2.IndexOf("\n");
                    if ((nextnewline2 < chop2) && (nextnewline2 != -1))
                    {
                        pass2 = col2.Substring(0, col2.IndexOf("\n"));
                        col2 = col2.Substring(col2.IndexOf("\n") + 1, col2.Length - col2.IndexOf("\n") - 1);
                    }
                    else
                    {


                        pass2 = col2.Substring(0, chop2);
                        lastspace2 = pass2.LastIndexOf(" ");
                        if ((lastspace2 < chop2) && (lastspace2 != -1))
                        {
                            pass2 = pass2.Substring(0, lastspace2);
                            chop2 = lastspace2;
                        }
                        col2 = col2.Substring(chop2 + 1, col2.Length - chop2 - 1);
                    }
                    Utility.Colorize(pass1 + pass2);

            }

        }

    }


}
