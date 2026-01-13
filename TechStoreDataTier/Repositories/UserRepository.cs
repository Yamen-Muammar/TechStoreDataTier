using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreApp_Core_.Models;
using TechStoreDataTier.Interfaces;

namespace TechStoreDataTier.Repositories
{
    public class UserRepository : IUserRepository
    {
        // Insert
        public int Add(User user)
        {
            throw new NotImplementedException();
        }
        
        // Delete
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        //Gets

        public User Login(string email,string hasedPassword)
        {
            throw new NotImplementedException();
        }
        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            User user = null;
            try
            {
                string query = "select * from Users where Id = @id";
                using (SqlConnection conn = new SqlConnection(DataBaseSettings.ConnectionString))                
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id",id);

                    conn.Open();

                    SqlDataReader reader =  cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User((int)reader["Id"], reader["First_Name"].ToString(), reader["Last_Name"].ToString(), reader["PhoneNumber"].ToString(), reader["Email"].ToString(), reader["HashedPassword"].ToString(), (int)reader["Permission"]);
                    }

                    reader.Close();

                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetUser by Id Function <User Repository>" + ex);                
            }

            return user;
        }

        public bool IsUserExists(string email)
        {
            throw new NotImplementedException();
        }

        //Update
        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
