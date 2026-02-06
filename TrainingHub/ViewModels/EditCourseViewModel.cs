using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using TrainingHub.Models;

namespace TrainingHub.ViewModels
{
    // Edit Course ViewModel
    public partial class EditCourseViewModel : ViewModelBase
    {
        // Collections
        public Course Course { get; }
        public ObservableCollection<Trainer> Trainers { get; } = new();

        // Observable properties
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

        // Error handling
        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        // Error message change notification
        partial void OnErrorMessageChanged(string value)
        {
            OnPropertyChanged(nameof(HasError));
        }

        // Constructor
        public EditCourseViewModel(Course course, IEnumerable<Trainer> trainers)
        {
            Course = course;

            foreach (var trainer in trainers)
            {
                Trainers.Add(trainer);
            }

            // Initialize fields
            CourseName = course.CourseName ?? string.Empty;
            Area = course.Area ?? string.Empty;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            SelectedTrainer = Trainers.FirstOrDefault(t => t == course.Trainer) ?? course.Trainer;
        }

        // Validation logic
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

        // Course update
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
