using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MounjangFoodTalk.DomainModels;

namespace MounjangFoodTalk.Repositories
{

    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int uid);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string Email, string Password);

        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();
    }
    public class UsersRepository : IUsersRepository
    {

        private readonly MounjangFoodTalkDatabaseDbContext _mounjangFoodTalkDatabaseDbContext;

        public UsersRepository()
        {
            _mounjangFoodTalkDatabaseDbContext = new MounjangFoodTalkDatabaseDbContext();
        }





        public void InsertUser(User u)
        {
            _mounjangFoodTalkDatabaseDbContext.Users.Add(u);
            _mounjangFoodTalkDatabaseDbContext.SaveChanges();
        }

        public void UpdateUserDetails(User u)
        {
            User us = _mounjangFoodTalkDatabaseDbContext.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (us != null)
            {
                us.Name = u.Name;
                us.Mobile = u.Mobile;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();
            }
        }

        public void UpdateUserPassword(User u)
        {
            User us = _mounjangFoodTalkDatabaseDbContext.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (us != null)
            {
                us.PasswordHash = u.PasswordHash;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();
            }
        }

        public void DeleteUser(int uid)
        {
            User us = _mounjangFoodTalkDatabaseDbContext.Users.Where(temp => temp.UserID == uid).FirstOrDefault();
            if (us != null)
            {
                _mounjangFoodTalkDatabaseDbContext.Users.Remove(us);
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> us = _mounjangFoodTalkDatabaseDbContext.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();
            return us;
        }

        public List<User> GetUsersByEmailAndPassword(string Email, string PasswordHash)
        {
            List<User> us = _mounjangFoodTalkDatabaseDbContext.Users.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).ToList();
            return us;
        }


        public List<User> GetUsersByEmail(string Email)
        {
            List<User> us = _mounjangFoodTalkDatabaseDbContext.Users.Where(temp => temp.Email == Email).ToList();
            return us;
        }

        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> us = _mounjangFoodTalkDatabaseDbContext.Users.Where(temp => temp.UserID == UserID).ToList();
            return us;
        }



        public int GetLatestUserID()
        {
            int uid = _mounjangFoodTalkDatabaseDbContext.Users.Select(temp => temp.UserID).Max();
            return uid;
        }

    }
}
