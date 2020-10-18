        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            if (s.Length == 1)
            {
                return 1;
            }
            List<char> charCollection = new List<char>();
            int max = 0;
            //滑动窗口实现
            for (int i = 0; i < s.Length; ++i)
            {
                if (charCollection.Contains(s[i]))
                {
                    int index =  charCollection.IndexOf(s[i]);
                    charCollection.RemoveRange(0, index+1);
                }
                charCollection.Add(s[i]);
                max = Math.Max(max, charCollection.Count);
            }
            return max;
        }