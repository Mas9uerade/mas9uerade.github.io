//1259.不相交的握手
//偶数 个人站成一个圆，总人数为 num_people 。每个人与除自己外的一个人握手，所以总共会有 num_people / 2 次握手。
//将握手的人之间连线，请你返回连线不会相交的握手方案数。
//由于结果可能会很大，请你返回答案 模 10^9+7 后的结果。
//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/handshakes-that-dont-cross
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

using System;
using System.Collections.Generic;

//解题思路
//以1和其他点相连作为分割点
//分割要求必须剩下的两堆点都是偶数个
namespace ByteDancePopular
{
    public partial class Solution
    {
        public int NumberOfWays(int num_people)
        {
            long[] dp = new long[num_people / 2 + 1];
            long modulus = (long)Math.Pow(10, 9) + 7;
            dp[0] = 1;
            for (int  i =1; i < num_people/2+1; ++i)
            {
                for (int j =1; j <= i; ++j)
                {
                    dp[i] += dp[j - 1] * dp[i - j];
                    dp[i] %= modulus;
                }
            }
            return (int)dp[num_people / 2];
        }

    }
}