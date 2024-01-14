using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _297SerializeandDeserializeBinaryTree
    {    // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            if (root != null)
            {
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count != 0)
                {
                    var node = queue.Dequeue();
                    if (node != null)
                    {
                        sb.AppendFormat("{0},", node.val);
                        queue.Enqueue(node.left);
                        queue.Enqueue(node.right);
                    }
                    else
                        sb.Append("|,");
                }
            }

            return sb.ToString();
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            TreeNode root = null;
            if (!string.IsNullOrEmpty(data))
            {
                //"1,2,3,|,|,4,5,"
                string[] nodes = data.Split(',');
                Queue<TreeNode> queue = new Queue<TreeNode>();
                root = new TreeNode(int.Parse(nodes[0]));
                queue.Enqueue(root);
                for (int index = 1; index < nodes.Length - 2; index += 2)
                {
                    var leftStr = nodes[index];
                    var rightStr = nodes[index + 1];
                    var parent = queue.Dequeue();
                    if (leftStr != "|")
                    {
                        parent.left = new TreeNode(int.Parse(leftStr));
                        queue.Enqueue(parent.left);
                    }
                    if (rightStr != "|")
                    {
                        parent.right = new TreeNode(int.Parse(rightStr));
                        queue.Enqueue(parent.right);
                    }
                }
            }

            return root;
        }
    }
}
