using DemoMySql.Models;
using DemoMySql.Repositories;

namespace DemoMySql;

public class Client
{
    private const string ConnectionString = "Server=localhost;Database=TestDb;User ID=root;Password=test123;";

    private readonly PersonMySqlDataRepository _mySqlDataRepo = new(ConnectionString);
    private readonly PersonDapperRepository _dapperRepo = new(ConnectionString);

    public void ExperimentWithMySqlDataRepo()
    {
        // ========================= GetAll =========================
        var allPersons = _mySqlDataRepo.GetAll();

        foreach (var person in allPersons)
        {
            Console.WriteLine(person != null ? person.ToString() : "Person not found.\n");
        }

        // ========================= GetByPersonId =========================
        var personById = _mySqlDataRepo.GetByPersonId(1);

        Console.WriteLine(personById != null ? personById.ToString() : "Person not found.\n");

        // ========================= Create =========================
        var personEntity = new PersonEntity
        {
            FirstName = "John",
            LastName = "Snow",
            City = "Lapland",
            Address = "123 Snowflake str.",
            Age = 25,
            IsMale = true
        };

        var personId = _mySqlDataRepo.Create(personEntity);

        Console.WriteLine($"Person with PersonId {personId} was created!\n");

        var personCreated = _mySqlDataRepo.GetByPersonId(personId);

        Console.WriteLine(personCreated != null ? personCreated.ToString() : "Person not found.\n");

        // ========================= Update =========================
        var personExisting = _mySqlDataRepo.GetByPersonId(personId);

        if (personExisting != null)
        {
            personExisting.FirstName = "Johnny";
            personExisting.LastName = "Lava";
            personExisting.City = "Tromso";
            personExisting.Address = "988 Snowman str.";
            personExisting.Age = 52;
            personExisting.IsMale = false;

            _mySqlDataRepo.Update(personExisting);

            Console.WriteLine("Person updated.\n");
        }
        else
        {
            Console.WriteLine("Person not found for update.\n");
        }

        var personUpdated = _mySqlDataRepo.GetByPersonId(personId);

        Console.WriteLine(personUpdated != null ? personUpdated.ToString() : "Person not found.\n");

        // ========================= Delete =========================
        var personIdToDelete = personId;
        _mySqlDataRepo.Delete(personIdToDelete);
        Console.WriteLine($"Person with PersonId {personIdToDelete} deleted.\n");

        // ========================= GetAll after Update and Delete =========================
        allPersons = _mySqlDataRepo.GetAll();
        Console.WriteLine("All Persons after update and delete:\n");

        foreach (var person in allPersons)
        {
            Console.WriteLine(person != null ? person.ToString() : "Person not found.\n");
        }
    }

    public void ExperimentWithDapperRepo()
    {
        // ========================= GetAll =========================
        var allPersons = _dapperRepo.GetAll();

        foreach (var person in allPersons)
        {
            Console.WriteLine(person != null ? person.ToString() : "Person not found.\n");
        }

        // ========================= GetByPersonId =========================
        var personById = _dapperRepo.GetByPersonId(1);

        Console.WriteLine(personById != null ? personById.ToString() : "Person not found.\n");

        // ========================= Create =========================
        var personEntity = new PersonEntity
        {
            FirstName = "John",
            LastName = "Snow",
            City = "Lapland",
            Address = "123 Snowflake str.",
            Age = 25,
            IsMale = true
        };

        var personId = _dapperRepo.Create(personEntity);

        Console.WriteLine($"Person with PersonId {personId} was created!\n");

        var personCreated = _dapperRepo.GetByPersonId(personId);

        Console.WriteLine(personCreated != null ? personCreated.ToString() : "Person not found.\n");

        // ========================= Update =========================
        var personExisting = _dapperRepo.GetByPersonId(personId);

        if (personExisting != null)
        {
            personExisting.FirstName = "Johnny";
            personExisting.LastName = "Lava";
            personExisting.City = "Tromso";
            personExisting.Address = "988 Snowman str.";
            personExisting.Age = 52;
            personExisting.IsMale = false;

            _dapperRepo.Update(personExisting);

            Console.WriteLine("Person updated.\n");
        }
        else
        {
            Console.WriteLine("Person not found for update.\n");
        }

        var personUpdated = _dapperRepo.GetByPersonId(personId);

        Console.WriteLine(personUpdated != null ? personUpdated.ToString() : "Person not found.\n");

        // ========================= Delete =========================
        var personIdToDelete = personId;
        _dapperRepo.Delete(personIdToDelete);
        Console.WriteLine($"Person with PersonId {personIdToDelete} deleted.\n");

        // ========================= GetAll after Update and Delete =========================
        allPersons = _dapperRepo.GetAll();
        Console.WriteLine("All Persons after update and delete:\n");

        foreach (var person in allPersons)
        {
            Console.WriteLine(person != null ? person.ToString() : "Person not found.\n");
        }
    }
}
