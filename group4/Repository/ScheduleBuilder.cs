using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ScheduleBuilder
    {

        private CodeHandler codeHandler;

        public ScheduleBuilder()
        {
            codeHandler = new CodeHandler();
        }

        public List<Lecture> LecturesFromApplicationCode(string applicationCode)
        {
            List<Application> applicationCodes = codeHandler.ConvertStringsToApplications(applicationCode);
            List<Schedule> schedules = GetAllSchedules(applicationCodes);

            Merger merger = new Merger();
            Schedule schedule = merger.Merge(schedules);
            schedule.Sort();
            return schedule.Lectures;
        }

        public List<Schedule> GetSchedulesFrom(List<Application> applicationCode)
        {
            CodeHandler search = new CodeHandler();
            List<Schedule> result = new List<Schedule>();
            applicationCode = applicationCode.GroupBy(x => x.Code).Select(s => s.First()).ToList();
            for (int i = 0; i < applicationCode.Count; i++)
                result.Add(CreateScheduleFromUrl(search.GetScheduleURL(applicationCode[i].Code), applicationCode[i]));
            return result;
        }

        private void removeDoubleApplicationCodes(List<Application> applicationCode)
        {
            
        }

        private Schedule CreateScheduleFromUrl(string urlToSchedule, Application application)
        {
            CsvParser csvParser = new CsvParser();
            Schedule schedule = new Schedule();
            Filehandler fileHandler = new Filehandler();
            if (fileHandler.GetFileFromUrl(urlToSchedule) != null)
                schedule.Build(csvParser.Parse(fileHandler.GetFileFromUrl(urlToSchedule)), application);
            return schedule;
        }

        public List<Application> RemoveEmptyApplicationCodes(List<Application> applicationCodes)
        {
            List<Application> result = new List<Application>();
            List<Schedule> schedules = GetSchedulesFrom(applicationCodes);
            int j = 0;
            foreach (Schedule s in schedules)
            {
                if (!s.IsEmpty())
                    result.Add(applicationCodes[j]);
                j++;
            }
            return result;
        }

        private List<Schedule> GetAllSchedules(List<Application> applicationCodes)
        {
            CodeHandler search = new CodeHandler();
            List<Schedule> result = new List<Schedule>();
            applicationCodes = applicationCodes.GroupBy(x => x.Code).Select(s => s.First()).ToList();
            foreach (Application applicationCode in applicationCodes)
                result.Add(CreateScheduleFromUrl(search.GetScheduleURL(applicationCode.Code), applicationCode));
            return result;
        }
    }
}
