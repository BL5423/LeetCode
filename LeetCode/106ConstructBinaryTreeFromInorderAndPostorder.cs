using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp106
{  
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    // https://www.geeksforgeeks.org/if-you-are-given-two-traversal-sequences-can-you-construct-the-binary-tree/
    // Given inorder traverse result, we can construct an unique binary tree with any other traverse(pre, post or level)
    // However, if without inorder, it won't be possible to rebuild unique binary tree. Example below:
    //               A                     A
    //              /                       \
    //             B                         B
    // Althought different, the above trees share same pre, post and level traverse results: pre A-B, post B-A, level A-B
    // But, there is one exception, if the binary is full(Full Binary Tree is a binary tree where every node has either 0 or 2 children), then inorder is not mandatory and we can construct the binary tree with only pre and post.
    //            17                         17
    //             \                         /
    //              13                      13
    // Preorder Traversal Follow this order Curr-Left-Right i.e. 17 _ 13 and 17 13 _ for the two trees
    // Postorder Traversal follow Left-Right-Curr i.e. _ 13 17 and 13 _ 17. So they are same for both the trees
    // As we ignore the blanks(_) we may get same answer for different trees
    // But if its a Full Binary Tree...That means there is no blanks
    // So without doubt we will get an unique Binary Tree
    // The conclusion above is based on assumption that either the values of each node is unique, or the traverse guarantees the order of a node in the tree.
    // If there are any dup values or the traverse does not preserve the order, then it is not possible to uniquely construct a binary tree:
    //              1                    1
    //             /                      \
    //            1                        1
    // Pre 1-1, Post 1-1, In 1-1, Level 1-1, unless we can differentiate the two 1s(i.e. 1' and 1), we can't build the unique result.

    public class _106ConstructBinaryTreeFromInorderAndPostorder
    {
        public TreeNode BuildTree_Iterative_Queue(int[] inorder, int[] postorder)
        {
            Dictionary<int, int> valueToIndexInInorder = new Dictionary<int, int>(inorder.Length);
            for (int index = 0; index < inorder.Length; ++index)
                valueToIndexInInorder[inorder[index]] = index;

            // (node, left/right, startIndexOfInorder, endIndexOfInorder, endIndexOfPostorder)
            Queue<(TreeNode, bool, int, int, int)> queue = new Queue<(TreeNode, bool, int, int, int)>(inorder.Length);
            var root = Push(postorder, queue, 0, inorder.Length - 1, postorder.Length - 1, valueToIndexInInorder);
            while (queue.Count > 0)
            {
                var top = queue.Dequeue();
                var node = top.Item1;
                var leftRight = top.Item2;
                var startIndexInorder = top.Item3;
                var endIndexInorder = top.Item4;
                var endIndexPostorder = top.Item5;

                if (startIndexInorder <= endIndexInorder && endIndexPostorder >= 0)
                {
                    var child = Push(postorder, queue, startIndexInorder, endIndexInorder, endIndexPostorder, valueToIndexInInorder);
                    if (leftRight)
                        node.left = child;
                    else
                        node.right = child;
                }
            }

            return root;
        }
        
        private TreeNode Push(int[] postorder, Queue<(TreeNode, bool, int, int, int)> queue, 
            int startIndexInorder, int endIndexInorder,
            int endIndexPostorder,
            Dictionary<int, int> valueToIndexInInorder)
        {
            TreeNode root = new TreeNode(postorder[endIndexPostorder]);
            var indexOfRootInInorder = valueToIndexInInorder[root.val];
            int leftSize = indexOfRootInInorder - startIndexInorder;
            int rightSize = endIndexInorder - indexOfRootInInorder;
            if (leftSize > 0)
            {
                // has left
                queue.Enqueue((root, true, startIndexInorder, indexOfRootInInorder - 1, endIndexPostorder - rightSize));
            }
            if (rightSize > 0)
            {
                // has right
                queue.Enqueue((root, false, indexOfRootInInorder + 1, endIndexInorder, endIndexPostorder - 1));
            }

            return root;
        }

        public TreeNode BuildTree_Iterative_Stack(int[] inorder, int[] postorder)
        {
            Dictionary<int, int> valueToIndexInInorder = new Dictionary<int, int>(inorder.Length);
            for (int index = 0; index < inorder.Length; ++index)
                valueToIndexInInorder[inorder[index]] = index;

            Stack<(TreeNode, bool, int, int, int, int)> stack = new Stack<(TreeNode, bool, int, int, int, int)>(inorder.Length);
            var root = Push(inorder, postorder, stack, 0, inorder.Length - 1, 0, postorder.Length - 1, valueToIndexInInorder);
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var node = top.Item1;
                var leftRight = top.Item2;
                var startIndexInorder = top.Item3;
                var endIndexInorder = top.Item4;
                var startIndexPostorder = top.Item5;
                var endIndexPostorder = top.Item6;

                if (startIndexInorder <= endIndexInorder && startIndexPostorder <= endIndexPostorder)
                {
                    var child = Push(inorder, postorder, stack, startIndexInorder, endIndexInorder, startIndexPostorder, endIndexPostorder, valueToIndexInInorder);
                    if (leftRight)
                        node.left = child;
                    else
                        node.right = child;
                }
            }

            return root;
        }

        private TreeNode Push(int[] inorder, int[] postorder, Stack<(TreeNode, bool, int, int, int, int)> stack, int startIndexInorder, int endIndexInorder, 
            int startIndexPostorder, int endIndexPostorder,
            Dictionary<int, int> valueToIndexInInorder)
        {
            TreeNode root = new TreeNode(postorder[endIndexPostorder]);
            var indexOfRootInInorder = valueToIndexInInorder[root.val];
            int leftSize = indexOfRootInInorder - startIndexInorder;
            int rightSize = endIndexInorder - indexOfRootInInorder;
            if (leftSize > 0)
            {
                // has left
                stack.Push((root, true, startIndexInorder, indexOfRootInInorder - 1, startIndexPostorder, startIndexPostorder + leftSize - 1));
            }
            if (rightSize > 0)
            {
                // has right
                stack.Push((root, false, indexOfRootInInorder + 1, endIndexInorder, startIndexPostorder + leftSize, endIndexPostorder - 1));
            }

            return root;
        }

        public TreeNode BuildTree_Recursively(int[] inorder, int[] postorder)
        {
            return BuildTree(inorder, 0, inorder.Length - 1, postorder, 0, postorder.Length - 1);
        }

        private TreeNode BuildTree(int[] inorder, int inorderStart, int inorderEnd, int[] postorder, int postOrderStart, int postOrderEnd)
        {
            if (inorderStart > inorderEnd || postOrderStart > postOrderEnd)
            {
                return null;
            }

            if (inorderStart == inorderEnd && postOrderEnd == postOrderStart)
            {
                return new TreeNode() { val = postorder[postOrderEnd] };
            }

            TreeNode root = new TreeNode() { val = postorder[postOrderEnd] };
            int indexInorder = Array.IndexOf(inorder, root.val, inorderStart);
            int nodesInLeft = indexInorder - inorderStart;
            int nodesInRight = inorderEnd - indexInorder;
            root.left =  BuildTree(inorder, inorderStart,     indexInorder - 1, postorder, postOrderStart,              postOrderStart + nodesInLeft - 1);
            root.right = BuildTree(inorder, indexInorder + 1, inorderEnd,       postorder, postOrderEnd - nodesInRight, postOrderEnd - 1);

            return root;
        }
    }
}
