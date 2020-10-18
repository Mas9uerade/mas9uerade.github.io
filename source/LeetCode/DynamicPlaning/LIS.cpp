class Solution {
public:
    /**
     * retrun the longest increasing subsequence
     * @param arr int整型vector the array
     * @return int整型vector
     */
    vector<int> LIS(vector<int>& arr) {
        // write code here
        // 如果有多个答案，输出其中字典序最小的
        // 二分，动态规划DP
        // 本题的本质是求最长上升子序列，采用动态规划
        // dp存储每个元素往前的最长子数列大小
        // dp[i] = max(dp[i], dp[j]+1), j为0到i-1中比arr[i]小的子数列数据
        // 为了避免重复比较，用end数组村粗最长上升子序列
        // 当arr[i]>end[len]时，arr[i]添加到end后面
        // 否则 从end 0->len范围内查找到第一个比arr[i]大的元素进行替换，此处采用二分法查找
         
        int n = arr.size();
        vector<int> end(n+1); // 下标从1开始
        // 存储每个元素的最大子序列个数
        vector<int> dp(n);
        int len = 1;
        // 子序列的第一个元素默认为数组的第一个元素
        end[1] = arr[0];
        // 第一个元素的最大子序列个数肯定是1
        dp[0] = 1;
        for(int i=0; i < n; i++) {
            if(end[len] < arr[i]){
                // 当arr[i] > end[len]时，arr[i]添加到end后面
                end[++len] = arr[i];
                dp[i] = len;
            }
            else {
                // 当前元素小于end中的最后一个元素，利用二分法寻找第一个大于arr[i]的元素
                // end[i] 替换为当前元素dp[]
                int l = 0;
                int r = len;
                while(l <= r) {
                    int mid = (l+r)>>1;  // 位运算，相当于除以2
                    if(end[mid] >= arr[i]) {
                        r = mid-1;
                    }
                    else l = mid+1;
                     
                }
                end[l] = arr[i];
                dp[i] = l;
            }
        }
         
        vector<int> res(len);
        for(int i = n-1; i >= 0; i--) {
            if(dp[i] == len) {
                res[--len] = arr[i];
            }
        }
        return res;
    }
};