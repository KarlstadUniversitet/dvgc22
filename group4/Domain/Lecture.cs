using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    /// <summary>
    /// Klass som ärver från CalenderPost som ska specifiera en lektion i kalendern. Har förutom CalenderPosts properties även properties för 
    /// lärare, lektionssal samt kurs.
    /// </summary>
    public class Lecture :  IComparable<Lecture>
    {
        public string info { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string teacher { get; set; }
        public string classroom { get; set; }
        public string course { get; set; }
        public Application application { get; set; }
        public bool isExam { get; set; }

        public static Lecture Break(DateTime startTime, DateTime endTime)
        {
            Lecture breakLecture = new Lecture();
            breakLecture.startTime = startTime;
            breakLecture.endTime = endTime;
            breakLecture.application = new Application(1);
            breakLecture.application.CourseCode = "Breaks";
            breakLecture.course = BreakTime(startTime, endTime);
            breakLecture.application.ID = -1;

            return breakLecture;
        }

        private static string BreakTime(DateTime startTime, DateTime endTime)
        {
            string result = "Break (";
            if (startTime.Hour == 12 && endTime.Hour == 13)
                result = "Lunch (";
            TimeSpan breakTime = endTime - startTime;
            if (breakTime.Hours == 0)
                result += breakTime.Minutes + "m)";
            else if (breakTime.Minutes == 0)
                result += breakTime.Hours + "h)";
            else
                result += breakTime.Hours + "h " + breakTime.Minutes + "m)"; 
            return result;
        }

        /// <summary>
        /// Bygger en hel lektion
        /// </summary>
        /// <param name="post">Varje post i arrayen innehåller ett attribut</param>
        /// <returns>Lektionen som byggts</returns>
        public static Lecture buildLecture(String[] post, Application application)
        {
            Lecture lecture = new Lecture();
            DateTime startDateTime = AddDate(post[0]);
            startDateTime = AddTime(startDateTime, post[1]);
            DateTime endDateTime = AddDate(post[2]);
            endDateTime = AddTime(endDateTime, post[3]);
            lecture.startTime = startDateTime;
            lecture.endTime = endDateTime;
            lecture.classroom = post[4].Trim();
            lecture.teacher = post[5].Trim('"');
            lecture.course = post[6].Trim();
            lecture.info = post[7].Trim('"');
            lecture.application = application;

            return lecture;
        }

        /// <summary>
        /// Tar in en Sträng. Splittar strängen och lägger in korrekt datum i Datetime objektet.
        /// Plockar även ut år, mån, dag från dateTime objektet och lägger in detta i den nya.
        /// </summary>
        /// <param name="dateTime">DateTime objekt som ska fyllas med datum</param>
        /// <param name="dateInput">Sträng av formatet yyyy-mm-dd</param>
        /// <returns>Ett nytt DateTime objekt med år, månad och dag specificerad</returns>
        private static DateTime AddDate(String dateInput)
        {
            String[] date = new String[3];
            date = dateInput.Split('-');
            return new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
        }
        /// <summary>
        /// Tar in ett DateTime objekt och en Sträng. Splittar strängen och lägger in korrekt tid i ett nytt DateTime objekt.
        /// Plockar även ut år, mån, dag från dateTime objektet och lägger in detta i den nya.
        /// </summary>
        /// <param name="dateTime">DateTime objekt som ska fyllas med Tid</param>
        /// <param name="timeInput">Sträng av formatet hh:mm</param>
        /// <returns>Ett nytt DateTime objekt med år, månad, dag, hours och minuites specificerad</returns>
        private static DateTime AddTime(DateTime dateTime, String timeInput)
        {
            String[] time = new String[2];
            time = timeInput.Split(':');
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(time[0]), int.Parse(time[1]), 0);
        }

        public int CompareTo(Lecture lecture)
        {
            return this.startTime.CompareTo(lecture.startTime);
        }

        public string getExamStatus()
        {
            if (this.isExam)
            {
                return "examBackground";
            }
            return "";
        }
    }

    /// <summary>
    /// Klass som hanterar sortering av lektioner, måste implementeras för att kunna använda ICompareble.
    /// </summary>
    public class SortLecturesOnStartTime : IComparer<Lecture>
    {
        public int Compare(Lecture lecture1, Lecture lecture2)
        {
            if (lecture1.startTime > lecture2.startTime)
                return 1;
            else if (lecture1.startTime < lecture2.startTime)
                return -1;
            else
                return 0;
        }
    }
}