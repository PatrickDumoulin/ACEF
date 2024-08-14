using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarEmployee;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Client;
using DataModels.BOL.SeminarEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class SeminarEmployeeEntityDAL : AcefEntityDAL, ISeminarEmployeeDAL
    {
        public SeminarEmployeeEntityDAL() { }

        public SeminarEmployeeEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public SeminarEmployeeBOL GetSeminarEmployeeById(int id)
        {
            SeminarsEmployees record = Db.SeminarsEmployees.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<SeminarEmployeeBOL>(record);
        }

        public List<SeminarEmployeeBOL> GetSeminarEmployeesBySeminarId(int seminarId)
        {
            var records = Db.SeminarsEmployees
                .Where(x => x.IdSeminar == seminarId)
                .ToList();
            return records.Select(record => new SeminarEmployeeBOL(record)).ToList();
        }
    }
}
