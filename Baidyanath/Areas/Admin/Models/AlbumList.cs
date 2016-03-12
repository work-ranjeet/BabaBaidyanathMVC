using Baidyanath.Areas.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Data;

namespace Baidyanath.Areas.Admin.Models
{
    public class AlbumList :IDisposable
    {

        public Task<BlockingCollection<Album>> GetAllAlbums
        {
            get
            {
                return Task.Run(() =>
                {
                    return new DbQuery().GetAlbums(null).Result.GetAlbumObjectCollection();
                });
            }
        }


        #region Memory
        private bool disposed = false;
        ~AlbumList()
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
