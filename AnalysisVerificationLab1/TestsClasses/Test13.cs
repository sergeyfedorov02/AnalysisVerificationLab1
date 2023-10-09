namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test13
    {
        private static int Test5(int x, int y)
        {
            var c = 0;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i--)
            {
                if (a > c)
                {
                    c++;
                }

                for (int j = 0; j > a; j++)
                {
                    for (int k = 0; k > a; k++)
                    {
                        a++;
                        if (c < 12)
                        {
                            a += 10;
                        }
                    }
                }

                if (c != a)
                {
                    c = c - 10;
                }
                else
                {
                    a = a - c;
                }
            }
            
            
            for (int s = 0; s < a; s++)
            {
                if (c != a)
                {
                    c = c - 10;
                }

                c += 100;
            }

            return c;
        }
    }
}