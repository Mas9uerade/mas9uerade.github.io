//5.最长回文子串
//给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为 1000。

//示例 1：
//输入: "babad"
//输出: "bab"
//注意: "aba" 也是一个有效答案。

//示例 2：
//输入: "cbbd"
//输出: "bb"

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/longest-palindromic-substring
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

// 思路
// 选定 回文中心， 向两边拓展， 拓展基于dp
// 回文中心 有两种 1个的 和两个的

using System;
namespace ByteDancePopular
{
    public partial class Solution
    {
        public string LongestPalindrome(string s)
        {
            if (s.Length == 0 || s.Length == 1)
            {
                return s;
            }
            int maxlen = 1;
            string ret = s[0].ToString();
            int markerleft = 0, markerright = 0;
            for (int i = 1; i < s.Length; ++i)
            {
                int left = i - 1;
                int right = i + 1;
                int len = 1;

                len = GetPalindromicSubstringLen(s, left, right, len);
                if (maxlen < len)
                {
                    markerleft = i - len / 2;
                    maxlen = len;
                }

                //偶数回文中心的情况
                left = i - 1;
                right = i;
                len = 0;

                len = GetPalindromicSubstringLen(s, left, right,  len);
                if (maxlen <  len)
                {
                    markerleft = i - len / 2;
                    maxlen = len;
                }
            }

            return s.Substring(markerleft, maxlen);
        }

        private int GetPalindromicSubstringLen (string s, int left, int right, int initlen)
        {
            bool dp = true;
            int len = initlen;

            while (left >= 0 && right < s.Length && dp)
            {
                if (s[left] == s[right])
                {
                    dp = true;
                    len = right - left + 1;
                    left--;
                    right++;
                }
                else
                {
                    dp = false;
                }
            }

            return len;
        }
    }
}
