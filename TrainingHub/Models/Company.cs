using System;
using System.Collections.Generic;
using System.Linq;
using TrainingHub.Services;

namespace TrainingHub.Models;

public class Company
{
    // Dependency Injection of IDateProvider
    
    private readonly IDateProvider _dateProvider;

    
    // Employee and Course Lists
    
    public List<Employee> Employees { get; private set; } = new List<Employee>();
    public List<Course> Courses { get; private set; } = new List<Course>();
    
    
    // Constructor
    
    public Company(IDateProvider dateProvider)
    {
        _dateProvider = dateProvider;
    }
    
    
    // Management methods
    
    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
    }

    public void AddCourse(Course course)
    {
        if (course.StartDate.Date > _dateProvider.Today.Date) 
        {
            Courses.Add(course);
        }
    }

    public void UpdateCriminalRecord(int employeeId, DateTime newDate)
    {
        Employee? employee = Employees.FirstOrDefault(e => e.Id == employeeId);
        if (employee != null)
        {
            employee.CriminalRecordEndDate = newDate.Date;
        }
    }
    
    
    // Query methods
    
    public List<Employee> GetValidContracts()
    {
        return Employees.Where(e => e.IsContractValid(_dateProvider.Today)).ToList();
    }

    public List<Employee> GetExpiredCriminalRecords()
    {
        return Employees.Where(e=>e.IsCriminalRecordExpired(_dateProvider.Today)).ToList();
    }
    
    
    // Warnings
    
    public List<Employee> GetContractsEndingToday()
    {
        return Employees.Where(e => e.ContractEndDate.Date == _dateProvider.Today.Date).ToList();
    }
    
    public List<Employee> GetCriminalRecordsEndingToday()
    {
        return Employees.Where(e => e.CriminalRecordEndDate.Date == _dateProvider.Today.Date).ToList();
    }
    
    
    // Expenses
    
    public decimal CalculateTotalMonthlyExpense()
    {
        return Employees
            .Where(e => e.IsContractValid(_dateProvider.Today))
            .Sum(e => e.CalculateMonthlySalary());
    }
    
}