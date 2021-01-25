namespace ByteDancePopular
{
    public partial class Solution
    {
        public ListNode ReverseList_Recursion(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            ListNode p = ReverseList_Recursion(head.next);
            head.next.next = head;
            head.next = null;
            return p;
        }

        public ListNode ReverseList_Iteration(ListNode head)
        {
            ListNode node = head;
            ListNode cur = null;
            while (node != null)
            {
                ListNode tmp = node.next;
                node.next = cur;
                cur = node;
                node = tmp;
            }
            return cur;
        }
    }
}