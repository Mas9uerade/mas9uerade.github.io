/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution 
{
    public class ListNode 
    {
      public int val;
      public ListNode next;
      public ListNode(int val=0, ListNode next=null) 
      {
          this.val = val;
          this.next = next;
      }
    }
    public void ReorderList(ListNode head) 
    {
        ListNode front = head;
        ListNode end  = head;

        Stack<ListNode> stackNode =  new Stack<ListNode>();

        while (end.next != null )
        {
            
        }
    }
}