﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class InterventionsNotes
{
    public int Id { get; set; }

    public int? IdIntervention { get; set; }

    public int? IdEmployee { get; set; }

    public string Comment { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Employees IdEmployeeNavigation { get; set; }

    public virtual Interventions IdInterventionNavigation { get; set; }
}