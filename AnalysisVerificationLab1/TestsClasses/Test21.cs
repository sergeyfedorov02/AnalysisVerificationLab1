namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test21
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
                if (c > 10)
                {
                    c = 10;
                    break;
                }
            }
            
            var result = c;
            return result;
        }
    }
}