using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class TeamsJournal
    {
        private List<TeamsJournalEntry> ListOfAllEvents = new List<TeamsJournalEntry>();
        public void Handler(object subject, ResearchTeamChangedEventArgs<string> e)
        {
            ListOfAllEvents.Add(new TeamsJournalEntry(e.CollectionName, e.TypeOfEvent, e.PropertyName, e.RegNumber));
        }

        public override string ToString()
        {
            string str = "";
            foreach (TeamsJournalEntry item in ListOfAllEvents)
            {
                str += item.ToString() + "\n";
            }
            return str;
        }
    }
}
