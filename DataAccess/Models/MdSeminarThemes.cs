﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class MdSeminarThemes
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Seminars> Seminars { get; set; } = new List<Seminars>();
}