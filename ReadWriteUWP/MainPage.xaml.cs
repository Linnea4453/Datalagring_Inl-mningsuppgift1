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
       private ItemViewModel itemViewModel { get; set; }

        private ObservableCollection<string> Item = new ObservableCollection<string>();
        public MainPage()
        {
            this.InitializeComponent();
            personViewModel = new PersonViewModel();
            itemViewModel = new ItemViewModel();

        }

        public async Task GetCsvFileAsync(string fileName, char delimiter = ';')
        {
            var items = <ObservableCollection<Item>>(await CsvService.GetCsvFileAsync(fileName));

            foreach (var item in items)
            {
                personViewModel.Items.Add(item);

            }

            var lines = File.ReadAllLines("file.cvs").ToList();


            //var items = new List<Person>();
            //foreach (var line in lines)
            //{
            //    var data = line.Split(delimiter);

            //    foreach (var item in items)
            //    {
            //        items.Add(new Person(data[0], data[1], Convert.ToInt32(data[2]), data[3]));
            //    }
            //}


            //CsvRowsListView.ItemsSource = CsvRows;

            //var data = File.ReadAllLines(fileName);

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

            //}

        }

        public async Task GetXmlFileAsync(string file)
        {
            using XmlTextReader xml = new XmlTextReader(file);
            xml.Read();

            while (xml.Read())
            {
                tbxml.Text = await CsvService.GetCsvFileAsync("file.xml");

                XmlNodeType ntype = xml.NodeType;
                if (ntype == XmlNodeType.Element)
                {
                    if (xml.Name == "book")
                    {
                        xml.GetAttribute("xml");
                        tbxml.Text = await CsvService.GetCsvFileAsync("file.xml");
                    }
                }
            }
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
                //    var items = new ObservableCollection<Item>(CsvService.GetCsvFileAsync(fileName));
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
                //    foreach (var item in items)
                //    {
                //        personViewModel.Items.Add(item);
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
                tbCsv.Text = await CsvService.GetCsvFileAsync("file.csv");
                GetCsvFileAsync(file.Name).GetAwaiter();
            }


            //CsvRowsListView.ItemsSource = CsvRowsListView;

        }


        private async void btn_getxml_Click(object sender, RoutedEventArgs e)
        {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.FileTypeFilter.Add(".xml");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                
                tbxml.Text = await CsvService.GetCsvFileAsync("file.xml");
                GetXmlFileAsync(file.Name).GetAwaiter();
            }
            //    //using (IRandomAccessStream writeStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            //    //{
            //    //    Stream s = writeStream.AsStreamForWrite();
            //    //    XmlWriterSettings settings = new XmlWriterSettings();
            //    //    settings.Async = true;
            //    //    using (XmlWriter writer = XmlWriter.Create(s, settings))
            //    //    {

            //    //        //writer.Flush();
            //    //        await writer.FlushAsync();
            //    //    }
            //    //}
            //}
        }
    }
}
    

