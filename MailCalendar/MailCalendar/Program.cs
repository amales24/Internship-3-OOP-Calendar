using MailCalendar;

var eventsList = new List<Event>()
{
    new Event(new List<string>{"antonia@gmail.com", "andricandrea@gmail.com", "stipic_stipe@gmail.com", "anaanic@gmail.com"})
    {
        Name = "Team Building",
        Location = "Rizvan, Lika",
        StartDate = new DateTime(2022,11,28),
        EndDate = new DateTime(2022,12,3)
    },
        new Event(new List<string>{"antonia@gmail.com", "petrica@petric.com", "ddoric@gmail.com"})
    {
        Name = "Seminar o kriptografiji",
        Location = "PMF, Zagreb",
        StartDate = new DateTime(2023, 2, 18, 12,0,0),
        EndDate = new DateTime(2023, 2, 18, 15, 30, 0)
    },
            new Event(new List<string>{"petrica@petric.com", "ddoric@gmail.com","kate@katic.hr","luketic@pmfst.hr"})
    {
        Name = "ENTER konferencija",
        Location = "Spaladium Arena, Split",
        StartDate = new DateTime(2022,12,2,15,0,0),
        EndDate = new DateTime(2022,12,3,20,0,0)
    },
                new Event(new List<string>{"antonia@gmail.com","andricandrea@gmail.com","kate@katic.hr",
                    "petrica@petric.com", "ddoric@gmail.com"})
    {
        Name = "Bozicna vecera",
        Location = "Kampus, Split",
        StartDate = new DateTime(2022,12,22,20,0,0),
        EndDate = new DateTime(2022,12,23,2,0,0)
    },
                    new Event(new List<string>{"anaanic@gmail.com", "maja@majic.hr",
                        "stipic_stipe@gmail.com","ante@antic.com"})
    {
        Name = "DUMP predavanje",
        Location = "FESB, Split",
        StartDate = new DateTime(2022,11,10,12,15,0),
        EndDate = new DateTime(2022,11,10,15,0,0)
    }
};

var peopleList = new List<Person>()
{
    new Person("Antonia","Antonic","antonia@gmail.com", new Dictionary<Guid, bool>()
    {
        {eventsList[0].Id,true},
        {eventsList[1].Id,true},
        {eventsList[3].Id,true}
    }),
    new Person("Andrea","Andric","andricandrea@gmail.com", new Dictionary<Guid, bool>()
    {
        {eventsList[0].Id,true},
        {eventsList[3].Id,true}
    }),
    new Person("Ana","Anic","anaanic@gmail.com", new Dictionary<Guid, bool>()
    {
        {eventsList[0].Id,true},
        {eventsList[4].Id,false}
    }),
    new Person("Ante","Antic","ante@antic.com", new Dictionary<Guid, bool>()
    {
        {eventsList[4].Id,true}
    }),
    new Person("Maja","Majic","maja@majic.hr", new Dictionary<Guid, bool>()
    {
        {eventsList[4].Id,true}
    }),
    new Person("Stipe","Stipic","stipic_stipe@gmail.com", new Dictionary<Guid, bool>()
    {
        {eventsList[0].Id,true},
        {eventsList[4].Id,false}
    }),
    new Person("Luka","Luketic","luketic@pmfst.hr", new Dictionary<Guid, bool>()
    {
        {eventsList[2].Id,true}
    }),
    new Person("Petra","Petric","petrica@petric.com", new Dictionary<Guid, bool>()
    {
        {eventsList[1].Id,true},
        {eventsList[2].Id,true},
        {eventsList[3].Id,true}
    }),
    new Person("Dora","Doric","ddoric@gmail.com", new Dictionary<Guid, bool>()
    {
        {eventsList[1].Id,true},
        {eventsList[2].Id,true},
        {eventsList[3].Id,true}
    }),
    new Person("Kate","Katic","kate@katic.hr", new Dictionary<Guid, bool>()
    {
        {eventsList[2].Id,true},
        {eventsList[3].Id,true}
    })
};

string StartMenu()
{
    Console.WriteLine("1 - Aktivni eventi \n2 - Nadolazeci eventi \n3 - Eventi koji su zavrsili " +
        "\n4 - Kreiraj event \n0 - Izadi iz programa");

    var myOptions = new List<string>() { "0", "1", "2", "3", "4" };
    var myChoice = Input(myOptions);

    Console.Clear();
    return myChoice;
}

string Input(List<string> myOptions)
{
    var myChoice = Console.ReadLine().Trim().ToUpper();

    while (!myOptions.Contains(myChoice))
    {
        Console.WriteLine("Ta opcija ne postoji, pokusajte ponovno:");
        myChoice = Console.ReadLine().Trim().ToUpper(); 
    }

    return myChoice;
}

