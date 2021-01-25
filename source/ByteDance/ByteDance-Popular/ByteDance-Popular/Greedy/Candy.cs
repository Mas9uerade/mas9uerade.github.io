//135.分发糖果
//老师想给孩子们分发糖果，有 N 个孩子站成了一条直线，老师会根据每个孩子的表现，预先给他们评分。

//你需要按照以下要求，帮助老师给这些孩子分发糖果：

//每个孩子至少分配到 1 个糖果。
//相邻的孩子中，评分高的孩子必须获得更多的糖果。
//那么这样下来，老师至少需要准备多少颗糖果呢？

//示例 1:

//输入:[1,0,2]
//输出: 5
//解释: 你可以分别给这三个孩子分发 2、1、2 颗糖果。
//示例 2:

//输入:[1,2,2]
//输出: 4
//解释: 你可以分别给这三个孩子分发 1、2、1 颗糖果。
//     第三个孩子只得到 1 颗糖果，这已满足上述两个条件。

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/candy
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//思路
// 1. 从左到右遍历，只要右边比左边大，则右边为左边加一
// 2. 从右到左遍历，只要左边比右边大，则左边比右边加一
using System;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public int Candy(int[] ratings)
        {
            if (ratings == null || ratings.Length == 0)
            {
                return 0;
            }
            if (ratings.Length == 1)
            {
                return 1;
            }
            int[] candice = new int[ratings.Length];
            //全部只给1颗
            for(int i = 0; i < ratings.Length; ++i)
            {
                candice[i] = 1;
            }

            for (int i = 1; i < ratings.Length; ++i)
            {
                if (ratings[i] > ratings[i-1])
                {
                    candice[i] = Math.Max(candice[i - 1] + 1, candice[i]);
                }
            }

            for (int i = ratings.Length -2; i >= 0; --i)
            {
                if (ratings[i] > ratings[i+1])
                {
                    candice[i] = Math.Max(candice[i + 1] + 1, candice[i]);
                }
            }
            int ret = 0;
            for (int i = 0; i < candice.Length; ++i)
            {
                ret += candice[i];
            }
            return ret;
        }
    }
}