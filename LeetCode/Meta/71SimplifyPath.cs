using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _71SimplifyPath
    {
        public string SimplifyPath(string path)
        {
            LinkedList<string> res = new LinkedList<string>();
            StringBuilder cur = new StringBuilder();
            int dots = 0, nonDots = 0;
            for (int i = 0; i <= path.Length; ++i)
            {
                char ch = i < path.Length ? path[i] : '/';
                if (ch == '/') // end of folder name
                {
                    if (cur.Length != 0)
                    {
                        if (dots == 2 && res.Count != 0)
                            res.RemoveLast();
                        else if (dots != 1 && dots != 2)
                        {
                            res.AddLast(cur.ToString());
                        }

                        cur.Clear();
                    }

                    nonDots = 0;
                    dots = 0;
                }
                else
                {
                    if (ch != '.')
                    {
                        ++nonDots;
                        dots = 0;
                    }
                    else if (ch == '.' && nonDots == 0)
                        ++dots;

                    cur.Append(ch);
                }
            }

            if (res.Count == 0)
                return "/";

            StringBuilder sb = new StringBuilder();
            foreach (var folder in res)
            {
                sb.Append("/");
                sb.Append(folder);
            }
            return sb.ToString();
        }

        public string SimplifyPathV1(string path)
        {
            var names = path.Split('/');
            var folders = new LinkedList<string>();
            foreach (var name in names)
            {
                if (string.IsNullOrEmpty(name))
                    continue;

                if (name == "..")
                {
                    if (folders.Count != 0)
                        folders.RemoveLast();
                    continue;
                }
                if (name == ".")
                    continue;

                folders.AddLast(name);
            }

            if (folders.Count == 0)
                return "/";

            StringBuilder sb = new StringBuilder();
            foreach (var folder in folders)
            {
                sb.Append("/");
                sb.Append(folder);
            }
            return sb.ToString();
        }
    }
}
