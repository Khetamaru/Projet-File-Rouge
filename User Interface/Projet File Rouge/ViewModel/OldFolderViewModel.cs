using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    class OldFolderViewModel : ViewModelBase
    {
        private List<RedWire> redWireList;

        private int actualPage;

        private int pageNumber;
        private DateTime date;
        internal int userId;
        private string clientName;
        private List<string> userList;
        private List<User> users;

        public OldFolderViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            FilterInit();
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
            userId = -1;
            clientName = string.Empty;
        }

        private void PageInit()
        {
            int redWireTotal = RequestCenter.GetRedWireNumber(date, (int)RedWire.state.fin, userId, clientName);

            CalculPageNumber(redWireTotal);

            redWireList = RequestCenter.GetRedWirePage(0, date, (int)RedWire.state.fin, userId, clientName);
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
        public int UserId
        {
            get => userId;
            set
            {
                userId = users[value].Id;
                OnPropertyChanged(nameof(UserListNullVisibility));
            }
        }
        public List<string> UserList
        {
            get => userList;
            set => userList = value;
        }
        public bool UserListNullVisibility { get => UserId != -1; }
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
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, (int)RedWire.state.fin, userId, clientName);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void RightArrow()
        {
            if (actualPage < pageNumber)
            {
                actualPage++;
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, (int)RedWire.state.fin, userId, clientName);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void Filter()
        {
            RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, (int)RedWire.state.fin, userId, clientName);
            CalculPageNumber(RequestCenter.GetRedWireNumber(date, (int)RedWire.state.fin, userId, clientName));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
        }

        public void AntiFilter()
        {
            FilterInit();
            RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, (int)RedWire.state.fin, userId, clientName);
            CalculPageNumber(RequestCenter.GetRedWireNumber(date, (int)RedWire.state.fin, userId, clientName));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(UserId));
            OnPropertyChanged(nameof(clientName));
            OnPropertyChanged(nameof(UserListNullVisibility));
        }

        public string Pagination 
        { 
            get => actualPage + "/" + pageNumber;
        }
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
