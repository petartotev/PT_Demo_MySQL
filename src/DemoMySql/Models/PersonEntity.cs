using System.Text;

namespace DemoMySql.Models;

public class PersonEntity
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
    public bool IsMale { get; set; }

    public static char GetGenderLetterByIsMaleBool(bool isMale)
    {
        return isMale ? 'M' : 'F';
    }

    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine($"ID: {PersonId}")
            .AppendLine($"Name: {FirstName} {LastName}")
            .AppendLine($"Age: {Age}")
            .AppendLine($"Gender: " + GetGenderLetterByIsMaleBool(IsMale))
            .AppendLine($"Address: {City}, {Address}")
            .ToString();
    }
}
