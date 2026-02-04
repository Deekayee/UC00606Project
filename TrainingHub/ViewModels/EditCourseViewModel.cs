using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using TrainingHub.Models;

namespace TrainingHub.ViewModels
{
    public partial class EditCourseViewModel : ViewModelBase
    {
        public Course Course { get; }
        public ObservableCollection<Trainer> Trainers { get; } = new();

        [ObservableProperty]
        private string _courseName = string.Empty;

        [ObservableProperty]
        private string _area = string.Empty;

        [ObservableProperty]
        private DateTimeOffset? _startDate;

        [ObservableProperty]
        private DateTimeOffset? _endDate;

        [ObservableProperty]
        private Trainer? _selectedTrainer;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        partial void OnErrorMessageChanged(string value)
        {
            OnPropertyChanged(nameof(HasError));
        }

        public EditCourseViewModel(Course course, IEnumerable<Trainer> trainers)
        {
            Course = course;
            
            foreach (var trainer in trainers)
            {
                Trainers.Add(trainer);
            }

            // Initialize fields
            CourseName = course.CourseName;
            Area = course.Area;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            SelectedTrainer = Trainers.FirstOrDefault(t => t == course.Trainer) ?? course.Trainer;
            
            // If the trainer is not in the list (e.g. fired?), we might need to handle it.
            // For now assuming the trainer instance is valid or we just match by reference.
            if (SelectedTrainer != null && !Trainers.Contains(SelectedTrainer))
            {
                 // Try to find by ID or Name if reference different?
                 // Models don't seem to have IDs.
                 // Assuming reference equality for now or that the passed list includes the current trainer.
                 // If the list comes from Company.Employees, it should be fine.
            }
        }

        public bool Validate()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(CourseName))
            {
                ErrorMessage = "Course Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Area))
            {
                ErrorMessage = "Area is required.";
                return false;
            }

            if (SelectedTrainer == null)
            {
                ErrorMessage = "A Trainer must be selected.";
                return false;
            }

            if (StartDate == null || EndDate == null)
            {
                ErrorMessage = "Start and End dates are required.";
                return false;
            }

            if (EndDate.Value.Date < StartDate.Value.Date)
            {
                ErrorMessage = "End Date cannot be earlier than Start Date.";
                return false;
            }

            return true;
        }

        public void UpdateCourse()
        {
            Course.CourseName = CourseName;
            Course.Area = Area;
            Course.StartDate = StartDate?.DateTime ?? DateTime.Today;
            Course.EndDate = EndDate?.DateTime ?? DateTime.Today;
            Course.Trainer = SelectedTrainer!;
        }
    }
}
