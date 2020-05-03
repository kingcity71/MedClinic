using MedClinic.Entity;
using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Interfaces
{
    public interface IScheduleService
    {
        List<Schedule> GetDaySchedule(DateTime date, Guid specializationId);
        List<ScheduleTimeModel> GetTimeSchedule(DateTime dateTime, Guid specializationId);
        void MakeAppointment(Guid schedId, Guid patientId);
    }
}
