using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp108
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

    public class _102BinaryTreeLevelOrderTraverse
    {
        public IList<IList<int>> LevelOrderV2(TreeNode root)
        {
            var res = new List<IList<int>>();
            if (root != null)
            {
                var level = new List<int>();
                int currentLevelNodes = 1, nextLevelNodes = 0;
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    level.Add(node.val);

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                        ++nextLevelNodes;
                    }
                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                        ++nextLevelNodes;
                    }

                    if (--currentLevelNodes == 0)
                    {
                        // processed all nodes in the current level
                        res.Add(level);
                        level = new List<int>();

                        // set the number of nodes of next level to current level
                        currentLevelNodes = nextLevelNodes;
                        nextLevelNodes = 0;
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> LevelOrderV1(TreeNode root)
        {
            var results = new List<IList<int>>();
            if (root != null)
            {
                IList<int> levelResult = null;
                Queue<TreeNode> queue = new Queue<TreeNode>();
                Queue<TreeNode> queueNextLevel = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    levelResult = new List<int>(queue.Count);
                    while (queue.Count > 0)
                    {
                        TreeNode node = queue.Dequeue();
                        levelResult.Add(node.val);

                        if (node.left != null)
                        {
                            queueNextLevel.Enqueue(node.left);
                        }
                        if (node.right != null)
                        {
                            queueNextLevel.Enqueue(node.right);
                        }
                    }

                    results.Add(levelResult);
                    var temp = queue;
                    queue = queueNextLevel;
                    queueNextLevel = temp;
                }
            }

            return results;
        }
    }
}
