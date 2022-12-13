using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {

        private readonly IDictionary<string,ISet<TUser>> _circles;
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            _circles = new Dictionary<string,ISet<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if(!_circles.ContainsKey(group))
            {
                var set = new HashSet<TUser>();
                set.Add(user);
                _circles[group] = set;
                return true;
            }
            else
            {
                return _circles[group].Add(user);

            }
            //throw new NotImplementedException("TODO add user to the provided group. Return false if the user was already in the group");
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                var setAllFolloewrs = new HashSet<TUser>();

                foreach(var x in _circles.Values)
                {
                    foreach(var y in x)
                    {
                        setAllFolloewrs.Add(y);
                    }
                }
                return new List<TUser>(setAllFolloewrs);
                //throw new NotImplementedException("TODO construct and return the list of all users followed by the current users, in all groups");
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if(_circles.ContainsKey(group))
            {
                return new HashSet<TUser>(_circles[group]);
            }
            else
            {
                return new HashSet<TUser>();
            }
            //throw new NotImplementedException("TODO construct and return a collection containing of all users followed by the current users, in group");
        }
    }
}
