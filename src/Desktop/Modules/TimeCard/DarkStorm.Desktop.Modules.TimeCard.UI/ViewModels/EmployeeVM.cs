using AutoMapper;
using DarkStorm.Desktop.Infrastructure.Domain.Core;
using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels;
using DarkStorm.Desktop.Modules.TimeCard.UI.RibbonTabItems;
using Microsoft.Practices.Prism.Commands;
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
    public class EmployeeVM :BindableBase, INavigationAware
    {
        private IRibbonService ribbonService;
        private RTIEmployeeView menu;
        
        private readonly DelegateCommand<bool?> saveCommand;

        private IEmployeeService employeeService;

        private BasicEmployeeVM employee;
        private bool IsNew;
        public BasicEmployeeVM Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public EmployeeVM(
            IRibbonService ribbonService,
            IEmployeeService employeeService
            )
        {
            this.ribbonService = ribbonService;
            this.saveCommand = new DelegateCommand<bool?>(this.Save, this.CanSave);
           
            employee = new BasicEmployeeVM(); 

            Mapper.CreateMap<WorkCode, BasicWorkCodeVM>();

            this.employeeService = employeeService;

        }

        private bool CanSave(bool? arg)
        {

            return true;
        }

        private void Save(bool? obj)
        {
            Mapper.CreateMap<BasicEmployeeVM, Employee>();
            if (IsNew)
            {
                employeeService.Add(Mapper.Map<BasicEmployeeVM, Employee>(employee));
            }
            else
            {
                employeeService.Modify(Mapper.Map<BasicEmployeeVM, Employee>(employee));
            }
            employeeService.Save();
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
            menu = new RTIEmployeeView();
            menu.SaveButton.Command = saveCommand;
           
            ribbonService.AddRibbonItem(menu, true);
            
            Mapper.CreateMap<Employee, BasicEmployeeVM>();
            if (navigationContext.Parameters["Id"] ==null)
            {
                IsNew = true;
                employee = new BasicEmployeeVM();
            }
            else
            { 
                int id =0;
                int.TryParse(navigationContext.Parameters["Id"].ToString(),out id);
                IsNew = false;
                employee =Mapper.Map<Employee,BasicEmployeeVM>(employeeService.GetFiltered(a => a.Id == id).FirstOrDefault());
            }
        }
    }
}
