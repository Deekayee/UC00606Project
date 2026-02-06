using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TrainingHub.Models;
using TrainingHub.Services;
 
namespace TrainingHub.ViewModels;
 
// Courses ViewModel
public partial class CoursesViewModel : ViewModelBase
{
    // Dependencies
    private readonly Company _company;
    private readonly IDialogService _dialogService;
 
    // Collections
    public ObservableCollection<Course> Courses { get; } = new();
    public ObservableCollection<Trainer> Trainers { get; } = new();
 
    // Constructor
    public CoursesViewModel(Company company, IDialogService dialogService)
    {
        _company = company;
        _dialogService = dialogService;
 
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
    private async Task OpenAddCourse()
    {
        // Refresh trainers list to ensure we have the latest
        Trainers.Clear();
        foreach (var trainer in _company.Employees.OfType<Trainer>())
        {
            Trainers.Add(trainer);
        }
 
        var newCourse = await _dialogService.ShowAddCourseDialogAsync(Trainers);
        if (newCourse != null)
        {
            _company.AddCourse(newCourse);
            // Verify if it was added (AddCourse checks date validity)
            if (_company.Courses.Contains(newCourse))
            {
                Courses.Add(newCourse);
            }
        }
    }
 
    // Open edit course modal
    [RelayCommand]
    private async Task EditCourse(Course course)
    {
        if (course == null)
            return;
 
        // Refresh trainers list
        Trainers.Clear();
        foreach (var trainer in _company.Employees.OfType<Trainer>())
        {
            Trainers.Add(trainer);
        }
 
        var result = await _dialogService.ShowEditCourseDialogAsync(course, Trainers);
 
        if (result)
        {
            // Refresh the list to show updated values
            var index = Courses.IndexOf(course);
            if (index >= 0)
            {
                Courses.RemoveAt(index);
                Courses.Insert(index, course);
            }
        }
    }
 
    // Open remove confirmation modal
    [RelayCommand]
    private async Task RemoveCourse(Course course)
    {
        if (course == null)
            return;
 
        var confirmed = await _dialogService.ShowDeleteCourseDialogAsync(
            "Delete Course",
            $"Are you sure you want to delete {course.CourseName}?"
        );
 
        if (confirmed)
        {
            _company.RemoveCourse(course);
            Courses.Remove(course);
        }
    }
}