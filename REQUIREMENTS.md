# 📝 System Requirements  
## Employee and Training Management System (Avalonia UI)

This document defines the **Functional Requirements (FR)** and **Non-Functional Requirements (NFR)** for the system.  
Its purpose is to clearly describe what the application must do and how it should behave.

---

# 1. System Overview

The system is intended to support the management of a training company by enabling:

- Registration and management of different types of employees  
- Monitoring contract validity and criminal record expiration  
- Management of courses and related costs  
- Data queries and reporting  
- Time-simulation through an internal system clock  

The application will be developed in **C#**, using **Avalonia UI**, and following the **MVVM (Model–View–ViewModel)** pattern.

---

# 2. Functional Requirements (FR)

## FR01 — Employee Management

### FR01.1 – Register a new employee  
The system shall allow the registration of a new employee, including:

- Employee type (Director, Secretary, Trainer, Coordinator)  
- Personal data: ID, First Name, Last Name, Address, Phone Number  
- Dates: Contract Start, Contract End, Criminal Record Expiration  
- Type-specific fields:
  - **Director:** base salary, monthly bonus, schedule exemption, company car  
  - **Secretary:** department, supervising director, monthly salary  
  - **Trainer:** teaching area, availability (day/evening/both), hourly rate  
  - **Coordinator:** monthly salary, associated trainers  

### FR01.2 – List employees  
The system shall display a list of all registered employees.

### FR01.3 – View employee details  
The system shall allow selecting an employee and viewing all related information.

### FR01.4 – Edit employee information  
The system shall allow updating any information associated with an employee.

### FR01.5 – Remove employee  
The system shall allow deleting an employee from the system.

---

## FR02 — Contracts and Criminal Records

### FR02.1 – List employees with valid contracts  
The system shall list employees whose contracts are valid on the current simulation date.

### FR02.2 – List employees with expired criminal records  
The system shall identify employees whose criminal records have expired.

### FR02.3 – Update criminal record  
The system shall allow updating the criminal record information for any employee.

---

## FR03 — Course Management

### FR03.1 – Create a Course  
The system shall allow creating a course with:

- Course name  
- Area  
- Assigned trainer  
- Start date and end date  

### FR03.2 – Assign trainer to course  
Each course shall be associated with exactly one valid trainer.

### FR03.3 – View course  
The system shall display a list of all courses.

---

## FR04 — Cost Calculations

### FR04.1 – Calculate course cost  
The system shall calculate the cost of a course based on:

- Number of days  
- 6 hours per day  
- Trainer’s hourly rate  

### FR04.2 – Calculate monthly company expenses  
The system shall compute the monthly cost associated with employee salaries and compensations.

---

## FR05 — Data Export

### FR05.1 – Export employee list to CSV  
The system shall export all employee records to a `.csv` file containing:

- ID, name, type, address, contact information, contract end date, criminal record expiration date, and status.

---

## FR06 — Date Simulation and Alerts

### FR06.1 – Display current simulation date  
The system shall display the current date used by the internal simulation clock.

### FR06.2 – Advance date  
The system shall allow advancing the internal simulation date by at least one day.

### FR06.3 – Recalculate employee states  
Whenever the date changes, the system shall automatically update:

- Contract validity  
- Criminal record validity  

### FR06.4 – Notify contract expirations  
The system shall notify the user when any contract expires on the current simulation date.

### FR06.5 – Notify criminal record expirations  
The system shall notify the user when a criminal record expires on the current simulation date.

---

# 3. Non-Functional Requirements (NFR)

## NFR01 — Technology and Architecture

### NFR01.1 – Framework  
The system shall be implemented in **C# using Avalonia UI**.

### NFR01.2 – Architectural pattern  
The system shall follow the MVVM (Model–View–ViewModel) architectural pattern, enforcing separation of concerns between the user interface, application logic, and data model, promoting maintainability, testability, and scalability.


---

## NFR02 — Usability

### NFR02.1 – Intuitive interface  
The user interface shall be simple, clear, and organized by modules.

### NFR02.2 – User feedback  
The system shall display clear success or error messages after important actions.

### NFR02.3 – UI consistency  
All views shall maintain a consistent layout and styling.

---

## NFR03 — Performance

### NFR03.1 – Response time  
Typical operations shall respond within a reasonable time under normal usage conditions.

---

## NFR04 — Reliability

### NFR04.1 – Input validation  
The system shall validate all user inputs to prevent invalid or incomplete data.

### NFR04.2 – Error handling  
The system shall handle unexpected situations in a controlled manner, displaying appropriate error messages.

---

## NFR05 — Portability

### NFR05.1 – Cross-platform support  
The system shall run on Windows and Linux and be prepared for execution on macOS.

---

## NFR06 — Maintainability

### NFR06.1 – Modular structure  
The project shall be organized into Models, ViewModels, Views, and supporting service classes responsible for business logic, data processing, validation, simulations, and file operations.

### NFR06.2 – Extensibility  
The system shall be extendable, allowing new employee types or features to be added in the future.

---

## NFR07 — Security

### NFR07.1 – Data protection  
The system shall protect user and employee data against unauthorized access.

### NFR07.2 – Input validation  
All user inputs shall be validated to prevent invalid or inconsistent data.

---

# 4. Conclusion

This document defines all functional and non-functional requirements for the system.  
It serves as a reference for planning, designing, implementing, and validating the application.
