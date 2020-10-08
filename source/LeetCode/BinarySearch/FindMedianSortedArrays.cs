public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int m = nums1.Length;
        int n = nums2.Length;
        //处理任何一个nums为空数组的情况
        if (m == 0)
        {
            if (n % 2 != 0)
            {
                return nums2[n / 2];
            }
            else
            {
                return (nums2[n / 2] + nums2[n / 2 - 1]) / 2.0;
            }

        }
        if (n == 0)
        {
            if (m % 2 != 0)
            {
                return 1.0 * nums1[m / 2];
            }
            else
            {
                return (nums1[m / 2] + nums1[m / 2 - 1]) / 2.0;
            }
        }
        int total = m + n;
        //总数为奇数，找第 total / 2 + 1 个数
        if ((total & 1) == 1)
        {
            return find_kth(nums1, 0, nums2, 0, total / 2 + 1);
        }
        //总数为偶数，找第 total / 2 个数和第total / 2 + 1个数的平均值
        return (find_kth(nums1, 0, nums2, 0, total / 2) + find_kth(nums1, 0, nums2, 0, total / 2 + 1)) / 2.0;

    }

    //寻找a 和 b 数组中，第k个数字
    double find_kth(int[] a, int a_begin, int[] b, int b_begin, int k)
    {
        //当a 或 b 超过数组长度，则第k个数为另外一个数组第k个数
        if (a_begin >= a.Length)
        {
            return b[b_begin + k - 1];
        }

        if (b_begin >= b.Length)
        {
            return a[a_begin + k - 1];
        }
        //k为1时，两数组最小的那个为第一个数
        if (k == 1)
        {
            return Math.Min(a[a_begin], b[b_begin]);
        }
        int mid_a = int.MaxValue;
        int mid_b = int.MaxValue;
        //mid_a / mid_b 分别表示 a数组、b数组中第 k / 2 个数
        if (a_begin + k / 2 - 1 < a.Length)
        {
            mid_a = a[a_begin + k / 2 - 1];
        }
        if (b_begin + k / 2 - 1 < b.Length)
        {
            mid_b = b[b_begin + k / 2 - 1];
        }
        //如果a数组的第 k / 2 个数小于b数组的第 k / 2 个数，表示总的第 k 个数位于 a的第k / 2个数的后半段，或者是b的第 k / 2个数的前半段
        //由于范围缩小了 k / 2 个数，此时总的第 k 个数实际上等于新的范围内的第 k - k / 2个数，依次递归
        if (mid_a < mid_b)
        {
            return find_kth(a, a_begin + k / 2, b, b_begin, k - k / 2);
        }
        //否则相反
        else
        {
            return find_kth(a, a_begin, b, b_begin + k / 2, k - k / 2);
        }
    }    