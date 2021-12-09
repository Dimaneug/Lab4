using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab4
{
    class ResearchTeamCollection<TKey>
    {
        Dictionary<TKey, ResearchTeam> ResearchTeamDictionary =
            new Dictionary<TKey, ResearchTeam>();

        KeySelector<TKey> MyKeySelector;
        public string NameDict { get; set; }
        public ResearchTeamCollection(KeySelector<TKey> tempKey)
        {
            this.MyKeySelector = tempKey;
        }

      /*  public void translateEvent(object subject, EventArgs e)
        {
            var me = (PropertyChangedEventArgs)e;
            var st = (ResearchTeam)subject;
            TKey key = MyKeySelector(st);
            ResearchTeamPropertyChanged(Revision.Property, me.PropertyName.ToString(), st.RegNumber);
        }*/

        public void AddDefaults()
        {
            ResearchTeam tempRT = new ResearchTeam();
            TKey key = MyKeySelector(tempRT);
            ResearchTeamDictionary.Add(key, tempRT);
            tempRT.PropertyChanged += ResearchTeamPropertyChanged;
        }

        public void AddResearchTeams(params ResearchTeam[] teams)
        {
            foreach (ResearchTeam item in teams)
            {
                TKey key = MyKeySelector(item);
                ResearchTeamDictionary.Add(key, item);
                item.PropertyChanged += ResearchTeamPropertyChanged;
            }
        }

        public bool Remove(ResearchTeam rt)
        {
            TKey key = MyKeySelector(rt);
            if (ResearchTeamDictionary.ContainsKey(key) == true)
            {
                ResearchTeamDictionary.Remove(key);
                ResearchTeamChanged(this, new ResearchTeamChangedEventArgs<TKey>(NameDict, Revision.Remove, "", rt.RegNumber));
                rt.PropertyChanged -= ResearchTeamPropertyChanged;
                return true;
            }
            return false;
        }

        public bool Replace(ResearchTeam rtold, ResearchTeam rtnew)
        {
            TKey key = MyKeySelector(rtold);
            if (ResearchTeamDictionary.ContainsKey(key) == true)
            {
                ResearchTeamDictionary[key] = rtnew;
                ResearchTeamChanged(this, new ResearchTeamChangedEventArgs<TKey>(NameDict, Revision.Replace, "", rtold.RegNumber));
                rtold.PropertyChanged -= ResearchTeamPropertyChanged;
                return true;
            }
            return false;

        }

        public event ResearchTeamChangedHandler<TKey> ResearchTeamChanged;

        private void ResearchTeamPropertyChanged(object subject, PropertyChangedEventArgs e)
        {
            if (ResearchTeamChanged != null)
            {
                ResearchTeamChanged(this, new ResearchTeamChangedEventArgs<TKey>(NameDict, Revision.Property, e.PropertyName, ((ResearchTeam)subject).RegNumber));
            }
        }

        public override string ToString()
        {
            string str = "";
            foreach (var item in ResearchTeamDictionary)
                str += item.Key.ToString() + "\n" + item.Value.ToString() + "\n";
            return str;
        }

        public string ToShortString()
        {
            string str = "";
            foreach (var item in ResearchTeamDictionary)
            {
                str += item.Key.ToString();
                str += item.Value.ToShortString();
            }
            return str;
        }

        public DateTime LastPublicationDate
        {
            get
            {
                if (ResearchTeamDictionary.Count > 0)
                {
                    List<Paper> dtList = new List<Paper>();

                    foreach (ResearchTeam rt in ResearchTeamDictionary.Values)
                    {
                        dtList.Add(rt.LatestPublication);
                    }
                    return dtList.Max(pap => pap.PublicationDate);
                }
                    
                return DateTime.Today;
            }
        }

        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> TimeFrameGroup(TimeFrame value)
        {
            return ResearchTeamDictionary.Where(x => x.Value.Duration == value);
        }

        public IEnumerable<IGrouping<TimeFrame, KeyValuePair<TKey, ResearchTeam>>> GroupByDuration
        {
            get
            {
                return ResearchTeamDictionary.GroupBy(x => x.Value.Duration);
            }
        }
    }
}
