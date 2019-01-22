using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyGallery.commands;
using MyGallery.models;
using MyGallery.services;
using MyGallery.services.impl;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyGallery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IRegistryStorageFolderService _registryStorageFolderService;

        public MainPage()
        {
            _registryStorageFolderService = RegistryStorageFolderService.CreateAsync().Result;
            RegistryStorageFolders = new ObservableCollection<RegistryStorageFolder>(_registryStorageFolderService.ListAllAsync().Result);
            RegistryStorageFolderCommand = new RegistryStorageFolderCommand(_registryStorageFolderService);

            this.InitializeComponent();
        }

        public RegistryStorageFolderCommand RegistryStorageFolderCommand { get; }

        public ObservableCollection<RegistryStorageFolder> RegistryStorageFolders { get; }

        private async void OnRefreshCollectionClick(object sender, RoutedEventArgs e)
        {
            RegistryStorageFolders.Clear();
            IEnumerable<RegistryStorageFolder> registryStorageFolders = await _registryStorageFolderService.ListAllAsync();
            foreach (var registryStorageFolder in registryStorageFolders)
            {
                RegistryStorageFolders.Add(registryStorageFolder);
            }
        }
    }
}
