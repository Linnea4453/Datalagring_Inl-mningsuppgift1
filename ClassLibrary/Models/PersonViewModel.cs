using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Service;
using Newtonsoft.Json;

namespace ClassLibrary.Models
{
    public class PersonViewModel
    {
        public ObservableCollection<Person> Persons { get; set; }
        public List<Item> Items { get; set; }
        //
        public PersonViewModel()
        {
            Persons = new ObservableCollection<Person>();
            Items = new List<Item> ();
        }


    }
}