void ReturnToStartMenu()
{
    Console.WriteLine("\nP - Povratak na glavni menu \n0 - Izlazak iz aplikacije");

    var myChoice = Input(new List<string>() { "P", "0" });

    switch(myChoice)
    {
        case "0":
            Console.Clear();
            Console.WriteLine("Aplikacija zatvorena!");
            Environment.Exit(0);
            break;
        case "P":
            break;
    }
}

string myChoice;
do
{
    Console.Clear();
    myChoice = StartMenu();

    switch (myChoice)
    {
        case "0":
            Console.Clear();
            Console.WriteLine("Aplikacija zatvorena!");
            Environment.Exit(0); 
            break;
        case "1":
            ActiveEvents();
            break;
        case "2":
            FutureEvents();
            break;
        case "3":
            PastEvents();
            break;
        case "4":
            CreateEvent();
            break;
    }
} while (true);

void ActiveEvents()
{
    Console.WriteLine("Svi trenutno aktivni eventi:\n");
    double endsIn;
    bool flag = false; 

    foreach (var e in eventsList)
    {
        if (e.EventStatus() == Status.Aktivni)
        {
            flag = true;
            endsIn = Math.Round((e.EndDate - DateTime.Now).TotalHours, 1);

            Console.WriteLine($"> Id: {e.Id} \n" +
                              $"> Naziv: {e.Name} - Lokacija: {e.Location} - Ends in: {endsIn} hours \n" +
                              $"> Popis sudionika: {(e.GetEmails().Count > 0 ? string.Join(", ", e.GetEmails()) : "Nema ih")} \n"); 
        }
    }

    if (!flag)
    {
        Console.WriteLine("Nema ih!");
        ReturnToStartMenu();
    }
    else
    {
        Console.WriteLine("SUBMENU \n1 - Zabiljezi neprisutnosti \nP - Povratak na glavni menu");

        var myChoice = Input(new List<string>() { "1", "P" });

        if (myChoice == "1")
            AbsenceRecord();
    }
}

void AbsenceRecord()
{
    Console.WriteLine("\nUnesite Id nekog aktivnog dogadaja na kojem zelite biljeziti neprisutnost:");
    var myEvent = InputEventId(Status.Aktivni);

    if (myEvent.GetEmails().Count == 0)
        Console.WriteLine("\nNema sudionika na ovom dogadaju!");
    else
    {
        Console.WriteLine("\nUnesite mailove osoba kojima zelite zabiljeziti neprisutnost, odvojite ih zarezom (bez razmaka):");
        var myMail = GetExistentMail(myEvent.GetEmails(), InputMail());

        if (myMail.Count == 0)
            Console.WriteLine("\nNiste unijeli nijednog sudionika ovog dogadaja!");
        else
        {
            Console.WriteLine($"\nJeste li sigurni da zelite zabiljeziti neprisutnost sljedecim osobama: {string.Join(", ", myMail)}?");
            if (ConfirmDialogue())
            {
                Person myPerson;
                foreach (var mail in myMail)
                {
                    myPerson = peopleList.Find(p => p.Email == mail);
                    myPerson.SetAttendanceToFalse(myEvent.Id);
                }
                Console.WriteLine("\nRadnja uspjesno izvrsena!");
            }
            else
            {
                Console.WriteLine("\nRadnja zaustavljena!");
            }
        }
    }

    ReturnToStartMenu();
}

Event InputEventId(Status myEventStatus)
{
    var myEventId = Console.ReadLine().Trim();
    Event myEvent;

    while (true)
    {
        myEvent = eventsList.Find(e => e.Id.ToString() == myEventId);

        if (myEvent == null)
        {
            Console.WriteLine("\nDogadaj s tim id-em ne postoji, pokusajte ponovno:");
            myEventId = Console.ReadLine().Trim();
        }
        else if (myEvent.EventStatus() != myEventStatus)
        {
            Console.WriteLine($"\nOvo nije {myEventStatus.ToString().ToLower()} dogadaj, pokusajte ponovno:");
            myEventId = Console.ReadLine().Trim();
        }
        else
            break;
    }

    return myEvent;
}

List<string> InputMail()
{
    var myInput = Console.ReadLine().Trim();

    while (true)
    {
        if (myInput == "" || myInput == ",")
        {
            Console.WriteLine("\nNiste nikoga unijeli, pokusajte ponovno:");
            myInput = Console.ReadLine().Trim();
        }
        else if (myInput.Contains(" ") || myInput.Contains(",,"))
        {
            Console.WriteLine("\nPogresan unos, mailovi ne smiju sadrzavati razmake ni zareze, pokusajte ponovno:");
            myInput = Console.ReadLine().Trim();
        }
        else
            break;
    }

    return myInput.Split(",").ToList<string>();
}

