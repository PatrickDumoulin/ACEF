﻿using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models 
{
    public partial class SeminarsEmployees : AbstractEntity, ISequenced
    {
        public string SequenceName { get { return "Seminars_Employees_Seq"; } }
    }
}