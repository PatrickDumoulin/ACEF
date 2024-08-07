﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class CowMilkSalesGeneralInfos
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public DateTime EffectiveMonthDate { get; set; }

    public decimal FatPrice { get; set; }

    public decimal ProteinPrice { get; set; }

    public decimal LactosePrice { get; set; }

    public decimal FatPremium { get; set; }

    public decimal FatOnNonFatRatio { get; set; }

    public virtual ICollection<CowMilkSalesGeneralInfoProductions> CowMilkSalesGeneralInfoProductions { get; set; } = new List<CowMilkSalesGeneralInfoProductions>();
}