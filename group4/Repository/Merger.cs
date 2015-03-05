using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository
{
    public class Merger
    {
        public Schedule Merge(List<Schedule> schedules)
        {
            Schedule result = new Schedule();
            foreach (Schedule schedule in schedules)
                foreach (Lecture lecture in schedule.Lectures)
                    result.Lectures.Add(lecture);
            return result;
        }
    }
}
