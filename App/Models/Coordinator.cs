namespace UC00606Project.Models;

public class Coordinator : Employee
{
    public ObservableCollection<Trainer> Trainers { get; set; }
    public string CoordinationArea { get; set; }
}