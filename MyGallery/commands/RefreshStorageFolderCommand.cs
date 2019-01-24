using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyGallery.models;
using MyGallery.services;

namespace MyGallery.commands
{
    public class RefreshStorageFolderCommand : ICommand
    {
        private ObservableCollection<RegistryStorageFolder> _registryStorageFolders;
        private IRegistryStorageFolderService _registryStorageFolderService;
        public ObservableCollection<RegistryStorageFolder> RegistryStorageFolders
        {
            get => _registryStorageFolders;
            set
            {
                _registryStorageFolders = value;
                CanExecuteChanged?.Invoke(this, null);
            }
        }

        public IRegistryStorageFolderService RegistryStorageFolderService
        {
            get => _registryStorageFolderService;
            set
            {
                _registryStorageFolderService = value;
                CanExecuteChanged?.Invoke(this, null);
            }
        }

        public bool CanExecute(object parameter)
        {
            return RegistryStorageFolders != null && RegistryStorageFolderService != null;
        }

        public async void Execute(object parameter)
        {
            RegistryStorageFolders.Clear();
            IEnumerable<RegistryStorageFolder> registryStorageFolders = await RegistryStorageFolderService.ListAllAsync();
            foreach (var registryStorageFolder in registryStorageFolders)
            {
                RegistryStorageFolders.Add(registryStorageFolder);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
