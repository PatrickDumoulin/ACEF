﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class InterventionsInterventionSolutions
{
    public int Id { get; set; }

    public int IdIntervention { get; set; }

    public int IdInterventionSolution { get; set; }

    public virtual Interventions IdInterventionNavigation { get; set; }

    public virtual MdInterventionSolution IdInterventionSolutionNavigation { get; set; }
}