namespace Phoneshop.Domain.Entities
{
    public class Colleague : User
    {
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
