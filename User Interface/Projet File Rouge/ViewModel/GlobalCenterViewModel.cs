using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Global Center for red wire view
    /// </summary>
    class GlobalCenterViewModel : ViewModelBase
    {
        private List<RedWire> redWireList;

        private int actualPage;
        private int pageNumber;

        private DateTime date;
        private int step;
        internal int userId;
        private string clientName;
        private List<string> stepList;
        private List<string> userList;
        private List<User> users;

        public GlobalCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up commands
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            // set up view objects
            FilterInit();
            stepList = new List<string>();
            foreach (string stepName in Enum.GetNames(typeof(RedWire.state)))
            {
                stepList.Add(stepName);
            }
            users = RequestCenter.GetUsers();
            userList = new List<string>();
            foreach (User user in users)
            {
                userList.Add(user.Name);
            }

            PageInit();
        }

        private void FilterInit()
        {
            Date = new DateTime();
            Step = -1;
            userId = -1;
            index = -1;
            clientName = string.Empty;
        }

        private void PageInit()
        {
            int redWireTotal = RequestCenter.GetRedWireNumberNoOld(date, step, userId, clientName);

            CalculPageNumber(redWireTotal);

            redWireList = RequestCenter.GetRedWirePageWithoutOld(0, date, step, userId, clientName);
            actualPage = 1;
        }

        private void CalculPageNumber(int nbr)
        {
            pageNumber = (int)(nbr / 10);

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
        public int UserId
        {
            get => index;
            set
            {
                userId = users[value].Id;
                index = value;
                OnPropertyChanged(nameof(UserListNullVisibility));
            }
        }
        public int UserSelected { get => userId; }
        private int index;
        public List<string> UserList
        {
            get => userList;
            set => userList = value;
        }
        public bool UserListNullVisibility { get => index != -1; }
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
                RedWireList = RequestCenter.GetRedWirePageWithoutOld(actualPage - 1, date, step, userId, clientName);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void RightArrow()
        {
            if (actualPage < pageNumber)
            {
                actualPage++;
                RedWireList = RequestCenter.GetRedWirePageWithoutOld(actualPage - 1, date, step, userId, clientName);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void Filter()
        {
            RedWireList = RequestCenter.GetRedWirePageWithoutOld(actualPage - 1, date, step, userId, clientName);
            CalculPageNumber(RequestCenter.GetRedWireNumberNoOld(date, step, userId, clientName));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
        }

        public void AntiFilter()
        {
            FilterInit();
            RedWireList = RequestCenter.GetRedWirePageWithoutOld(actualPage - 1, date, step, userId, clientName);
            CalculPageNumber(RequestCenter.GetRedWireNumberNoOld(date, step, userId, clientName));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Step));
            OnPropertyChanged(nameof(UserId));
            OnPropertyChanged(nameof(clientName));
            OnPropertyChanged(nameof(StepListNullVisibility));
            OnPropertyChanged(nameof(UserListNullVisibility));
        }

        public string Pagination 
        { 
            get => actualPage + "/" + pageNumber;
        }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
