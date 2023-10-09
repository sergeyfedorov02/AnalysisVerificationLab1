namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test20
    {
        private static int TestName(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";
            
            for (var i=0; i < a; i++)
            {
                if( c != 6)
                {
                    while (a!=10)
                    {
                        c = c - 10;
                        c++;
                        c--;
                    }
                }

                c += 10;
            }
            
            var result = c;
            return result;
        }
    }
}