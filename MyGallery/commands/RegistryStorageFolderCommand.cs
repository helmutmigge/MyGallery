using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using MyGallery.helpers;
using MyGallery.services;

namespace MyGallery.commands
{
    public class RegistryStorageFolderCommand : ICommand
    {
        private readonly IRegistryStorageFolderService _registryStorageFolderService;
        public event EventHandler CanExecuteChanged;

        public RegistryStorageFolderCommand(IRegistryStorageFolderService registryStorageFolderService)
        {
            this._registryStorageFolderService = registryStorageFolderService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            StorageFolder folder = await SelectVideoStorageFolderAsync();
            if (folder != null)
            {
                await this._registryStorageFolderService.RegistryAsync(folder);
            }

        }

        private async Task<StorageFolder> SelectVideoStorageFolderAsync()
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.ViewMode = PickerViewMode.Thumbnail;
            foreach (string videoExtension in FileExtensions.Video)
            {
                folderPicker.FileTypeFilter.Add(videoExtension);
            }

            folderPicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            return await folderPicker.PickSingleFolderAsync();
        }

    }
}
