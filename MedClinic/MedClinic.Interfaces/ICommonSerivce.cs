﻿using MedClinic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Interfaces
{
    public interface ICommonSerivce
    {
        int[][] GetCalendarMatrix(DateTime date);
        Specialization GetSpecialization(Guid id);
    }
}
