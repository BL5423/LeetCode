using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _165CompareVersionNumbers
    {
        public int CompareVersion(string version1, string version2)
        {
            int index1 = 0, index2 = 0;
            while (index1 < version1.Length || index2 < version2.Length)
            {
                int revision1Int = GetNextRevision(version1, ref index1);
                int revision2Int = GetNextRevision(version2, ref index2);
                int compare = revision1Int.CompareTo(revision2Int);
                if (compare != 0)
                    return compare;
            }

            return 0;
        }

        private static int GetNextRevision(string version, ref int startIndex)
        {
            if (startIndex >= version.Length)
                return 0;

            StringBuilder sb = new StringBuilder();
            while (startIndex < version.Length)
            {
                char c = version[startIndex++];
                if (c != '.')
                    sb.Append(c);
                else
                    break;
            }

            if (sb.Length == 0)
                return 0;

            return int.Parse(sb.ToString());
        }

        public int CompareVersionV1(string version1, string version2)
        {
            string[] parts1 = version1.Split('.');
            string[] parts2 = version2.Split('.');

            int index = 0;
            while (index < parts1.Length || index < parts2.Length)
            {
                string revision1 = index < parts1.Length ? parts1[index] : "0";
                string revision2 = index < parts2.Length ? parts2[index] : "0";
                int revision1Int = int.Parse(revision1);
                int revision2Int = int.Parse(revision2);
                int compare = revision1Int.CompareTo(revision2Int);
                if (compare != 0)
                    return compare;

                ++index;
            }

            return 0;
        }
    }
}
