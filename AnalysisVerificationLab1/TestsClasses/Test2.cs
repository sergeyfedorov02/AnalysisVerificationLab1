namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test2
    {
        private static int TestName(int x, int y)
        {
            var c = 0;
            c++;
            int a = 5;
            var b = "str";
            
            for (var i = 0; i < a; i--)
            {
                c += a;
                if (c < 1)
                {
                    c++;
                    if (a < 12)
                    {
                        a += 1;
                    }
                }
                a -= c;
            }

            var result = c;
            return result;
        }
    }
}