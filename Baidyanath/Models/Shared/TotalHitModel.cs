using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Baidyanath.Models.DataAccess;
using Baidyanath.Helper;

namespace Baidyanath.Models.Shared
{
    public class TotalHitModel : IDisposable
    {

        public string GetTotalHit()
        {
            DBQuery dbQuery = new DBQuery();
            return Helpers.CommaSeperatedNumber(dbQuery.GetTotalHit());
        }

        public void InsertTotalHit()
        {
            Task.Run(() =>
             {
                 DBQuery dbQuery = new DBQuery();
                 dbQuery.InsertTotalHit();
             });

        }

        #region Memory
        private bool disposed = false;
        ~TotalHitModel()
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