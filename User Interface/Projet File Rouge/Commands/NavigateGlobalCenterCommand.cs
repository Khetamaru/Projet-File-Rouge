﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Commands
{
    public class NavigateGlobalCenterCommand : CommandBase
    {
        private readonly NavigationStore navigationStore;
        public CacheStore cacheStore;

        public NavigateGlobalCenterCommand(NavigationStore navigationStore, CacheStore cacheStore)
        {
            this.navigationStore = navigationStore;
            this.cacheStore = cacheStore;
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new GlobalCenterViewModel(navigationStore, cacheStore);
        }
    }
}
