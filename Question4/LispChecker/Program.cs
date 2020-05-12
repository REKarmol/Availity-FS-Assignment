using System;

namespace LispChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            string sampleString = "";
            while (true)
            {
                Console.Write("Enter LISP string: ");
                sampleString = Console.ReadLine();
                if (sampleString.Equals(""))
                {
                    break;
                }
                else
                {
                    LispReader lispReader = new LispReader(sampleString);
                    if (lispReader.CheckParentheses())
                    {
                        Console.WriteLine("  True: {0}", sampleString);
                    }
                    else
                    {
                        Console.WriteLine("  False: {0}", sampleString);
                    }
                }
            }
        }
    }
}
