using MedClinic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Interfaces
{
    public interface IScheduleService
    {
        List<Schedule> GetDaySchedule(DateTime date, Guid specializationId);
    }
}
