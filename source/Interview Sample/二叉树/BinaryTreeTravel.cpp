/**
 * struct TreeNode {
 *	int val;
 *	struct TreeNode *left;
 *	struct TreeNode *right;
 * };
 */

class Solution 
{
public:
    /**
     * 
     * @param root TreeNode类 
     * @return int整型vector<vector<>>
     */
    vector<vector<int> > levelOrder(TreeNode* root) 
    {
        // write code here
        vector<vector<int>> result;
        if (root == NULL)
        {
            return result;
        }
        queue<TreeNode*> queueTree;
        queueTree.push(root);
        while(!queueTree.empty())
        {
            int len = queueTree.size();
            vector<int> val;
            for (int i = 0; i <len; i++)
            {
                TreeNode* root = queueTree.front();
                queueTree.pop();
                val.push_back(root->val);
                if(root->left != NULL)
                {
                    queueTree.push(root->left);
                }
                if(root->right != NULL)
                {
                    queueTree.push(root->right);
                }
            }
            result.push_back(val);
        }
        return result;
    }
};