﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Windows.Storage;

namespace ClassLibrary.Service
{
    public static class JsonService
    {
        public static async Task<string> GetJsonFileAsync(string fileName)
        {

            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile file = await storageFolder.GetFileAsync(fileName);

            return await FileIO.ReadTextAsync(file);
        }


        
    }

}
