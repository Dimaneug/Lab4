using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class TeamsJournalEntry
    {
        public string CollectionName { get; set; }
        public Revision TypeOfEvent { get; set; }
        public string PropertyName { get; set; }
        public int RegNumber { get; set; }
        public TeamsJournalEntry(string Collection, Revision Type, string Property, int Reg)
        {
            CollectionName = Collection;
            TypeOfEvent = Type;
            PropertyName = Property;
            RegNumber = Reg;
        }
        public override string ToString()
        {
            return "Collection name: " + CollectionName + "\n" + "Event: " + TypeOfEvent.ToString() + "\n"
                + "Property: " + PropertyName + "\n" + "Registration number: " + RegNumber.ToString() 
                + "\n\n";
        }
    }
}
