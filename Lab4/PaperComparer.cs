using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class PaperComparer : IComparer<Paper>
    {
        public int Compare(Paper x, Paper y)
        {
            return x.Author.Surname.CompareTo(y.Author.Surname);
        }
    }
}
