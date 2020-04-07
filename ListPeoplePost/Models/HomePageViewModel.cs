using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListPeoplePost.Data;

namespace ListPeoplePost.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Person> People { get; set; }
        public string Message { get; set; }
    }
}
