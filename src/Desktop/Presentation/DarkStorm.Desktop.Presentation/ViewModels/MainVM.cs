using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Presentation.Dialogues;
using DarkStorm.Desktop.Presentation.RibbonTabItems;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Presentation.ViewModels
{
    public class MainVM :BindableBase,INavigationAware
    {
        private IRibbonService ribbonService;
        private RTIMainView menu;
        public DelegateCommand aboutCommand { set; get; } 
        public MainVM(
            IRibbonService ribbonService,
            IRegionManager regionManager
            )
         {
            this.ribbonService = ribbonService;
            this.regionManager = regionManager;

            this.aboutCommand = new DelegateCommand(this.About);
         }
        private void About()
        {
            new About().ShowDialog();
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ribbonService.RemoveRibbonItem(menu);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            menu = new RTIMainView();
            menu.AboutButton.Command = aboutCommand;

            ribbonService.AddRibbonItem(menu, true);
        }

        public IRegionManager regionManager { get; set; }
    }
}
