namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test15
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";
            
            if (c >= a)
            {
                return 0;
            }

            if (a < c)
            {
                return 1;
            }

            var result = c;
            return result;
        }
    }
}