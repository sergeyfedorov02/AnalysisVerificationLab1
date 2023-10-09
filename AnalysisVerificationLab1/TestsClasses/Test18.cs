namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test18
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
                        return c;
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