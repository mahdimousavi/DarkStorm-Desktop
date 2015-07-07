using DarkStorm.Desktop.Infrastructure;
using DarkStorm.Desktop.Infrastructure.Application;
using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Presentation.Dialogues;
using DarkStorm.Desktop.Presentation.RibbonTabItems;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DarkStorm.Desktop.Presentation.ViewModels
{
    public class ShellVM : BindableBase
    {
       
        private IRegionManager regionManager;
        IRibbonService ribbonService;

        public DelegateCommand goBackCommand{set;get;}
        public DelegateCommand goForwardCommand { set; get; }
        public ShellVM(IRegionManager regionManager,IRibbonService ribbonService)
        {
            this.regionManager = regionManager;
            this.ribbonService = ribbonService;
            this.goBackCommand = new DelegateCommand(GoBack);
            this.goForwardCommand = new DelegateCommand(GoForward);
        }

        private void GoForward()
        {
            var region = regionManager.Regions[RegionNames.WorkingArea];
            region.NavigationService.Journal.GoForward();
        }

        private void GoBack()
        {
            var region = regionManager.Regions[RegionNames.WorkingArea];
            region.NavigationService.Journal.GoBack();
        }
    }
}
