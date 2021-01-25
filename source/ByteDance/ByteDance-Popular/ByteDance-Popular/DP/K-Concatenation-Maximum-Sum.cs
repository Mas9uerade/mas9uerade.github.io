//1191. K 次串联后最大子数组之和
//给你一个整数数组 arr 和一个整数 k。
//首先，我们要对该数组进行修改，即把原数组 arr 重复 k 次。
//举个例子，如果 arr = [1, 2] 且 k = 3，那么修改后的数组就是 [1, 2, 1, 2, 1, 2]。
//然后，请你返回修改后的数组中的最大的子数组之和。
//注意，子数组长度可以是 0，在这种情况下它的总和也是 0。
//由于 结果可能会很大，所以需要 模（mod） 10^9 + 7 后再返回。 

//示例 1：
//输入：arr = [1,2], k = 3
//输出：9

//示例 2：
//输入：arr = [1, -2, 1], k = 5
//输出：2

//示例 3：
//输入：arr = [-1, -2], k = 7
//输出：0

//提示：
//1 <= arr.length <= 10 ^ 5
//1 <= k <= 10 ^ 5
//- 10 ^ 4 <= arr[i] <= 10 ^ 4

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/k-concatenation-maximum-sum
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//思路
// 1. 求出单个数组的最大子数组的值
// 2. 求出数组值的和
// 3. DP求最大值

using System;
using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        private static int InArray = 1, OutArray = 2;
        
        public int KConcatenationMaxSum(int[] arr, int k)
        {
            int mod = (int) Math.Pow(10, 9) + 7;
            int sum = 0;
            for (int  i = 0; i< arr.Length; ++i)
            {
                sum = (sum + arr[i]) % mod;
            }
            sum = Math.Max(0, sum);

            List<int> list = new List<int>();
            list.AddRange(arr);
            list.AddRange(arr);
            int left = 0, right = 0;
            int[] dp = new int[list.Count];
            int[] cost = new int[list.Count];
            int[] status = new int[list.Count];
            dp[0] = list[0] > 0? list[0] :0;
            status[0] = list[0]> 0 ? InArray:OutArray;
            for (int i = 1; i < list.Count; ++i)
            {
                //正数/0，考虑接入最大子数组
                if (list[i] >= 0)
                {
                    if (status[i-1] == OutArray)
                    {
                        //考虑是否续接
                        if (cost[i-1] + dp[i-1] + list[i] >= dp[i-1] && cost[i - 1] + dp[i - 1] + list[i] >= list[i])
                        {
                            //续接
                            status[i] = InArray;
                            cost[i] = 0;
                            dp[i] = (cost[i - 1] + dp[i - 1] + list[i])% mod;
                        }
                        //不续接
                        else
                        {
                            // 1. 延续之前的DP值
                            if (dp[i-1] > cost[i-1] + dp[i-1] + list[i] && dp[i-1] > list[i])
                            {
                                status[i] = OutArray;
                                cost[i] = (list[i] + cost[i-1])% mod;
                                dp[i] = (dp[i - 1]) % mod;
                            }
                            // 2. 使用当前的数
                            else if (list[i] >= dp[i-1] && list[i] > dp[i-1] + cost[i-1] + list[i])
                            {
                                status[i] = InArray;
                                dp[i] = list[i] % mod;
                                cost[i] = 0;
                            }
                        }
                    }
                    else
                    {
                        //已经连接上了
                        status[i] = InArray;
                        cost[i] = 0;
                        dp[i] = (dp[i - 1] + list[i]) % mod; 
                    }
                }
                //负数的情况
                else
                {
                    if (status[i-1] == InArray)
                    {
                        //断链
                        status[i] = OutArray;
                        dp[i] = dp[i - 1];
                        cost[i] = list[i];
                    }
                    //已断链的情况下
                    else
                    {
                        status[i] = OutArray;
                        dp[i] = dp[i - 1];
                        cost[i] = (cost[i - 1] + list[i]) % mod;
                    }
                }
            }
            int max1 = dp[arr.Length - 1];
            int max2 = dp[dp.Length-1];
            int k2max = 0;
            if (k >= 2)
            {
                k2max = Math.Max(0, Math.Max(max2, ((k - 2) * sum + max2) % mod));
            }
            int k1max = 0;
            if (k >= 1)
            {
                k1max = Math.Max(0, Math.Max(((k - 1) * sum + max1) % mod, max1)); 
            }

            return Math.Max(Math.Max(k1max, k2max), k * sum % mod);
        }
    }
}