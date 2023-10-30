using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _105ConstructBinaryTreefromPreorderandInorderTraversal
    {
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            var root = new TreeNode(preorder[0]);
            Stack<TreeNode> stack = new Stack<TreeNode>(preorder.Length);
            TreeNode curNode = root;
            int inorderIndex = 0;
            for(int index = 1; index < preorder.Length; ++index)
            {
                var node = new TreeNode(preorder[index]);
                if (curNode.val != inorder[inorderIndex])
                {
                    // complete left first
                    curNode.left = node;
                    stack.Push(curNode);
                    curNode = node;
                }
                else// complete right child
                {
                    // the current val of inorder has been matched by curNode, so move it forward for next val
                    ++inorderIndex;

                    // keep looking for the right most val in inorder
                    while (stack.Count > 0 && stack.Peek().val == inorder[inorderIndex])
                    {
                        ++inorderIndex;
                        curNode = stack.Pop();
                    }

                    // do not need to push curNode into stack again since all its children are set
                    curNode.right = node;
                    curNode = node;
                }
            }

            return root;
        }

        public TreeNode BuildTreeV1(int[] preorder, int[] inorder)
        {
            int inorderIndex = 0;
            var root = new TreeNode(preorder[0]);
            var curNode = root;
            Stack<TreeNode> stack = new Stack<TreeNode>(preorder.Length);
            for(int index = 1; index < preorder.Length; ++index)
            {
                var child = new TreeNode(preorder[index]);
                if (inorder[inorderIndex] != curNode.val)
                {
                    curNode.left = child;
                    stack.Push(curNode);
                    curNode = child;
                }
                else // inorder[inorderIndex] == curNode.val, which means we have done setup of left branch for curNode
                {
                    // move to next node in inorder
                    ++inorderIndex;
                    // keep finding the right-most node in inorder as the parent of child
                    // take below tree as example
                    // preorder 3 9 20 15 7
                    // inorder  9 3 15 20 7
                    // assuming curNode is 20, and 3,9 are in the stack
                    // 3 is the right most node for 20, if we don't use 3 as 20's parent but 9 instead
                    // since 9 is on the left of 3, then 20(as 9's right child) should precede 3 in inorder, however that's not the case in above example
                    while (stack.Count > 0 && stack.Peek().val == inorder[inorderIndex])
                    {
                        curNode = stack.Pop();
                        ++inorderIndex;
                    }

                    curNode.right = child;
                    curNode = child;
                }
            }

            return root;
        }

        public TreeNode BuildTree_Recursively(int[] preorder, int[] inorder)
        {
            return BuildTree(preorder, inorder, int.MaxValue);
        }

        int preIndex = 0;
        int inIndex = 0;

        private TreeNode BuildTree(int[] preorder, int[] inorder, int stopInorderVal)
        {
            if (preIndex >= preorder.Length)
                return null;
            
            // Each recursive call gets told where to stop,
            // and it tells its subcalls where to stop.
            // It gives its own root value as stopper to its left subcall and its parent`s stopper as stopper to its right subcall.
            if (inorder[inIndex] == stopInorderVal)
            {
                ++inIndex;
                return null;
            }

            var node = new TreeNode(preorder[preIndex++]);
            node.left = BuildTree(preorder, inorder, node.val);
            node.right = BuildTree(preorder, inorder, stopInorderVal);
            return node;
        }

        public TreeNode BuildTree_Iterative_Split_Stack(int[] preorder, int[] inorder)
        {
            Dictionary<int, int> valueToIndexInInorder = new Dictionary<int, int>(inorder.Length);
            for(int index = 0; index < inorder.Length; ++index)
            {
                valueToIndexInInorder[inorder[index]] = index;
            }

            Stack<(TreeNode, bool, int, int, int, int)> stack = new Stack<(TreeNode, bool, int, int, int, int)>(preorder.Length);
            var root = new TreeNode(preorder[0]);
            var indexOfRootInInorder = valueToIndexInInorder[root.val];
            int leftSize = indexOfRootInInorder;
            if (leftSize > 0)
            {
                stack.Push((root, true, 1, leftSize, 0, indexOfRootInInorder - 1));
            }
            int rightSize = inorder.Length - 1 - indexOfRootInInorder;
            if (rightSize > 0)
            {
                stack.Push((root, false, preorder.Length - rightSize, preorder.Length - 1, indexOfRootInInorder + 1, inorder.Length - 1));
            }

            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var parent = top.Item1;
                var leftRight = top.Item2;
                var startIndexPreorder = top.Item3;
                var endIndexPreorder = top.Item4;
                var startIndexInorder = top.Item5;
                var endIndexInorder = top.Item6;

                var node = new TreeNode(preorder[startIndexPreorder]);
                if (leftRight)
                {
                    parent.left = node;
                }
                else
                {
                    parent.right = node;
                }

                indexOfRootInInorder = valueToIndexInInorder[node.val];
                leftSize = indexOfRootInInorder - startIndexInorder;
                if (leftSize > 0)
                {
                    stack.Push((node, true, startIndexPreorder + 1, startIndexInorder + leftSize, startIndexInorder, indexOfRootInInorder - 1));
                }
                rightSize = endIndexInorder - indexOfRootInInorder;
                if (rightSize > 0)
                {
                    stack.Push((node, false, startIndexPreorder + leftSize + 1, endIndexPreorder, indexOfRootInInorder + 1, endIndexInorder));
                }
            }

            return root;
        }

        public TreeNode BuildTree_Iterative_Split_Queue(int[] preorder, int[] inorder)
        {
            Dictionary<int, int> valueToIndexInInorder = new Dictionary<int, int>(inorder.Length);
            for (int index = 0; index < inorder.Length; ++index)
            {
                valueToIndexInInorder[inorder[index]] = index;
            }

            Queue<(TreeNode, bool, int, int, int)> queue = new Queue<(TreeNode, bool, int, int, int)>(preorder.Length);
            var root = new TreeNode(preorder[0]);
            var indexOfRootInInorder = valueToIndexInInorder[root.val];
            int leftSize = indexOfRootInInorder;
            if (leftSize > 0)
            {
                queue.Enqueue((root, true, 1, 0, indexOfRootInInorder - 1));
            }
            int rightSize = inorder.Length - 1 - indexOfRootInInorder;
            if (rightSize > 0)
            {
                queue.Enqueue((root, false, preorder.Length - rightSize, indexOfRootInInorder + 1, inorder.Length - 1));
            }

            while (queue.Count > 0)
            {
                var top = queue.Dequeue();
                var parent = top.Item1;
                var leftRight = top.Item2;
                var startIndexPreorder = top.Item3;
                var startIndexInorder = top.Item4;
                var endIndexInorder = top.Item5;

                var node = new TreeNode(preorder[startIndexPreorder]);
                if (leftRight)
                {
                    parent.left = node;
                }
                else
                {
                    parent.right = node;
                }

                indexOfRootInInorder = valueToIndexInInorder[node.val];
                leftSize = indexOfRootInInorder - startIndexInorder;
                if (leftSize > 0)
                {
                    queue.Enqueue((node, true, startIndexPreorder + 1, startIndexInorder, indexOfRootInInorder - 1));
                }
                rightSize = endIndexInorder - indexOfRootInInorder;
                if (rightSize > 0)
                {
                    queue.Enqueue((node, false, startIndexPreorder + leftSize + 1, indexOfRootInInorder + 1, endIndexInorder));
                }
            }

            return root;
        }
    }
}