List<string> GetExistentMail(List<string> eventMail, List<string> myMail)
{ 
    var existingMail = new List<string>() { };
    var nonExistentMail = new List<string>() { };

    foreach (var mail in myMail ) 
    {
        if (eventMail.Contains(mail))
            existingMail.Add(mail);
        else
            nonExistentMail.Add(mail);
    }
    
    if (nonExistentMail.Count > 0)
        Console.WriteLine($"\nOsobe s mailom: {string.Join(", ", nonExistentMail)} ne nalaze se na popisu!");

    return existingMail;
}

bool ConfirmDialogue()
{
    Console.WriteLine("\nY - Zelim \nN - Ne zelim");
    var myChoice = Input(new List<string>() { "Y", "N" });

    return myChoice == "Y";
}

void FutureEvents()
{
    Console.WriteLine("Svi nadolazeci eventi:\n");
    int startsIn;
    double lastsFor;
    bool flag = false;

    foreach (var e in eventsList)
    {
        if (e.EventStatus() == Status.Buduci)
        {
            flag = true;
            startsIn = (e.StartDate - DateTime.Now).Days;
            lastsFor = Math.Round((e.EndDate - e.StartDate).TotalHours, 1);

            Console.WriteLine($"> Id: {e.Id} \n" +
                              $"> Naziv: {e.Name} - Lokacija: {e.Location} - Pocinje za {startsIn} dana - Trajanje : {lastsFor} sati\n" +
                              $"> Popis sudionika: {(e.GetEmails().Count > 0 ? string.Join(", ", e.GetEmails()) : "Nema ih")} \n");
        }
    }

    if (!flag)
    {
        Console.WriteLine("Nema ih!");
        ReturnToStartMenu();
    }
    else
    {
        Console.WriteLine("SUBMENU \n1 - Izbrisi event \n2 - Ukloni osobe s eventa \nP - Povratak na glavni menu");

        var myChoice = Input(new List<string>() { "1", "2", "P" });

        switch (myChoice)
        {
            case "1":
                DeleteEvent();
                break;
            case "2":
                RemovePeople();
                break;
            case "P":
                break;
        }
    }
}

void DeleteEvent()
{
    Console.WriteLine("\nUnesite Id nekog nadolazeceg dogadaja kojeg zelite izbrisati:");
    var myEvent = InputEventId(Status.Buduci);

    Console.WriteLine($"\nJeste li sigurni da zelite izbrisati dogadaj {myEvent.Name} koji pocinje za " +
        $"{(myEvent.StartDate - DateTime.Now).Days} dana na lokaciji {myEvent.Location}?");

    if (!ConfirmDialogue())
        Console.WriteLine("\nRadnja zaustavljena!");
    else
    {
        Person myPerson;

        eventsList.Remove(myEvent);

        foreach (var mail in myEvent.GetEmails())
        {
            myPerson = peopleList.Find(p => p.Email == mail);
            myPerson.RemoveEvent(myEvent.Id);
        }
        Console.WriteLine("\nRadnja uspjesno izvrsena! Izbrisan je dogadaj i podatci o prisutnosti sudionika.");
    }

    ReturnToStartMenu();
}

void RemovePeople()
{
    Console.WriteLine("\nUnesite Id nekog nadolazeceg dogadaja kojem zelite ukloniti sudionike:");
    var myEvent = InputEventId(Status.Buduci);

    if (myEvent.GetEmails().Count == 0)
        Console.WriteLine("\nNema sudionika na ovom dogadaju!");
    else
    {
        Console.WriteLine("\nUnesite mailove osoba koje zelite ukloniti s eventa:");
        var myMail = GetExistentMail(myEvent.GetEmails(), InputMail());

        if (myMail.Count == 0)
            Console.WriteLine("\nNiste unijeli nijednog sudionika ovog dogadaja!");
        else
        {
            Console.WriteLine($"\nJeste li sigurni da zelite ukloniti sljedece osobe: {string.Join(", ", myMail)}?");

            if (ConfirmDialogue())
            {
                Person myPerson;

                foreach (var mail in myMail)
                {
                    myEvent.RemoveEmails(mail);

                    myPerson = peopleList.Find(p => p.Email == mail);
                    myPerson.RemoveEvent(myEvent.Id);
                }
                Console.WriteLine("\nRadnja uspjesno izvrsena!");
            }
            else
                Console.WriteLine("\nRadnja zaustavljena!");
        }
    }

    ReturnToStartMenu();
}

