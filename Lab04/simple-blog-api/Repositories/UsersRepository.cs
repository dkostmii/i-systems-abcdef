using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using simple_blog_api.Models;

#nullable enable

namespace simple_blog_api
{
    public class UsersRepository
    {
        private List<User> users;
        private int nextId;

        public UsersRepository(List<User> users)
        {
            this.users = users;
        }

        private void SetNextId()
        {
            this.nextId = users.Count;
        }

        public List<User> GetAllUsers() => users;
        public User? GetUserById(int id) => users.Find(item => item.Id == id);

        public User? InsertUser(User user)
        {
            user.Id = nextId;
            SetNextId();
            users.Add(user);
            return user;
        }

        public void DeleteUser(int id) => users = users.Where(record => record.Id != id).ToList();
    }
}
