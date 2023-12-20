using DemoMySql.Models;
using DemoMySql.Repositories;

namespace DemoMySql;

public class Program
{
    public static void Main()
    {
        string connectionString = "Server=localhost;Database=TestDb;User ID=root;Password=adminadmin;";

        var repository = new PersonRepository(connectionString);

        // ========================= GetAll =========================
        var allPersons = repository.GetAll();

        foreach (var person in allPersons)
        {
            Console.WriteLine(person != null ? person.ToString() : "Person not found.\n");
        }

        // ========================= GetByPersonId =========================
        var personById = repository.GetByPersonId(1);

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

        repository.Create(personEntity);
        Console.WriteLine("Person created!\n");

        var personCreated = repository.GetByPersonId(8);

        Console.WriteLine(personCreated != null ? personCreated.ToString() : "Person not found.\n");

        // ========================= Update =========================
        var personExisting = repository.GetByPersonId(8);

        if (personExisting != null)
        {
            personExisting.FirstName = "Johnny";
            personExisting.LastName = "Lava";
            personExisting.City = "Tromso";
            personExisting.Address = "988 Snowman str.";
            personExisting.Age = 52;
            personExisting.IsMale = false;

            repository.Update(personExisting);

            Console.WriteLine("Person updated.\n");
        }
        else
        {
            Console.WriteLine("Person not found for update.\n");
        }

        var personUpdated = repository.GetByPersonId(8);

        Console.WriteLine(personUpdated != null ? personUpdated.ToString() : "Person not found.\n");

        // ========================= Delete =========================
        var personIdToDelete = 8;
        repository.Delete(personIdToDelete);
        Console.WriteLine($"Person with PersonId {personIdToDelete} deleted.\n");

        // ========================= GetAll after Update and Delete =========================
        allPersons = repository.GetAll();
        Console.WriteLine("All Persons after update and delete:\n");

        foreach (var person in allPersons)
        {
            Console.WriteLine(person != null ? person.ToString() : "Person not found.\n");
        }
    }
}