using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System.IO;
using Fluent;
using System.Windows;
using DarkStorm.Desktop.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Infrastructure.Services;
using DarkStorm.Desktop.Modules.TimeCard.UI;
using DarkStorm.Desktop.Presentation.Views;
using DarkStorm.Desktop.Infrastructure.Application;

namespace DarkStorm.Desktop.Presentation
{
    class DSBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }
        protected override void ConfigureModuleCatalog()
        {
            Type moduletc = typeof(TimeCardModule);
            ModuleCatalog.AddModule(new ModuleInfo() { ModuleName = moduletc.Name, ModuleType = moduletc.AssemblyQualifiedName, });
       }
        protected override void ConfigureContainer()
        {
            Container.RegisterType<IRibbonService, RibbonService>();
            base.ConfigureContainer();

            Container.RegisterType<Object, ReportWindowView>("ReportWindowView");
            Container.RegisterType<Object, MainView>("MainView");

            Container.RegisterType<IAppModule, AppModule>();
        }
        protected override Microsoft.Practices.Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(Ribbon), Container.Resolve<RibbonRegionAdapter>());
            mappings.RegisterMapping(typeof(RibbonTabItem), Container.Resolve<RibbonItemRegionAdapter>());
            return mappings;
        }
    }
}
