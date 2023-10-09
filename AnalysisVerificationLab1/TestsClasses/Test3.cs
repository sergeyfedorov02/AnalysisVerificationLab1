namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test3
    {
        private static int TestName(int x, int y)
        {
            var c = 0;
            c++;
            int a = 5;

            if (c != a)
            {
                c += 8;
                if (a > 54)
                {
                    a -= 5;
                    if (a != 4)
                    {
                        a = 4;
                    }
                    else
                    {
                        a = 8;
                    }
                    a += 1;
                    c++;
                }
                else
                {
                    a = 54;
                    if (c > 12)
                    {
                        a++;
                    }

                    c = 111;
                }
            }

            var result = c;
            return result;
        }
    }
}