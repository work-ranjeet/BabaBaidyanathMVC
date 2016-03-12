using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Baidyanath.Helper;
using Baidyanath.Models.DataAccess;

namespace Baidyanath.Models.Informations
{
    public class LatestPostModel : IDisposable
    {

        public Task<BlockingCollection<InformationEntity>> UserLatestPost
        {
            get
            {
                return Task.Run(() =>
                {
                    return new DBQuery().GetInfoForUserView(2).Result.GetInformationObjectCollection();
                });
            }
        }

        public async Task<BlockingCollection<InformationEntity>> LatestPostDescription(Int64? InformationID)
        {

            return await Task.Run(() =>
            {
                return new DBQuery().GetInfomationDescription(InformationID).Result.GetInformationObjectCollection();
            });

        }

        #region Memory
        private bool disposed = false;
        ~LatestPostModel()
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