//31. 下一个排列
//实现获取 下一个排列 的函数，算法需要将给定数字序列重新排列成字典序中下一个更大的排列。
//如果不存在下一个更大的排列，则将数字重新排列成最小的排列（即升序排列）。
//必须 原地 修改，只允许使用额外常数空间。

//示例 1：
//输入：nums = [1,2,3]
//输出：[1,3,2]

//示例 2：
//输入：nums = [3, 2, 1]
//输出：[1,2,3]

//示例 3：
//输入：nums = [1, 1, 5]
//输出：[1,5,1]

//示例 4：
//输入：nums = [1]
//输出：[1]

//提示：

//1 <= nums.length <= 100
//0 <= nums[i] <= 100

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/next-permutation
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

using System;
using System.Collections.Generic;

//思路
//前序遍历找到递增的位置，倒序遍历找到比这个值大一点的数
namespace ByteDancePopular
{
    public partial class Solution
    {
        public void NextPermutation(int[] nums)
        {
            //需要替换的位置
            int incrementIndex = -1;
            int val = -1;
            for (int i = nums.Length -1; i >= 1; --i)
            {
                if (nums[i] > nums[i-1])
                {
                    incrementIndex = i - 1;
                    val = nums[incrementIndex];
                    break;
                }
            }
            //则为全递减排列，直接反转
            if (incrementIndex == -1)
            {
                ReversePermutation(ref nums, 0, nums.Length -1);
                return;
            }

            int nextIndex = incrementIndex + 1;
            int nextVal = nums[nextIndex];
            for (int i = nums.Length - 1; i > incrementIndex; --i)
            {
                if (nums[i] > val)
                {
                    if (nextVal >= nums[i])
                    {
                        nextVal = nums[i];
                        nextIndex = Math.Max(i, nextIndex);
                    }
                }
            }
            //交换位置
            nums[incrementIndex] = nextVal;
            nums[nextIndex] = val;
            ReversePermutation(ref nums, incrementIndex + 1, nums.Length - 1);
        }


        private void ReversePermutation(ref int[] nums, int left, int right)
        {
            int tmp = -1;
            while (left < right)
            {
                tmp = nums[left];
                nums[left] = nums[right];
                nums[right] = tmp;
                left++;
                right--;
            }
        }
    }
}
