using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Storage;

namespace ClassLibrary.Service
{
    public class XmlService
    {

        //public static async Task<string>GetXmlFileAsync(string file)
        //{
        //    StorageFile file = KnownFolders.DocumentsLibrary
        //}
        

        //StorageFile tempFile = await ApplicationData.Current.LocalFolder.GetXmlFileAsync("");
        //String datas = await FileIO.ReadTextAsync(tempFile);
        //XDocument loadedData = XDocument.Load(datas);
        //var data = from query in loadedData.Descendants("mvinfo")
        //           select new MVData
        //           {
        //               VideoTitle = (string)query.Element("title"),
        //               VideoYear = (string)query.Element("year"),
        //               VideoSource = (string)query.Element("link"),
        //               ImageSource = (string)query.Element("imgSource")
        //           };
        //YouTubeMV.ItemsSource = data;
    }
}
