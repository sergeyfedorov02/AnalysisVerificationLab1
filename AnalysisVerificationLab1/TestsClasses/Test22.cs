namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test22
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            for (var k = 0; k < a; k++)
            {
                if (k > a)
                {
                    break; 
                }

                return a;

            }

            for (var i = 0; i < a; i++)
            {
                if (c != 6)
                {
                    while (a != 10)
                    {
                        c = c - 10;
                        c++;
                        break;
                    }
                }

                if (c > 10)
                {
                    c = 10;
                    break;
                }

                c += 10;
            }

            var result = c;
            return result;
        }
    }
}