using AutoMapper;
using DarkStorm.Desktop.Infrastructure;
using DarkStorm.Desktop.Infrastructure.Domain.Core;
using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels;
using DarkStorm.Desktop.Modules.TimeCard.UI.RibbonTabItems;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DarkStorm.Desktop.Modules.TimeCard.UI.ViewModels
{
    public class EmployeesVM :BindableBase, INavigationAware
    {
        private IRibbonService ribbonService;
        private RTIEmployeesView menu;
        
        private readonly DelegateCommand<bool?> newCommand;
        private readonly DelegateCommand<bool?> deleteCommand;
        private readonly DelegateCommand<bool?> editCommand;
        private IEmployeeService employeeService;
        public  IRegionManager RegionManager;
        public InteractionRequest<INotification> ViewRequest { get; private set; }

        public ICollectionView EmployeesCV { get; private set; }

        private ObservableCollection<BasicEmployeeVM> employees;
        public ObservableCollection<BasicEmployeeVM> Employees
        {
            get { return employees; }
            set { employees = value; }
        }
        public EmployeesVM(
            IRibbonService ribbonService,
            IEmployeeService employeeService,
            IRegionManager regionManager
            )
        {
            this.ribbonService = ribbonService;
            this.newCommand = new DelegateCommand<bool?>(this.New, this.CanNew);
            this.deleteCommand = new DelegateCommand<bool?>(this.Delete, this.CanDelete);
            this.editCommand = new DelegateCommand<bool?>(this.Edit, this.CanEdit);

            this.ViewRequest = new InteractionRequest<INotification>();

            employees = new ObservableCollection<BasicEmployeeVM>();
            RegionManager = regionManager;
            Mapper.CreateMap<Employee, BasicEmployeeVM>();
            Mapper.CreateMap<WorkCode, BasicWorkCodeVM>();
            Mapper.CreateMap<WorkHour, BasicWorkHourVM>();

            Mapper.CreateMap<BasicWorkCodeVM, WorkCode>();
            Mapper.CreateMap<BasicEmployeeVM, Employee>();

            Mapper.CreateMap<ICollection<WorkHour>, ICollection<BasicWorkHourVM>>();
            Mapper.CreateMap<ICollection<WorkCode>, ICollection<BasicWorkCodeVM>>();
            Mapper.CreateMap<ICollection<Employee>, ICollection<BasicEmployeeVM>>();
            Mapper.CreateMap<ICollection<BasicWorkHourVM>, ICollection<WorkHour>>();

            foreach (Employee data in employeeService.GetAll())
            {
                employees.Add(Mapper.Map<Employee, BasicEmployeeVM>(data));
            }

            EmployeesCV = new ListCollectionView(Employees);
            EmployeesCV.CurrentChanged += EmployeesCV_CurrentChanged;
            this.employeeService = employeeService;
        }

        private bool CanNew(bool? arg)
        {
            return true;
        }

        private void New(bool? obj)
        {
            RegionManager.RequestNavigate(RegionNames.WorkingArea,new Uri("EmployeeView", UriKind.Relative));
        }

        private bool CanEdit(bool? arg)
        {
            return true;
        }

        private void Edit(bool? obj)
        {
            BasicEmployeeVM current = EmployeesCV.CurrentItem as BasicEmployeeVM;
            if (current != null)
            {
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add("Id", current.Id);
                RegionManager.RequestNavigate(RegionNames.WorkingArea,
                     new Uri("EmployeeView" + navigationParameters.ToString(), UriKind.Relative));
            }
        }

        void EmployeesCV_CurrentChanged(object sender, EventArgs e)
        {
            BasicEmployeeVM current = EmployeesCV.CurrentItem as BasicEmployeeVM;
            if (current != null && current.ObjectState != ObjectState.Added)
            current.ObjectState = ObjectState.Modified;
        } 

        private bool CanDelete(bool? arg)
        { 
            return true;
        }

        private void Delete(bool? obj)
        {
            BasicEmployeeVM current = EmployeesCV.CurrentItem as BasicEmployeeVM;
            if(current!=null)
            {
                try
                {
                    employeeService.Remove(current.Id);
                    employeeService.Save();
                    employees.Remove(current);
                }
                catch
                {
                    this.ViewRequest.Raise(
                                   new Notification { Title = "Error" });
                }
            }
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
            menu = new RTIEmployeesView();
            menu.NewButton.Command = newCommand;
            menu.DeleteButton.Command = deleteCommand;
            menu.EditButton.Command = editCommand;
            
            ribbonService.AddRibbonItem(menu, true);
            
        }
    }
}
