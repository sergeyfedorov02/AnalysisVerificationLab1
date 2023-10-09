namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test14
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i++)
            {
                for (var j = 1; j > a; j--)
                {
                    if (c != a)
                    {
                        a++;
                    }
                }
            }

            var result = c;
            return result;
        }
    }
}