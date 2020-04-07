using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ListPeoplePost.Data
{
    public class PersonManager
    {
        private string _connectionString;

        public PersonManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People";
                List<Person> ppl = new List<Person>();
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ppl.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }

                return ppl;
            }
        }

        public void AddPeople(IEnumerable<Person> ppl)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                                  "VALUES (@firstName, @lastName, @age)";
                conn.Open();
                foreach (var person in ppl)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", person.LastName);
                    cmd.Parameters.AddWithValue("@age", person.Age);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}