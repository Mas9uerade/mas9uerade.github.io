//二分查找确定最长子字串的长度，再确定具体的子字串

using System;
using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public string LongestDupSubstring(string s)
        {
            int n = s.Length;

            if (n == 0 || n == 1)
            {
                return "";
            }
            //把字符串转换成数字
            int[] nums = new int[s.Length];
            for (int i = 0; i < s.Length; ++i)
            {
                nums[i] = (int)s[i] - (int)'a';
            }

            long modulus = (long)Math.Pow(2,32);
            int a = 26;
            int left = 1;
            int right = s.Length;
            int L;
            //
            while (left != right)
            {

                L = left + (right - left) / 2;
                if (SubStringBinarySerach(L, a, modulus, n, nums, s) != -1)
                {
                    left = L + 1;
                }
                else
                {
                    right = L;
                }
            }
            int start = SubStringBinarySerach(left - 1, a, modulus, n, nums,s);
            return start != -1 ? s.Substring(start, left - 1) : "";
        }

        int SubStringBinarySerach(int L, int a, long modulus, int n, int[] nums, string s)
        {
            long h = 0;
            for (int i = 0; i < L; ++i) h = (h * a + nums[i]) % modulus;

            // already seen hashes of strings of length L
            HashSet<long> seen = new HashSet<long>();
            Dictionary<long, int> dictStart = new Dictionary<long, int>();
            seen.Add(h);
            dictStart[h] = 0;
            //tmpStr.Add(s.Substring(0, L));
            // const value to be used often : a**L % modulus
            long aL = 1;
            for (int i = 1; i <= L; ++i) aL = (aL * a) % modulus;

            for (int start = 1; start < n - L + 1; ++start)
            {
                // compute rolling hash in O(1) time
                h = (h * a - nums[start - 1] * aL % modulus + modulus) % modulus;
                h = (h + nums[start + L - 1]) % modulus;
                if (seen.Contains(h))
                {
                    if (CompareTwoArray(nums, dictStart[h], start, L))
                    {
                        return start;
                    }
                }
                seen.Add(h);
                dictStart[h] = start;
            }
            return -1;

        }

        bool CompareTwoArray(int[] nums, int index1, int index2, int len)
        {
            for (int i = 0; i < len; ++i)
            {
                if (nums[index1+i] != nums[i+index2])
                {
                    return false;
                }
            }
            return true;
        }
    }
}