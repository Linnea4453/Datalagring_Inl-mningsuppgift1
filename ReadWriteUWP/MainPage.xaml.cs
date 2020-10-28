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
        //private ItemViewModel itemViewModel { get; set; }


        public MainPage()
        {
            this.InitializeComponent();
            personViewModel = new PersonViewModel();
            

        }

        //private async Task GetCsvFileAsync(string fileName)
        //{
          
        //    var data = File.ReadAllLines(fileName);

        //    var items = new List<Item>();

        //    foreach (var line in data)
        //    {
        //        var lineInfo = line.Split(';');

        //        items.Add(new Item()
        //        {
        //            Age = Convert.ToInt32(lineInfo[0]),
        //            FirstName = lineInfo[1],
        //            LastName = lineInfo[2],
        //            Email = lineInfo[3]
        //        });

        //    }

        //}

        public async Task PopulateCustomerViewModel(string fileName, string filePath)
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

            if (bcsv)
            {
                //var items = new ObservableCollection<Item>();
                var data = File.ReadAllLines(fileName);
                var items = new List<Item>();
            

            foreach (var line in data)
            {
                var lineInfo = line.Split(';');

                items.Add(new Item()
                {
                    Age = Convert.ToInt32(lineInfo[0]),
                    FirstName = lineInfo[1],
                    LastName = lineInfo[2],
                    Email = lineInfo[3]
                });

            }


                //var data = File.ReadAllLines(filePath);

                //var items = new List<Item>();

                //foreach (var line in data)
                //{
                //    var lineInfo = line.Split(';');

                //    items.Add(new Item()
                //    {
                //        Age = Convert.ToInt32(lineInfo[0]),
                //        FirstName = lineInfo[1],
                //        LastName = lineInfo[2],
                //        Email = lineInfo[3]
                //    });

                //    }
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
                    PopulateCustomerViewModel(file.Name, file.Path).GetAwaiter();
                }

                //else
                //{
                //    OutputTextBlock.Text = "Operation cancelled.";
                //}
                //var Storagefile = await picker.PickSingleFileAsync();

                //PopulateCustomerViewModel("file").GetAwaiter();
                //PopulateCustomerViewModel("file.json").GetAwaiter();
            }

        private void btn_getcsv_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    }

