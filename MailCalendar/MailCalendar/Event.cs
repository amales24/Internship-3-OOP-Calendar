using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailCalendar
{
    public class Event
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private List<string> Emails { get; }

        public Event(List<string> emails) 
        {
            Id = Guid.NewGuid();
            Emails = emails;
        }

        public List<string> GetEmails()
        {
            return Emails.AsReadOnly().ToList<string>();
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
            if (!Emails.Contains(mail))
                Console.WriteLine($"{mail} se ne nalazi na popisu sudionika ovog dogadaja!");
            else
                Emails.Remove(mail);
        }

        public bool IsThisAParticipant(string mail)
        {
            if (Emails.Contains(mail))
                return true;
            return false;
        }
    }
}
