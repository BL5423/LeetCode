using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _100SameTree
    {
        public bool IsSameTree_Level(TreeNode p, TreeNode q)
        {
            Queue<(TreeNode, TreeNode)> queue = new Queue<(TreeNode, TreeNode)>();
            queue.Enqueue((p, q));
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                var node1 = head.Item1;
                var node2 = head.Item2;
                if (node1 == null && node2 == null)
                    continue;

                if ((node1 != null && node2 == null) || (node1 == null && node2 != null) || (node1.val != node2.val))
                    return false;

                queue.Enqueue((node1.left, node2.left));
                queue.Enqueue((node1.right, node2.right));
            }

            return true;
        }

        public bool IsSameTree_Morris(TreeNode p, TreeNode q)
        {
            var results1 = GetMorrisPreorder(p).GetEnumerator();
            var results2 = GetMorrisPreorder(q).GetEnumerator();
            if (!Equal(results1, results2))
                return false;

            //results1 = GetMorrisInorder(p).GetEnumerator();
            //results2 = GetMorrisInorder(q).GetEnumerator();

            results1 = GetMorrisPostorder(p).GetEnumerator();
            results2 = GetMorrisPostorder(q).GetEnumerator();
            if (!Equal(results1, results2))
                return false;

            return true;    
        }

        private bool Equal(IEnumerator<TreeNode> enumerator1, IEnumerator<TreeNode> enumerator2)
        {
            while (true)
            {
                var pHasNext = enumerator1.MoveNext();
                var qHasNext = enumerator2.MoveNext();
                if (pHasNext != qHasNext)
                    return false;

                if (!pHasNext)
                    break;

                if (enumerator1.Current.val != enumerator2.Current.val)
                    return false;

                if (enumerator1.Current.left?.val != enumerator2.Current.left?.val ||
                    enumerator1.Current.right?.val != enumerator2.Current.right?.val)
                    return false;
            }

            return true;
        }

        public IEnumerable<TreeNode> GetMorrisPostorder(TreeNode root)
        {
            var list = new LinkedList<TreeNode>();
            var node = root;
            while(node != null)
            {
                var right = node.right;
                if (right != null)
                {
                    var left = right;
                    while (left.left != null && left.left != node)
                    {
                        left = left.left;
                    }

                    if (left.left == null)
                    {
                        list.AddFirst(node);
                        left.left = node;
                        node = node.right;
                    }
                    else // left.left == node
                    {
                        left.left = null;
                        node = node.left;
                    }
                }
                else
                {
                    list.AddFirst(node);
                    node = node.left;
                }
            }

            return list;
        }
        
        public IEnumerable<TreeNode> GetMorrisInorder(TreeNode root)
        {
            var node = root;
            while (node != null)
            {
                var left = node.left;
                if (left != null)
                {
                    var toRight = left;
                    while (toRight.right != null && toRight.right != node)
                    {
                        toRight = toRight.right;
                    }

                    if (toRight.right == null)
                    {
                        toRight.right = node;
                        node = node.left;
                    }
                    else // toRight.right == node
                    {
                        toRight.right = null;
                        
                        yield return node;

                        node = node.right;
                    }
                }
                else
                {
                    yield return node;
                    node = node.right;
                }
            }
        }
        
        public IEnumerable<TreeNode> GetMorrisPreorder(TreeNode root)
        {
            var node = root;
            while (node != null)
            {
                var left = node.left;
                if (left != null)
                {
                    var toRight = left;
                    while (toRight.right != null && toRight.right != node)
                    {
                        toRight = toRight.right;
                    }

                    if (toRight.right == null)
                    {
                        yield return node;

                        // connect toRight and node
                        toRight.right = node;
                        node = node.left;
                    }
                    else // toRight.right == node
                    {
                        // disconnect toRight and node 
                        toRight.right = null;
                        node = node.right;
                    }
                }
                else // left == null
                {
                    // output node and start with node.right
                    yield return node;

                    node = node.right;
                }
            }
        }

        public bool IsSameTree_IterativePostorder(TreeNode p, TreeNode q)
        {
            Stack<(TreeNode, TreeNode)> stack = new Stack<(TreeNode, TreeNode)>();
            stack.Push((p, q));
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var node1 = top.Item1;
                var node2 = top.Item2;
                if (node1 == null && node2 == null)
                    continue;

                if ((node1 != null && node2 == null) || (node1 == null && node2 != null))
                    return false;

                if (node1.val != node2.val)
                    return false;

                if (node1.left != null || node2.left != null)
                {
                    stack.Push((node1.left, node2.left));
                }
                if (node1.right != null || node2.right != null)
                {
                    stack.Push((node1.right, node2.right));
                }
            }

            return true;
        }

        public bool IsSameTree_IterativeInorder(TreeNode p, TreeNode q)
        {
            Stack<(TreeNode, TreeNode)> stack = new Stack<(TreeNode, TreeNode)>();
            stack.Push((p, q));
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var node1 = top.Item1;
                var node2 = top.Item2;
                if (node1 == null && node2 == null)
                    continue;

                if ((node1 != null && node2 == null) || (node1 == null && node2 != null))
                    return false;

                if (node1.right != null || node2.right != null)
                {
                    stack.Push((node1.right, node2.right));
                }
                if (node1.left != null || node2.left != null)
                {
                    stack.Push((new TreeNode(node1.val), new TreeNode(node2.val)));
                    stack.Push((node1.left, node2.left));
                }
                else
                {
                    if (node1.val != node2.val)
                        return false;
                }
            }

            return true;
        }

        public bool IsSameTree_IterativePreorder(TreeNode p, TreeNode q)
        {
            Stack<(TreeNode, TreeNode)> stack = new Stack<(TreeNode, TreeNode)>();
            stack.Push((p, q));
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var node1 = top.Item1;
                var node2 = top.Item2;
                if (node1 == null && node2 == null)
                    continue;

                if ((node1 != null && node2 == null) || (node1 == null && node2 != null) || (node1.val != node2.val))
                    return false;

                stack.Push((node1.right, node2.right));
                stack.Push((node1.left, node2.left));
            }

            return true;
        }

        public bool IsSameTreeV1_Recursive(TreeNode p, TreeNode q)
        {
            return IsSameRecursively(p, q);
        }

        private bool IsSameRecursively(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
                return true;

            if ((p == null && q != null) || (p != null && q == null) || (p.val != q.val))
                return false;

            if (!IsSameRecursively(p.left, q.left))
                return false;

            return IsSameRecursively(p.right, q.right);
        }
    }
}
