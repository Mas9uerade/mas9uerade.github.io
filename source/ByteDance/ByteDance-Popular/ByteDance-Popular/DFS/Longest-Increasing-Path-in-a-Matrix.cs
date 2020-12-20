//给定一个整数矩阵，找出最长递增路径的长度。

//对于每个单元格，你可以往上，下，左，右四个方向移动。 你不能在对角线方向上移动或移动到边界外（即不允许环绕）。

//示例 1:

//输入: nums =
//[
//  [9, 9, 4],
//  [6,6,8],
//  [2,1,1]
//] 
//输出: 4
//解释: 最长递增路径为[1, 2, 6, 9]。
//示例 2:

//输入: nums =
//[
//  [3, 4, 5],
//  [3,2,6],
//  [2,2,1]
//] 
//输出: 4
//解释: 最长递增路径是[3, 4, 5, 6]。注意不允许在对角线方向上移动。

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/longest-increasing-path-in-a-matrix
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。


using System;
using System.Collections.Generic;
/// <summary>
/// 思路 dfs +  记忆化
/// 因为是递增的路线，所以如果可以连接上已经走过的点，则可以直接使用该值
/// </summary>
namespace ByteDancePopular
{
    public partial class Solution
    {

        int[][] dir = new int[4][] { new int[2] { -1, 0 }, new int[2] { 1, 0 }, new int[2] { 0, -1 }, new int[2] { 0, 1 } };
        int rows, cols;
        public int LongestIncreasingPath(int[][] matrix)
        {
            rows = matrix.Length;
            cols = matrix[0].Length;
            int[][] memory = new int[rows][];
            
            for (int i = 0; i < rows; ++i)
            {
                memory[i] = new int[cols];
            }

            int ans = 0;
            for (int i = 0; i <rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    ans = Math.Max(ans, DfsLongestIncreasePath(memory, matrix, i, j));
                }
            }
            return ans;

        }

        private int DfsLongestIncreasePath(int[][] memory, int[][] matrix, int i, int j)
        {
            if (memory[i][j] != 0)
            {
                return memory[i][j];
            }
            memory[i][j]++;
            for (int k = 0; k < dir.Length; ++k)
            {
                int newRow = i + dir[k][0], newColumn = j + dir[k][1];
                if (newRow >= 0 && newRow < matrix.Length && newColumn >= 0 && newColumn < cols && matrix[newRow][newColumn] > matrix[i][j])
                {
                    memory[i][j] = Math.Max(memory[i][j], DfsLongestIncreasePath(memory, matrix, newRow, newColumn) + 1);
                }
            }
            return memory[i][j];

        }
    }
}