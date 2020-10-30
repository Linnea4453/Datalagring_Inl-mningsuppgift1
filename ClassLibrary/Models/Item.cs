using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using Windows.UI.Xaml.Controls.Primitives;

namespace ClassLibrary.Models
{
    public class Item
    {
        public Item(int age, string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Age = age;
            Email = email;
        }


        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

   
}