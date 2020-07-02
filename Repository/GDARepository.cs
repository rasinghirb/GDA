using GDA.Data;
using GDA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GDA.Repository
{

    public class GDARepository
    {
        string connectionString = ConnectionString._DCS;
        private string message = string.Empty;
        [ViewData]
        public string Title { get; set; }
        //get userlist by joining various table
        public IEnumerable<ViewAllUsers> GetAllUsers()
        {
            List<ViewAllUsers> UserList = new List<ViewAllUsers>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SpTbleUserView", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ViewAllUsers user = new ViewAllUsers();
                    user.PISNo = Convert.ToInt32(rdr["PISNo"]);
                    user.Name = rdr["Name"].ToString();
                    user.CoyName = rdr["CoyName"].ToString();
                    user.RankName = rdr["RankName"].ToString();

                    UserList.Add(user);
                }
                con.Close();

            }
            return UserList;

        }
        //To Add new User record    
        public void AddNewUser(UserModel user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SptbleUserInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@Password", user.UserPassword);
                cmd.Parameters.AddWithValue("@UserRole", user.UserRole);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        //To Update the records of a particluar employee  
        public void UpdateUser(UserModel user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SptbleUserUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@Password", user.UserPassword);
                cmd.Parameters.AddWithValue("@UserRole", user.UserRole);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public IEnumerable<UnitModel> GetUnitList()
        {
            List<UnitModel> unitList = new List<UnitModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tbleUnit"))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        adp.SelectCommand = cmd;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            UnitModel unit = new UnitModel();
                            unit.UnitName = dr["UnitName"].ToString();
                            unitList.Add(unit);
                        }
                    }
                    return unitList;
                }
                {

                }
            }
        }
        //Get the details of a particular User by ID 
        public UserModel GetUserById(int? id)
        {
            UserModel user = new UserModel();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SptbleUserFind", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    user.UserId = Convert.ToInt32(rdr["UserId"]);
                    user.UserPassword = rdr["UserPassword"].ToString();
                    user.UserRole = Convert.ToInt32(rdr["UserRole"]);
                    user.Email = rdr["Email"].ToString();

                }
            }
            return user;
        }
        //Get the details of a particular User by UserID  
        public UserModel GetUserByUserId(string id)
        {
            UserModel user = new UserModel();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SptbleUserFindbyuserId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.UserId = Convert.ToInt32(rdr["UserId"]);
                    user.UserPassword = rdr["UserPassword"].ToString();
                    user.UserRole = Convert.ToInt32(rdr["UserRole"]);
                    user.Email = rdr["Email"].ToString();
                                    }
            }
            return user;
        }
        //To Delete the record on a particular employee  
        public void DeleteUser(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SptbleUserDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public DataTable GetSPDataTable(string spName)
        {
            DataTable retVal = new DataTable();
            //string _connectionString = "Enter your connection string here";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = spName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    try
                    {
                        conn.Open();
                        adp.Fill(retVal);
                    }
                    catch /*(Exception e)*/
                    {
                        //ex = e;
                    }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
            return retVal;
        }

    }
}
