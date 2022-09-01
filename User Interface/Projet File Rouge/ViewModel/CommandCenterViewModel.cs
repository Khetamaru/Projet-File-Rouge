using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Command Center View
    /// </summary>
    class CommandCenterViewModel : ViewModelBase
    {
        private List<CommandList> commandList;

        private int actualPage;
        private int pageNumber;

        public CommandCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateCommandCommand = new NavigateCommandCommand(navigationStore, cacheStore);
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            // set up view objects
            PageInit();
        }

        private void PageInit()
        {
            int commandListTotal = RequestCenter.GetCommandListNumber();

            CalculPageNumber(commandListTotal);

            commandList = RequestCenter.GetCommandListPage(0);
            actualPage = 1;
        }

        private void CalculPageNumber(int nbr)
        {
            pageNumber = (int)(nbr / Math.Pow(10, 1) % 10);

            if (nbr % 10 != 0)
            {
                pageNumber += 1;
            }
        }

        public List<CommandList> CommandList
        {
            get { return commandList; }
            set 
            { 
                commandList = value;
                OnPropertyChanged(nameof(CommandList));
            }
        }

        public int ActualPage
        {
            get { return actualPage; }
            set { actualPage = value; }
        }

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

        public void LeftArrow()
        {
            if (actualPage > 1)
            {
                actualPage--;
                CommandList = RequestCenter.GetCommandListPage(actualPage - 1);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void RightArrow()
        {
            if (actualPage < pageNumber)
            {
                actualPage++;
                CommandList = RequestCenter.GetCommandListPage(actualPage - 1);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public string Pagination 
        { 
            get => actualPage + "/" + pageNumber;
        }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateCommandCommand NavigateCommandCommand { get; }
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
