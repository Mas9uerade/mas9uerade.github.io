//HashMap

using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public bool CanConvert(string str1, string str2)
        {
            Dictionary<char, char> map = new Dictionary<char, char>(26);
            HashSet<char> usedInStr2 = new HashSet<char>();
            
            if(string.Equals(str1, str2))
            {
                return true;
            }

            for (int i = 0; i < str2.Length; ++i)
            {
                char c2 = str2[i];
                char c1 = str1[i];

                if (!usedInStr2.Contains(c2))
                {
                    usedInStr2.Add(c2);
                }
                if (map.ContainsKey(c1))
                {
                    if (map[c1] != c2)
                    {
                        return false;
                    }
                }
                else
                {
                    map[c1] = c2;
                }
            }

            if (usedInStr2.Count >= 26)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}