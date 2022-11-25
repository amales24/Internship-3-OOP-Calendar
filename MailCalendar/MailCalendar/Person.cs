using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCalendar
{
    internal class Person
    {
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        private Dictionary<Guid,bool> Attendance { get; set; }

        public Person(string name, string surname, string email, Dictionary<Guid,bool> attendance)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Attendance = attendance;
        }

        public bool IsAttending(Guid id)
        {
            return Attendance[id];
        }

        public void SetAttendanceToFalse(Guid id)
        {
            Attendance[id] = false;
        }

        public void RemoveEvent(Guid id)
        {
            Attendance.Remove(id);
        }

        public bool IsOverlaping(DateTime startDate, DateTime endDate, List<Event> eventsList)
        {
            Event myEvent;

            foreach (var eventId in Attendance.Keys)
            {
                myEvent = eventsList.Find(e => e.Id == eventId);

                if (startDate <= myEvent.StartDate && endDate > myEvent.StartDate ||
                    startDate > myEvent.StartDate && startDate < myEvent.EndDate)
                    return true; // it is enough to find one event that overlaps with the new one
            }
            return false;
        }

        public void AddEvent(Guid id)
        {
             Attendance.Add(id, true);
        }
    }
}
