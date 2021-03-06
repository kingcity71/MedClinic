﻿using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedClinic.Services
{
    public class CommonService : ICommonSerivce
    {
        private readonly MedClinicContext context;

        public CommonService(MedClinicContext context)
        {
            this.context = context;
        }
        public Specialization GetSpecialization(Guid id)
        {
            return context.Specializations.FirstOrDefault(x => x.Id == id);
        }
        public int[][] GetCalendarMatrix(DateTime date)
        {
            var dayOfWeek = (int)new DateTime(date.Year, date.Month, 1).DayOfWeek;

            var difference = (dayOfWeek != 0 ? dayOfWeek : 7) - 1;

            var daysInMonth = Enumerable.Range(0, difference).Select(x => 0).ToList();
            daysInMonth.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month)).ToList());
            daysInMonth.AddRange(Enumerable.Range(daysInMonth.Last(),
                                    daysInMonth.Count() <= 7 * 5
                                            ? 7 * 5 - daysInMonth.Count()
                                            : 7 * 6 - daysInMonth.Count()).Select(x => 0));

            var result = daysInMonth.Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / 7)
            .Select(x => x.Select(v => v.Value).ToArray())
            .ToArray();

            return result;
        }
    }
}
