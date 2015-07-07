using DarkStorm.Desktop.Modules.TimeCard.UI.ViewModels;
using Microsoft.Practices.Unity;
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

namespace DarkStorm.Desktop.Modules.TimeCard.UI.Views
{
    /// <summary>
    /// Interaction logic for WorkCodeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        public EmployeeView(IUnityContainer Container)
        {
            InitializeComponent();
            var context = Container.Resolve<EmployeeVM>();
            
              this.DataContext = context;
        }
    }
}
