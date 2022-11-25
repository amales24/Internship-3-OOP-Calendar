using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCalendar
{
    internal class Event
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private List<string> Emails { get; set; }

        public Event(List<string> emails) 
        {
            Id = Guid.NewGuid();
            Emails = emails;
        }

        public List<string> GetEmails()
        {
            return Emails.AsReadOnly().ToList<string>(); // this prevents adding or removing elements outside the class
        }

        public Status EventStatus()
        {
            if (StartDate < DateTime.Now && DateTime.Now < EndDate)
                return Status.Aktivni;
            else if (StartDate > DateTime.Now)
                return Status.Buduci;
            else
                return Status.Prosli;
        }

        public void RemoveEmails(string mail)
        {
            Emails.Remove(mail);
        }
    }
}
