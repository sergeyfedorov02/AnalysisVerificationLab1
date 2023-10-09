namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test16
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";
            
            if (c >= a)
            {
                if (c != 6)
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