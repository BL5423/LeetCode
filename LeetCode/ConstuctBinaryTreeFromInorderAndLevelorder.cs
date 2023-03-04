using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    // Java version: https://practice.geeksforgeeks.org/problems/construct-tree-from-inorder-and-levelorder/1
    public class ConstuctBinaryTreeFromInorderAndLevelorder
    {
        public TreeNode ConstructFromInLevel(int[] inorder, int[] levelorder)
        {
            Dictionary<int, int> val2InorderIndex = new Dictionary<int, int>(inorder.Length);
            for(int i = 0; i < inorder.Length; ++i)
            {
                val2InorderIndex[inorder[i]] = i;
            }

            int nextChildIndex = 1;
            bool[] processed = new bool[inorder.Length];
            Dictionary<int, TreeNode> index2TreeNode = new Dictionary<int, TreeNode>(levelorder.Length);
            for(int i = 0; i < levelorder.Length; ++i)
            {
                if (!index2TreeNode.TryGetValue(i, out TreeNode node))
                {
                    node = new TreeNode(levelorder[i]);
                    index2TreeNode.Add(i, node);
                }

                int indexInorder = val2InorderIndex[node.val];
                processed[indexInorder] = true;

                int leftChildIndex = nextChildIndex;
                if (leftChildIndex < levelorder.Length)
                {
                    if (indexInorder - 1 >= 0 && !processed[indexInorder - 1])
                    {
                        ++nextChildIndex;
                        node.left = new TreeNode(levelorder[leftChildIndex]);
                        index2TreeNode.Add(leftChildIndex, node.left);
                    }
                }

                int rigthChildIndex = nextChildIndex;
                if (rigthChildIndex < levelorder.Length)
                {
                    if (indexInorder + 1 < inorder.Length && !processed[indexInorder + 1])
                    {
                        ++nextChildIndex;
                        node.right = new TreeNode(levelorder[rigthChildIndex]);
                        index2TreeNode.Add(rigthChildIndex, node.right);
                    }
                }
            }

            return index2TreeNode[0];
        }
    }
}
