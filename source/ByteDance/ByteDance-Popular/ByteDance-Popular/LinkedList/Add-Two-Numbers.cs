//2.两数相加
//给你两个 非空 的链表，表示两个非负的整数。它们每位数字都是按照 逆序 的方式存储的，并且每个节点只能存储 一位 数字。
//请你将两个数相加，并以相同形式返回一个表示和的链表。
//你可以假设除了数字 0 之外，这两个数都不会以 0 开头。

//示例 1：
//输入：l1 = [2,4,3], l2 = [5, 6, 4]
//输出：[7,0,8]
//解释：342 + 465 = 807.

//示例 2：
//输入：l1 = [0], l2 = [0]
//输出：[0]

//示例 3：
//输入：l1 = [9, 9, 9, 9, 9, 9, 9], l2 = [9, 9, 9, 9]
//输出：[8,9,9,9,0,0,0,1]

//提示：
//每个链表中的节点数在范围[1, 100] 内
//0 <= Node.val <= 9
//题目数据保证列表表示的数字不含前导零

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/add-two-numbers
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//思路
// 1. 逆序表示只需要两个链表合并从头节点开始相加合并即可

namespace ByteDancePopular
{
    public partial class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode node = new ListNode(0);
            ListNode head = node;
            int incre = 0;
            int val = 0;
            int sum = 0;
            while (l1 != null && l2 != null)
            {
                sum = l1.val + l2.val + incre;
                val = sum >= 10 ? sum - 10 : sum;
                incre = sum >= 10 ? 1 : 0;
                node.val = val;
                l1 = l1.next;
                l2 = l2.next;

                if (l1 == null && l2 == null)
                {
                    break;
                }
                else
                {
                    node.next = new ListNode(0);
                    node = node.next;
                }
            }

            while (l1 == null && l2 != null)
            {
                sum = l2.val + incre;
                val = sum >= 10 ? sum - 10 : sum;
                incre = sum >= 10 ? 1 : 0;

                node.val = val;
                l2 = l2.next;
                if (l2 != null)
                {
                    node.next = new ListNode(0);
                    node = node.next;
                }
            }

            while (l2 == null && l1 != null)
            {
                sum = l1.val + incre;
                val = sum >= 10 ? sum - 10 : sum;
                incre = sum >= 10 ? 1 : 0;

                node.val = val;
                l1 = l1.next;
                if (l1 != null)
                {
                    node.next = new ListNode(0);
                    node = node.next;
                }
            }

            if (incre == 1)
            {
                node.next = new ListNode(incre);
            }

            return head;
        }
    }
}
