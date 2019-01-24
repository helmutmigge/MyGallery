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
        public MainPage()
        {
            RegistryStorageFolderService.CreateAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    IRegistryStorageFolderService registryStorageFolderService = task.Result;
                    RegistryStorageFolderCommand.RegistryStorageFolderService = registryStorageFolderService;
                    RefreshStorageFolderCommand.RegistryStorageFolderService = registryStorageFolderService;
                }
                else
                {
                    //TODO handler exception maybe show dialog error
                }

            });
            RegistryStorageFolders = new ObservableCollection<RegistryStorageFolder>();
            RegistryStorageFolderCommand = new RegistryStorageFolderCommand();
            RefreshStorageFolderCommand = new RefreshStorageFolderCommand
            {
                RegistryStorageFolders = RegistryStorageFolders
            };

            this.InitializeComponent();
        }

        public RefreshStorageFolderCommand RefreshStorageFolderCommand { get; }
        public RegistryStorageFolderCommand RegistryStorageFolderCommand { get; }

        public ObservableCollection<RegistryStorageFolder> RegistryStorageFolders { get; }
    }
}
