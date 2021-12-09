using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lab4
{
    public class ResearchTeam : Team, INameAndCopy, IEnumerable, INotifyPropertyChanged
    {
        string theme;
        TimeFrame duration;
        List<Paper> publications;
        List<Person> participants;

        public event PropertyChangedEventHandler PropertyChanged;

       /* private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }*/

        public ResearchTeam(string nameValue, int regNumberValue, string themeValue, 
            TimeFrame durationValue) : base(nameValue, regNumberValue)
        {
            theme = themeValue;
            duration = durationValue;
            publications = new List<Paper>();
            participants = new List<Person>();
        }

        public ResearchTeam(string nameValue, int regNumberValue, string themeValue, 
            TimeFrame durationValue, List<Paper> papers, List<Person> members) : 
            base(nameValue, regNumberValue)
        {
            theme = themeValue;
            duration = durationValue;
            publications = new List<Paper>();
            publications.AddRange(papers);
            participants = new List<Person>();
            participants.AddRange(members);
        }

        public ResearchTeam() : this("Не назначено", 0, "Не назначена", TimeFrame.Year)
        {
        }

        public string Theme
        {
            get { return theme; }
            set 
            { 
                theme = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Theme")); 
            }
        }

        public TimeFrame Duration
        {
            get { return duration; }
            set 
            { 
                duration = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Duration")); 
            }
        }

        public List<Paper> Publications
        {
            get { return publications; }
            set { publications = value; }
        }

        public List<Person> Participants
        {
            get { return participants; }
            set { participants = value; }
        }

        public Team Teams
        {
            get { return new Team(name, regNumber);}
            set
            {
                Team temp = value;
                name = temp.Name;
                regNumber = temp.RegNumber;
            }
        }

        public Paper LatestPublication
        {
            get
            {
                if (publications.Count != 0)
                {
                    Paper temp = (Paper)publications[0];
                    foreach (var item in publications)
                    {
                        Paper temp1 = (Paper)item;
                        if (temp.PublicationDate >= temp1.PublicationDate)
                        {
                            temp = temp1;
                        }
                    }
                    return temp;
                }
                else return null;
            }
        }

        public bool this[TimeFrame t]
        {
            get { return duration == t; }
        }
        public void AddPapers(params Paper[] papers)
        {
                publications.AddRange(papers);
        }

        public void AddMembers(params Person[] people)
        {
                participants.AddRange(people);
        }

        public override string ToString()
        {
            string str = theme + " " + name + " " + regNumber.ToString() + " " + duration.ToString() + "\n";
            str += "\nСписок публикаций:\n";
            foreach (var item in publications)
            {
                str += item.ToString() + "\n";
            }
            str += "\nСписок участников:\n";
            foreach (var item in participants)
            {
                str += item.ToString() + "\n";
            }
            return str;
        }
        public virtual string ToShortString()
        {
            return name + " " + regNumber.ToString() + " " + theme  + " " + duration.ToString();
        }
        public override object DeepCopy()
        {
            ResearchTeam copy = new ResearchTeam(name, regNumber, theme, duration);
            copy.publications.AddRange(publications);
            copy.participants.AddRange(participants);
            return copy;
        }

        public IEnumerable ParticipantsWithoutPublication()
        {
            foreach (var item in participants)
            {
                Person temp = (Person)item;
                bool hasPublications = false;
                foreach(var item1 in publications)
                {
                    Paper temp1 = (Paper)item1;
                    if (temp1.Author == temp)
                    {
                        hasPublications = true;
                        break;
                    }
                       
                }
                if (hasPublications == false)
                    yield return item;
            }
        }
        public IEnumerable LastPublicationsForYears(int years)
        {
            foreach (var item in publications)
            {
                Paper temp = (Paper)item;
                if (DateTime.Today.Year - temp.PublicationDate.Year <= years)
                    yield return item;
            }
        }

        public IEnumerable ParticipantsWithMoreThanOnePublication()
        {
            
            foreach (var member in participants)
            {
                int publicationsCount = 0;
                foreach (var paper in publications)
                {
                    if ((Person)member == ((Paper)paper).Author)
                    {
                        publicationsCount++;
                        if (publicationsCount == 2)
                        {
                            yield return member;
                            break;
                        }  
                    }
                }
            }
        }

        public IEnumerable PublicationsForLastYear()
        {
            foreach (var paper in publications)
            {
                if (DateTime.Today.Year - ((Paper)paper).PublicationDate.Year <= 1)
                    yield return paper;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new ResearchTeamEnumerator(publications, participants);
        }

        public void SortByDate()
        {
            publications.Sort();
        }

        public void SortByTitle()
        {
            IComparer<Paper> comp = new Paper();
            publications.Sort(comp);
        }

        public void SortBySurname()
        {
            IComparer<Paper> comp = new PaperComparer();
            publications.Sort(comp);
        }
    }
}