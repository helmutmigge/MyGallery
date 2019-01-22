using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using MyGallery.models;

namespace MyGallery.services
{

    public interface IRegistryStorageFolderService
    {
        Task<RegistryStorageFolder> RegistryAsync(StorageFolder folder);
        Task<StorageFolder> GetStorageFolderTask(RegistryStorageFolder registryStorageFolder);
        Task<IEnumerable<RegistryStorageFolder>> ListAllAsync();
    }
}
