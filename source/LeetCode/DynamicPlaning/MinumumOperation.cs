public class Solution {
    public int MinimumOperations(string leaves)
    {
        int n = leaves.Length;
        int[,] dp = new int[3, n];
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                dp[i, j] = 100000000;
            }
        }
        dp[0, 0] = leaves[0] == 'r' ? 0 : 1;
        for (int i = 1; i < n; i++)
        {
            //第一步，计算把 0~ n全红的cost
            dp[0, i] = dp[0, i - 1] + (leaves[i] == 'y' ? 1 : 0);
            //第二步，计算 从0~n全红到 1~n 全黄的cost
            dp[1, i] = Math.Min(dp[0, i - 1], dp[1, i - 1]) + (leaves[i] == 'r' ? 1 : 0);
            //第三步， 计算从 1~n 全黄 到2~n全红的cost
            dp[2, i] = Math.Min(dp[1, i - 1], dp[2, i - 1]) + (leaves[i] == 'y' ? 1 : 0);
        }
        return dp[2, n - 1];
    }
}