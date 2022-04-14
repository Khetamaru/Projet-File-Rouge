﻿using System;
using System.Windows;
using Projet_File_Rouge.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Projet_File_Rouge.Stores;

namespace Projet_File_Rouge
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            CacheStore cacheStore = new CacheStore();

            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore, cacheStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}