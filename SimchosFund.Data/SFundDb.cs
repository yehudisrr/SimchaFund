using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimchosFund.Data
{
    public class SFundDb
    {
        private readonly string _connectionString;
        public SFundDb(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddSimcha(Simcha simcha)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Simchas " +
                "(Name, Date) " +
                "VALUES(@Name, @Date)";
            cmd.Parameters.AddWithValue("@Name", simcha.Name);
            cmd.Parameters.AddWithValue("@Date", simcha.Date);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public List<Simcha> GetSimchas()
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Simchas ORDER BY Date Desc";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Simcha> simchas = new List<Simcha>();
            while (reader.Read())
            {
                simchas.Add(new Simcha
                {
                    Id = (int)reader["Id"],
                    Date = (DateTime)reader["Date"],
                    Name = (string)reader["Name"]
                });
            }
            return simchas;
        }
    }
}
