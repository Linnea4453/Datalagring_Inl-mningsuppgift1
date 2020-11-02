using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClassLibrary.Models
{
    public class Person
    {
        public Person()
        {

        }

        public Person(string v1, string v2, int v3, string v4)
        {
        }

        [JsonProperty(propertyName:"id")]
         public int Id { get; set; }

        [JsonProperty(propertyName: "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(propertyName: "lastname")]
        public string LastName { get; set; }

        [JsonProperty(propertyName: "email")]
        public string Email { get; set; }

        public string DisplayNamen => $"{FirstName} {LastName}";

    }


}
