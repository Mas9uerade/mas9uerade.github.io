public static class Solution
{
    public static int MyAtoi(string s)
    {
        string content = s.Trim(' ');
        char[] byContent = content.ToCharArray();
        if (byContent == null|| bycontent.length == 0)
        {
            return 0;
        }
        int sign = 1;
        int integer = 0;
        int last = 0;
        int startIndex = 0;
        if (byContent[0] == '+')
        {
            sign = 1;
            startIndex++;
        }
        else if (byContent[0] == '-')
        {
            sign = -1;
            startIndex++;
        }
        if (startIndex >= byContent.Length)
        {
            return 0;
        }
        if (byContent[startIndex] > '9' || byContent[startIndex] <'0')
        {
            return 0;
        }
        else
        {
            for (int i = startIndex; i < byContent.Length; ++i)
            {
                if (byContent[i] > '9' || byContent[i] < '0')
                {
                    return integer;
                }
                else
                {
                    last = integer;
                    integer = sign * (byContent[i] - '0') + integer*10;

                    if (integer / 10 != last)
                    {
                        if (sign == 1)
                        {
                            return int.MaxValue;
                        }
                        else
                        {
                            return int.MinValue;
                        }
                    }
                }
            }
        }
        return  integer;
    }
}