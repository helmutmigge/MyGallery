using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using MyGallery.exceptions;
using MyGallery.models;

namespace MyGallery.services.impl
{
    public class RegistryStorageFolderService : IRegistryStorageFolderService
    {
        private readonly ConcurrentDictionary<string, RegistryStorageFolder> _registryStorageFolders;


        private RegistryStorageFolderService()
        {
            this._registryStorageFolders = new ConcurrentDictionary<string, RegistryStorageFolder>();

        }

        public static async Task<RegistryStorageFolderService> CreateAsync()
        {
            RegistryStorageFolderService registryStorageFolderService = new RegistryStorageFolderService();
            return await registryStorageFolderService.InitializeAsync();
        }

        private Task<RegistryStorageFolderService> InitializeAsync()
        {
            return Task.Run(async () =>
            {
                foreach (AccessListEntry accessListEntry in StorageApplicationPermissions.FutureAccessList.Entries)
                {
                    string token = accessListEntry.Token;
                    RegistryStorageFolder registryStorageFolder = new RegistryStorageFolder(token, this);
                    StorageFolder storageFolder = await registryStorageFolder.GetStorageFolderAsync();
                    this._registryStorageFolders[storageFolder.Path] = registryStorageFolder;
                }
                return this;
            });
        } 

        public Task<RegistryStorageFolder> RegistryAsync(StorageFolder folder)
        {
            return Task.Run(() =>
            {
                lock (_registryStorageFolders)
                {

                    if (_registryStorageFolders.Count ==
                        StorageApplicationPermissions.FutureAccessList.MaximumItemsAllowed)
                    {
                        throw new FullRecordRegistryStorageFolderException(
                            $"Maximum items allowed {StorageApplicationPermissions.FutureAccessList.MaximumItemsAllowed}");
                    }

                    RegistryStorageFolder registryStorageFolder = null;
                    if (_registryStorageFolders.TryGetValue(folder.Path, out var oldfolder))
                    {
                        StorageApplicationPermissions.FutureAccessList.AddOrReplace(oldfolder.Token, folder);
                        registryStorageFolder = new RegistryStorageFolder(oldfolder.Token, this);
                    }
                    else
                    {

                        string token = StorageApplicationPermissions.FutureAccessList.Add(folder);
                        registryStorageFolder = new RegistryStorageFolder(token, this);

                    }

                    _registryStorageFolders[folder.Path] = registryStorageFolder;
                    return registryStorageFolder;
                }
            });

        }

        public async Task<StorageFolder> GetStorageFolderTask(RegistryStorageFolder registryStorageFolder)
        {
            return await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(registryStorageFolder.Token); ;
        }

        public Task<IEnumerable<RegistryStorageFolder>> ListAllAsync()
        {

            return Task.FromResult(_registryStorageFolders.Values.AsEnumerable());
        }
    }
}
