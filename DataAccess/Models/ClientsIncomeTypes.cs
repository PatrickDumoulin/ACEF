﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class ClientsIncomeTypes
{
    public int Id { get; set; }

    public int IdClient { get; set; }

    public int IdIncomeType { get; set; }

    public virtual Clients IdClientNavigation { get; set; }

    public virtual MdIncomeType IdIncomeTypeNavigation { get; set; }
}