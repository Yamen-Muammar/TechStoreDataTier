using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreApp_Core_.Models;
using TechStoreDataTier.Repositories;




namespace TechStoreApp__Business_Data_Tier_.Services
{   
    public class UserServices
    {        
        public User user;

        private enum enMode
        {
            eAdd = 1 , eUpdate = 2 
        }

        private enMode mode;     
        
        public UserServices(int? id , string firstName , string lastName , string phoneNumber , string email , string hashedPassword , int permission)
        {           
            user = new User(id,firstName,lastName,phoneNumber,email,hashedPassword,permission);

            mode = (id == null) ?enMode.eAdd: enMode.eUpdate;           
        }

        public static UserServices Find(int ID)
        {
            UserRepository _userRepo = new UserRepository();

            User tempUser = _userRepo.GetUserById(ID);

            if (tempUser == null)
            { return  null; }

            return new UserServices(tempUser.Id,tempUser.FirstName,tempUser.LastName,tempUser.PhoneNumber,tempUser.Email,tempUser.HashedPassword,tempUser.Permission);
        }

        public static UserServices Find(string email , string hashedPassword)
        {
            UserRepository _userRepo = new UserRepository();
            User tempUser = _userRepo.Login(email,hashedPassword);

            if (tempUser == null) { return null; }

            return new UserServices(tempUser.Id, tempUser.FirstName, tempUser.LastName, tempUser.PhoneNumber, tempUser.Email, tempUser.HashedPassword, tempUser.Permission);
        }
        public static List<User> GetUsersList()
        {
            UserRepository _userRepo = new UserRepository();
            return _userRepo.GetAllUsers();
        }

        private bool _add()
        {
            UserRepository _userRepo = new UserRepository();

            if (!userDataValidation())
            {
                return false;
            }

            if (_userRepo.IsUserExists(this.user.Email))
            {
                return false;
            }

            if (_userRepo.Add(this.user) == -1)
            {
                return false;
            }
            else
            {
                mode = enMode.eUpdate;
                return true;
            }
        }

        private bool _update()
        {
            UserRepository _userRepo = new UserRepository();

            if (!userDataValidation())
            {
                return false;
            }

            return _userRepo.Update(user);
        }

        public bool Save()
        {
            switch (this.mode)
            {
                case enMode.eAdd:
                    if (_add())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.eUpdate:

                    if (_update())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }
        }


        public static bool Delete(int id)
        {
            UserRepository _userRepo = new UserRepository();

            if (_userRepo.GetUserById(id) == null )
            {
                return false;

            }
            
            if (_userRepo.Delete(id))
            {               
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
        private bool userDataValidation()
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

            return true;
        }
    }
}
