using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    class GlobalCenterViewModel : ViewModelBase
    {
        private List<RedWire> redWireList;

        private int actualPage;

        private int pageNumber;
        private DateTime date;
        private int step;
        internal int userId;
        private string clientCM;
        private List<string> stepList;
        private List<string> userList;
        private List<User> users;

        public GlobalCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

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
            ClientCM = string.Empty;
        }

        private void PageInit()
        {
            int redWireTotal = RequestCenter.GetRedWireNumber(date, step, userId, clientCM);

            CalculPageNumber(redWireTotal);

            redWireList = RequestCenter.GetRedWirePage(0, date, step, userId, clientCM);
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
            get { return date; }
            set { date = value; }
        }
        public int Step
        {
            get { return step; }
            set { step = value; }
        }
        public List<string> StepList
        {
            get => stepList;
            set => stepList = value;
        }
        public int UserId
        {
            get { return userId; }
            set { userId = users[value].Id; }
        }
        public List<string> UserList
        {
            get => userList;
            set => userList = value;
        }
        public string ClientCM
        {
            get { return clientCM; }
            set { clientCM = value; }
        }

        public void LeftArrow()
        {
            if (actualPage > 1)
            {
                actualPage--;
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientCM);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void RightArrow()
        {
            if (actualPage < pageNumber)
            {
                actualPage++;
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientCM);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void Filter()
        {
            RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientCM);
            CalculPageNumber(RequestCenter.GetRedWireNumber(date, step, userId, clientCM));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
        }

        public void AntiFilter()
        {
            FilterInit();
            RedWireList = RequestCenter.GetRedWirePage(actualPage - 1, date, step, userId, clientCM);
            CalculPageNumber(RequestCenter.GetRedWireNumber(date, step, userId, clientCM));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Step));
            OnPropertyChanged(nameof(UserId));
            OnPropertyChanged(nameof(ClientCM));
        }

        public string Pagination 
        { 
            get => actualPage + "/" + pageNumber;
        }
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
