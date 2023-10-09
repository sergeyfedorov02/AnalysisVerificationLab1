using System.Collections.Generic;

namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test1
    {
        private static int TestName(int x, string y)
        {
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();
            List<string> myList = new List<string>();
            var c = 100 + 10;
            int a = 5;
            string xc = "ff";          
            a += 4;
            a += c;
            int b = 7;
            b = a - 3;
            c = b * 10 + a - 32 / 4;
            c = (b * 10 + a - 32) / 4;
            b += 4;
            if (a >= b)
            {
                c = a - b;
            }
            else
            {
                c = b - a;
            }

            var result = c;
            result++;

            return result; 
        }
    }
}