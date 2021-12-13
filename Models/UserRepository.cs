using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Test.Context;

namespace Test.Models
{
    public class UserRepository
    {
        private readonly ProjectDBContext projectDBContext = new ProjectDBContext();


        public IQueryable<User> GetUserByEmailandPassword(Login login)
        {
            var regUsers = projectDBContext.Users.Where(data => data.Email.Equals(login.Email) && data.Password.Equals(login.Password));
            return regUsers;
        }
        public List<User> GetUsers()
        {
            return projectDBContext.Users.ToList();
        }
        public User GetUserById(int? userId)
        {
            return projectDBContext.Users.Find(userId);
        }
        public int InsertUser(User user)
        {
            var resPonse = projectDBContext.Users.Add(user);
            projectDBContext.SaveChanges();
            return resPonse.UserId;
        }

        public void UpdateUser(User user)
        {
            User userToUpdate = projectDBContext
                .Users.SingleOrDefault(x => x.UserId == user.UserId);
            userToUpdate.Name = user.Name;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Password = user.Password;
            userToUpdate.ConfirmPassword = user.Password;
            userToUpdate.Role = user.Role;
            projectDBContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            User userToDelete = projectDBContext
                .Users.SingleOrDefault(x => x.UserId == user.UserId);
            projectDBContext.Users.Remove(userToDelete);
            projectDBContext.SaveChanges();
        }
    }
}