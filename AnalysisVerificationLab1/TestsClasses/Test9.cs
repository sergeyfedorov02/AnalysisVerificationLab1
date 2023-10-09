namespace AnalysisVerificationLab1.TestsClasses
{
    public class Test9
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
                    for (var k = 5; k < 10; k++)
                    {
                        c = c - 10;
                    }
                }
            }
            
            var result = c;
            return result;
        }
    }
}