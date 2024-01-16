namespace DemoMySql;

public class Program
{
    public static void Main()
    {
        var client = new Client();

        client.ExperimentWithMySqlDataRepo();
        client.ExperimentWithDapperRepo();
    }
}