using Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Infrastructure.Services.Core
{
    public interface IRibbonService
    {
        void AddRibbonItem(RibbonTabItem ribbonItem,bool isActive);
        void RemoveRibbonItem(RibbonTabItem ribbonItem);
    }
}
