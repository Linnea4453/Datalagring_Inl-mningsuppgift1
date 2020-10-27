using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using ClassLibrary.Models;
using ClassLibrary.Service;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection.PlayReady;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReadWriteUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PersonViewModel personViewModel { get; set; }
        private static List<Person> persons = new List<Person>();

        public MainPage()
        {
            this.InitializeComponent();
            personViewModel = new PersonViewModel();

        }

        public async Task PopulateCustomerViewModel(string fileName)
        {
            bool bjson = false;
            bool bcsv = false;
            bool bxml = false;

            bjson = fileName.Contains(".json");
            bcsv = fileName.Contains(".csv");
            bxml = fileName.Contains(".xml");

            if (bjson)
            {
                var persons = JsonConvert.DeserializeObject<ObservableCollection<Person>>(await JsonService.GetJsonFileAsync(fileName));

                foreach (var person in persons)
                {
                    personViewModel.Persons.Add(person);
                }
            }
            else
                if (bcsv)
            {
                var persons = new ObservableCollection<Person>(CsvService.ReadFromFile(fileName));

                foreach (var person in persons)
                {
                    var data = fileName.Split(',');
                    persons.Add(new Person(data[0], data[1], Convert.ToInt32(data[2]), data[3]));
                }
            }

            //if (bxml)
            //{
            //    var doc = new XmlDocument();
            //    var path = Directory.GetCurrentDirectory() + @"\data.xml";
            //    doc.Load(path);


            //    XmlNodeList titles = doc.GetElementsByTagName("title");

            //    foreach (XmlNode title in titles)
            //    {
            //        personViewModel.Persons.Add(title);
            //    }

            //}
        }

        private async void btn_getjson_Click(object sender, RoutedEventArgs e)
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.FileTypeFilter.Add(".json");
                picker.FileTypeFilter.Add(".csv");
                picker.FileTypeFilter.Add(".xml");

                StorageFile file = await picker.PickSingleFileAsync();

                if (file != null)
                {
                    PopulateCustomerViewModel(file.Name).GetAwaiter();
                }

                //else
                //{
                //    OutputTextBlock.Text = "Operation cancelled.";
                //}
                //var Storagefile = await picker.PickSingleFileAsync();

                //PopulateCustomerViewModel("file").GetAwaiter();
                //PopulateCustomerViewModel("file.json").GetAwaiter();
            }
        }
    }

