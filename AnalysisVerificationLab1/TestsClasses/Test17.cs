namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test17
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
                else
                {
                    a += 1;
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