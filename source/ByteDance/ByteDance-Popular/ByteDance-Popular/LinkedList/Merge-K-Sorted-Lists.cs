//23.合并K个升序链表
//给你一个链表数组，每个链表都已经按升序排列。
//请你将所有链表合并到一个升序链表中，返回合并后的链表。

//示例 1：

//输入：lists = [[1, 4, 5],[1,3,4],[2,6]]
//输出：[1,1,2,3,4,4,5,6]
//解释：链表数组如下：
//[
//  1->4->5,
//  1->3->4,
//  2->6
//]
//将它们合并到一个有序链表中得到。
//1->1->2->3->4->4->5->6
//示例 2：

//输入：lists = []
//输出：[]
//示例 3：

//输入：lists = [[]]
//输出：[]


//提示：

//k == lists.length
//0 <= k <= 10 ^ 4
//0 <= lists[i].length <= 500
//- 10 ^ 4 <= lists[i][j] <= 10 ^ 4
//lists[i] 按 升序 排列
//lists[i].length 的总和不超过 10^4

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/merge-k-sorted-lists
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

// 思路
// 1. 堆排序/优先队列，逐个插入， 小根堆实现
// 2. 分治，两两合并

namespace ByteDancePopular
{
    public partial class Solution
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
            {
                return null;
            }
            return Merge(lists, 0, lists.Length - 1);
        }

        public ListNode Merge(ListNode[] lists, int l, int r)
        {
            if (l == r)
            {
                return lists[l];
            }
            if (l > r)
            {
                return null;
            }
            int mid = (l + r) / 2;
            return MergeTwoSortedLinkLists(Merge(lists, l, mid), Merge(lists, mid + 1, r));
        }

        public ListNode MergeTwoSortedLinkLists( ListNode l1, ListNode l2)
        {
            if (l1 == null) return l2;
            if (l2 == null) return l1;

            ListNode head = new ListNode();
            ListNode tail = head;

            ListNode tl1 = l1;
            ListNode tl2 = l2;

            while (tl1 != null && tl2!=null)
            {
                if (tl1.val < tl2.val)
                {
                    tail.next = tl1;
                    tl1 = tl1.next;
                }
                else
                {
                    tail.next = tl2;
                    tl2 = tl2.next;
                }
                tail = tail.next;
            }
            tail.next = tl1 == null ? tl2 : tl1;

            return head.next;
        }
    }
}
