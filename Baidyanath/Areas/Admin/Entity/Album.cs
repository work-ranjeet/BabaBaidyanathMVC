using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Baidyanath.Areas.Admin.Entity
{
    public class Album : IAlbum
    {
        public int AlbumID { get; set; }
        public int AlbumTypeID { get; set; }

        [Display(Name = "Album Title")]
        [Required(ErrorMessage = "Please enter album title")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Album description")]
        public string AlbumInformation { get; set; }

        [Display(Name = "Cover Image")]
        public string CoverUrl { get; set; }

        [Display(Name = "Album Like Count")]
        public int AlbumLikeCount { get; set; }

        [Display(Name = "Album Order")]
        [Required(ErrorMessage = "Enter album order (123..")]
        public int AlbumOrder { get; set; }

        [Display(Name = "Is Active")]
        [Required(ErrorMessage = "Select Is Active or Not")]
        public bool IsActive { get; set; }



        #region Memory
        private bool disposed = false;
        ~Album()
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