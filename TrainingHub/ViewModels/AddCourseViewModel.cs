using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TrainingHub.Models;

namespace TrainingHub.ViewModels;

// Add Course ViewModel
public partial class AddCourseViewModel : ViewModelBase
{
    // Collections
    public ObservableCollection<Trainer> Trainers { get; } = new();

    // Observable properties
    [ObservableProperty]
    private string _courseName = string.Empty;

    [ObservableProperty]
    private string _area = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _startDate = DateTime.Today.AddDays(1);

    [ObservableProperty]
    private DateTimeOffset? _endDate = DateTime.Today.AddDays(5);

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
    public AddCourseViewModel(IEnumerable<Trainer> trainers)
    {
        foreach (var trainer in trainers)
        {
            Trainers.Add(trainer);
        }
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

    // Course creation
    public Course CreateCourse()
    {
        return new Course(
            CourseName,
            Area,
            StartDate?.DateTime ?? DateTime.Today,
            EndDate?.DateTime ?? DateTime.Today,
            SelectedTrainer!
        );
    }
}