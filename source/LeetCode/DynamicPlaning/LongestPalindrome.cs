public static string LongestPalindrome(string s)
{
    if (s.Length == 1)
    {
        return s;
    }
    char[] charArr = s.ToCharArray();
    bool[,] dpCache = new bool[charArr.Length, charArr.Length];
    for (int i = 0; i < charArr.Length; ++i)
    {
        dpCache[i, i] = true;
    }
    int leftIndex = 0;
    int maxLen = 1;
    for (int i = 1; i < charArr.Length; ++i)
    {
        for (int j = 0; j <= i; j++)
        {
            if (charArr[i] != charArr[j])
            {
                dpCache[j, i] = false;
            }
            else
            {
                if (i - j < 3)
                {
                    dpCache[j, i] = true;
                }
                else
                {
                    dpCache[j, i] = dpCache[j + 1, i - 1];
                }
            }
            if (dpCache[j, i] == true && i - j + 1 > maxLen)
            {
                maxLen = i - j + 1;
                leftIndex = j;
            }

        }
    }
    return s.Substring(leftIndex, maxLen);
}