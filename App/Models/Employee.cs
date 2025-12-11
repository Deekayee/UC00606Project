namespace UC00606Project.Models;

public abstract class Employee
{
    protected int Id { get; set; }
    protected string FirstName { get; set; }
    protected string LastName { get; set; }
    protected string Address { get; set; }
    protected string PhoneNumber { get; set; }
    protected DateTime ContractStartDate { get; set; }
    protected DateTime ContractEndDate { get; set; }
    protected DateTime CriminalRecordEndDate { get; set; }
    protected decimal SalaryBase { get; set; }
}