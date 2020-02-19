using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project_Code_First.Models.Search
{
    interface ISearch
    {
        List<Book> SearchByName(string name);
        List<Book> QuickSearchByName(string name);

    }
}
