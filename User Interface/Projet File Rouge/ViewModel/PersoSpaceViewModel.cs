using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    class PersoSpaceViewModel : ViewModelBase
    {
        private List<RedWire> redWireList;

        private int actualPage;

        private int pageNumber;
        private DateTime date;
        private int step;
        private int userId;
        private string clientName;
        private List<string> stepList;

        public PersoSpaceViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            FilterInit();
            stepList = new List<string>();
            foreach (string stepName in Enum.GetNames(typeof(RedWire.state)))
            {
                stepList.Add(stepName);
            }
            userId = cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser);

            PageInit();
        }

        private void FilterInit()
        {
            Date = new DateTime();
            Step = -1;
            clientName = string.Empty;
        }

        private void PageInit()
        {
            int redWireTotal = RequestCenter.GetRedWireNumber(date, step, userId, clientName);

            CalculPageNumber(redWireTotal);

            redWireList = RequestCenter.GetRedWirePage(0, date, step, userId, clientName);
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

        public List<RedWire> RedWireList
        {
            get { return redWireList; }
            set 
            { 
                redWireList = value;
                OnPropertyChanged(nameof(RedWireList));
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

        public DateTime Date
        {
            get => date;
            set => date = value;
        }
        public int Step
        {
            get => step;
            set
            {
                step = value;
                OnPropertyChanged(nameof(StepListNullVisibility));
            }
        }
        public List<string> StepList
        {
            get => stepList;
            set => stepList = value;
        }
        public bool StepListNullVisibility { get => Step != -1; }
        public string ClientName
        {
            get => clientName;
            set => clientName = value;
        }

        public void LeftArrow()
        {
            if (actualPage > 1)
            {
                actualPage--;
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientName);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void RightArrow()
        {
            if (actualPage < pageNumber)
            {
                actualPage++;
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientName);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void Filter()
        {
            RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientName);
            CalculPageNumber(RequestCenter.GetRedWireNumber(date, step, userId, clientName));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
        }

        public void AntiFilter()
        {
            FilterInit();
            RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientName);
            CalculPageNumber(RequestCenter.GetRedWireNumber(date, step, userId, clientName));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Step));
            OnPropertyChanged(nameof(clientName));
            OnPropertyChanged(nameof(StepListNullVisibility));
        }

        public string Pagination 
        { 
            get => actualPage + "/" + pageNumber;
        }
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
