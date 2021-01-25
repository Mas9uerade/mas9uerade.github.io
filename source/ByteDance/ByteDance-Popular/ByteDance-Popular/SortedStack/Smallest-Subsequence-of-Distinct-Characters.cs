//316.去除重复字母

//单调栈

//给你一个字符串 s ，请你去除字符串中重复的字母，使得每个字母只出现一次。需保证 返回结果的字典序最小（要求不能打乱其他字符的相对位置）。

//注意：该题与 1081 https://leetcode-cn.com/problems/smallest-subsequence-of-distinct-characters 相同

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/remove-duplicate-letters
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
using System;
using System.Text;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public string RemoveDuplicateLetters(string s)
        {
            bool[] visited = new bool[26];
            int[] nums = new int[26];
            for (int i = 0; i < s.Length; ++i)
            {
                nums[s[i] - 'a']++;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; ++i)
            {
                //当遍历到字符串上的某个字符时，判断是否比当前末尾的字符小，如果比当前末尾的字符小，且末尾字符在后续还会出现，则移除末尾字符，替换为当前字符
                char ch = s[i];
                int index = ch - 'a';
                if (!visited[index])
                {
                    if (sb.Length>0)
                    {
                        char lastCh = sb[sb.Length - 1];
                        while (sb.Length > 0 && ch < lastCh && nums[lastCh - 'a'] > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            visited[lastCh - 'a'] = false;
                            if (sb.Length > 0)
                            {
                                lastCh = sb[sb.Length - 1];
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    sb.Append(ch);
                    visited[index] = true;
                }
                nums[index]--;
            }
            return sb.ToString();
        }
    }
}