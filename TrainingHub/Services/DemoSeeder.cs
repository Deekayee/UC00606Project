using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingHub.Models;

namespace TrainingHub.Services
{
    public static class DemoSeeder
    {
        public static void Seed(Company company)
        {
            var d1 = new Director(
                id: 1,
                firstName: "Ana",
                lastName: "Silva",
                address: "Rua A",
                phoneNumber: "910000001",
                contractStartDate: new DateTime(2025, 1, 1),
                contractEndDate: new DateTime(2026, 12, 31),
                criminalRecordEndDate: new DateTime(2027, 1, 1),
                salaryBase: 1600m,
                flexibleHours: true,
                monthlyBonus: 200m,
                companyCar: true
            );

            var s1 = new Secretary(
                id: 2,
                firstName: "Bruno",
                lastName: "Costa",
                address: "Rua B",
                phoneNumber: "910000002",
                contractStartDate: new DateTime(2025, 6, 1),
                contractEndDate: new DateTime(2026, 1, 28),
                criminalRecordEndDate: new DateTime(2026, 12, 1),
                salaryBase: 1100m,
                reportsToDirector: d1,
                area: "Admin"
            );

            var t1 = new Trainer(
                id: 3,
                firstName: "Carla",
                lastName: "Mendes",
                address: "Rua C",
                phoneNumber: "910000003",
                contractStartDate: new DateTime(2025, 1, 1),
                contractEndDate: new DateTime(2026, 12, 31),
                criminalRecordEndDate: new DateTime(2026, 11, 15),
                salaryBase: 0m, // trainers that are paid hourly may have 0 base salary
                teachingSubject: "C#",
                trainerAvailability: Trainer.Availability.Both,
                hourlyRate: 25m
            );

            var t2 = new Trainer(
                id: 4,
                firstName: "Diogo",
                lastName: "Ramos",
                address: "Rua D",
                phoneNumber: "910000004",
                contractStartDate: new DateTime(2025, 3, 1),
                contractEndDate: new DateTime(2026, 3, 31),
                criminalRecordEndDate: new DateTime(2027, 1, 10),
                salaryBase: 0m,
                teachingSubject: "SQL",
                trainerAvailability: Trainer.Availability.Evening,
                hourlyRate: 30m
            );

            var c1 = new Coordinator(
                id: 5,
                firstName: "Eva",
                lastName: "Lima",
                address: "Rua E",
                phoneNumber: "910000005",
                contractStartDate: new DateTime(2025, 10, 1),
                contractEndDate: new DateTime(2026, 10, 31),
                criminalRecordEndDate: new DateTime(2027, 2, 1),
                salaryBase: 1400m,
                coordinationArea: "IT"
            );

            company.AddEmployee(d1);
            company.AddEmployee(s1);
            company.AddEmployee(t1);
            company.AddEmployee(t2);
            company.AddEmployee(c1);

            // 2) Courses to appoint trainers to
            // January 2026 (t1)
            var course1 = new Course(
                courseName: "Intro C#",
                area: "Programming",
                startDate: new DateTime(2026, 1, 10),
                endDate: new DateTime(2026, 1, 14),
                trainer: t1
            );

            // February 2026 (t2)
            var course2 = new Course(
                courseName: "SQL Basics",
                area: "Database",
                startDate: new DateTime(2026, 2, 5),
                endDate: new DateTime(2026, 2, 7),
                trainer: t2
            );

            // Used in AddCourse to link course to trainer
            company.AddCourse(course1);
            company.AddCourse(course2);

            // AssignedCourses to capulate trainer's schedule
            t1.AssignedCourses.Add(course1);
            t2.AssignedCourses.Add(course2);
        }
    }
}
