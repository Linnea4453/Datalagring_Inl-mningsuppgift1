using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ClassLibrary.Models;
using ClassLibrary.Service;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection.PlayReady;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.Storage.Streams;
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
       

        private ObservableCollection<string> Item = new ObservableCollection<string>();
        public MainPage()
        {
            this.InitializeComponent();
            personViewModel = new PersonViewModel();
        }

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

            }


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

        }


        private async void btn_getcsv_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".csv");
            StorageFile file = await picker.PickSingleFileAsync();
            Item.Clear();

            if (file != null)
            {
                var persons = new List<Person>();
                var list = await FileIO.ReadLinesAsync(file);

                foreach(var item in list)
                {
                    var person = item.Split(';');

                    persons.Add(new Person
                    {
                        Id = Convert.ToInt32(person[0]),
                        FirstName = person[1],
                        LastName = person[2],
                        Email = person[3]
                    });
                }

                lvGetCSVInformation.ItemsSource = persons;
            }
        }

        private async void btn_getxml_Click(object sender, RoutedEventArgs e)
        {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.FileTypeFilter.Add(".xml");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                using (var stream = await file.OpenStreamForReadAsync())
                {
                    var persons = new List<Person>();

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(stream);


                    XmlNodeList nodes = xdoc.GetElementsByTagName("person");

                    foreach(XmlNode node in nodes)
                    {
                        var temp = new List<string>();

                        foreach(XmlNode child in node.ChildNodes)
                        {
                            temp.Add(child.InnerText);
                        }

                        persons.Add(new Person
                        {
                            Id = Convert.ToInt32(temp[0]),
                            FirstName = temp[1],
                            LastName = temp[2],
                            Email = temp[3]
                        });
                    }
                    lvXml.ItemsSource = persons;
                }
            }
        }
    }
}
    

