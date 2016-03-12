using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Baidyanath.Helper;
using Baidyanath.Models.DataAccess;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Baidyanath.Models.Services
{
    public class ServicesModel
    {
       
        public Task<BlockingCollection<ServiceEntity>> Services
        {
            get
            {
                return Task.Run(() =>
                {
                    return new DBQuery().SerViceDetails(ServiceType.Service).Result.GetServiceObjectCollection();
                });
            }
        }


        #region Memory
        private bool disposed = false;
        ~ServicesModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }


                disposed = true;
            }
        }
        #endregion
    }


}