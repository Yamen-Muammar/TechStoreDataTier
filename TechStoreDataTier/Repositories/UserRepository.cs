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
            bool isDeleted = false;

            try
            {
                string query = "delete from Users where Id = @id;";

                using (SqlConnection conn = new SqlConnection(DataBaseSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    isDeleted = rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Delete Function <User Repository>" + ex);               
            }
            return isDeleted;
        }

        //Gets

        public User Login(string email,string hasedPassword)
        {
            User user = null;
            try
            {
                string query = "select * from Users where Email = @email and HashedPassword =@hpassword ";
                using (SqlConnection conn = new SqlConnection(DataBaseSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@hpassword", hasedPassword);


                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User((int)reader["Id"], reader["First_Name"].ToString(), reader["Last_Name"].ToString(), reader["PhoneNumber"].ToString(), reader["Email"].ToString(), reader["HashedPassword"].ToString(), (int)reader["Permission"]);
                    }

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Login Function <User Repository>" + ex);
            }

            return user;
        }
        public List<User> GetAllUsers()
        {
            List<User> usersList = new List<User>();
            try
            {
                string query = "select * from Users;";
                using (SqlConnection conn = new SqlConnection(DataBaseSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);                    

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        usersList.Add(new User((int)reader["Id"], reader["First_Name"].ToString(), reader["Last_Name"].ToString(), reader["PhoneNumber"].ToString(), reader["Email"].ToString(), reader["HashedPassword"].ToString(), (int)reader["Permission"]));
                    }

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("***Error in GetAllUsers Function <User Repository>" + ex);
            }

            return usersList;
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
            bool isFound = false;
            try
            {
                string query = "select  Found = 1 from Users where Email = @email;";
                using (SqlConnection conn = new SqlConnection(DataBaseSettings.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", email);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    isFound = reader.HasRows;   

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in IsUserExists Function <User Repository>" + ex);
            }            

            return isFound;
        }

        //Update
        public bool Update(User user)
        {
            bool isUpdated = false; 

            try
            {                
                string query = "update Users set First_Name=@firstName, Last_Name=@lastName, " +
                    "PhoneNumber=@phoneNumber, Email=@email, HashedPassword=@hpassword, Permission=@permission" +
                    " where Id=@id;";

                using (SqlConnection conn = new SqlConnection(DataBaseSettings.ConnectionString))
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstName",user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@hpassword", user.HashedPassword);
                    cmd.Parameters.AddWithValue("@permission", user.Permission);
                    cmd.Parameters.AddWithValue("@id", user.Id);

                    conn.Open();

                    int effectedRows = cmd.ExecuteNonQuery();

                    isUpdated = effectedRows > 0;

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Error in Update Function <User Repository>" + ex);
            }

            return isUpdated;
        }
    }
}
