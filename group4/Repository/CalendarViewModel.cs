using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Repository
{
    public class CalendarViewModel
    {
        ColorHandler colorHandler;
        public string Description { get; set; }
        public List<Lecture> lectures { get; set; }

        public CalendarViewModel()
        {
            lectures = new List<Lecture>();
            colorHandler = new ColorHandler();
        }
        public int Days()
        {
            if (IsNullOrEmpty(lectures))
                return 0;

            Lecture firstLecture = lectures.First<Lecture>();
            Lecture lastLecture = lectures.Last<Lecture>();
            TimeSpan timeSpan = lastLecture.startTime.Subtract(firstLecture.endTime);
            int days = timeSpan.Days;
            if (days == 0)
                days = 1;

            if (days < 7)
                days = 7;
            return days;
        }

        public int Weeks()
        {
            if (IsNullOrEmpty(lectures))
                return 0;

            TimeSpan timeSpan = lectures.Last().endTime - DateTime.Today.AddDays(-GetFirstDayOfWeek((DateTime.Today.DayOfWeek)));
            int weeks = timeSpan.Days / 7;

            return weeks+1;
        }

        public int GetFirstDayOfWeek(DayOfWeek firstDayInMonth)
        {
            int setBackDays = 0;
            switch (firstDayInMonth)
            {
                case DayOfWeek.Monday: setBackDays = 0; break;
                case DayOfWeek.Tuesday: setBackDays = 1; break;
                case DayOfWeek.Wednesday: setBackDays = 2; break;
                case DayOfWeek.Thursday: setBackDays = 3; break;
                case DayOfWeek.Friday: setBackDays = 4; break;
                case DayOfWeek.Saturday: setBackDays = 5; break;
                case DayOfWeek.Sunday: setBackDays = 6; break;
            }
            return setBackDays;
        }


        public bool IsToday(DateTime today)
        {
            return DateTime.Now.Date == today.Date ? true : false;
        }

        public DateTime GetFirstLectureDate()
        {
           if (IsNullOrEmpty(lectures))
               return DateTime.MinValue;
           return lectures.First<Lecture>().startTime;
        }

        public string GetColor(Lecture lecture)
        {
            string result = null;
            result = colorHandler.GetSavedColor(lecture);
            if (String.IsNullOrEmpty(result))
            {
                colorHandler.AddColor(lecture);
                result = colorHandler.GetSavedColor(lecture);
            }
            return result;
        }

        public List<Lecture> GetUniqueCourseList()
        {
            List<Lecture> result = new List<Lecture>();
            List<int> usedIDList = new List<int>();
            foreach (Lecture lec in lectures)
            {
                if (!usedIDList.Contains(lec.application.ID) && lec.application.Code != 1)
                {
                    result.Add(lec);
                    usedIDList.Add(lec.application.ID);
                }
            }
            return result;
        }

        private bool IsNullOrEmpty(List<Lecture> listOfLectures)
        {
            return listOfLectures == null || listOfLectures.Count == 0;
        }

        public void AddBreaksBetweenLectures()
        {
            if (lectures.Count == 0)
                return;
            DateTime startTime = DateTime.MinValue, endTimeHigh = new DateTime(lectures[0].startTime.Year, lectures[0].startTime.Month, lectures[0].startTime.Day, 8, 15, 00);
            TimeSpan minBreakTime = TimeSpan.FromMinutes(30);
            List<Lecture> breaks = new List<Lecture>();
            foreach (Lecture lec in lectures)
            {
                startTime = lec.startTime;
                if ((startTime - endTimeHigh) >= minBreakTime)
                    if (startTime.Hour >= 8 && endTimeHigh.Hour <= 17 )
                    {
                        if(endTimeHigh.Date == startTime.Date)
                            breaks.Add(Lecture.Break(endTimeHigh, startTime));
                        else
                        {
                            DateTime end = new DateTime(endTimeHigh.Year, endTimeHigh.Month, endTimeHigh.Day, 17, 00, 00);
                            if ((end - endTimeHigh).TotalMinutes >= 30)
                                breaks.Add(Lecture.Break(endTimeHigh, end));
                            if (lec.startTime.Hour >= 8)
                            {
                                DateTime start = new DateTime(startTime.Year, startTime.Month, startTime.Day, 8, 15, 00);
                                if ((startTime - start).TotalMinutes >=30) 
                                    breaks.Add(Lecture.Break(start, startTime));
                            }
                        }
                    }
                if (lec.endTime > endTimeHigh)
                    endTimeHigh = lec.endTime;
                if (lectures.IndexOf(lec) == (lectures.Count-1))
                {
                    startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 17, 00, 00);
                    if ((startTime - lec.endTime).Minutes >= 30)
                        breaks.Add(Lecture.Break(lec.endTime, startTime));
                }
            }
            lectures.AddRange(breaks);
            lectures.Sort(new SortLecturesOnStartTime());
        }

        public void AddExams_New(List<Lecture> exams, String courseCode)
        {
            foreach (Lecture exam in exams)
            {
                if (exam.course == courseCode)
                {
                    exam.isExam = true;
                    lectures.Add(exam);
                }
            }
            lectures.Sort(new SortLecturesOnStartTime());
        }

        public void AddExams(List<Lecture> exams)
        {
            foreach(Lecture exam in exams)
            {
                if(ExamBelongToLectures(exam))
                {
                    exam.isExam = true;
                    lectures.Add(exam);
                }
            }
            lectures.Sort(new SortLecturesOnStartTime());
        }

        public bool ExamBelongToLectures(Lecture exam)
        {
            foreach (Lecture lecture in lectures)
            {
                if (lecture.course.Contains(exam.course))
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetCourseCode(List<Domain.Lecture> exams, string appCode)
        {
            CodeHandler ch = new CodeHandler();
            foreach (Lecture exam in exams)
            {
                foreach (Application app in ch.GetApplicationCodeList(exam.course))
                {
                    if (app.Code.ToString().Equals(appCode))
                    {
                        return exam.course;
                    }
                }
            }
            return "undefined";
        }
    }
}