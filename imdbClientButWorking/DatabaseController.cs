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
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace imdbClientButWorking
{
    public class DatabaseController
    {
        private string urlLink = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        private IMDbApiLib.ApiLib imdApiLib = new ApiLib("k_9o16s5zl");
        private String sqlDatoToJson(SqlDataReader dataReader)
        {
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dataTable);
            return JSONString;
        }
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
                        string nemovieid = reader.GetString(0);
                        movie_id_list.Add(nemovieid);
                        
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

        public async Task<TitleData> GetMovieFromImdbTask(string movieid)
        {
            return await imdApiLib.TitleAsync(movieid);
        }
    }
}
