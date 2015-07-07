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
    public class WorkCodeVM :BindableBase, INavigationAware
    {
        private IRibbonService ribbonService;
        private RTIWorkCodeView menu;
        
        private readonly DelegateCommand<bool?> saveCommand;
        private readonly DelegateCommand<bool?> deleteCommand;

        private IWorkCodeService workCodeService;
        public ObservableCollection<BasicWorkCodeVM> WorkCodes { set; get; }
        public ICollectionView WorkCodesCV { get; private set; }
        public List<BasicWorkCodeVM> DeletedWorkCodes { set; get; }
        public BasicWorkCodeVM SelectedWorkCode { set; get; }
        public DelegateCommand SortListCommand{ set; get; }
        public string SortData { set; get; }
        public Type EntityType { set; get; }
        public DelegateCommand<bool?> Dumy{ set; get; }
        public WorkCodeVM(
            IRibbonService ribbonService,
            IWorkCodeService workCodeService
            )
        {
            EntityType =typeof(BasicWorkCodeVM);
            this.ribbonService = ribbonService;
            this.saveCommand = new DelegateCommand<bool?>(this.Save, this.CanSave);
            this.deleteCommand = new DelegateCommand<bool?>(this.Delete, this.CanDelete);
            this.workCodeService=workCodeService;

            WorkCodes = new ObservableCollection<BasicWorkCodeVM>();
            DeletedWorkCodes = new List<BasicWorkCodeVM>();
            Mapper.CreateMap<WorkCode, BasicWorkCodeVM>();
            foreach (WorkCode data in workCodeService.GetAll())
            {
                data.ObjectState = ObjectState.Unchanged;
                
                WorkCodes.Add(Mapper.Map<WorkCode, BasicWorkCodeVM>(data));
            }
            WorkCodesCV = new ListCollectionView(WorkCodes);
            WorkCodesCV.CurrentChanged += WorkCodesCV_CurrentChanged;

        }


        void WorkCodesCV_CurrentChanged(object sender, EventArgs e)
        {
            BasicWorkCodeVM current = WorkCodesCV.CurrentItem as BasicWorkCodeVM;
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
            DeletedWorkCodes.Add(SelectedWorkCode);
            WorkCodes.Remove(SelectedWorkCode);
        }

        private bool CanSave(bool? arg)
        {

            return true;
        }

        private void Save(bool? obj)
        {
            Mapper.CreateMap<BasicWorkCodeVM, WorkCode>();

            foreach (var data in WorkCodes)
            {
                if (data.ObjectState == ObjectState.Added)
                    workCodeService.Add(Mapper.Map<BasicWorkCodeVM,WorkCode >(data));
                else if (data.ObjectState == ObjectState.Modified)
                    workCodeService.Modify(Mapper.Map<BasicWorkCodeVM, WorkCode>(data));

                data.ObjectState = ObjectState.Unchanged;
            }
            foreach (var data in DeletedWorkCodes)
            {
                    workCodeService.Remove(Mapper.Map<BasicWorkCodeVM, WorkCode>(data).Id);
            }
            DeletedWorkCodes.Clear();
            workCodeService.Save();
        }

        private bool CanGo(bool? arg)
        {
            return true;
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
