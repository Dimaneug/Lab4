using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class ResearchTeamChangedEventArgs<TKey> : EventArgs
    {
        public string CollectionName { get; set; }
        public Revision TypeOfEvent { get; set; }
        public string PropertyName { get; set; }
        public int RegNumber { get; set; }
        public ResearchTeamChangedEventArgs(string Name, Revision Type, string Property, int Reg)
        {
            CollectionName = Name;
            TypeOfEvent = Type;
            PropertyName = Property;
            RegNumber = Reg;
        }
        public override string ToString()
        {
            return CollectionName + "\n" + TypeOfEvent.ToString() + "\n"
                + PropertyName + "\n" + RegNumber.ToString() + "\n\n";
        }
    }
}
