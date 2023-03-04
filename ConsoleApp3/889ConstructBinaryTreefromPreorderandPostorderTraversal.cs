using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _889ConstructBinaryTreefromPreorderandPostorderTraversal
    {
        /*
          Create a node TreeNode(pre[preIndex]) as the root.
          Becasue root node will be lastly iterated in post order,
          if root.val == post[posIndex],
          it means we have constructed the whole tree,

          If we haven't completed constructed the whole tree,
          So we recursively constructFromPrePost for left sub tree and right sub tree.

          And finally, we'll reach the posIndex that root.val == post[posIndex].
          We increment posIndex and return our root node.
         */
        int preIndex = 0;
        int postIndex = 0;

        public TreeNode ConstructFromPrePost(int[] preorder, int[] postorder)
        {
            var root = new TreeNode(preorder[preIndex++]);
            if (root.val != postorder[postIndex])
                root.left = ConstructFromPrePost(preorder, postorder);
            if (root.val != postorder[postIndex])
                root.right = ConstructFromPrePost(preorder, postorder);

            ++postIndex;
            return root;
        }

        public TreeNode ConstructFromPrePostV1(int[] preorder, int[] postorder)
        {
            var root = new TreeNode(preorder[preIndex++]);
            if (root.val != postorder[postIndex])
                root.left = ConstructFromPrePost(preorder, postorder);
            if (root.val != postorder[postIndex])
                root.right = ConstructFromPrePost(preorder, postorder);
            ++postIndex;

            return root;
        }

        public TreeNode ConstructFromPrePost_Iterative(int[] preorder, int[] postorder)
        {
            int post = 0;
            var root = new TreeNode(preorder[0]);
            Stack<TreeNode> stack = new Stack<TreeNode>(preorder.Length);
            stack.Push(root);
            for(int index = 1; index < preorder.Length; ++index)
            {
                var node = new TreeNode(preorder[index]);
                var parent = stack.Peek();
                if (parent.left == null)
                    parent.left = node;
                else
                    parent.right = node;
                stack.Push(node);

                while (stack.Count > 0 && stack.Peek().val == postorder[post])
                {
                    ++post;
                    stack.Pop();
                }
            }

            return root;
        }

        public TreeNode ConstructFromPrePost_IterativeV1(int[] preorder, int[] postorder)
        {
            /*
            We will preorder generate TreeNodes, push them to stack and postorder pop them out.
            Iterate on pre array and construct node one by one.
            stack save the current path of tree.
            node = new TreeNode(pre[i]), if not left child, add node to the left. otherwise add it to the right.
            If we meet a same value in the pre and post, it means we complete the construction for current subtree. We pop it from stack.
            */
            int postIndex = 0;
            TreeNode root = new TreeNode(preorder[0]);
            Stack<TreeNode> stack = new Stack<TreeNode>(preorder.Length);
            stack.Push(root);
            for(int preIndex = 1; preIndex < preorder.Length; ++preIndex)
            {
                var node = new TreeNode(preorder[preIndex]);
                var parent = stack.Peek();
                if (parent.left == null)
                    parent.left = node;
                else
                    parent.right = node;

                stack.Push(node);

                while (stack.Count > 0 && stack.Peek().val == postorder[postIndex])
                {
                    ++postIndex;

                    // pop up to another root
                    stack.Pop();
                }
            }

            return root;
        }

        public TreeNode ConstructFromPrePost_Iterative_Split_Queue(int[] preorder, int[] postorder)
        {
            Dictionary<int, int> valueToIndexInPreorder = new Dictionary<int, int>(preorder.Length);
            for (int index = 0; index < preorder.Length; ++index)
                valueToIndexInPreorder[preorder[index]] = index;

            Dictionary<int, int> valueToIndexInPostorder = new Dictionary<int, int>(postorder.Length);
            for (int index = 0; index < postorder.Length; ++index)
                valueToIndexInPostorder[postorder[index]] = index;

            Queue<(TreeNode, bool, int, int, int)> queue = new Queue<(TreeNode, bool, int, int, int)>(preorder.Length);
            var root = new TreeNode(preorder[0]);
            var indexOfLeftInPreorder = 1;
            if (indexOfLeftInPreorder < preorder.Length)
            {
                var indexOfLeftInPostorder = valueToIndexInPostorder[preorder[indexOfLeftInPreorder]];

                var indexOfRightInPostorder = postorder.Length - 2;
                if (indexOfRightInPostorder > indexOfLeftInPostorder)
                {
                    var indexOfRightInPreorder = valueToIndexInPreorder[postorder[indexOfRightInPostorder]];

                    queue.Enqueue((root, true, indexOfLeftInPreorder, indexOfRightInPreorder - 1, indexOfLeftInPostorder));
                    queue.Enqueue((root, false, indexOfRightInPreorder, preorder.Length - 1, indexOfRightInPostorder));
                }
                else
                {
                    queue.Enqueue((root, true, indexOfLeftInPreorder, preorder.Length - 1, indexOfLeftInPostorder));
                }
            }

            while (queue.Count > 0)
            {
                var top = queue.Dequeue();
                var parent = top.Item1;
                var leftRight = top.Item2;
                var preStartIndex = top.Item3;
                var preEndIndex = top.Item4;
                var postEndIndex = top.Item5;

                TreeNode child = null;
                if (leftRight)
                {
                    child = new TreeNode(preorder[preStartIndex]);
                    parent.left = child;
                }
                else
                {
                    child = new TreeNode(preorder[preStartIndex]);
                    parent.right = child;
                }

                indexOfLeftInPreorder = preStartIndex + 1;
                if (indexOfLeftInPreorder <= preEndIndex)
                {
                    var indexOfLeftInPostorder = valueToIndexInPostorder[preorder[indexOfLeftInPreorder]];

                    var indexOfRightInPostorder = postEndIndex - 1;
                    if (indexOfRightInPostorder > indexOfLeftInPostorder)
                    {
                        var indexOfRightInPreorder = valueToIndexInPreorder[postorder[indexOfRightInPostorder]];

                        queue.Enqueue((child, true, indexOfLeftInPreorder, indexOfRightInPreorder - 1, indexOfLeftInPostorder));
                        queue.Enqueue((child, false, indexOfRightInPreorder, preEndIndex, indexOfRightInPostorder));
                    }
                    else
                    {
                        queue.Enqueue((child, true, indexOfLeftInPreorder, preEndIndex, indexOfLeftInPostorder));
                    }
                }
            }

            return root;
        }

        public TreeNode ConstructFromPrePost_Iterative_Split_Stack(int[] preorder, int[] postorder)
        {
            Dictionary<int, int> valueToIndexInPreorder = new Dictionary<int, int>(preorder.Length);
            for (int index = 0; index < preorder.Length; ++index)
                valueToIndexInPreorder[preorder[index]] = index;

            Dictionary<int, int> valueToIndexInPostorder = new Dictionary<int, int>(postorder.Length);
            for (int index = 0; index < postorder.Length; ++index)
                valueToIndexInPostorder[postorder[index]] = index;

            Stack<(TreeNode, bool, int, int, int, int)> stack = new Stack<(TreeNode, bool, int, int, int, int)>(preorder.Length);
            var root = new TreeNode(preorder[0]);
            var indexOfLeftInPreorder = 1;
            if (indexOfLeftInPreorder < preorder.Length)
            {
                var indexOfLeftInPostorder = valueToIndexInPostorder[preorder[indexOfLeftInPreorder]];

                var indexOfRightInPostorder = postorder.Length - 2;
                if (indexOfRightInPostorder > indexOfLeftInPostorder)
                {
                    var indexOfRightInPreorder = valueToIndexInPreorder[postorder[indexOfRightInPostorder]];

                    stack.Push((root, true,  indexOfLeftInPreorder,  indexOfRightInPreorder - 1, 0, indexOfLeftInPostorder));
                    stack.Push((root, false, indexOfRightInPreorder, preorder.Length - 1, indexOfLeftInPostorder + 1, indexOfRightInPostorder));
                }
                else
                {
                    stack.Push((root, true, indexOfLeftInPreorder, preorder.Length - 1, 0, indexOfLeftInPostorder));
                }
            }

            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var parent = top.Item1;
                var leftRight = top.Item2;
                var preStartIndex = top.Item3;
                var preEndIndex = top.Item4;
                var postStartIndex = top.Item5;
                var postEndIndex = top.Item6;

                TreeNode child = null;
                if (leftRight)
                {
                    child = new TreeNode(preorder[preStartIndex]);
                    parent.left = child;
                }
                else
                {
                    child = new TreeNode(preorder[preStartIndex]);
                    parent.right = child;
                }

                indexOfLeftInPreorder = preStartIndex + 1;
                if (indexOfLeftInPreorder <= preEndIndex)
                {
                    var indexOfLeftInPostorder = valueToIndexInPostorder[preorder[indexOfLeftInPreorder]];

                    var indexOfRightInPostorder = postEndIndex - 1;
                    if (indexOfRightInPostorder > indexOfLeftInPostorder)
                    {
                        var indexOfRightInPreorder = valueToIndexInPreorder[postorder[indexOfRightInPostorder]];

                        stack.Push((child, true, indexOfLeftInPreorder, indexOfRightInPreorder - 1, postStartIndex, indexOfLeftInPostorder));
                        stack.Push((child, false, indexOfRightInPreorder, preEndIndex, indexOfLeftInPostorder + 1, indexOfRightInPostorder));
                    }
                    else
                    {
                        stack.Push((child, true, indexOfLeftInPreorder, preEndIndex, postStartIndex, indexOfLeftInPostorder));
                    }
                }
            }

            return root;
        }

        public TreeNode ConstructFromPrePost_Recursively(int[] preorder, int[] postorder)
        {
            Dictionary<int, int> valueToIndexInPreorder = new Dictionary<int, int>(preorder.Length);
            for (int index = 0; index < preorder.Length; ++index)
                valueToIndexInPreorder[preorder[index]] = index;

            Dictionary<int, int> valueToIndexInPostorder = new Dictionary<int, int>(postorder.Length);
            for (int index = 0; index < postorder.Length; ++index)
                valueToIndexInPostorder[postorder[index]] = index;

            return ConstructFromPrePost(preorder, 0, preorder.Length - 1, postorder, 0, postorder.Length - 1, valueToIndexInPreorder, valueToIndexInPostorder);
        }

        private TreeNode ConstructFromPrePost(int[] preorder, int preStart, int preEnd, int[] postorder, int postStart, int postEnd, Dictionary<int, int> valueToIndexInPreorder, Dictionary<int, int> valueToIndexInPostorder)
        {
            var root = new TreeNode(preorder[preStart]);

            if (preStart < preEnd)
            {
                var leftNodeIndexInPreorder = preStart + 1;
                var leftNodeIndexInPostorder = valueToIndexInPostorder[preorder[leftNodeIndexInPreorder]];

                var rightNodeIndexInPostorder = postEnd - 1;
                if (rightNodeIndexInPostorder > leftNodeIndexInPostorder)
                {
                    var rightNodeIndexInPreorder = valueToIndexInPreorder[postorder[rightNodeIndexInPostorder]];

                    root.left = ConstructFromPrePost(preorder, leftNodeIndexInPreorder, rightNodeIndexInPreorder - 1, postorder, postStart, leftNodeIndexInPostorder, valueToIndexInPreorder, valueToIndexInPostorder);
                    root.right = ConstructFromPrePost(preorder, rightNodeIndexInPreorder, preEnd, postorder, leftNodeIndexInPostorder + 1, rightNodeIndexInPostorder, valueToIndexInPreorder, valueToIndexInPostorder);
                }
                else
                {
                    root.left = ConstructFromPrePost(preorder, leftNodeIndexInPreorder, preEnd, postorder, postStart, leftNodeIndexInPostorder, valueToIndexInPreorder, valueToIndexInPostorder);
                }
            }

            return root;
        }
    }
}
