using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RedOpalV2.Model
{
    public class PersonRepository
    {
        private readonly string _connectionString;
        //lets read in the DB file and its data
        public PersonRepository()
        {
            _connectionString = @"Data Source=EDISON2\SQLEXPRESS;Initial Catalog=RedOpalInnovations;Integrated Security=True;Trust Server Certificate=True";
        }

        public List<Person> GetAllPeople()
        {
            List<Person> people = new List<Person>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM People";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            people.Add(new Person
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                DepartmentId = reader.IsDBNull(reader.GetOrdinal("Department")) ? 0 : (int)reader["Department"],
                                Street = reader["Street"].ToString(),
                                City = reader["City"].ToString(),
                                State = reader["State"].ToString(),
                                ZIP = reader["ZIP"].ToString(),
                                Country = reader["Country"].ToString()
                            });
                        }
                    }
                }
            }

            return people;
        }

        // AddPerson method, UpdatePerson, DeletePerson, GetPersonById methods...
        public void AddPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO People (Name, Phone, Department, Street, City, State, ZIP, Country) VALUES (@Name, @Phone, @Department, @Street, @City, @State, @ZIP, @Country)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Phone", person.Phone);
                    command.Parameters.AddWithValue("@Department", person.DepartmentId);
                    command.Parameters.AddWithValue("@Street", person.Street);
                    command.Parameters.AddWithValue("@City", person.City);
                    command.Parameters.AddWithValue("@State", person.State);
                    command.Parameters.AddWithValue("@ZIP", person.ZIP);
                    command.Parameters.AddWithValue("@Country", person.Country);
                    command.ExecuteNonQuery();
                }
            }
        }
        //Update Person method
        public void UpdatePerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE People SET Name = @Name, Phone = @Phone, Department = @Department, Street = @Street, City = @City, State = @State, ZIP = @ZIP, Country = @Country WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", person.Id);
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Phone", person.Phone);
                    command.Parameters.AddWithValue("@Department", person.DepartmentId);
                    command.Parameters.AddWithValue("@Street", person.Street);
                    command.Parameters.AddWithValue("@City", person.City);
                    command.Parameters.AddWithValue("@State", person.State);
                    command.Parameters.AddWithValue("@ZIP", person.ZIP);
                    command.Parameters.AddWithValue("@Country", person.Country);
                    command.ExecuteNonQuery();
                }
            }
        }
        //Delete Person method
        public void DeletePerson(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM People WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
