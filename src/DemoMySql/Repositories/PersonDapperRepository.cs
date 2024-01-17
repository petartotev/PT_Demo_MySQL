using Dapper;
using DemoMySql.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace DemoMySql.Repositories;

public class PersonDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public PersonDapperRepository(string connectionString)
    {
        _dbConnection = new MySqlConnection(connectionString);
    }

    public List<PersonEntity> GetAll()
    {
        string query = "SELECT * FROM TestDb.Person";

        var allPersonsFromDb = _dbConnection.Query<PersonEntity>(query);

        return (allPersonsFromDb != null && allPersonsFromDb.Any()) ? allPersonsFromDb.ToList() : null;
    }

    public PersonEntity GetByPersonId(int personId)
    {
        string query = "SELECT * FROM TestDb.Person WHERE PersonId = @PersonId";

        return _dbConnection.QueryFirstOrDefault<PersonEntity>(query, new { PersonId = personId });
    }

    public int Create(PersonEntity personEntity)
    {
        string query = "INSERT INTO TestDb.Person (FirstName, LastName, City, Address, Age, IsMale) " +
                       "VALUES (@FirstName, @LastName, @City, @Address, @Age, @IsMale);" +
                       "SELECT LAST_INSERT_ID();";

        return _dbConnection.ExecuteScalar<int>(query, personEntity);
    }

    public void Update(PersonEntity personEntity)
    {
        string query = "UPDATE TestDb.Person SET FirstName = @FirstName, LastName = @LastName, " +
               "City = @City, Address = @Address, Age = @Age, IsMale = @IsMale " +
               "WHERE PersonId = @PersonId";

        _dbConnection.Execute(query, personEntity);
    }

    public void Delete(int personId)
    {
        string query = "DELETE FROM TestDb.Person WHERE PersonId = @PersonId";

        _dbConnection.Execute(query, new { PersonId = personId });
    }
}
