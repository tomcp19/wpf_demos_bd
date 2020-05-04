namespace App.Models
{
    public class Employee //test de git - probleme de clonage pour les versions precedentes...
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string HomePhone { get; set; }

        public override string ToString() => $"{LastName}, {FirstName}";

    }
}
