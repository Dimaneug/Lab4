using System;
namespace Lab4
{
    public class Person
    {
        string name;
        string surname;
        DateTime birthday;

        public Person(string nameValue, string surnameValue, DateTime birthdayValue)
        {
            name = nameValue;
            surname = surnameValue;
            birthday = birthdayValue;
        }

        public Person() : this("Иван", "Иванов", new DateTime(2000, 1, 1))
        { 
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
        public int Year
        {
            get
            {
                return birthday.Year;
            }
            set
            {
                birthday.AddYears(value - birthday.Year);
            }
        }
        public override string ToString()
        {
            return name + " " + surname + " " + birthday.ToShortDateString();
        }
        public virtual string ToShortString()
        {
            return name + " " + surname;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Person temp = (Person)obj;
            return (birthday.CompareTo(temp.Birthday) == 0) & (name == temp.Name) & (surname == temp.Surname);
        }
        
        public static bool operator ==(Person a, Person b)
        {
            if ((object)a == null || (object)b == null)
            {
                return false;
            }
            return (a.birthday.CompareTo(b.Birthday) == 0) & (a.name == b.Name) & (a.surname == b.Surname);
        }
        public static bool operator !=(Person a, Person b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return name.GetHashCode() ^ surname.GetHashCode() ^ birthday.GetHashCode();
        }
        public virtual Person DeepCopy()
        {
            return new Person(name, surname, birthday);
        }
    }
    
}