void PastEvents()
{
    Console.WriteLine("Svi eventi koji su zavrsili:\n");

    int endedDaysAgo;
    double lastsFor;
    Person myPerson;
    List<string> mailsOfAttendingParticipants, mailsOfAbsentParticipants;
    bool flag = false;

    foreach (var e in eventsList)
    {
        if (e.EventStatus() == Status.Prosli)
        {
            flag = true;
            endedDaysAgo = (DateTime.Now - e.EndDate).Days;
            lastsFor = Math.Round((e.EndDate - e.StartDate).TotalHours, 1);

            mailsOfAttendingParticipants = new List<string>() { };
            mailsOfAbsentParticipants = new List<string>() { };

            foreach (var mail in e.GetEmails())
            {
                myPerson = peopleList.Find(p => p.Email == mail);
                if (myPerson.IsAttending(e.Id))
                    mailsOfAttendingParticipants.Add(mail);
                else
                    mailsOfAbsentParticipants.Add(mail);
            }

            Console.WriteLine($"> Id: {e.Id} \n" +
                              $"> Naziv: {e.Name} - Lokacija: {e.Location} - Zavrsilo prije {endedDaysAgo} dana - Trajanje : {lastsFor} sati\n" +
                              $"> Popis prisutnih sudionika: " +
                              $"{(mailsOfAttendingParticipants.Count > 0 ? string.Join(", ", mailsOfAttendingParticipants) : "Nema ih")} \n" +
                              $"> Popis ne prisutnih sudionika: " +
                              $"{(mailsOfAbsentParticipants.Count > 0 ? string.Join(", ", mailsOfAbsentParticipants) : "Nema ih")} \n");
        }
    }
    if (!flag)
        Console.WriteLine("Nema ih!");

    ReturnToStartMenu();
}

void CreateEvent()
{
    Console.WriteLine("Unesite naziv eventa:");
    var myEventName = InputName();

    Console.WriteLine("\nUnesite naziv lokacije:");
    var myLocationName = InputName();

    Console.WriteLine("\nUnesite datum pocetka eventa (predlazem format godina/mjesec/dan sati:minute:sekunde):");
    var myStartDate = InputDate(DateTime.Now);

    Console.WriteLine("\nUnesite datum kraja eventa (predlazem format godina/mjesec/dan sati:minute:sekunde):");
    var myEndDate = InputDate(myStartDate);

    Console.WriteLine("\nUnesite mailove sudionika, razdvojite ih zarezom (bez razmaka):");
    var myMail = InputMail();

    var myPeople = new List<Person>() { };
    var nonExistent = new List<string>() { };
    var available = new List<string>() { };
    var overlaping = new List<string>() { };

    Person myPerson;

    foreach (var mail in myMail)
    {
        myPerson = peopleList.Find(p => p.Email == mail);

        if (myPerson == null)
        {
            nonExistent.Add(mail);
        }
        else if (myPerson.IsOverlaping(myStartDate, myEndDate, eventsList))
        {
            overlaping.Add(mail);
        }
        else
        {
            myPeople.Add(myPerson);
            available.Add(mail);
        }
    }

    if (nonExistent.Count > 0)
        Console.WriteLine($"\nOsobe s mailom {string.Join(", ",nonExistent)} ne nalaze se na popisu osoba!");
    if (overlaping.Count > 0)
        Console.WriteLine($"\nOsobe s mailom {string.Join(", ", overlaping)} imaju preklapanja s nekim drugim dogadajima!");

    Console.WriteLine($"\nJeste li sigurni da zelite dodati sljedeci dogadaj: \n" +
        $"Naziv: {myEventName} \nLokacija: {myLocationName} \nTrajanje: {myStartDate} - {myEndDate} \n" +
        $"Popis sudionika: {(available.Count > 0 ? string.Join(", ",available) : "Nema ih")} ?");

    if (!ConfirmDialogue())
        Console.WriteLine("\nRadnja zaustavljena!");
    else
    {
        var myEvent = new Event(available)
        {
            Name = myEventName,
            Location = myLocationName,
            StartDate = myStartDate,
            EndDate = myEndDate
        };

        eventsList.Add(myEvent);

        foreach (var person in myPeople)
        {
            person.AddEvent(myEvent.Id);
        }
        Console.WriteLine("\nRadnja uspjesno izvrsena!");
    }

    ReturnToStartMenu();
}

string InputName()
{
    var myName = Console.ReadLine().Trim();

    while (myName == "")
    {
        Console.WriteLine("\nNiste nista unijeli, pokusajte ponovno:");
        myName = Console.ReadLine().Trim();
    }

    return myName;
}

DateTime InputDate(DateTime lowerLimit)
{
    string myDateString;
    DateTime myDate;

    while (true)
    {
        myDateString = Console.ReadLine().Trim();

        try
        {
            myDate = DateTime.Parse(myDateString);
            if (myDate <= lowerLimit)
                Console.WriteLine("Ne mozete unijeti taj datum, probajte neki kasniji:");
            else
                break;
        }
        catch
        {
            Console.WriteLine("Pogresan unos, pokusajte ponovno:");
        }
    }

    return myDate;
}