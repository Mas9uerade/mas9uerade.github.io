/**
 * struct TreeNode {
 *	int val;
 *	struct TreeNode *left;
 *	struct TreeNode *right;
 * };
 */

class Solution {
public:
    /**
     * 
     * @param root TreeNode类 
     * @return int整型
     */
    int maxDepth(TreeNode* root) 
    {
        if (root == NULL) 
        {
            return 0;
        }
        return max(maxDepth(root->left), maxDepth(root->right))+1;
        // write code here
    }
    

};