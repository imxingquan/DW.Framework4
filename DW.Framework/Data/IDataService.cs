using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DW.Framework.Data
{
   
    public interface IDataService : IServiceProvider
    {
        void AddService(Type serviceType, object serviceInstance);
        void RemoveService(Type serviceType);
        void ClearService();
    }

}
