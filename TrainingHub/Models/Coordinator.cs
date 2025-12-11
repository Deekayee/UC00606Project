using System.Collections.ObjectModel;

namespace TrainingHub.Models;

public class Coordinator : Employee
{
    public ObservableCollection<Trainer> Trainers { get; set; }
    public string CoordinationArea { get; set; }
}