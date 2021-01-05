#### [97. 交错字符串](https://leetcode-cn.com/problems/interleaving-string/)

难度困难384

给定三个字符串 `s1`、`s2`、`s3`，请你帮忙验证 `s3` 是否是由 `s1` 和 `s2` **交错** 组成的。

两个字符串 `s` 和 `t` **交错** 的定义与过程如下，其中每个字符串都会被分割成若干 **非空** 子字符串：

- `s = s1 + s2 + ... + sn`
- `t = t1 + t2 + ... + tm`
- `|n - m| <= 1`
- **交错** 是 `s1 + t1 + s2 + t2 + s3 + t3 + ...` 或者 `t1 + s1 + t2 + s2 + t3 + s3 + ...`

**提示：**`a + b` 意味着字符串 `a` 和 `b` 连接。

 

**示例 1：**

![img](https://assets.leetcode.com/uploads/2020/09/02/interleave.jpg)

```
输入：s1 = "aabcc", s2 = "dbbca", s3 = "aadbbcbcac"
输出：true
```

**示例 2：**

```
输入：s1 = "aabcc", s2 = "dbbca", s3 = "aadbbbaccc"
输出：false
```

**示例 3：**

```
输入：s1 = "", s2 = "", s3 = ""
输出：true
```

 

**提示：**

- `0 <= s1.length, s2.length <= 100`
- `0 <= s3.length <= 200`
- `s1`、`s2`、和 `s3` 都由小写英文字母组成



### 题解

输入：s1 = "aabcc", s2 = "dbbca", s3 = "aadbbcbcac"
输出：true



|      | 0    | a/1  | a/2     | b/3               | c                 | c             |
| ---- | ---- | ---- | ------- | ----------------- | ----------------- | ------------- |
| 0    | T    | T(a) | T(aa)   | F(aad ! = aab)    | F(aadb !=  aabc ) |               |
| d/1  | F    | F    | T(aad)  | T(aadb)           | F                 |               |
| b/2  |      |      | T(aadb) | T(aadbb) 3+2-1 == | T(aadbbc)         | F             |
| b    |      |      | F       | F                 | T(aadbbcb)        | T(aadbbcbc)   |
| c    |      |      |         |                   | T(aadbbcbc)       | F             |
| a    |      |      |         |                   | T(aadbbcbca)      | T(aadbbcbcac) |

