using MedClinic.Entity;
using MedClinic.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Interfaces
{
    public interface IScheduleService
    {
        Schedule GetSchedule(Guid id);
        List<Schedule> GetDoctorDaySchedule(DateTime date, Guid doctorId);
        List<Schedule> GetDaySchedule(DateTime date, Guid specializationId);
        List<ScheduleTimeModel> GetTimeSchedule(DateTime dateTime, Guid specializationId);
        void MakeAppointment(Guid schedId, Guid patientId);
        void CancelAppointment(Guid schedId);
        void UpdateAppointment(ScheduleTimeModel model);
        void RemoveAppointment(Guid schedId);
        bool IsAppointmentExist(DateTime date, Guid doctorId);
        bool IsTimeToMakeAppointment(Guid patientId, DateTime date);
    }
}
