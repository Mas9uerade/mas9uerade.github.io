//93. 复原IP地址

//给定一个只包含数字的字符串，复原它并返回所有可能的 IP 地址格式。
//有效的 IP 地址 正好由四个整数（每个整数位于 0 到 255 之间组成，且不能含有前导 0），整数之间用 '.' 分隔。
//例如："0.1.2.201" 和 "192.168.1.1" 是 有效的 IP 地址，但是 "0.011.255.245"、"192.168.1.312" 和 "192.168@1.1" 是 无效的 IP 地址。

//示例 1：
//输入：s = "25525511135"
//输出：["255.255.11.135","255.255.111.35"]

//示例 2：
//输入：s = "0000"
//输出：["0.0.0.0"]

//示例 3：
//输入：s = "1111"
//输出：["1.1.1.1"]

//示例 4：
//输入：s = "010010"
//输出：["0.10.0.10","0.100.1.0"]

//示例 5：
//输入：s = "101023"
//输出：["1.0.10.23","1.0.102.3","10.1.0.23","10.10.2.3","101.0.2.3"]

//提示：
//0 <= s.length <= 3000
//s 仅由数字组成

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/restore-ip-addresses
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

using System.Collections.Generic;
using System.Text;
//思路 -> 深搜回溯
//1. 当不满足条件时回溯 -> 1.字段数值 不属于 0~0xff 2.遍历完，不满足字符串和ip字段的长度限制  
namespace ByteDancePopular
{
    public partial class Solution
    {
        static int SEG_COUNT = 4;
        List<string> ans = new List<string>();
        int[] segments = new int[SEG_COUNT];


        public IList<string> RestoreIpAddresses(string s)
        {
            ans = new List<string>();
            BackTraceIPAddress(s, 0, 0);
            return ans;
        }

        private void BackTraceIPAddress(string s, int left,int segid)
        {
            if (segid == SEG_COUNT)
            {
                if (left == s.Length)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < segments.Length; ++i)
                    {
                        sb.Append(segments[i]);
                        if (i != SEG_COUNT -1)
                        {
                            sb.Append(".");
                        }
                    }
                    ans.Add(sb.ToString());
                }
                return;
            }

            //ip字段超出4个直接回溯
            if (segid >= SEG_COUNT)
            {
                return;
            }

            //当回溯完最后一个字符，还不满足4个ip字段的话，则回溯
            if (left == s.Length)
            {
                return;
            }

            //由于不能有前导0，如果有0，则该字段必然为0，直接下一个字段
            if (s[left] == '0')
            {
                segments[segid] = 0;
                BackTraceIPAddress(s, left + 1, segid+1);
            }

            int addr = 0;
            for (int right = left; right< s.Length; ++right)
            {
                addr = addr * 10 + (s[right] - '0');
                if (addr > 0 && addr <= 0xFF)
                {
                    segments[segid] = addr;
                    BackTraceIPAddress(s, right + 1, segid + 1);
                }
                else
                {
                    break;
                }
            }
        }
    }
}