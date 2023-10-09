namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test6
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
                c -= 1;
                if (c != 43)
                {
                    a += 1;
                    if (a < 4)
                    {
                        for (int j = 1; j != c; j++)
                        {
                            a++;
                        }

                        if (c < 4)
                        {
                            c++;
                        }
                        c--;
                    }
                }
            }

            var result = c;
            return result;
        }
    }
}