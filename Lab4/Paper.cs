using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab4
{
    public class Paper : IComparable, IComparer<Paper>
    {
        public string Title { get; set; }
        public Person Author { get; set; }
        public DateTime PublicationDate { get; set; }

        public Paper(string TitleValue, Person AuthorValue, DateTime PublicationDateValue)
        {
            Title = TitleValue;
            Author = AuthorValue;
            PublicationDate = PublicationDateValue;
        }

        public Paper() : this("Название публикации", new Person(), new DateTime(2000, 1, 1))
        { 
        }

        public override string ToString()
        {
            return Title + " " + Author.ToString() + " " + PublicationDate.ToShortDateString();
        }

        public int CompareTo(object obj)
        {
            return PublicationDate.CompareTo(((Paper)obj).PublicationDate);
        }

        public int Compare(Paper x, Paper y)
        {
            return x.Title.CompareTo(y.Title);
        }

    }
}
