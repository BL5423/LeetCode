using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _199BinaryTreeRightSideView
    {
        public IList<int> RightSideView(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                DFS(root, 0, res);
            }

            return res;
        }

        private void DFS(TreeNode node, int level, IList<int> res)
        {
            if (level == res.Count)
            {
                res.Add(node.val);
            }    

            if (node.right != null)
            {
                DFS(node.right, level + 1, res);
            }
            if (node.left != null)
            {
                DFS(node.left, level + 1, res);
            }
        }

        public IList<int> RightSideView_LevelTraverse(TreeNode root)
        {
            IList<int> res = new List<int>();
            if (root != null)
            {
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    TreeNode last = null;
                    int count = queue.Count;
                    while (--count >= 0)
                    {
                        last = queue.Dequeue();
                        if (last.left != null)
                            queue.Enqueue(last.left);
                        if (last.right != null)
                            queue.Enqueue(last.right);
                    }

                    res.Add(last.val);
                }
            }

            return res;
        }
    }
}
