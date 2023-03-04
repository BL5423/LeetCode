using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _721AccountsMerge
    {
        private int[] representives;

        private int[] groupsSize;

        private int GetRepresentive(int representive)
        {
            int parentRepresentive = this.representives[representive];
            if (parentRepresentive == representive)
                return parentRepresentive;

            // path compression
            return this.representives[representive] = this.GetRepresentive(parentRepresentive);
        }

        private void Union(int representive1, int representive2)
        {
            int parentRepresentive1 = this.GetRepresentive(representive1);
            int parentRepresentive2 = this.GetRepresentive(representive2);
            if (parentRepresentive1 == parentRepresentive2)
                return;

            if (this.groupsSize[parentRepresentive1] >= this.groupsSize[parentRepresentive2])
            {
                this.groupsSize[parentRepresentive1] += this.groupsSize[parentRepresentive2];
                this.representives[parentRepresentive2] = parentRepresentive1;
            }
            else
            {
                this.groupsSize[parentRepresentive2] += this.groupsSize[parentRepresentive1];
                this.representives[parentRepresentive1] = parentRepresentive2;
            }
        }

        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            this.representives = new int[accounts.Count];
            this.groupsSize = new int[accounts.Count];
            int index = 0;
            foreach(var account in accounts)
            {
                // init representives and group size
                this.representives[index] = index;
                this.groupsSize[index] = account.Count;
                ++index;
            }

            Dictionary<string, int> emailToRepresentive = new Dictionary<string, int>();
            index = 0;
            foreach(var account in accounts)
            {
                foreach(var email in account.Skip(1)) // do not consider user name(first element)
                {
                    if (!emailToRepresentive.TryGetValue(email, out int representive))
                    {
                        emailToRepresentive.Add(email, index);
                    }
                    else
                    {
                        // if email has appeared before, then union the two groups since they belong to same user
                        this.Union(index, representive);
                    }
                }

                ++index;
            }

            Dictionary<int, List<string>> representiveToEmails = new Dictionary<int, List<string>>();
            foreach(var kv in emailToRepresentive)
            {
                var email = kv.Key;

                // explicitly call GetRepresentive to get the representive instead of the representive[] since the former will recursively get the parent representive
                // consider the below hierarchy:
                //               Root
                //               / \
                //              A   B
                //                 / \
                //                C   D
                // during the Union process, representives A, B, C, D are union-ed in below order:
                // C -> B
                // D -> B
                // A and B -> Root
                // The last union won't update representives(root node) of C and D, so representive[C/D] still points to B.
                var representive = this.GetRepresentive(kv.Value); 
                if (!representiveToEmails.TryGetValue(representive, out List<string> list))
                {
                    list = new List<string>();
                    representiveToEmails.Add(representive, list);
                }

                list.Add(email);
            }

            var res = new List<IList<string>>(representiveToEmails.Count);
            foreach(var kv in representiveToEmails)
            {
                var list = new List<string>(kv.Value.Count + 1);
                list.Add(accounts[kv.Key].First()); // add user name
                kv.Value.Sort((a, b) => string.CompareOrdinal(a, b));
                list.AddRange(kv.Value);

                res.Add(list);
            }

            return res;
        }
    }

    public class _721AccountsMergeV1
    {
        private int GetAcountGroupId(int accountGroup, IList<int> accountGroups)
        {
            var groupId = accountGroups[accountGroup];
            if (groupId == accountGroup)
            {
                return groupId;
            }

            // compression
            return accountGroups[accountGroup] = GetGroupId(groupId, accountGroups);
        }

        private void UnionAccount(int accountIndex1, int accountIndex2, IList<int> accountGroups, IList<int> accountSize)
        {
            var groupId1 = this.GetGroupId(accountIndex1, accountGroups);
            var groupId2 = this.GetGroupId(accountIndex2, accountGroups);
            if (groupId1 == groupId2)
                return;

            var accountSize1 = accountSize[groupId1];
            var accountSize2 = accountSize[groupId2];
            if (accountSize1 > accountSize2)
            {
                // set email2's user to user1
                accountGroups[groupId2] = groupId1;

                // merge user2's emails into user1's
                accountSize[groupId1] += accountSize2;
            }
            else
            {
                // set email1's user to user2
                accountGroups[groupId1] = groupId2;

                // merge user1's emails into user2's
                accountSize[groupId2] += accountSize1;
            }
        }

        private bool DoesEmailOverlap(HashSet<string> emails1, HashSet<string> emails2)
        {
            foreach(var email in emails1)
                if (emails2.Contains(email))
                    return true;

            return false;
        }

        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            int[] accountGroups = new int[accounts.Count];
            int[] accountSize = new int[accounts.Count];
            HashSet<string>[] accountEmails = new HashSet<string>[accounts.Count];
            for (int i = 0; i < accounts.Count; ++i)
            {
                accountGroups[i] = i;
                accountSize[i] = accounts[i].Count - 1;
                accountEmails[i] = new HashSet<string>();
                foreach(var email in accounts[i].Skip(1))
                {
                    accountEmails[i].Add(email);
                }
            }

            for (int i = 0; i < accountGroups.Length; ++i)
            {
                var account1 = accountEmails[i];
                for (int j = i + 1; j < accountGroups.Length; ++j)
                {
                    var account2 = accountEmails[j];
                    if (this.DoesEmailOverlap(account1, account2))
                        this.UnionAccount(i, j, accountGroups, accountSize);
                }
            }

            HashSet<int> processedGroupIds = new HashSet<int>();
            var res = new List<IList<string>>();
            for (int i = 0; i < accountGroups.Length; ++i)
            {
                int targetGroupId = this.GetGroupId(i, accountGroups);
                if (processedGroupIds.Add(targetGroupId))
                {
                    var list = new HashSet<string>();
                    var emails = accounts[i];
                    for (int e = 1; e < emails.Count; ++e)
                    {
                        list.Add(emails[e]);
                    }

                    for (int j = i + 1; j < accountGroups.Length; ++j)
                    {
                        var groupId = this.GetGroupId(j, accountGroups);
                        if (groupId == targetGroupId)
                        {
                            emails = accounts[j];
                            for (int e = 1; e < emails.Count; ++e)
                            {
                                list.Add(emails[e]);
                            }
                        }
                    }

                    var output = list.ToList();
                    output.Sort((a, b) => string.CompareOrdinal(a, b));
                    output.Insert(0, emails.First());
                    res.Add(output);
                }
            }
            return res;
        }

        private int GetGroupId(int emailGroup, IList<int> emailGroups)
        {
            var groupId = emailGroups[emailGroup];
            if (groupId == emailGroup)
            {
                return groupId;
            }

            // compression
            return emailGroups[emailGroup] = GetGroupId(groupId, emailGroups);
        }

        private void Union(int emailIndex1, int emailIndex2, IList<int> groupSize, IList<int> emailGroups)
        {
            var groupId1 = this.GetGroupId(emailIndex1, emailGroups);
            var groupId2 = this.GetGroupId(emailIndex2, emailGroups);
            if (groupId1 == groupId2)
                return;

            var accountSize1 = groupSize[groupId1];
            var accountSize2 = groupSize[groupId2];
            if (accountSize1 > accountSize2)
            {
                // set email2's user to user1
                emailGroups[groupId2] = groupId1;

                // merge user2's emails into user1's
                groupSize[groupId1] += accountSize2;
            }
            else
            {
                // set email1's user to user2
                emailGroups[groupId1] = groupId2;

                // merge user1's emails into user2's
                groupSize[groupId2] += accountSize1;
            }
        }

        public IList<IList<string>> AccountsMergeV2(IList<IList<string>> accounts)
        {
            var emails = new List<string>();
            for (int i = 0; i < accounts.Count; ++i)
            {
                var account = accounts[i];
                var size = account.Count - 1;
                emails.AddRange(account.Skip(1));
            }

            int[] emailGroups = new int[emails.Count];
            int[] emailAccounts = new int[emails.Count];
            int[] emailGroupSize = new int[emails.Count];
            int groupId = 0, offset = 0;
            for (int i = 0; i < accounts.Count; ++i)
            {
                var account = accounts[i];
                var size = account.Count - 1;
                for (int j = 0; j < size; ++j)
                {
                    emailGroups[j + offset] = offset;
                    emailAccounts[j + offset] = groupId;
                    emailGroupSize[j + offset] = size;
                }

                offset += size;
                ++groupId;
                emails.AddRange(account.Skip(1));
            }

            for (int i = 0; i < emailGroups.Length; ++i)
            {
                var email1 = emails[i];
                var account1 = accounts[emailAccounts[i]];
                for (int j = i + 1; j < emailGroups.Length; ++j)
                {
                    var email2 = emails[j];
                    var account2 = accounts[emailAccounts[j]];
                    if (account1 != account2 && (account1.Contains(email2) || account2.Contains(email1)))
                        this.Union(i, j, emailGroupSize, emailGroups);
                }
            }

            HashSet<int> processedGroupIds = new HashSet<int>();
            var res = new List<IList<string>>();
            for(int i = 0; i < emailGroups.Length; ++i)
            {
                int targetGroupId = this.GetGroupId(i, emailGroups);
                if (processedGroupIds.Add(targetGroupId))
                {
                    var list = new HashSet<string>();
                    list.Add(emails[i]);
                    for (int j = i + 1; j < emailGroups.Length; ++j)
                    {
                        if (this.GetGroupId(j, emailGroups) == targetGroupId)
                            list.Add(emails[j]);
                    }

                    var output = list.ToList(); 
                    output.Sort((a, b) => string.CompareOrdinal(a, b));
                    output.Insert(0, accounts[emailAccounts[targetGroupId]].First());
                    res.Add(output);
                }
            }
            return res;
        }

        public class User
        {
            public string name;

            public HashSet<string> emails;

            public User(IList<string> account)
            {
                this.name = account.First();
                this.emails = new HashSet<string>(account.Count);                
                for(int i = 1; i < account.Count; ++i)
                {
                    this.emails.Add(account[i]);
                }
            }
        }

        public IList<IList<string>> AccountsMergeV1(IList<IList<string>> accounts)
        {
            var users = new HashSet<User>();
            foreach(var account in accounts)
            {
                users.Add(new User(account));
            }

            var results = new List<IList<string>>();
            while (users.Count > 0)
            {
                var user = users.First();
                var sameUsers = this.DFS(user, users);
                var result = new HashSet<string>();
                foreach(var sameUser in sameUsers)
                {
                    foreach(var email in sameUser.emails)
                        result.Add(email); // dedup emails
                }

                var list = result.ToList();
                list.Sort((a, b) => string.CompareOrdinal(a, b));
                list.Insert(0, user.name);
                results.Add(list);
            }

            return results;
        }

        private LinkedList<User> DFS(User firstUser, HashSet<User> users)
        {
            LinkedList<User> sameUser = new LinkedList<User>();
            Stack<User> stack = new Stack<User>();
            stack.Push(firstUser);
            users.Remove(firstUser);
            while (stack.Count > 0)
            {
                var user = stack.Pop();
                sameUser.AddLast(user);
                foreach (var email in user.emails)
                {
                    var potentialUsers = new LinkedList<User>();
                    foreach (var potentialUser in users)
                    {
                        if (potentialUser.emails.Contains(email))
                        {
                            potentialUsers.AddLast(potentialUser);
                        }
                    }

                    foreach(var nextUser in potentialUsers)
                    {
                        users.Remove(nextUser); // avoid cycles
                        stack.Push(nextUser);
                    }
                }
            }

            return sameUser;
        }
    }
}
