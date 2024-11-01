﻿using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarEmployee;
using DataAccess.BOL.SeminarParticipant;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    /*[MB] - Ce BLL et les méthodes ne sont pas nécessaires. */
    public class SeminarEmployeeBLL : AbstractBLL<ISeminarEmployeeDAL>, ISeminarEmployeeBLL
    {
        public SeminarEmployeeBLL() { }
        public SeminarEmployeeBLL(ProviderDALTypes dalType) : base(dalType){ }
        public SeminarEmployeeBLL(IDAL externalDAL) : base(externalDAL) { }

        public GetItemResponse<SeminarEmployeeBOL> GetSeminarEmployeeById(int id)
        {
            var seminarEmployee = base.dal.GetSeminarEmployeeById(id);
            return new GetItemResponse<SeminarEmployeeBOL>(seminarEmployee);
        }

        public GetListResponse<SeminarEmployeeBOL> GetSeminarEmployeesBySeminarId(int seminarId)
        {
            var seminarEmployees = base.dal.GetSeminarEmployeesBySeminarId(seminarId);
            return new GetListResponse<SeminarEmployeeBOL>(seminarEmployees);
        }

        public void CreateSeminarEmployee(SeminarEmployeeBOL seminarEmployee)
        {
            base.dal.CreateSeminarEmployee(seminarEmployee);
        }

        public void DeleteSeminarEmployee(int id)
        {
            base.dal.DeleteSeminarEmployee(id);
        }
    }
}
