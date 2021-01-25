//4. 寻找两个正序数组的中位数

//给定两个大小为 m 和 n 的正序（从小到大）数组 nums1 和 nums2。请你找出并返回这两个正序数组的中位数。
//进阶：你能设计一个时间复杂度为 O(log (m+n)) 的算法解决此问题吗？

//示例 1：
//输入：nums1 = [1, 3], nums2 = [2]
//输出：2.00000
//解释：合并数组 = [1, 2, 3] ，中位数 2

//示例 2：
//输入：nums1 = [1, 2], nums2 = [3, 4]
//输出：2.50000
//解释：合并数组 = [1, 2, 3, 4] ，中位数(2 + 3) / 2 = 2.5

//示例 3：
//输入：nums1 = [0, 0], nums2 = [0, 0]
//输出：0.00000

//示例 4：
//输入：nums1 = [], nums2 = [1]
//输出：1.00000

//示例 5：
//输入：nums1 = [2], nums2 = []
//输出：2.00000

//提示：
//nums1.length == m
//nums2.length == n
//0 <= m <= 1000
//0 <= n <= 1000
//1 <= m + n <= 2000
//- 10^6 <= nums1[i], nums2[i] <= 10^6

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/median-of-two-sorted-arrays
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。


//思路：
// 1. 二分查找 先剔除K/2的元素，再剔除K/4元素， 直到K个元素在最前 
using System;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;
      
            //奇数为中位数
            double ret = 0;
            //偶数要取中间两个值
            if ((m + n)%2 == 0)
            {
                int median1 = GetKthElement(nums1, nums2, (m + n) / 2 + 1);
                int median2 = GetKthElement(nums1, nums2, (m + n) / 2);

                ret = (double)(median1 + median2) * 0.5f;

            }
            else
            {
                ret = GetKthElement(nums1, nums2, (m + n) / 2 + 1);
            }
            
            return ret;
        }

        /// <summary>
        /// 返回第N个数
        /// </summary>
        /// <returns></returns>
        private int GetKthElement(int[] nums1, int[] nums2, int k)
        {
            int m = nums1.Length;
            int n = nums2.Length;

            int index1 = 0, index2 = 0;
            while (true)
            {
                if (index1 == m)
                {
                    return nums2[index2 + k - 1];
                }
                if (index2 == n)
                {
                    return nums1[(index1 + k - 1)];
                }
                if (k == 1)
                {
                    return Math.Min(nums1[index1], nums2[index2]);
                }


                int half = k / 2;

                int newIndex1 = Math.Min(half + index1, m) - 1;
                int newIndex2 = Math.Min(half + index2, n) - 1;

                int a = nums1[newIndex1];
                int b = nums2[newIndex2];

                if (a < b)
                {
                    k -= (newIndex1 - index1 + 1);
                    index1 = newIndex1 + 1;
                }
                else
                {
                    k -= (newIndex2 - index2 + 1);
                    index2 = newIndex2 + 1;
                }
            }
        }
    }
}