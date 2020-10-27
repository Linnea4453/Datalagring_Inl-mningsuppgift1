using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Windows.Storage;

namespace ClassLibrary.Service
{
    public static class CsvService
    {
		public static async Task<string> GetCsvFileAsync(string fileName)
		{

			StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
			StorageFile file = await storageFolder.GetFileAsync(fileName);

            return await FileIO.ReadTextAsync(file);
        }
        
        public static IEnumerable<Person> ReadFromFile(string filepath, char delimiter = ';')
        {
            var lines = File.ReadAllLines(filepath).ToList();
            var persons = new List<Person>();

            foreach (var line in lines)
            {
                var data = line.Split(delimiter);
                persons.Add(new Person(data[0], data[1], Convert.ToInt32(data[2]), data[3]));
            }
            return persons;
        }
    }
}
