//#### [97. 交错字符串](https://leetcode-cn.com/problems/interleaving-string/)
//给定三个字符串 `s1`、`s2`、`s3`，请你帮忙验证 `s3` 是否是由 `s1` 和 `s2` **交错** 组成的。
//两个字符串 `s` 和 `t` **交错** 的定义与过程如下，其中每个字符串都会被分割成若干 **非空** 子字符串：

//- `s = s1 + s2 + ... + sn`
//- `t = t1 + t2 + ... + tm`
//- `|n - m| <= 1`
//- **交错** 是 `s1 + t1 + s2 + t2 + s3 + t3 + ...` 或者 `t1 + s1 + t2 + s2 + t3 + s3 + ...`

//**提示：**`a + b` 意味着字符串 `a` 和 `b` 连接。



//**示例 1：**

//![img](https://assets.leetcode.com/uploads/2020/09/02/interleave.jpg)

//```
//输入：s1 = "aabcc", s2 = "dbbca", s3 = "aadbbcbcac"
//输出：true
//```

//**示例 2：**

//```
//输入：s1 = "aabcc", s2 = "dbbca", s3 = "aadbbbaccc"
//输出：false
//```

//**示例 3：**

//```
//输入：s1 = "", s2 = "", s3 = ""
//输出：true
//```



//**提示：**

//- `0 <= s1.length, s2.length <= 100`
//- `0 <= s3.length <= 200`
//- `s1`、`s2`、和 `s3` 都由小写英文字母组成



//### 题解

//输入：s1 = "aabcc", s2 = "dbbca", s3 = "aadbbcbcac"
//输出：true


//| S2\S1| 0    | a1   | a2      | b        | c            | c             |
//| ---- | ---- | ---- | ------- | -------- | ------------ | ------------- |
//| 0    | T    | T(a) | T(aa)   | F        |              |               |
//| d 1  | F    | F(ad)| T(aad)  | T(aadb)  | F            |               |
//| b 2  |      |F(adb)| T(aadb) | T(aadbb) | T(aadbbc)    | F             |
//| b    |      |      | F       | F        | T(aadbbcb)   | T(aadbbcbc)   |
//| c    |      |      |         |          | T(aadbbcbc)  | F             |
//| a    |      |      |         |          | T(aadbbcbca) | T(aadbbcbcac) |


// 即 DP[i,j] 是基于 dp[i-1,j] 或 dp[i, j-1]的基础上进行计算
// DP[n,m] = new bool[n,m]
// DP[0,0] = T
// DP[0,1] = s1[0] == s3[0]? T:F
// DP[1,0] = s2[0] == s3[0]? T:F
// DP[1,1] = (DP[0,1] && s3[2] == s2[1])|(DP[1,0] && s3[2] == s1[2])
// aad 
// DP[2,1] = (DP[1,1] && s3[2+1 -1] == s2[2]) || DP[2,0] && s3[2+1-1] == S1[1]
// DP[i,j] = DP[i-1,j]
namespace ByteDancePopular
{
    public partial class Solution
    {


        public void IsInterleaveUnitTest()
        {
            string s1 = "aabcc";
            string s2 = "dbbca";
            string s3 = "aadbbcbcac";

            IsInterleave(s1, s2, s3);
    }

        public bool IsInterleave(string s1, string s2, string s3)
        {
            int m = s1.Length;
            int n = s2.Length;
            if (s3.Length != n+m)
            {
                return false;
            }
            bool[,] dp = new bool[n+1, m+1];
            dp[0, 0] = true;
            dp[0, 1] = s3[0] == s1[0];
            dp[1, 0] = s3[0] == s2[0];

            for (int i = 1; i <=m; ++i)
            {
                dp[0, i] = dp[0, i - 1] && s3[i - 1] == s1[i-1];
            }

            for (int i = 1; i <= n; ++i)
            {
                dp[i, 0] = dp[i-1, 0] && s3[i - 1] == s2[i - 1];
            }


            for (int  i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= m; ++j)
                {
                    dp[i, j] = (dp[i - 1, j] && s3[i + j - 1] == s2[i - 1]) || (dp[i, j - 1] && s3[i + j - 1]==s1[j-1]);
                }
            }
            return dp[n, m];
        }
    } 
}