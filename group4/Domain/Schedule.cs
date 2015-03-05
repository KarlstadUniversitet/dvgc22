using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    /// <summary>
    /// Work in progress. En klass som just nu bara innehåller en lista med lektioner. Kommer antagligen senare innehålla mer?
    /// </summary>
    public class Schedule
    {
        // Konstant som innehåller värdet på antalet rader som skall tas bort, då dessa innehåller irrelevant data.
        public const int REM_UP_TO_THIS_INDEX = 4;

        // Lista med lektioner.
        public List<Lecture> Lectures { get; set; }

        public Schedule()
        {
            Lectures = new List<Lecture>();
        }
        
        /// <summary>
        /// Bygger schemat för en specifik instans av ett schema.
        /// </summary>
        /// <param name="posts">Datan som används för att fylla schemat</param>
        public void Build(List<String[]> posts, Application application)
        {
            posts.RemoveRange(0, REM_UP_TO_THIS_INDEX);
            foreach (String[] post in posts)
            {
                if (post.Length > 5)
                    AddLecture(Lecture.buildLecture(post, application));
            }
        }
        /// <summary>
        /// Lägger till en lektion till ett schema.
        /// </summary>
        /// <param name="lecture">Lektionen som skall läggas till</param>
        private void AddLecture(Lecture lecture)
        {
            Lectures.Add(lecture);
        }

        public bool IsEmpty()
        {
            return Lectures.Count == 0 ? true : false;
        }

        public void Sort()
        {
            this.Lectures.Sort(new SortLecturesOnStartTime());
           
        }
    }
}