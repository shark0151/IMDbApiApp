using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDbApiLib;
using IMDbApiLib.Models;
using imdbClientButWorking.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace imdbClientButWorking
{
    public class DatabaseController
    {
        private string urlLink = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        private IMDbApiLib.ApiLib imdApiLib = new ApiLib("k_9o16s5zl");
        public int UserId = 0;
        private String sqlDatoToJson(SqlDataReader dataReader)
        {
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dataTable);
            return JSONString;
        }
        #region Users
        public async Task<List<TitleData>> GetFavListAsync(int userid)
        {
            string query = "Select movie_id From [favourite_list] where user_id = @id";
            List<TitleData> mylist = new List<TitleData>();
            List<string> movie_id_list = new List<string>();
            using (SqlConnection conn = new SqlConnection(urlLink))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@id", userid);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        string newmovieid = reader.GetString(0);
                        movie_id_list.Add(newmovieid);
                        
                        /*string jsonString = sqlDatoToJson(reader);
                        TitleData newtitle = JsonSerializer.Deserialize<TitleData>(jsonString)!;
                        mylist.Add(newtitle);*/
                    }
                    catch
                    {
                        
                    }

                }
            }

            foreach (var VARIABLE in movie_id_list)
            {
                TitleData x = new TitleData();
                x = await GetMovieFromImdbTask(VARIABLE);
                mylist.Add(x);
            }
            return mylist;
        }

        public async Task<bool> CheckUserExists(string username)
        {
            string query = "Select * From [Users] where username = @username";
            List<string> mylist = new List<string>();
            using (SqlConnection conn = new SqlConnection(urlLink))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        string existinguser = reader.GetInt32(0).ToString();
                        mylist.Add(existinguser);

                        /*string jsonString = sqlDatoToJson(reader);
                        TitleData newtitle = JsonSerializer.Deserialize<TitleData>(jsonString)!;
                        mylist.Add(newtitle);*/
                    }
                    catch
                    {

                    }

                }
            }

            return mylist.Count != 0;
        }

        public async Task<bool> CreateUser(User newUser)
        {
            string query = "insert into Users(username, password) values( @username, @password)";
            List<string> mylist = new List<string>();
            int rowsAffected;
            using (SqlConnection conn = new SqlConnection(urlLink))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", newUser.username);
                command.Parameters.AddWithValue("@password", newUser.password);
                rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");
            }

            return rowsAffected > 0;
        }

        public async Task<bool> CheckUserCredentials(User user)
        {
            User user1 = new User();
            string query = "Select * From [Users] where username = @username";
            List<string> mylist = new List<string>();
            using (SqlConnection conn = new SqlConnection(urlLink))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", user.username);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        UserId = reader.GetInt32(0);
                        user1.user_id = UserId.ToString();
                        user1.username = reader.GetString(1);
                        user1.password = reader.GetString(2);
                    }
                    catch
                    {

                    }

                }
            }

            return user.password == user1.password && user.username == user1.username;
        }

        #endregion Users

        #region IMDB

        public async Task<TitleData> GetMovieFromImdbTask(string movieid)
        {
            return await imdApiLib.TitleAsync(movieid);
        }
        public async Task<SearchData> SearchImdbTask(string exp)
        {
            return await imdApiLib.SearchTitleAsync(exp);
        }
        #endregion IMDB
    }
}
