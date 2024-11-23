namespace Accounting
{
    class Account
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        private string Type { get; set; }
        public string Status { get; set; }
        public Account(int id, string name, string type, bool status)
        {
            Id = id;
            NickName = name;
            Type = type;
            Status = status ? "Open" : "Closed";
        }

        public override string ToString()
        {
            return $"{Type,-20} | {NickName,-100} | {Status,-6}";
        }
    }
}
