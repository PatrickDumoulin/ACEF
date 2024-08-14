using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface ISeminarEmployeeBLL : IBLL
    {
        GetItemResponse<SeminarEmployeeBOL> GetSeminarEmployeeById(int id);
        GetListResponse<SeminarEmployeeBOL> GetSeminarEmployeesBySeminarId(int seminarId);
    }
}
