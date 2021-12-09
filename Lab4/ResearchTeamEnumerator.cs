using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab4
{
    public class ResearchTeamEnumerator : IEnumerator
    {
        List<Paper> publications;
        List<Person> participants;
        int index = -1;
        
        public ResearchTeamEnumerator(List<Paper> publications, List<Person> participants)
        {
            this.publications = publications;
            this.participants = participants;
        }


        public object Current
        {
            get { return participants[index]; }
        }

        bool IEnumerator.MoveNext()
        {
            for (int i = index + 1; i < participants.Count; i++)
            {
                foreach (var item in publications)
                {
                    if ((Person)participants[i] == ((Paper)item).Author)
                    {
                        index = i;
                        return true;
                    }
                }
            }
            return false;
        }

        void IEnumerator.Reset()
        {
            
             index = -1;
        }
    }
}
