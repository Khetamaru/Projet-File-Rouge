using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Log Center View
    /// </summary>
    public class LogCenterViewModel : ViewModelBase
    {
        private List<Log> stackPanelContent;

        private DateTime date;
        private int userId;
        private List<User> users;
        private List<string> userList;
        private int type;
        private List<string> typeList;

        private int actualPage;
        private int pageNumber;

        public LogCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up 
            NavigateParameterMenuCommand = new NavigateParameterMenuCommand(navigationStore, cacheStore);

            // set up view objects
            FilterInit();
            TypeList = new List<string>();
            foreach (string stepName in Enum.GetNames(typeof(Log.LogTypeEnum)))
            {
                TypeList.Add(stepName);
            }
            Users = RequestCenter.GetUsers();
            UserList = new List<string>();
            foreach (User user in users)
            {
                UserList.Add(user.Name);
            }
            StackPanelContent = RequestCenter.GetLogFiltered(0, Date, Type, UserId);
            PageInit();
        }

        private void FilterInit()
        {
            Date = new DateTime();
            UserId = -1;
            Type = -1;
        }

        private void PageInit()
        {
            int redWireTotal = RequestCenter.GetLogNumber(Date, Type, UserId);

            CalculPageNumber(redWireTotal);

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

        public void LeftArrow()
        {
            if (actualPage > 1)
            {
                actualPage--;
                StackPanelContent = RequestCenter.GetLogFiltered(actualPage - 1, Date, Type, UserId);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void RightArrow()
        {
            if (actualPage < pageNumber)
            {
                actualPage++;
                StackPanelContent = RequestCenter.GetLogFiltered(actualPage - 1, Date, Type, UserId);
                OnPropertyChanged(nameof(Pagination));
            }
        }

        public void Filter()
        {
            StackPanelContent = RequestCenter.GetLogFiltered(actualPage - 1, Date, Type, UserId);
            CalculPageNumber(RequestCenter.GetLogNumber(date, type, userId));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
        }

        public void AntiFilter()
        {
            FilterInit();
            StackPanelContent = RequestCenter.GetLogFiltered(actualPage - 1, date, type, userId);
            CalculPageNumber(RequestCenter.GetLogNumber(date, type, userId));
            actualPage = 1;
            OnPropertyChanged(nameof(Pagination));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(UserId));
            OnPropertyChanged(nameof(TypeListNullVisibility));
            OnPropertyChanged(nameof(UserListNullVisibility));
        }

        public List<Log> StackPanelContent
        {
            get { return stackPanelContent; }
            set
            {
                stackPanelContent = value;
                OnPropertyChanged(nameof(StackPanelContent));
            }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserListNullVisibility));
            }
        }

        public List<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        public bool UserListNullVisibility { get => UserId != -1; }
        public List<string> UserList
        {
            get { return userList; }
            set { userList = value; }
        }

        public int Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(TypeListNullVisibility));
            }
        }

        public bool TypeListNullVisibility { get => Type != -1; }
        public List<string> TypeList
        {
            get => typeList;
            set => typeList = value;
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

        public string Pagination
        {
            get => actualPage + "/" + pageNumber;
        }

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateParameterMenuCommand NavigateParameterMenuCommand { get; }
    }
}
