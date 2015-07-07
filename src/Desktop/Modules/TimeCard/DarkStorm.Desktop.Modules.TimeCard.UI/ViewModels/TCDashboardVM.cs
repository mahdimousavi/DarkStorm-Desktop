using AutoMapper;
using DarkStorm.Desktop.Infrastructure;
using DarkStorm.Desktop.Infrastructure.Application;
using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using DarkStorm.Desktop.Modules.TimeCard.Services;
using DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels;
using DarkStorm.Desktop.Modules.TimeCard.UI.RibbonTabItems;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Modules.TimeCard.UI.ViewModels
{
    public class TCDashboardVM : BindableBase, INavigationAware
    {
        private IRibbonService ribbonService;
        private RTITCDashboard menu;
        private IRegionNavigationService navigationService;
        public InteractionRequest<INotification> ViewRequest { get; private set; }
        private readonly DelegateCommand aboutCommand;
        public IRegionManager regionManager;
        public IRegionViewRegistry regionRegistry;
        private IWorkHourService workHourService;
        private IEmployeeService employeeService;
        public DelegateCommand employeesCommand{set;get;}
        public DelegateCommand addEmployeeCommand { set; get; }
        public DelegateCommand workCodesCommand { set; get; }
        public DelegateCommand workHoursCommand { set; get; }
        public DelegateCommand bheCommand { set; get; }
        public DelegateCommand nbheCommand { set; get; }
        public DelegateCommand phonesCommand { set; get; }
        public DelegateCommand addressCommand { set; get; }



        public TCDashboardVM(
          IRibbonService ribbonService,
            IRegionManager regionManager,
            IRegionViewRegistry regionRegistry,
            IWorkHourService workHourService,
            IEmployeeService employeeService
          )
        {
            this.ribbonService = ribbonService;
            this.aboutCommand = new DelegateCommand(this.About);
            employeesCommand = new DelegateCommand(this.Employees);
            addEmployeeCommand = new DelegateCommand(this.AddEmployees);
            workHoursCommand = new DelegateCommand(this.WorkHoursEmployees);
            bheCommand = new DelegateCommand(this.BHE);
            nbheCommand = new DelegateCommand(this.NBHE);
            phonesCommand = new DelegateCommand(this.Phones);
            addressCommand = new DelegateCommand(this.Address);
            workCodesCommand = new DelegateCommand(this.WorkCodes);
            this.ViewRequest = new InteractionRequest<INotification>();
            this.regionManager = regionManager;
            this.regionRegistry = regionRegistry;
            this.workHourService = workHourService;
            this.employeeService = employeeService;
        }

        private void WorkCodes()
        {
            this.regionManager.RequestNavigate(RegionNames.WorkingArea, ViewsUri.WorkCodeView);
        }

        private void Address()
        {
            var navigationParameters = new NavigationParameters();
            var data = employeeService.GetAll();
            navigationParameters.Add("DataToDisplay", data.GetHashCode().ToString());
            AppParameters.Save(data.GetHashCode(), data);
            navigationParameters.Add("ReportPath", @"Reports\EmployeeAddressBook.rdlc");
            //navigationParameters.Add("Parameters",);
            navigationParameters.Add("ReportDataSourceName", "EmployeeAddressBook");

            this.regionManager.RequestNavigate(RegionNames.WorkingArea,
                 new Uri("ReportWindowView" + navigationParameters.ToString(), UriKind.Relative));
        }

        private void Phones()
        {
            var navigationParameters = new NavigationParameters();
            var data = employeeService.GetAll();
            navigationParameters.Add("DataToDisplay", data.GetHashCode().ToString());
            AppParameters.Save(data.GetHashCode(), data);
            navigationParameters.Add("ReportPath", @"Reports\EmployeePhoneList.rdlc");
            //navigationParameters.Add("Parameters",);
            navigationParameters.Add("ReportDataSourceName", "EmployeePhoneList");

            this.regionManager.RequestNavigate(RegionNames.WorkingArea,
                 new Uri("ReportWindowView" + navigationParameters.ToString(), UriKind.Relative));
        }

        private void NBHE()
        {
            var navigationParameters = new NavigationParameters();
            Mapper.CreateMap<WorkHour, BasicWorkHourVM>();
            Mapper.CreateMap<Employee, BasicEmployeeVM>();
            Mapper.CreateMap<WorkCode, BasicWorkCodeVM>();

            var data = workHourService.GetReport().Where(a => a.WorkCode.Billable == false).ToList();
            var bworkhours = new List<BasicWorkHourVM>();
            foreach (WorkHour wh in data)
            {
                bworkhours.Add(Mapper.Map<WorkHour, BasicWorkHourVM>(wh));
            }
            navigationParameters.Add("DataToDisplay", bworkhours.GetHashCode().ToString());
            AppParameters.Save(bworkhours.GetHashCode(), bworkhours);
            navigationParameters.Add("ReportPath", @"Reports\HoursByEmployee.rdlc");
            navigationParameters.Add("ReportDataSourceName", "HoursByEmployee");

            this.regionManager.RequestNavigate(RegionNames.WorkingArea,
                 new Uri("ReportWindowView" + navigationParameters.ToString(), UriKind.Relative));
        }

        private void BHE()
        {
            var navigationParameters = new NavigationParameters();
            Mapper.CreateMap<WorkHour, BasicWorkHourVM>();
            Mapper.CreateMap<Employee, BasicEmployeeVM>();
            Mapper.CreateMap<WorkCode, BasicWorkCodeVM>();

            var data = workHourService.GetReport().Where(a => a.WorkCode.Billable == true).ToList();
            var bworkhours = new List<BasicWorkHourVM>();
            foreach (WorkHour wh in data)
            {
                bworkhours.Add(Mapper.Map<WorkHour, BasicWorkHourVM>(wh));
            }
            navigationParameters.Add("DataToDisplay", bworkhours.GetHashCode().ToString());
            AppParameters.Save(bworkhours.GetHashCode(), bworkhours);
            navigationParameters.Add("ReportPath", @"Reports\HoursByEmployee.rdlc");
            navigationParameters.Add("ReportDataSourceName", "HoursByEmployee");

            this.regionManager.RequestNavigate(RegionNames.WorkingArea,
                 new Uri("ReportWindowView" + navigationParameters.ToString(), UriKind.Relative));
        }

        private void WorkHoursEmployees()
        {
            this.regionManager.RequestNavigate(RegionNames.WorkingArea, ViewsUri.WorkHourView);
        }

        private void AddEmployees()
        {
            regionManager.RequestNavigate(RegionNames.WorkingArea, new Uri("EmployeeView", UriKind.Relative));
        }

        private void Employees()
        {
            this.regionManager.RequestNavigate(RegionNames.WorkingArea, ViewsUri.EmployeesView);            
        }

        private void About()
        {
            this.ViewRequest.Raise(
                 new Notification {Title="About" });
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
            menu = new RTITCDashboard();
            menu.AboutButton.Command = aboutCommand;
            navigationService = navigationContext.NavigationService;
            ribbonService.AddRibbonItem(menu, true);
        }
    }
}
