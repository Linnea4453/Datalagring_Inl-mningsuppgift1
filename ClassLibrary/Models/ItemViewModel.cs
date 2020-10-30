using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Service;

namespace ClassLibrary.Models
{
    public class ItemViewModel
    {
        public ObservableCollection<Item> Items { get; set; }

        public ItemViewModel()
        {
            Items = new ObservableCollection<Item>();
           
        }
    }
}
