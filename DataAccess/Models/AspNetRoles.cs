﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class AspNetRoles
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string NormalizedName { get; set; }

    public string ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaims>();

    public virtual ICollection<AspNetUsers> User { get; set; } = new List<AspNetUsers>();
}