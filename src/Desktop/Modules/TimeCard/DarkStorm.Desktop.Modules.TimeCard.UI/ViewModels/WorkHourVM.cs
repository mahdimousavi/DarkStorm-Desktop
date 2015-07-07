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
    public class WorkHourVM :BindableBase, INavigationAware
    {
        private IRibbonService ribbonService;
        private RTIWorkCodeView menu;
        
        private readonly DelegateCommand<bool?> saveCommand;
        private readonly DelegateCommand<bool?> deleteCommand;

        private IWorkCodeService workCodeService;
        private IWorkHourService workHourService;
        public ICollectionView WorkHoursCV { get; private set; }
        public List<BasicWorkHourVM> DeletedWorkHours { set; get; }

        private ObservableCollection<BasicEmployeeVM> employees;

        public ObservableCollection<BasicEmployeeVM> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        private ObservableCollection<BasicWorkHourVM> workHours;
        public ObservableCollection<BasicWorkHourVM> WorkHours
        {
            get { return workHours; }
            set { workHours = value; }
        }
        private ObservableCollection<BasicWorkCodeVM> workCodes;
        public ObservableCollection<BasicWorkCodeVM> WorkCodes
        {
            get { return workCodes; }
            set { workCodes = value; }
        }
        public WorkHourVM(
            IRibbonService ribbonService,
            IWorkCodeService workCodeService,
            IEmployeeService employeeService,
            IWorkHourService workHourService
            )
        {
            this.ribbonService = ribbonService;
            this.saveCommand = new DelegateCommand<bool?>(this.Save, this.CanSave);
            this.deleteCommand = new DelegateCommand<bool?>(this.Delete, this.CanDelete);
            this.workCodeService=workCodeService;
            this.workHourService = workHourService;

            WorkCodes = new ObservableCollection<BasicWorkCodeVM>();
            employees = new ObservableCollection<BasicEmployeeVM>();
            workHours = new ObservableCollection<BasicWorkHourVM>();


            DeletedWorkHours = new List<BasicWorkHourVM>();

          

            Mapper.CreateMap<Employee, BasicEmployeeVM>();
            Mapper.CreateMap<BasicEmployeeVM, Employee>();
            Mapper.CreateMap<ICollection<Employee>, ICollection<BasicEmployeeVM>>();
            Mapper.CreateMap<ICollection<BasicEmployeeVM>, ICollection<Employee>>();
            
            Mapper.CreateMap<WorkCode, BasicWorkCodeVM>();
            Mapper.CreateMap<ICollection<WorkCode>, ICollection<BasicWorkCodeVM>>();
            Mapper.CreateMap<BasicWorkCodeVM, WorkCode>();
            Mapper.CreateMap<ICollection<BasicWorkCodeVM>, ICollection<WorkCode>>();
            
            Mapper.CreateMap<WorkHour, BasicWorkHourVM>();
            Mapper.CreateMap<BasicWorkHourVM, WorkHour>();
            Mapper.CreateMap<ICollection<WorkHour>, ICollection<BasicWorkHourVM>>();
            Mapper.CreateMap<ICollection<BasicWorkHourVM>, ICollection<WorkHour>>();

            foreach (Employee data in employeeService.GetAll())
            {
                employees.Add(Mapper.Map<Employee, BasicEmployeeVM>(data));
            }
            
            foreach (WorkCode data in workCodeService.GetAll())
            {
                WorkCodes.Add(Mapper.Map<WorkCode, BasicWorkCodeVM>(data));
            }

            foreach (WorkHour data in workHourService.GetAll())
            {
                data.ObjectState = ObjectState.Unchanged;
                workHours.Add(Mapper.Map<WorkHour, BasicWorkHourVM>(data));
            }
            WorkHoursCV = new ListCollectionView(WorkHours);
            WorkHoursCV.CurrentChanged += WorkHoursCV_CurrentChanged;
        }

        void WorkHoursCV_CurrentChanged(object sender, EventArgs e)
        {
            BasicWorkHourVM current = WorkHoursCV.CurrentItem as BasicWorkHourVM;
            if (current != null && current.ObjectState != ObjectState.Added)
            current.ObjectState = ObjectState.Modified;
        } 

        private bool CanCancel(bool? arg)
        {
            return true;
        }

        private void Cancel(bool? obj)
        {
          
        }

        private bool CanDelete(bool? arg)
        {
            return true;
        }

        private void Delete(bool? obj)
        {
            BasicWorkHourVM current = WorkHoursCV.CurrentItem as BasicWorkHourVM;
            if (current != null)
                DeletedWorkHours.Add(current);
            workHours.Remove(current);
        }

        private bool CanSave(bool? arg)
        {

            return true;
        }

        private void Save(bool? obj)
        {
            Mapper.CreateMap<BasicWorkCodeVM, WorkCode>();

            foreach (var data in workHours)
            {
                if (data.ObjectState == ObjectState.Added)
                    workHourService.Add(Mapper.Map<BasicWorkHourVM, WorkHour>(data));
                else if (data.ObjectState == ObjectState.Modified)
                    workHourService.Modify(Mapper.Map<BasicWorkHourVM, WorkHour>(data));

                data.ObjectState = ObjectState.Unchanged;
            }
            foreach (var data in DeletedWorkHours)
            {
                    workHourService.Remove(Mapper.Map<BasicWorkHourVM, WorkHour>(data).Id);
            }
            DeletedWorkHours.Clear();
            workHourService.Save();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ribbonService.RemoveRibbonItem(menu);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            menu = new RTIWorkCodeView();
            menu.SaveButton.Command = saveCommand;
            menu.DeleteButton.Command = deleteCommand;
            ribbonService.AddRibbonItem(menu, true);
        }
    }
}
