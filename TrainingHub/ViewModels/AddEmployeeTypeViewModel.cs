using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TrainingHub.Models;

namespace TrainingHub.ViewModels
{
    public partial class AddEmployeeTypeViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _selectedType;

        public ObservableCollection<string> EmployeeTypes { get; } = new ObservableCollection<string>
        {
            "Director",
            "Coordinator",
            "Secretary",
            "Trainer"
        };
        
        public AddEmployeeTypeViewModel()
        {
            SelectedType = EmployeeTypes.FirstOrDefault();
        }
    }
}
