using DarkStorm.Desktop.Infrastructure.Services.Core;
using Fluent;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Infrastructure.Services
{
    public class RibbonService : IRibbonService
    {
        private IRegionManager regionManager;
        public RibbonService(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void AddRibbonItem(RibbonTabItem ribbonItem,bool isActive)
        {
            regionManager.Regions[RegionNames.Ribbon].Add(ribbonItem);
            if(isActive)
            (ribbonItem.Parent as RibbonTabControl).SelectedItem = ribbonItem;
        }

        public void RemoveRibbonItem(RibbonTabItem ribbonItem)
        {
            regionManager.Regions[RegionNames.Ribbon].Remove(ribbonItem);            
        }
    }
}
