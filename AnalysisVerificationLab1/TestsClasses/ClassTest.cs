using System.Collections.Generic;

namespace AnalysisVerificationLab1.TestsClasses
{
    public class ClassTest
    {
        private static int Test1()
        {
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();
            List<string> myList = new List<string>();
            var c = 100 + 10;
            int a = 5;
            string xc = "ff";
            a += 4;
            a += c;
            int b = 7;
            b = a - 3;
            c = b * 10 + a - 32 / 4;
            c = (b * 10 + a - 32) / 4;
            b += 4;
            if (a >= b)
            {
                c = a - b;
            }
            else
            {
                c = b - a;
            }

            var result = c;
            result++;

            return result;
        }

        private static int Test2(int x, int y)
        {
            var c = 0;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i--)
            {
                c += a;
                if (c < 1)
                {
                    c++;
                    if (a < 12)
                    {
                        a += 1;
                    }
                }

                a -= c;
            }

            var result = c;
            return result;
        }

        private static int Test3(int x, int y)
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

        private static int Test4(int x, int y)
        {
            var c = 0;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i--)
            {
                c += a;
                c -= 1;
                if (c != 43)
                {
                    a += 1;
                    if (a < 4)
                    {
                        for (int j = 1; j != c; j++)
                        {
                            a++;
                        }

                        if (c < 4)
                        {
                            c++;
                        }
                    }
                }

                a -= c;
            }

            var result = c;
            return result;
        }

        private static int Test5(int x, int y)
        {
            var c = 0;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i--)
            {
                c += a;
                c -= 1;
                if (c != 43)
                {
                    a += 1;
                    if (a < 4)
                    {
                        for (int j = 1; j != c; j++)
                        {
                            a++;
                        }

                        if (c < 4)
                        {
                            c++;
                        }

                        c--;
                    }
                }

                a -= c;
            }

            var result = c;
            return result;
        }

        private static int Test6(int x, int y)
        {
            var c = 0;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i--)
            {
                c += a;
                c -= 1;
                if (c != 43)
                {
                    a += 1;
                    if (a < 4)
                    {
                        for (int j = 1; j != c; j++)
                        {
                            a++;
                        }

                        if (c < 4)
                        {
                            c++;
                        }

                        c--;
                    }
                }
            }

            var result = c;
            return result;
        }

        private static int Test7(int x, int y)
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

        private static int Test8(int x, int y)
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

        private static int Test9(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i++)
            {
                if (c != 6)
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

        private static int Test10(int x, int y)
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
            }

            var result = c;
            return result;
        }

        private static int Test11(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

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

                    for (int i = 0; i > j; i++)
                    {
                        a++;
                    }
                }

                for (int k = 0; k > 8; k++)
                {
                    a++;
                }
            }
            else
            {
                c += 1;
            }

            var result = c;
            return result;
        }

        private static int Test12(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

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

                    for (int i = 0; i > j; i++)
                    {
                        a++;
                    }
                }
            }
            else
            {
                c += 1;
            }

            var result = c;
            return result;
        }

        private static int Test13(int x, int y)
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

        private static int Test14(int x, int y)
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

        private static int Test15(int x, int y)
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

        private static int Test16(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            if (c >= a)
            {
                if (c != 6)
                {
                    if (c > a)
                    {
                        c = c - 10;
                        return c;
                    }
                }
            }
            else
            {
                c += 1;
            }

            var result = c;
            return result;
        }

        private static int Test17(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            if (c >= a)
            {
                if (c != 6)
                {
                    if (c > a)
                    {
                        c = c - 10;
                        return c;
                    }
                }
                else
                {
                    a += 1;
                }
            }
            else
            {
                c += 1;
            }

            var result = c;
            return result;
        }

        private static int Test18(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            if (c >= a)
            {
                for (var j = 1; j > a; j--)
                {
                    if (c > a)
                    {
                        c = c - 10;
                        return c;
                    }
                }
            }
            else
            {
                c += 1;
            }

            var result = c;
            return result;
        }

        private static int Test19(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i++)
            {
                if (c != 6)
                {
                    for (var k = 5; k < 10; k++)
                    {
                        c = c - 10;
                        return c;
                    }
                }

                return x;
            }

            var result = c;
            return result;
        }

        private static int Test20(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i++)
            {
                if (c != 6)
                {
                    while (a != 10)
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

        private static int Test21(int x, int y)
        {
            var c = 10;
            c++;
            int a = 5;
            var b = "str";

            for (var i = 0; i < a; i++)
            {
                if (c != 6)
                {
                    while (a != 10)
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

        private static int Test22(int x, int y)
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