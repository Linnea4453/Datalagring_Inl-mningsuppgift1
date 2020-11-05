using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Networking.NetworkOperators;
using Windows.Storage;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;

namespace ClassLibrary.Service
{
    public class XmlService
    {
        public static async Task<string> GetXmlFileAsync(string fileName)
        {

            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile file = await storageFolder.GetFileAsync(fileName);

            return await FileIO.ReadTextAsync(file);
        }

    }
}
