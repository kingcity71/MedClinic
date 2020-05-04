using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedClinic.Services
{
    public class PatientDataService : IPatientDataService
    {
        private readonly MedClinicContext context;

        public PatientDataService(MedClinicContext context)
        {
            this.context = context;
        }
        public PatientDataModel Create(PatientDataModel model)
        {
            var patientData = new PatientData()
            {
                Date = model.Date,
                Id = Guid.NewGuid(),
                PatientId = model.PatientId,
                ScheduleId = model.ScheduleId,
                PropertyId = model.PropertyId,
                Value = model.PropValue
            };
            model.Id = patientData.Id;
            context.PatientDatas.Add(patientData);
            context.SaveChanges();
            return model;
        }
        public PatientDataModel GetById(Guid guid)
        {
            var data = context.PatientDatas.FirstOrDefault(x => x.Id == guid);
            if (data == null) return new PatientDataModel();
            var props = context.Properties.ToList();
            return new PatientDataModel()
            {
                Date = data.Date,
                ScheduleId = data.ScheduleId,
                Id = data.Id,
                PatientId = data.PatientId,
                PropertyId = data.PropertyId,
                PropValue = data.Value,
                PropName = props.FirstOrDefault(x => x.Id == data.PropertyId).Name
            };
        }
        public IEnumerable<PatientDataModel> GetBySchedId(Guid schedId)
        {
            var data = context.PatientDatas
                .Where(x => x.ScheduleId == schedId)
                .ToList();
            var properties = context.Properties.ToList();
            var models = data.Select(x => new PatientDataModel()
            {
                Date = x.Date,
                ScheduleId = x.ScheduleId,
                Id = x.Id,
                PatientId = x.PatientId,
                PropertyId = properties.FirstOrDefault(y => y.Id == x.PropertyId).Id,
                PropName = properties.FirstOrDefault(y => y.Id == x.PropertyId).Name,
                PropValue = x.Value
            }).ToList();
            return models;
        }

        public IEnumerable<Property> GetProps() => context.Properties.ToList();

        public void Remove(Guid id)
        {
            var data = context.PatientDatas.FirstOrDefault(x => x.Id == id);
            context.PatientDatas.Remove(data);
        }
        public PatientDataModel Update(PatientDataModel model)
        {
            var data = context.PatientDatas.FirstOrDefault(x => x.Id == model.Id);
            data.Value = model.PropValue;
            data.PropertyId = model.PropertyId;
            data.Date = model.Date;
            context.SaveChanges();
            return model;
        }
    }
}
