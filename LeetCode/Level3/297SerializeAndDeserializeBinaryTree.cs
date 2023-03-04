using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }

    public class Codec
    {
        private const string Delimiter = ";";

        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            // Each node and its children will be serialize as:
            // 1. id@val l id@left r id@right;
            // 2. id@val l id@left;
            // 3. id@val r id@right;
            // 4. id@val;
            // id is unique for each val/node
            if (root != null)
            {
                int id = 1;
                StringBuilder sb = new StringBuilder();
                Queue<(int, TreeNode)> queue = new Queue<(int, TreeNode)>();
                queue.Enqueue((id, root));
                while (queue.Count != 0)
                {
                    var node = queue.Dequeue();
                    if (node.Item2.left != null || node.Item2.right != null)
                    {
                        if (sb.Length != 0)
                            sb.Append(Delimiter);

                        sb.AppendFormat("{0}@{1}", node.Item1, node.Item2.val);

                        if (node.Item2.left != null)
                        {
                            sb.AppendFormat(" l {0}@{1}", ++id, node.Item2.left.val);

                            queue.Enqueue((id, node.Item2.left));
                        }

                        if (node.Item2.right != null)
                        {
                            sb.AppendFormat(" r {0}@{1}", ++id, node.Item2.right.val);

                            queue.Enqueue((id, node.Item2.right));
                        }
                    }
                }

                // single node as root
                if (sb.Length == 0)
                {
                    sb.AppendFormat("1@{0}", root.val);
                }

                return sb.ToString();
            }

            return string.Empty;
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                // Each node and its children will be serialize as:
                // 1. id@val l id@left r id@right;
                // 2. id@val l id@left;
                // 3. id@val r id@right;
                // 4. id@val;
                // id is unique for each val/node
                var lines = data.Split(Delimiter);
                var id2Node = new Dictionary<int, TreeNode>();
                foreach (var line in lines)
                {
                    var parts = line.Split(" ");
                    int index = parts[0].IndexOf("@");
                    int id = int.Parse(parts[0].Substring(0, index));
                    int val = int.Parse(parts[0].Substring(index + 1));
                    TreeNode curRoot = null;
                    if (!id2Node.TryGetValue(id, out curRoot))
                    {
                        curRoot = new TreeNode(val);
                        id2Node.Add(id, curRoot);
                    }

                    if (parts.Length > 1)
                    {
                        index = parts[2].IndexOf("@");
                        id = int.Parse(parts[2].Substring(0, index));
                        val = int.Parse(parts[2].Substring(index + 1));
                        var curChild = new TreeNode(val);
                        id2Node.Add(id, curChild);

                        if (parts[1] == "l")
                        {
                            curRoot.left = curChild;

                            if (parts.Length == 5)
                            {
                                // build right
                                index = parts[4].IndexOf("@");
                                id = int.Parse(parts[4].Substring(0, index));
                                val = int.Parse(parts[4].Substring(index + 1));
                                curChild = new TreeNode(val);
                                id2Node.Add(id, curChild);

                                curRoot.right = curChild;
                            }
                        }
                        else
                        {
                            curRoot.right = curChild;
                        }
                    }
                }

                return id2Node[1];
            }

            return null;
        }
    }
}
