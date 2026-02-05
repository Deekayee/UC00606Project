# TrainingHub

![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux%20%7C%20macOS-blue)
![Framework](https://img.shields.io/badge/.NET-9.0-purple)
![UI](https://img.shields.io/badge/UI-Avalonia%2011.3-green)
 

Final project for UC00606 Desenvolver programas em linguagem estruturada, whose objective was to develop a Desktop application built with **C# + Avalonia UI** to manage a training center.

## Tech Stack
- .NET (e.g., net9.0)
- Avalonia UI
- CommunityToolkit.Mvvm (MVVM)
- GitHub Projects (Kanban)

## Requirements
- .NET SDK installed (compatible with the project)
- Visual Studio 2022 or VS Code

## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/Deekayee/UC00606Project.git
   cd TrainingHub

2. Restore dependencies:
    ```bash
    dotnet restore

3.  Build and run:
    ```bash
    dotnet run --project TrainingHub/TrainingHub.csproj

## Credentials

-Username: admin
-Password: 1234


## Project Structure



```
.
TrainingHub/
├─ Assets/
│  ├─ education-graduation.png
│  └─ ...
├─ Models/
│  ├─ Employee.cs
│  ├─ Director.cs
│  ├─ Secretary.cs
│  ├─ Trainer.cs
│  ├─ Coordinator.cs
│  ├─ Course.cs
│  ├─ Company.cs
│  └─ ...
├─ Services/
│  ├─ DateProvider.cs
│  ├─ DemoSeeder.cs
│  ├─ DialogService.cs
│  ├─ IDateProvider.cs
│  ├─ ExportService.cs
│  ├─ IExportService.cs
│  └─ IDiologService.cs
├─ ViewModels/
│  ├─ AddEmployeeTypeViewModel.cs
│  ├─ AddEmployeeViewModel.cs
│  ├─ AddCourseViewModel.cs
│  ├─ CourseViewModel.cs
│  ├─ DashboardViewModel.cs
│  ├─ EditCourseViewModel.cs
│  ├─ EditEmployeeViewModel.cs
│  ├─ EmployeeDetailsViewModel.cs
│  ├─ EmployeeViewModel.cs
│  ├─ EmployeesViewModel.cs
│  ├─ ExpensesViewModel.cs
│  ├─ MainWindowViewModel.cs
│  └─ ViewModelBase.cs
├─ Views/
│  ├─ AddCourseWindow.axaml
│  ├─ AddEmployeeWindow.axaml
│  ├─ CoursesView.axaml
│  ├─ DashboardView.axaml
│  ├─ DeleteEnployeeWindows.axaml
│  ├─ DeleteCourseWindows.axaml
│  ├─ EditCourseWindows.axaml
│  ├─ EditEmployeeWindow.axaml
│  ├─ EmployeeDetailsWindow.axaml
│  ├─ EmployeesView.axaml
│  ├─ EmployeeTypeWindow.axaml
│  ├─ ExpensesView.axaml
│  └─ MainWindow.axaml
├─ App.axaml
├─ Program.cs
└─ TrainingHub.csproj
```

Demo Seed Data

A DemoSeeder exists to generate demo data (employees, trainers, and courses) for testing. Its use will be only in the development and testing environment.