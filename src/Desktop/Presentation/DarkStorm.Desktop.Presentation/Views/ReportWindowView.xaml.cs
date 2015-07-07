using DarkStorm.Desktop.Presentation.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DarkStorm.Desktop.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ReportWindowView.xaml
    /// </summary>
    public partial class ReportWindowView : UserControl
    {
        public ReportWindowView(IUnityContainer Container)
        {
            InitializeComponent();
            var context = Container.Resolve<ReportWindowVM>();
            context.ReportViewerControl = _reportViewer;
            this.DataContext = context;
        }
    }
}
