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

        public Simcha GetSimcha(int Id)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Simchas WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", Id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            Simcha simcha = new Simcha();
            simcha.Id = (int)reader["Id"];
            simcha.Date = (DateTime)reader["Date"];
            simcha.Name = (string)reader["Name"];

            return simcha;
        }
        public void AddContributor(Contributor contributor)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Contributors " +
                "(FirstName, LastName, CreatedDate, CellNumber, AlwaysInclude) " +
                "VALUES(@FirstName, @LastName, @CreatedDate, @CellNumber, @AlwaysInclude) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@FirstName", contributor.FirstName);
            cmd.Parameters.AddWithValue("@LastName", contributor.LastName);
            cmd.Parameters.AddWithValue("@CreatedDate", contributor.CreatedDate);
            cmd.Parameters.AddWithValue("@CellNumber", contributor.CellNumber);
            cmd.Parameters.AddWithValue("@AlwaysInclude", contributor.AlwaysInclude);
            connection.Open();
            contributor.Id = (int)(decimal)cmd.ExecuteScalar();

        }
        public void EditContributor(Contributor contributor)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Contributors " +
                                "SET FirstName = @FirstName, LastName = @LastName, CellNumber = @CellNumber, CreatedDate = @CreatedDate, AlwaysInclude = @AlwaysInclude " +
                                "WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@FirstName", contributor.FirstName);
            cmd.Parameters.AddWithValue("@LastName", contributor.LastName);
            cmd.Parameters.AddWithValue("@CreatedDate", contributor.CreatedDate);
            cmd.Parameters.AddWithValue("@CellNumber", contributor.CellNumber);
            cmd.Parameters.AddWithValue("@AlwaysInclude", contributor.AlwaysInclude);
            cmd.Parameters.AddWithValue("@Id", contributor.Id);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public List<Contributor> GetContributors()
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Contributors ORDER BY LastName Asc";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Contributor> contributors = new List<Contributor>();
            while (reader.Read())
            {
                contributors.Add(new Contributor
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    CreatedDate = (DateTime)reader["CreatedDate"],
                    CellNumber = (string)reader["CellNumber"],
                    AlwaysInclude = (bool)reader["AlwaysInclude"],
                    Balance = GetBalance((int)reader["Id"])
                  
                });
            }
            connection.Close();
            return contributors;
        }

        public Contributor GetContributor(int id)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Contributors WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Contributor contributor = new Contributor();
            contributor.Id = (int)reader["Id"];
            contributor.FirstName = (string)reader["FirstName"];
            contributor.LastName = (string)reader["LastName"];
            contributor.CellNumber = (string)reader["CellNumber"];
            contributor.AlwaysInclude = (bool)reader["AlwaysInclude"];
            contributor.Balance = GetBalance((int)reader["Id"]);

            return contributor;
        }
        public void AddContribution(Contribution contribution)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Contributions " +
                "(ContributorId, SimchaId, Amount) " +
                "VALUES(@ContributorId, @SimchaId, @Amount)";
            cmd.Parameters.AddWithValue("@ContributorId", contribution.ContributorId);
            cmd.Parameters.AddWithValue("@SimchaId", contribution.SimchaId);
            cmd.Parameters.AddWithValue("@Amount", contribution.Amount);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void ClearContributions(int simchaId)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE from Contributions WHERE SimchaId = @SimchaId";
            cmd.Parameters.AddWithValue("@SimchaId", simchaId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void AddDeposit(Deposit deposit)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Deposits " +
                "(Amount, Date, ContributorId) " +
                "VALUES(@Amount, @Date, @ContributorId)";
            cmd.Parameters.AddWithValue("@Amount", deposit.Amount);
            cmd.Parameters.AddWithValue("@Date", deposit.Date);
            cmd.Parameters.AddWithValue("@ContributorId", deposit.ContributorId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public decimal GetBalance(int id)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT (SELECT ISNULL(SUM(d.Amount), '0') From Contributors c " +
                              "JOIN Deposits d ON c.Id = d.ContributorId WHERE c.Id = @Id) " +
                              "-(SELECT ISNULL(SUM(cs.Amount), '0') From Contributors c " +
                              "JOIN Contributions cs ON c.Id = cs.ContributorId WHERE c.Id = @Id) as 'Balance' ";
            cmd.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            decimal balance = 0;
            while (reader.Read())
            {
                balance = (decimal)reader["Balance"];
            }
            connection.Close();
            return balance;
            
        }

        public List<Transaction> GetDeposits(int contributorId)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Deposits WHERE contributorId = @id";
            cmd.Parameters.AddWithValue("@id", contributorId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            List<Transaction> deposits = new List<Transaction>();
            while (reader.Read())
            {
                deposits.Add(new Transaction
                {
                    Name = "Deposit",
                    ContributorId = contributorId,
                    Date = (DateTime)reader["Date"],
                    Amount = (decimal)reader["Amount"]
                });
            }

            return deposits;
        }

        public List<Transaction> GetContributions(int contributorId)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Contributions cs JOIN Simchas s ON cs.SimchaId = s.Id WHERE cs.ContributorId = @conId";
            cmd.Parameters.AddWithValue("@conId", contributorId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            List<Transaction> contributions = new List<Transaction>();
            while (reader.Read())
            {
                contributions.Add(new Transaction
                {
                    Name = "Contribution",
                    ContributorId = contributorId,
                    Date = (DateTime)reader["Date"],
                    Amount = (decimal)reader["Amount"],
                    SimchaName = (string)reader["Name"]
                });
            }
            return contributions;
        }
        public List<Contribution> GetContributions()
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Contributions cs JOIN Simchas s ON cs.SimchaId = s.Id";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            List<Contribution> contributions = new List<Contribution>();
            while (reader.Read())
            {
                contributions.Add(new Contribution
                {
                    ContributorId = (int)reader["ContributorId"],
                    Amount = (decimal)reader["Amount"],
                    SimchaId = (int)reader["SimchaId"]
                });
            }
            return contributions;
        }
        
    }
}
