using DarkStorm.Desktop.Infrastructure.Application;
using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Presentation.RibbonTabItems;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Presentation.ViewModels
{
    public class ReportWindowVM : BindableBase, INavigationAware
    {

        private ReportViewer reportViewerControl;

        public ReportViewer ReportViewerControl
        {
            get { return reportViewerControl; }
            set { reportViewerControl = value; }
        }

        private bool _isReportViewerLoaded;

        public bool IsReportViewerLoaded
        {
            get { return _isReportViewerLoaded; }
            set { _isReportViewerLoaded = value; }
        }
        private object dataToDisplay;
        public object DataToDisplay
        {
            get { return dataToDisplay; }
            set { dataToDisplay = value; }
        }
        private string reportPath;

        public string ReportPath
        {
            get { return reportPath; }
            set { reportPath = value; }
        }
        private ReportParameter[] parameters;

        public ReportParameter[] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        private string reportDataSourceName;

        public string ReportDataSourceName
        {
            get { return reportDataSourceName; }
            set { reportDataSourceName = value; }
        }

        private IRibbonService ribbonService;
        private RTIReportWindow menu;

        public ReportWindowVM(IRibbonService ribbonService)
        {
            this.ribbonService = ribbonService;
            printCommand=new DelegateCommand(Print);
        }

        private void Print()
        {
            ReportViewerControl.PrintDialog();
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
            int hash = int.Parse(navigationContext.Parameters["DataToDisplay"].ToString());
            dataToDisplay = (object)AppParameters.Request(hash);
            reportPath = navigationContext.Parameters["ReportPath"].ToString();
            parameters = (ReportParameter[])navigationContext.Parameters["Parameters"];
            reportDataSourceName = navigationContext.Parameters["ReportDataSourceName"].ToString();

            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource = new
                Microsoft.Reporting.WinForms.ReportDataSource();

                reportDataSource.Name = reportDataSourceName;

                reportDataSource.Value = DataToDisplay;
                this.ReportViewerControl.LocalReport.DataSources.Add(reportDataSource);
                this.ReportViewerControl.LocalReport.ReportPath = ReportPath;

                if (parameters != null)
                    this.ReportViewerControl.LocalReport.SetParameters(parameters);
                ReportViewerControl.RefreshReport();
                _isReportViewerLoaded = true;
            }

            menu = new RTIReportWindow();
            menu.PrintButton.Command = printCommand;
            ribbonService.AddRibbonItem(menu, true);
        }

        private readonly DelegateCommand printCommand;
    }
}
