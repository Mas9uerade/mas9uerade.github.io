struct ListNode {
	int val;
	struct ListNode* next;
	ListNode(int x) :
		val(x), next(NULL) {
	}
};

ListNode* mergeTwoLists(ListNode* l1, ListNode* l2)
{
	ListNode* tmpNode = nullptr;
	if (l2 == nullptr)
		return l1;
	else if (l1 == nullptr)
		return l2;
	if (l1->val < l2->val)
	{
		tmpNode = l1;
		l1 = l1->next;
	}
	else
	{
		tmpNode = l2;
		l2 = l2->next;
	}
	ListNode* head = tmpNode;
	while (!(l1 == nullptr && l2 == nullptr))
	{
		if (l1 == nullptr)
		{
			tmpNode->next = l2;
			l2 = l2->next;
			tmpNode = tmpNode->next;
		}
		else if (l2 == nullptr)
		{
			tmpNode->next = l1;
			l1 = l1->next;
			tmpNode = tmpNode->next;
		}
		else
		{
			if (l1->val < l2->val)
			{
				tmpNode->next = l1;
				l1 = l1->next;
				tmpNode = tmpNode->next;
			}
			else
			{
				tmpNode->next = l2;
				l2 = l2->next;
				tmpNode = tmpNode->next;
			}
		}
	}
	return head;
}