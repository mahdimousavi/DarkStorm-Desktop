using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Infrastructure.Application
{
    public class AppModule:IAppModule
    {
        public void RegisterBCItems()
        {
           
        }
    }
    public interface IAppModule
    {
        void RegisterBCItems();
    }
}
