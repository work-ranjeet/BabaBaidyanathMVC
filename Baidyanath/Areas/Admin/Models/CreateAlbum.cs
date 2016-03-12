using Baidyanath.Areas.Admin.Entity;
using Baidyanath.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Baidyanath.Areas.Admin.Models
{
    public class CreateAlbum : Album
    {
        [Required(ErrorMessage = "Please select album type")]
        [Display(Name = "Enter Album Type")]
        public string AlbumType { get; set; }

        #region Memory
        private bool disposed = false;
        ~CreateAlbum()
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
