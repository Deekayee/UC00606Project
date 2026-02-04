using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TrainingHub.Models;
using TrainingHub.Services;

namespace TrainingHub.ViewModels;

// Courses ViewModel
public partial class CoursesViewModel : ViewModelBase
{
    // Dependencies
    private readonly Company _company;
    private readonly IDateProvider _dateProvider;

    // Collections
    public ObservableCollection<Course> Courses { get; } = new();
    public ObservableCollection<Trainer> Trainers { get; } = new();

    // New course fields
    [ObservableProperty] private string newCourseName = string.Empty;
    [ObservableProperty] private string newCourseArea = string.Empty;
    [ObservableProperty] private DateTimeOffset newStartDate;
    [ObservableProperty] private DateTimeOffset newEndDate;
    [ObservableProperty] private Trainer? selectedNewTrainer;

    // UI state
    [ObservableProperty] private bool isAddingCourse;
    [ObservableProperty] private bool isDeletingCourse;
    [ObservableProperty] private string errorMessage;
    [ObservableProperty] private string modalTitle = "Adding New Course";

    // Internal state
    private Course? _editingCourse;
    private Course? _courseToDelete;

    // Constructor
    public CoursesViewModel(Company company, IDateProvider dateProvider)
    {
        _company = company;
        _dateProvider = dateProvider;

        NewStartDate = _dateProvider.Today.AddDays(1);
        NewEndDate = _dateProvider.Today.AddDays(5);

        RefreshData();
    }

    // Data loading
    public void RefreshData()
    {
        Courses.Clear();
        foreach (var course in _company.Courses)
        {
            Courses.Add(course);
        }

        Trainers.Clear();
        foreach (var trainer in _company.Employees.OfType<Trainer>())
        {
            Trainers.Add(trainer);
        }
    }

    // Commands

    // Open add course modal
    [RelayCommand]
    private void OpenAddCourse()
    {
        _editingCourse = null;
        ModalTitle = "Adding New Course";

        ErrorMessage = string.Empty;
        NewCourseName = string.Empty;
        NewCourseArea = string.Empty;
        SelectedNewTrainer = null;
        NewStartDate = _dateProvider.Today.AddDays(1);
        NewEndDate = _dateProvider.Today.AddDays(5);

        IsAddingCourse = true;
    }

    // Open edit course modal
    [RelayCommand]
    private void EditCourse(Course course)
    {
        _editingCourse = course;
        ModalTitle = "Editing Course";
        ErrorMessage = string.Empty;

        NewCourseName = course.CourseName;
        NewCourseArea = course.Area;
        NewStartDate = course.StartDate;
        NewEndDate = course.EndDate;
        SelectedNewTrainer = course.Trainer;

        IsAddingCourse = true;
    }

    // Open remove confirmation modal
    [RelayCommand]
    private void RemoveCourse(Course course)
    {
        _courseToDelete = course;
        IsDeletingCourse = true;
    }

    // Confirm course removal
    [RelayCommand]
    private void ConfirmRemoveCourse()
    {
        if (_courseToDelete != null)
        {
            if (_company.Courses.Contains(_courseToDelete))
            {
                _company.Courses.Remove(_courseToDelete);
            }

            if (Courses.Contains(_courseToDelete))
            {
                Courses.Remove(_courseToDelete);
            }
        }

        IsDeletingCourse = false;
        _courseToDelete = null;
    }

    // Cancel course removal
    [RelayCommand]
    private void CancelRemoveCourse()
    {
        IsDeletingCourse = false;
        _courseToDelete = null;
    }

    // Cancel add/edit course
    [RelayCommand]
    private void CancelAddCourse()
    {
        IsAddingCourse = false;
    }

    // Save new or edited course
    [RelayCommand]
    private void SaveCourse()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(NewCourseName) || SelectedNewTrainer == null)
        {
            ErrorMessage = "Please fill in the course name and select a trainer.";
            return;
        }

        if (NewEndDate.Date < NewStartDate.Date)
        {
            ErrorMessage = "End Date cannot be earlier than Start Date.";
            return;
        }

        try
        {
            if (_editingCourse != null)
            {
                _editingCourse.CourseName = NewCourseName;
                _editingCourse.Area = NewCourseArea;
                _editingCourse.StartDate = NewStartDate.DateTime;
                _editingCourse.EndDate = NewEndDate.DateTime;
                _editingCourse.Trainer = SelectedNewTrainer;

                int index = Courses.IndexOf(_editingCourse);
                if (index != -1)
                {
                    Courses.RemoveAt(index);
                    Courses.Insert(index, _editingCourse);
                }

                IsAddingCourse = false;
            }
            else
            {
                var newCourse = new Course(
                    NewCourseName,
                    NewCourseArea,
                    NewStartDate.DateTime,
                    NewEndDate.DateTime,
                    SelectedNewTrainer
                );

                _company.AddCourse(newCourse);

                if (_company.Courses.Contains(newCourse))
                {
                    Courses.Add(newCourse);
                    IsAddingCourse = false;
                }
            }
        }
        catch (ArgumentException ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}