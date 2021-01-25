//445.两数相加 II
//给你两个 非空 链表来代表两个非负整数。数字最高位位于链表开始位置。它们的每个节点只存储一位数字。将这两数相加会返回一个新的链表。
//你可以假设除了数字 0 之外，这两个数字都不会以零开头。
//进阶：
//如果输入链表不能修改该如何处理？换句话说，你不能对列表中的节点进行翻转。
//示例：
//输入：(7 -> 2 -> 4 -> 3) +(5-> 6-> 4)
//输出：7-> 8-> 0-> 7
//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/add-two-numbers-ii
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

//和第一题的区别在于是倒序的，且不能翻转链表
//思路 -> 空间换时间，存储链表数据 ->  先遍历的在底下，用栈

using System;
using System.Collections;
using System.Collections.Generic;

namespace ByteDancePopular
{
    public partial class Solution
    {
        public ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
        {
            Stack<int> stack1 = new Stack<int>();
            Stack<int> stack2 = new Stack<int>();

            while (l1 != null)
            {
                stack1.Push(l1.val);
                l1 = l1.next;
            }
            while (l2 != null)
            {
                stack2.Push(l2.val);
                l2 = l2.next;
            }

            int a, b;
            int val = 0;
            int sum = 0;
            int carry = 0;
            //ListNode head = new ListNode(0);
            ListNode cur = null;
            while (stack1.Count > 0 || stack2.Count > 0 || carry > 0)
            {
                a = stack1.Count > 0 ? stack1.Pop() : 0;
                b = stack2.Count > 0 ? stack2.Pop() : 0;
                sum = a + b + carry;
                val = sum >= 10 ? sum - 10 : sum;
                carry = sum >= 10 ? 1 : 0;

                ListNode node = new ListNode(val);
                node.next = cur;
                cur = node;
                //head.next = node;
            }
            return cur;
        }
    }
}
