using CoreLib.Definitions;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarEmployee;
using DataAccess.BOL.SeminarParticipant;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface ISeminarEmployeeDAL: IDAL
    {
        SeminarEmployeeBOL GetSeminarEmployeeById(int id);
        List<SeminarEmployeeBOL> GetSeminarEmployeesBySeminarId(int seminarId);

        void CreateSeminarEmployee(SeminarEmployeeBOL seminarEmployee);
        void DeleteSeminarEmployee(int id);
    }
}
