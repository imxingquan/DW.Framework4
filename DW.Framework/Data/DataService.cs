using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DW.Framework.Data
{
    public class DataService : IDataService
    {
        private Hashtable serviceTable;
        
        public DataService()
        {
            serviceTable = new Hashtable();
        }

        public void AddService(Type serviceType, object serviceInstance)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (serviceInstance == null)
            {
                throw new ArgumentNullException("serviceInstance");
            }
            if (!serviceType.IsInstanceOfType(serviceInstance)) // service must is instance.
            {
                throw new ArgumentException("ExceptionStringTable.AddServiceMustBeCalledWithAnInstanceOfSpecifiedType", "serviceInstance");
            }
            this.serviceTable.Add(serviceType, serviceInstance);
        }

        public void RemoveService(Type serviceType)
        {
            if (serviceType == null)
            {
                return;
                //throw new ArgumentNullException("serviceType");
            }
            this.serviceTable.Remove(serviceType);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
                //throw new ArgumentNullException("serviceType");
            }
            return this.serviceTable[serviceType];
        }

        public void ClearService()
        {
            if (serviceTable != null)
            {
                serviceTable.Clear();
            }
        }
    }
}
