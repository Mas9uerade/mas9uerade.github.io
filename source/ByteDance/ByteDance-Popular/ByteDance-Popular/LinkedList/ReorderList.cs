//143.重排链表
//给定一个单链表 L：L0→L1→…→Ln-1→Ln ，
//将其重新排列后变为： L0→Ln→L1→Ln-1→L2→Ln-2→…

//你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。

//示例 1:

//给定链表 1->2->3->4, 重新排列为 1->4->2->3.
//示例 2:

//给定链表 1->2->3->4->5, 重新排列为 1->5->2->4->3.
//通过次数74,391提交次数124,797

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/reorder-list
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//思路 --->
// 1. 快慢指针找到链表重点
// 2. 反转右半部分链表
// 3. 合并链表
namespace ByteDancePopular
{
    public partial class Solution
    {
        public void ReorderList(ListNode head)
        {
            ListNode mid = GetMiddleNode(head);
            ListNode l1 = head;
            ListNode l2 = mid.next;
            mid.next = null;
            l2 = ReverseList(l2);
            MergeList(l1, l2);
        }

        private ListNode GetMiddleNode(ListNode head)
        {
            ListNode fast = head;
            ListNode slow = head;
            while (fast.next != null && fast.next.next!= null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }

            return slow;
        }

        private ListNode ReverseList(ListNode head)
        {
            ListNode cur = head;
            ListNode prev = null;
            while (cur!=null)
            {
                ListNode nextTmp = cur.next;
                cur.next = prev;
                prev = cur;
                cur = nextTmp;
            }
            return prev;
        }

        private void MergeList(ListNode l1, ListNode l2)
        {
            ListNode l1_tmp;
            ListNode l2_tmp;

            while (l1 != null && l2 !=null)
            {
                l1_tmp = l1.next;
                l2_tmp = l2.next;

                l1.next = l2;
                l1 = l1_tmp;

                l2.next = l1;
                l2 = l2_tmp;
            }
        }
    }
}
