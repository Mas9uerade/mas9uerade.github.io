    ListNode* removeNthFromEnd(ListNode* head, int n) 
    {
        // write code here
        if(head == nullptr || n <=0)
        {
            return head;
        }
        ListNode* low = head;
        ListNode* fast = head;
        while(n-- && fast)
        {
            fast = fast->next;
        }
        if (fast == NULL) 
        {
            return head->next;
        }
        while(fast->next != NULL)
        {
            fast = fast->next;
            low = low->next;
        }
        low->next = low->next->next;
        return head;
    }