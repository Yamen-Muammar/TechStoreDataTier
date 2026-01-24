using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreApp_Core_.Models;
using TechStoreDataTier.Repositories;




namespace TechStoreApp__Business_Data_Tier_.Services
{   
    public static class UserServices
    {                        
        public static User Find(int ID)
        {
            UserRepository _userRepo = new UserRepository();

            User findedUser = _userRepo.GetUserById(ID);

            if (findedUser == null)
            { return  null; }

            return findedUser;
        }

        public static User Find(string email , string hashedPassword)
        {
            UserRepository _userRepo = new UserRepository();

            User findedUser = _userRepo.Login(email,hashedPassword);

            if (findedUser == null) { return null; }

            return findedUser;
        }
        public static List<User> GetUsersList()
        {
            UserRepository _userRepo = new UserRepository();
            return _userRepo.GetAllUsers();
        }

        public static bool AddUser(User user)
        {
            UserRepository _userRepo = new UserRepository();

            if (!_userDataValidation(user))
            {
                return false;
            }

            if (_userRepo.IsUserExists(user.Email))
            {               
                return false;
            }

            if (_userRepo.Add(user) == -1)
            {
                return false;
            }
            else
            {               
                return true;
            }
        }

        public static bool UpdateUser(User user)
        {
            UserRepository _userRepo = new UserRepository();

            if (!_userDataValidation(user))
            {
                return false;
            }

            return _userRepo.Update(user);
        }

        
        public static bool Delete(int id)
        {
            UserRepository _userRepo = new UserRepository();

            User userToDelete = _userRepo.GetUserById(id);

            if (userToDelete == null )
            {
                return false;

            }
            
            if (_userRepo.Delete(id))
            {
                userToDelete = null;
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public static bool IsUserExists(string email)
        {            
            UserRepository _userRepo = new UserRepository();

            return _userRepo.IsUserExists(email);
        }
        private static bool _userDataValidation(User user)
        {
            if(user == null)
            {
                return false;
            }

            if (!user.Email.Contains("@") || string.IsNullOrWhiteSpace(user.Email))
            {
                Debug.WriteLine("**Error >> Invalid Email**");
                return false;
            }

            if (user.Permission < -1)
            {
                return false;
            }

            return true;
        }
    }
}
