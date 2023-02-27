namespace Accounting
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int ID { get; set; }

        public Person(int id, string first, string last, string middle)
        {
            ID = id;
            FirstName = first;
            LastName = last;
            MiddleName = middle;
        }

        public override string ToString()
        {
            return $"{ID,5} {FirstName} {MiddleName} {LastName}";
        }
    }
}
