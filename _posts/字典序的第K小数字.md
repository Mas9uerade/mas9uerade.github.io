#### [440. 字典序的第K小数字](https://leetcode-cn.com/problems/k-th-smallest-in-lexicographical-order/)

难度-困难

给定整数 `n` 和 `k`，找到 `1` 到 `n` 中字典序第 `k` 小的数字。

注意：1 ≤ k ≤ n ≤ 10^9。

**示例 :**

```
输入:
n: 13   k: 2

输出:
10

解释:
字典序的排列是 [1, 10, 11, 12, 13, 2, 3, 4, 5, 6, 7, 8, 9]，所以第二小的数字是 10。
```



题解思路：

1. 字典序 -> 十叉树遍历（需要去除头节点的0，从1～9作为根节点分别遍历）
2. 确定某个节点下的子节点个数 

```c#
        /* 计算[n,n+1]之间存在多少个数字 */
        /* 计算每扩大10倍有多少数字，相加即可 */
        long GetNodeNums(int n, int max)
        {
            long ans = 1;
            long left = (long)n * 10 + 0; // 扩大十倍的左边界 
            long right = (long)n * 10 + 9; // 扩大十倍的右边界
            while (max >= left)
            {
                if (max <= right)
                {
                    ans += (max - left + 1);   // max 在 [l,r]之间
                }
                else
                {
                    ans += (right - left + 1); // max 在 下一层
                }
                left = left * 10 + 0; // 下一层
                right = right * 10 + 9;
            }
            return ans;
        }


```

二、

初始化 l = 1, r = 9, k = k（还差数字个数）
令f(x)表示[x,x+1]之间数字个数
步骤1:遍历 i -> [l,r]
步骤2:
如果 k > i， k-=f(i).即第k个数不在[i,i+1]中，还需要找k-=f(i)个数。
如果 k <= i, k--,l=i10 ,r=i10+9,返回步骤1. 即第k个数在[i,i+1]中，还需找k-1个数，接下来查找区间为[i10,i10+9]。

```c#
 int findKthNumber(int n, int k) 
 {
        int l = 1;
        int r = 9;
        while(k) 
        {
            for(int i = l; i <= r; ++i) 
            {
                int f = GetNodeNums(i, n);
                if(k > f) 
                {
                    k -= f;
                }
              	else 
                {
                    k--;
                    if(k == 0) return i;
                    l = i * 10 + 0;
                    r = i * 10 + 9;
                    break;
                }
            }
        }
        return 0;
    }
```










