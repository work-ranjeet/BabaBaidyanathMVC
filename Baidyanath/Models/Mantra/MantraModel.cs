using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Baidyanath.Helper;
using Baidyanath.Models.DataAccess;

namespace Baidyanath.Models.Mantra
{
    public class MantraModel : IDisposable
    {
        public Task<BlockingCollection<MantraEntity>> Mantra
        {
            get
            {
                return Task.Run(() =>
                {
                    return new DBQuery().MantraDetails(0).Result.GetMantraObjectCollection();
                });
            }
        }

        public async Task<BlockingCollection<MantraEntity>> MantraByParentID(Int64 ParentID)
        {
            
                return await Task.Run(() =>
                {
                    return new DBQuery().MantraDetails(ParentID).Result.GetMantraObjectCollection();
                });
          
        }

        public async Task<BlockingCollection<MantraEntity>> MantraDescription(Int64 MantraID)
        {

            return await Task.Run(() =>
            {
                return new DBQuery().MantraDetails(MantraID, 0).Result.GetMantraObjectCollection();
            });

        }

      

        #region Memory
        private bool disposed = false;
        ~MantraModel()
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