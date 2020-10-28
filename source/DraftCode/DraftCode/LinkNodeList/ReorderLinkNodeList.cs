using System.Collections.Generic;

public class Solution
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    public void ReorderList(ListNode head)
    {
        if (head == null)
        {
            return;
        }

        ListNode front = head;
        ListNode end = head;

        Stack<ListNode> stackNode = new Stack<ListNode>();

        while (end != null)
        {
            stackNode.Push(end);
            end = end.next;
        }

        while (head != stackNode.Peek())
        {
            ListNode last = stackNode.Pop();
            ListNode tmp = head.next;
            if (tmp != last)
            {
                head.next = last;
                last.next = tmp;
                head = tmp;
            }
            else
            {
                head.next = last;
                last.next = null;
                return;
            }
        }
        head.next = null;
    }
}