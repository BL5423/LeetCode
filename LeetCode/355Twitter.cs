using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Twitter
    {
        const int T = 10;

        public Dictionary<int, User> users;

        public Twitter()
        {
            this.users = new Dictionary<int, User>();
        }

        public void PostTweet(int userId, int tweetId)
        {
            if (!this.users.TryGetValue(userId, out User user))
            {
                user = new User(userId);
                this.users.Add(userId, user);
            }

            user.tweets.AddFirst(new Tweet(tweetId));
        }

        public IList<int> GetNewsFeed(int userId)
        {
            IList<int> feeds = new List<int>(0);
            if (this.users.TryGetValue(userId, out User user))
            {
                var minHeap = new MinHeap<Tweet>(T);
                var list = new LinkedList<User>();
                list.AddLast(user);
                foreach(var followee in user.followees)
                {
                    list.AddLast(this.users[followee]);
                }

                foreach(var u in list)
                {
                    int num = 0;
                    var latestTweets = u.tweets.GetEnumerator();
                    while (latestTweets.MoveNext() && num++ < T)
                    {
                        minHeap.Push(latestTweets.Current);
                    }
                }

                var temp = new LinkedList<int>();
                while (minHeap.Size() > 0)
                {
                    temp.AddFirst(minHeap.Pop().Id);
                }
                feeds = temp.ToList();
            }

            return feeds;
        }

        public void Follow(int followerId, int followeeId)
        {
            if (!this.users.TryGetValue(followerId, out User user))
            {
                user = new User(followerId);
                this.users.Add(followerId, user);
            }
            if (!this.users.TryGetValue(followeeId, out User user1))
            {
                user1 = new User(followeeId);
                this.users.Add(followeeId, user1);
            }

            user.followees.Add(followeeId);
        }

        public void Unfollow(int followerId, int followeeId)
        {
            if (!this.users.TryGetValue(followerId, out User user))
            {
                user = new User(followerId);
                this.users.Add(followerId, user);
            }
            if (!this.users.TryGetValue(followeeId, out User user1))
            {
                user1 = new User(followeeId);
                this.users.Add(followeeId, user1);
            }

            user.followees.Remove(followeeId);
        }
    }

    public class User
    {
        public int Id { get; private set; }

        public LinkedList<Tweet> tweets;

        public HashSet<int> followees;

        public User(int id)
        {
            this.Id = id;
            this.tweets = new LinkedList<Tweet>();
            this.followees = new HashSet<int>();
        }
    }

    public class TwitterV2
    {
        const int T = 10;

        public Dictionary<int, UserV2> users;

        public TwitterV2()
        {
            this.users = new Dictionary<int, UserV2>();
        }

        public void PostTweet(int userId, int tweetId)
        {
            if (!this.users.TryGetValue(userId, out UserV2 user))
            {
                user = new UserV2(userId);
                this.users.Add(userId, user);
            }

            var tweet = new TweetV2(tweetId, userId);
            user.Feeds.Push(tweet);
            foreach(var follower in user.Followers)
            {
                this.users[follower].Feeds.Push(tweet);
            }
        }

        public IList<int> GetNewsFeed(int userId)
        {
            IList<int> feeds = new List<int>(0);
            if (this.users.TryGetValue(userId, out UserV2 user))
            {
                var list = new LinkedList<int>();
                var userFeeds = user.Feeds.GetEnumerator();
                int num = 0;
                while (userFeeds.MoveNext() && num++ < T)
                {
                    if (userFeeds.Current.UserId == user.Id || user.Followees.Contains(userFeeds.Current.UserId))
                    {
                        list.AddFirst(userFeeds.Current.Id);
                    }
                }

                feeds = list.ToList();
            }

            return feeds;
        }

        public void Follow(int followerId, int followeeId)
        {
            if (!this.users.TryGetValue(followerId, out UserV2 follower))
            {
                follower = new UserV2(followerId);
                this.users.Add(followerId, follower);
            }
            if (!this.users.TryGetValue(followeeId, out UserV2 followee))
            {
                followee = new UserV2(followeeId);
                this.users.Add(followeeId, followee);
            }

            follower.Followees.Add(followeeId);
            followee.Followers.Add(followerId);

            var userFeeds = followee.Feeds.GetEnumerator();
            int num = 0;
            while (userFeeds.MoveNext() && num++ < T)
            {
            }
        }

        public void Unfollow(int followerId, int followeeId)
        {
            if (!this.users.TryGetValue(followerId, out UserV2 follower))
            {
                follower = new UserV2(followerId);
                this.users.Add(followerId, follower);
            }
            if (!this.users.TryGetValue(followeeId, out UserV2 followee))
            {
                followee = new UserV2(followeeId);
                this.users.Add(followeeId, followee);
            }

            follower.Followees.Remove(followeeId);
            followee.Followers.Remove(followerId);
        }
    }

    public class UserV2
    {
        const int T = 10;

        public int Id { get; private set; }

        public Stack<TweetV2> Feeds { get; private set; }

        public HashSet<int> Followees { get; private set; }

        public HashSet<int> Followers { get; private set; }

        public UserV2(int id)
        {
            this.Id = id;
            this.Feeds = new Stack<TweetV2>();
            this.Followees = new HashSet<int>();
            this.Followers = new HashSet<int>();
        }
    }

    public class TweetV2
    {
        public int Id { get; private set; }

        public int UserId { get; private set; }

        public TweetV2(int id, int userId)
        {
            this.Id = id;
            this.UserId = userId;
        }
    }


    public class Tweet : IComparable<Tweet>
    {
        public int Id { get; private set; }

        long ticks;

        public Tweet(int id)
        {
            this.Id = id;
            this.ticks = DateTime.Now.Ticks;    
        }

        public int CompareTo(Tweet other)
        {
            return this.ticks >= other.ticks ? 1 : -1;
        }
    }
}
