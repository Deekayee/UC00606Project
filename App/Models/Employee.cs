namespace UC00606Project.Models;

public abstract class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime ContractStartDate { get; set; }
    public DateTime ContractEndDate { get; set; }
    public DateTime CriminalRecordEndDate { get; set; }
    public decimal SalaryBase { get; set; }
}