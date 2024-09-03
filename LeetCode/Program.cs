using ConsoleApp117;
using ConsoleApp2;
using ConsoleApp2.Graph;
using ConsoleApp2.Level2;
using ConsoleApp2.Level3;
using ConsoleApp236;
using LC;
using LC.Amazon;
using LC.DP;
using LC.Google;
using LC.Graph;
using LC.Level3;
using LC.LinkedIn;
using LC.Meta;
using LC.SegmentTree;
using LC.Top100Medium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using TreeNode = ConsoleApp2.TreeNode;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var n1 = new TreeNode(0);
            var n2 = new TreeNode(0);
            var n3 = new TreeNode(0);
            n1.left = n2;
            n1.right = n3;

            var n4 = new TreeNode(0);
            n2.left = n4;

            var n5 = new TreeNode(0);
            n3.right = n5;

            var n6 = new TreeNode(0);
            n5.right = n6;

            FindDuplicateSubtrees(n1);
        }

        public static IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
        {
            var cache = new Dictionary<string, TreeNode>();
            var res = new Dictionary<string, TreeNode>();
            InOrder(root, cache, res);
            return res.Select(kv => kv.Value).ToList();
        }

        private static string InOrder(TreeNode root, IDictionary<string, TreeNode> cache, IDictionary<string, TreeNode> res)
        {
            if (root == null)
                return "null";

            string left = string.Concat("(", InOrder(root.left, cache, res), "),", root.val);
            string right = InOrder(root.right, cache, res);
            string inorder = string.Concat(left, ",(", right, ")");
            string key = null;
            if (cache.ContainsKey(left))
            {
                left = key;
            }
            else if(cache.ContainsKey(inorder))
            {
                key = inorder;
            }
            else
            {
                string rootAndRight = string.Concat(root.val, ",(", right, ")");
                if (cache.ContainsKey(rootAndRight))
                    key = rootAndRight;
            }

            if (key != null && !res.ContainsKey(key))
            {
                res.Add(key, root);
            }

            cache[inorder] = root;
            return inorder;
        }
    }
}