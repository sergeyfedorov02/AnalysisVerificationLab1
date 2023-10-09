namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test8
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i++)
            {
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

                        c++;
                    }
                }

                c--;
            }

            var result = c;
            return result;
        }
    }
}