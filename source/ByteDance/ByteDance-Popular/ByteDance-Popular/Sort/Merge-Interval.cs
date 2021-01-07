//56. 合并区间

//给出一个区间的集合，请合并所有重叠的区间。

//示例 1:
//输入: intervals = [[1, 3],[2,6],[8,10],[15,18]]
//输出:[[1,6],[8,10],[15,18]]
//解释: 区间[1, 3] 和[2, 6] 重叠, 将它们合并为[1, 6].

//示例 2:
//输入: intervals = [[1, 4],[4,5]]
//输出:[[1,5]]
//解释: 区间[1, 4] 和[4, 5] 可被视为重叠区间。
//注意：输入类型已于2019年4月15日更改。 请重置默认代码定义以获取新方法签名。

//提示：

//intervals[i][0] <= intervals[i][1]

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/merge-intervals
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//思路
// 先排序起点，再合并
using System;
using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public int[][] Merge(int[][] intervals)
        {
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < intervals.Length; ++i)
            {
                list.Add(intervals[i]);
            }

            list.Sort((a, b) =>
            {
                return a[0] - b[0];
            });

            List<int[]> ret = new List<int[]>();
            ret.Add(list[0]);

            for (int i = 1; i < list.Count; ++i)
            {
                int retIndex = ret.Count - 1;

                //可合并的区间
                if (list[i][0] <=  ret[retIndex][1])
                {
                    ret[retIndex][1] = Math.Max(ret[retIndex][1], list[i][1]);
                }
                else
                {
                    ret.Add(list[i]);
                }
            }
            return ret.ToArray();

        }
    }
}
