using DemoMySql.Models;
using MySql.Data.MySqlClient;

namespace DemoMySql.Repositories;

public class PersonMySqlDataRepository
{
    private readonly string _connectionString;

    public PersonMySqlDataRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<PersonEntity> GetAll()
    {
        var allPersons = new List<PersonEntity>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM TestDb.Person";

            using (var cmd = new MySqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PersonEntity person = MapDataToPersonEntity(reader);
                        allPersons.Add(person);
                    }
                }
            }
        }

        return allPersons;
    }

    public PersonEntity GetByPersonId(int personId)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM TestDb.Person WHERE PersonId = @PersonId";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@PersonId", personId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapDataToPersonEntity(reader);
                    }
                }
            }
        }

        return null;
    }

    public int Create(PersonEntity personEntity)
    {
        int personIdCreated = -1;

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO TestDb.Person (FirstName, LastName, City, Address, Age, IsMale) " +
                           "VALUES (@FirstName, @LastName, @City, @Address, @Age, @IsMale);" +
                           "SELECT LAST_INSERT_ID();";

            using (var cmd = new MySqlCommand(query, connection))
            {
                MapPersonEntityToParameters(personEntity, cmd);

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out personIdCreated))
                {
                    Console.WriteLine($"Repository created Person with PersonId {personIdCreated}!\n");
                }
            }
        }

        return personIdCreated;
    }

    public void Update(PersonEntity personEntity)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string query = "UPDATE TestDb.Person SET FirstName = @FirstName, LastName = @LastName, " +
                           "City = @City, Address = @Address, Age = @Age, IsMale = @IsMale " +
                           "WHERE PersonId = @PersonId";

            using (var cmd = new MySqlCommand(query, connection))
            {
                MapPersonEntityToParameters(personEntity, cmd, true);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int personId)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            string query = "DELETE FROM TestDb.Person WHERE PersonId = @PersonId";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@PersonId", personId);

                cmd.ExecuteNonQuery();
            }
        }
    }

    private static PersonEntity MapDataToPersonEntity(MySqlDataReader reader)
    {
        return new PersonEntity
        {
            PersonId = reader.GetInt32("PersonId"),
            FirstName = reader.GetString("FirstName"),
            LastName = reader.GetString("LastName"),
            City = reader.GetString("City"),
            Address = reader.GetString("Address"),
            IsMale = reader.GetBoolean("IsMale"),
            Age = reader.GetInt32("Age")
        };
    }

    private static void MapPersonEntityToParameters(PersonEntity personEntity, MySqlCommand cmd, bool isPersonIdParam = false)
    {
        if (isPersonIdParam)
        {
            cmd.Parameters.AddWithValue("@PersonId", personEntity.PersonId);
        }

        cmd.Parameters.AddWithValue("@FirstName", personEntity.FirstName);
        cmd.Parameters.AddWithValue("@LastName", personEntity.LastName);
        cmd.Parameters.AddWithValue("@City", personEntity.City);
        cmd.Parameters.AddWithValue("@Address", personEntity.Address);
        cmd.Parameters.AddWithValue("@Age", personEntity.Age);
        cmd.Parameters.AddWithValue("@IsMale", personEntity.IsMale);
    }
}
