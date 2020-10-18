    vector<vector<int> > threeOrders(TreeNode* root) 
    {
        vector<vector<int>> result(3);
        if (root == NULL)
        {
            return result;
        }
        travel_deep(root, result);
    }
    
    void travel_deep(TreeNode* root, vector<vector<int>> &result)
    {
        if (root)
        {
            result[0].push_back(root->val);
            travel_deep(root->left, result);
            result[1].push_back(root->val);
            travel_deep(root->right, result);
            result[2].push_back(root->val);
        }
    }