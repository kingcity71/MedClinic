using MedClinic.Entity;
using MedClinic.Model;
using System;
using System.Collections.Generic;

namespace MedClinic.Interfaces
{
    public interface IPatientDataService
    {
        IEnumerable<Property> GetProps();
        IEnumerable<PatientDataModel> GetBySchedId(Guid schedId);
        PatientDataModel GetById(Guid guid);
        PatientDataModel Update(PatientDataModel model);
        PatientDataModel Create(PatientDataModel model);
        void Remove(Guid id);
    }
}
