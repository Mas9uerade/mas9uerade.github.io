//424.替换后的最长重复字符
//给你一个仅由大写英文字母组成的字符串，你可以将任意位置上的字符替换成另外的字符，总共可最多替换 k 次。在执行上述操作后，找到包含重复字母的最长子串的长度。
//注意:
//字符串长度 和 k 不会超过 10^4。
//示例 1:
//输入:
//s = "ABAB", k = 2
//输出:
//4
//解释:
//用两个'A'替换为两个 'B',反之亦然。

//示例 2:
//输入:
//s = "AABABBA", k = 1
//输出:
//4
//解释:
//将中间的一个'A'替换为 'B',字符串变为 "AABBBBA"。
//子串 "BBBB" 有最长重复字母, 答案为 4。

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/longest-repeating-character-replacement
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//思路
// 1. 当k >= s长度时， 替换即可满足 s.Length
// 2. 当k < s.Length时， 只需要维护一个长度动态变更的滑动窗口
// 3. 当窗口扩张时，必定是因为 k有余裕 或者 扩张进入的是一个目前最大出现次数的值
// 4. 当窗口收缩时，必定是因为 扩张的值并非最大出现次数的值，且k不余裕，所以收缩会伴随着滑动
using System;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public int CharacterReplacement(string s, int k)
        {
            if (s.Length == 0)
            {
                return 0 ;
            }
            if (k >= s.Length)
            {
                return s.Length;
            }
            int[] count = new int[26];
            int left = 0, right = 0, max = 0, ret = 0;
            while(right < s.Length)
            {
                int idx = s[right] - 'A';
                count[idx]++;
                max = Math.Max(max, count[idx]);
                if (right - left +1 -max >k)
                {
                    count[s[left] - 'A']--;
                    left++;
                }
                ret = Math.Max(ret, right - left + 1);
                right++;
            }
            return ret;
        }
    }
}