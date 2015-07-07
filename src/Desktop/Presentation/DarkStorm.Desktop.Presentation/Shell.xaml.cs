using DarkStorm.Desktop.Presentation.ViewModels;
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
using System.Windows.Shapes;

namespace DarkStorm.Desktop.Presentation
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell 
    {
        public Shell(IUnityContainer Container)
        {
            InitializeComponent();
            var context = Container.Resolve<ShellVM>();
            
              this.DataContext = context;
        }
    }
}
