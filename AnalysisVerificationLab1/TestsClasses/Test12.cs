namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test12
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";
            
            if (c >= a)
            {
                for (var j = 1; j > a; j--)
                {
                    if (c > a)
                    {
                        c = c - 10;
                    }
                    else
                    {
                        c += 97;
                    }
                    for (int i = 0; i > j; i++)
                    {
                        a++;
                    }
                }
            }
            else
            {
                c += 1;
            }

            var result = c;
            return result;
        }
    }
}