//199.二叉树的右视图
//给定一棵二叉树，想象自己站在它的右侧，按照从顶部到底部的顺序，返回从右侧所能看到的节点值。

//示例:

//输入:[1,2,3,null,5,null,4]
//输出:[1, 3, 4]
//解释:

//1 < ---

///   \
//2     3 < ---
// \     \
//  5     4 < ---

//来源：力扣（LeetCode）
//链接：https://leetcode-cn.com/problems/binary-tree-right-side-view
//著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

using System;
using System.Collections.Generic;

//思路
// 1.  右序优先的深度遍历，记录信息，深度，最大深度，值
// 2.  左序优先的层级遍历，记录信息，深度，最大深度
namespace ByteDancePopular
{
     public partial class Solution
     {
        public IList<int> RightSideView(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>(0);
            }

            TreeNode node = root;
            Stack<TreeNode> nodeStack = new Stack<TreeNode>();
            Stack<int> depthStack = new Stack<int>();
            Dictionary<int, int> rightView = new Dictionary<int, int>();
            nodeStack.Push(root);
            depthStack.Push(0);
            int maxDepth = -1;
            int depth = -1;

            while (nodeStack.Count > 0)
            {
                node = nodeStack.Pop();
                depth = depthStack.Pop();

                if (node!= null)
                {
                    maxDepth = Math.Max(depth, maxDepth);

                    if (!rightView.ContainsKey(depth))
                    {
                        rightView.Add(depth, node.val);
                    }
                }
                //栈先进后出
                if (node.right != null)
                {
                    nodeStack.Push(node.right);
                    depthStack.Push(depth + 1);
                }
                if (node.left !=  null)
                {
                    nodeStack.Push(node.left);
                    depthStack.Push(depth + 1);
                }
            }

            List<int> ret = new List<int>(maxDepth +1);

            for (int i = 0; i <= maxDepth; ++i)
            {
                ret.Add(rightView[i]);
            }
            return ret;

        }

        public IList<int> RightSideView2(TreeNode root)
        {
            if (root == null)
            {
                return new List<int>(0);
            }
            Dictionary<int, int> rightView = new Dictionary<int, int>();
            Queue<TreeNode> nodeQueue = new Queue<TreeNode>();
            Queue<int> depthQueue = new Queue<int>();
            TreeNode node;
            int depth = 0;
            int maxDepth = 0;

            nodeQueue.Enqueue(root);
            depthQueue.Enqueue(0);

            while (nodeQueue.Count > 0)
            {
                node =nodeQueue.Dequeue();
                depth = depthQueue.Dequeue();

                maxDepth = Math.Max(maxDepth, depth);

                if (!rightView.ContainsKey(depth))
                {
                    rightView.Add(depth, node.val);
                }

                if (node.right != null)
                {
                    nodeQueue.Enqueue(node.right);
                    depthQueue.Enqueue(depth + 1);
                }

                if (node.right != null)
                {
                    nodeQueue.Enqueue(node.left);
                    depthQueue.Enqueue(depth + 1);
                }
            }
            List<int> ret = new List<int>(maxDepth + 1);

            for (int i = 0; i <= maxDepth; ++i)
            {
                ret.Add(rightView[i]);
            }
            return ret;
        }

    }
}
