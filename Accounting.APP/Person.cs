namespace Accounting.APP;

public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public int ID { get; set; }

    public override string ToString()
    {
        return $"{ID,5} {FirstName} {MiddleName} {LastName}";
    }
}
