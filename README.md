# TrainingHub

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
│  ├─ IDateProvider.cs
│  ├─ DateProvider.cs
│  └─ DemoSeeder.cs
├─ ViewModels/
│  ├─ ViewModelBase.cs
│  ├─ MainWindowViewModel.cs
│  ├─ DashboardViewModel.cs
│  ├─ EmployeesViewModel.cs
│  ├─ CoursesViewModel.cs
│  └─ ExpensesViewModel.cs
├─ Views/
│  ├─ MainWindow.axaml
│  ├─ MainWindow.axaml.cs
│  ├─ DashboardView.axaml
│  ├─ DashboardView.axaml.cs
│  ├─ EmployeesView.axaml
│  ├─ EmployeesView.axaml.cs
│  ├─ CoursesView.axaml
│  ├─ CoursesView.axaml.cs
│  ├─ ExpensesView.axaml
│  └─ ExpensesView.axaml.cs
├─ App.axaml
├─ App.axaml.cs
├─ Program.cs
└─ TrainingHub.csproj
```

Demo Seed Data

A DemoSeeder exists to generate demo data (employees, trainers, and courses) for testing. Its use will be only in the development and testing environment.