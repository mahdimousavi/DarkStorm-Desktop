using DarkStorm.Desktop.Infrastructure;
using DarkStorm.Desktop.Modules.TimeCard.Data.Repositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Services;
using DarkStorm.Desktop.Modules.TimeCard.UI.Navigations;
using DarkStorm.Desktop.Modules.TimeCard.UI.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Modules.TimeCard.UI
{
    public class TimeCardModule:IModule
    {
        public  IRegionViewRegistry RegionRegistry;
        public  IUnityContainer Container;
        public  IRegionManager RegionManager;
        public TimeCardModule(IUnityContainer container,
                              IRegionViewRegistry regionRegistry,
                              IRegionManager regionManager)
        {
            RegionRegistry = regionRegistry;
            Container = container;
            RegionManager = regionManager;
        }

        public void Initialize()
        { 
            Container.RegisterType<Object, WorkCodeView>("WorkCodeView");
            Container.RegisterType<Object, EmployeesView>("EmployeesView");
            Container.RegisterType<Object, EmployeeView>("EmployeeView");
            Container.RegisterType<Object, WorkHourView>("WorkHourView");
            Container.RegisterType<Object, TCDashboard>("TCDashboard");

            Container.RegisterType<IWorkCodeService, WorkCodeService>();
            Container.RegisterType<IWorkCodeRepository, WorkCodeRepository>();

            Container.RegisterType<IEmployeeService, EmployeeService>();
            Container.RegisterType<IEmployeeRepository, EmployeeRepository>();

            Container.RegisterType<IWorkHourService, WorkHourService>();
            Container.RegisterType<IWorkHourRepository, WorkHourRepository>();

            this.RegionManager.RegisterViewWithRegion(RegionNames.NavigationPane, typeof(NPITimeCard));

          
        }
    }
}