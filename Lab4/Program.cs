using System;
using System.Collections.Generic;

namespace Lab4
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    delegate TKey KeySelector<TKey>(ResearchTeam rt);
    delegate void ResearchTeamChangedHandler<TKey>(object source, ResearchTeamChangedEventArgs<TKey> args);


    class MainClass
    {
        public static void Main(string[] args)
        {
            ResearchTeam apple = new ResearchTeam("Apple", 29301, "IT", TimeFrame.Long);
            Person steveJobs = new Person("Steve", "Jobs", new DateTime(1955, 2, 24));
            Paper firstIphone = new Paper("New iPhone", steveJobs, new DateTime(2012, 9, 21));
            Paper firstIpad = new Paper("New Ipad", steveJobs, new DateTime(2010, 4, 3));
            Person timCook = new Person("Tim", "Cook", new DateTime(1960, 11, 1));
            Paper iphone13 = new Paper("iPhone 13", timCook, new DateTime(2021, 9, 21));
            Person dimaIvk = new Person("Dmitry", "Ivkuchev", new DateTime(2002, 2, 17));
            apple.AddPapers(firstIpad, firstIphone, iphone13);
            apple.AddMembers(steveJobs, timCook, dimaIvk);

            ResearchTeam microsoft = new ResearchTeam("Microsoft", 453923, "IT", TimeFrame.TwoYears);
            Person billGates = new Person("Bill", "Gates", new DateTime(1955, 10, 28));
            Paper firstWindows = new Paper("First Windows launched", billGates, new DateTime(2012, 9, 21));
            Paper windows11 = new Paper("New Windows 11", billGates, new DateTime(2021, 10, 5));
            Person bradSmith = new Person("Brad", "Smith", new DateTime(1959, 1, 17));
            Paper arrivedInMicrosoft = new Paper("Brad Smith now in Microsoft", bradSmith, new DateTime(2021, 9, 21));
            microsoft.AddPapers(firstWindows, windows11, arrivedInMicrosoft);
            microsoft.AddMembers(billGates, bradSmith);

            ResearchTeam firstResearchTeam = new ResearchTeam("First", 56745, "Different", TimeFrame.Year);
            Person person1 = new Person("Ivan", "Ivanov", new DateTime(1976, 11, 15));
            Paper paper1 = new Paper("First paper", person1, new DateTime(2016, 9, 12));
            Paper paper2 = new Paper("Second paper", person1, new DateTime(2021, 3, 27));
            Person person2 = new Person("Oleg", "Olegov", new DateTime(1969, 4, 13));
            Paper paper3 = new Paper("Third paper", person2, new DateTime(2019, 3, 21));
            firstResearchTeam.AddPapers(paper1, paper2, paper3);
            firstResearchTeam.AddMembers(person1, person2);

            ResearchTeam secondResearchTeam = new ResearchTeam("Second", 65830, "Different", TimeFrame.Year);
            Person person3 = new Person("Alex", "Alexa", new DateTime(1987, 11, 16));
            Paper paper4 = new Paper("Fourth paper", person3, new DateTime(2017, 9, 12));
            Paper paper5 = new Paper("Fifth paper", person3, new DateTime(2015, 3, 27));
            Person person4 = new Person("Stas", "Stasov", new DateTime(1958, 4, 13));
            Paper paper6 = new Paper("Sixth paper", person4, new DateTime(2018, 3, 21));
            secondResearchTeam.AddPapers(paper4, paper5, paper6);
            secondResearchTeam.AddMembers(person3, person4);

            KeySelector<string> selector = delegate (ResearchTeam input)
            {
                return input.GetHashCode().ToString();
            };

            ResearchTeamCollection<string> collection1 = new ResearchTeamCollection<string>(selector);
            collection1.NameDict = "First collection";
            ResearchTeamCollection<string> collection2 = new ResearchTeamCollection<string>(selector);
            collection2.NameDict = "Second collection";

            TeamsJournal journal = new TeamsJournal();

            collection1.ResearchTeamChanged += journal.Handler;
            collection2.ResearchTeamChanged += journal.Handler;


            collection1.AddDefaults();
            collection1.AddResearchTeams(apple, microsoft);
            collection2.AddDefaults();
            collection2.AddResearchTeams(firstResearchTeam, secondResearchTeam);

            firstResearchTeam.Duration = TimeFrame.Long;
            secondResearchTeam.Theme = "New Theme";

            collection2.Remove(secondResearchTeam);
            secondResearchTeam.Theme = "Another New Theme";

            collection1.Replace(microsoft, firstResearchTeam);
            microsoft.Duration = TimeFrame.Year;

            Console.WriteLine(journal.ToString());





            /*
            var obj = new ResearchTeamCollection<string>(selector);
            obj.AddResearchTeams(apple, microsoft);
            Console.WriteLine(obj.ToString());

            Console.WriteLine("Методы ResearchTeamCollection<string>:");

            Console.WriteLine(obj.LastPublicationDate);
            Console.WriteLine();

            foreach (var item in obj.TimeFrameGroup(TimeFrame.Long))
                Console.WriteLine(item.ToString());
            Console.WriteLine();

            foreach (var item in obj.GroupByDuration)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine();
                foreach (var name in item)
                    Console.WriteLine(name);
            }

            Console.WriteLine("Создание TestCollection");

            GenerateElement<Team, ResearchTeam> d = delegate (int j)
            {
                var key = new Team("Имя", j);
                var value = new ResearchTeam(key.Name, key.RegNumber, "Тема", TimeFrame.Year);
                return new KeyValuePair<Team, ResearchTeam>(key, value);
            };

            int c = Convert.ToInt32(Console.ReadLine());
            var testObj = new TestCollections<Team, ResearchTeam>(c, d);
            testObj.searchInTKeyList();
            testObj.searchInStrList();
            testObj.searcInTKeyDictionary();
            testObj.searcInStrDictionary();
            testObj.searcInTKeyDictionaryByValue();
            testObj.searcInStrDictionaryByValue();
            */
            Console.ReadLine();
        }
    }
}
