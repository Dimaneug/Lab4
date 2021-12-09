using System;
namespace Lab4
{
    public class Team : INameAndCopy
    {
        protected string name;
        protected int regNumber;

        public Team(string nameValue, int regNumberValue)
        {
            name = nameValue;
            regNumber = regNumberValue;
        }
        public Team() : this("Название организации", 0)
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
        public int RegNumber
        {
            get
            {
                return regNumber;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Номер меньше или равен 0");
                else
                    regNumber = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Team temp = (Team)obj;
            return (name == temp.Name) && (regNumber == temp.RegNumber);
        }
        public static bool operator ==(Team a, Team b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Team a, Team b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return name.GetHashCode() ^ regNumber.GetHashCode();
        }
        public virtual object DeepCopy()
        {
            return new Team(name, regNumber);
        }
        public override string ToString()
        {
            return name + " " + regNumber.ToString();
        }

    }
}
