class Solution {
public:
    bool hasCycle(ListNode *head) 
    {
        ListNode* fast = head;
        ListNode* slow = head;
        while (fast->next != nullptr && fast!= nullptr)
        {
            fast = fast->next->next;
            slow = slow->next;
            if (fast == slow)
            {
                return true;
            }
        }
        return false;
    }
};