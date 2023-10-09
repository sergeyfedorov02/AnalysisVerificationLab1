namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test7
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";


            if (c >= a)
            {
                if (c > a)
                {
                    c = c - 10;
                }
            }


            var result = c;
            return result;
        }
    }
}