using AutoMapper;
using DarkStorm.Desktop.Infrastructure;
using DarkStorm.Desktop.Infrastructure.Application;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DarkStorm.Desktop.Modules.TimeCard.UI.Navigations
{
    /// <summary>
    /// Interaction logic for NPITimeCard.xaml
    /// </summary>
    public partial class NPITimeCard
    {
        public IRegionManager regionManager;
        public IRegionViewRegistry regionRegistry;
        private IWorkHourService workHourService;
        private IEmployeeService employeeService;
        public NPITimeCard
            (
            IRegionManager regionManager,
            IRegionViewRegistry regionRegistry,
            IWorkHourService workHourService,
            IEmployeeService employeeService
            )
        {
            InitializeComponent();
            this.regionManager = regionManager;
            this.regionRegistry = regionRegistry;
            this.workHourService = workHourService;
            this.employeeService = employeeService;
        }

        private void GoToDashboard(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.WorkingArea, ViewsUri.TCDashboard);            
        }

        private void GoToEmployees(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.WorkingArea, ViewsUri.EmployeesView);            
        }

        private void GoToWorkCode(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.WorkingArea, ViewsUri.WorkCodeView);
        }

        private void GoToWorkHours(object sender, RoutedEventArgs e)
        {
            this.regionManager.RequestNavigate(RegionNames.WorkingArea, ViewsUri.WorkHourView);
        }
        private void GoToBillableHoursEmp(object sender, RoutedEventArgs e)
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
            //navigationParameters.Add("Parameters",);
            navigationParameters.Add("ReportDataSourceName", "HoursByEmployee");

            this.regionManager.RequestNavigate(RegionNames.WorkingArea,
                 new Uri("ReportWindowView" + navigationParameters.ToString(), UriKind.Relative));
        }

        private void GoToEmpAddressBook(object sender, RoutedEventArgs e)
        {
            var navigationParameters = new NavigationParameters();
            var data = employeeService.GetAll();
            navigationParameters.Add("DataToDisplay", data.GetHashCode().ToString());
            AppParameters.Save(data.GetHashCode(), data);
            navigationParameters.Add("ReportPath", @"Reports\EmployeeAddressBook.rdlc");
            navigationParameters.Add("ReportDataSourceName", "EmployeeAddressBook");

            this.regionManager.RequestNavigate(RegionNames.WorkingArea,
                 new Uri("ReportWindowView" + navigationParameters.ToString(), UriKind.Relative));
        }

        private void GoToEmpPhoneList(object sender, RoutedEventArgs e)
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

        private void GoToNonBillablebyEmp(object sender, RoutedEventArgs e)
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
            //navigationParameters.Add("Parameters",);
            navigationParameters.Add("ReportDataSourceName", "HoursByEmployee");

            this.regionManager.RequestNavigate(RegionNames.WorkingArea,
                 new Uri("ReportWindowView" + navigationParameters.ToString(), UriKind.Relative));
        } 
    }
}
