using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using MyGallery.services;

namespace MyGallery.models
{
    public class RegistryStorageFolder
    {
        private readonly string _token;
        private readonly IRegistryStorageFolderService _registryStorageFolderService;

        public RegistryStorageFolder(string token,IRegistryStorageFolderService registryStorageFolderService )
        {
            this._token = token;
            this._registryStorageFolderService = registryStorageFolderService;
        }

        public string Token
        {
            get { return _token; }
        }

        public async Task<StorageFolder> GetStorageFolderAsync()
        {
            return await this._registryStorageFolderService.GetStorageFolderTask(this);
        }
    }
}
