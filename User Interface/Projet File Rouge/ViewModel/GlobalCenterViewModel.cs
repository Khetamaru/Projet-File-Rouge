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

        public GlobalCenterViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateRedWireCommand = new NavigateRedWireCommand(navigationStore, cacheStore);
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            PageInit();
        }

        private void PageInit()
        {
            var redWireTotal = RequestCenter.GetRedWireNumber();

            pageNumber = (int)(redWireTotal / Math.Pow(10, 1) % 10);

            if (redWireTotal % 10 != 0)
            {
                pageNumber += 1;
            }

            redWireList = RequestCenter.GetRedWirePage(0);
            actualPage = 1;
        }

        public List<RedWire> RedWireList
        {
            get { return redWireList; }
            set 
            { 
                redWireList = value;
                OnPropertyChanged("RedWireList");
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
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1);
                OnPropertyChanged("Pagination");
            }
        }

        public void RightArrow()
        {
            if (actualPage < pageNumber)
            {
                actualPage++;
                RedWireList = RequestCenter.GetRedWirePage(actualPage - 1);
                OnPropertyChanged("Pagination");
            }
        }

        public string Pagination 
        { 
            get => actualPage + "/" + pageNumber;
        }
        public NavigateRedWireCommand NavigateRedWireCommand { get; }
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}
