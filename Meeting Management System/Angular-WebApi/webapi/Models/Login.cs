﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class Department
{
    public long DepartmentID { get; set; }
    public string DepartmentName { get; set; }
}

public partial class Employee
{
    public long EmployeeID { get; set; }
    public string EmployeeName { get; set; }
    public string Department { get; set; }
    public string MailID { get; set; }
    public Nullable<System.DateTime> DOJ { get; set; }
    public Nullable<long> Phone { get; set; }
    public string Address { get; set; }
    public Nullable<long> Salary { get; set; }
    public Nullable<int> Age { get; set; }
}

public partial class Employeemaster
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string LoginName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string ContactNo { get; set; }
    public string Address { get; set; }
    public Nullable<int> IsApporved { get; set; }
    public Nullable<int> Status { get; set; }
    public Nullable<int> TotalCnt { get; set; }
}

public partial class Usp_Login_Result
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string LoginName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string ContactNo { get; set; }
    public string Address { get; set; }
    public int IsApporved { get; set; }
    public int Status { get; set; }
}
