using System;
namespace Baidyanath.Areas.Admin.Entity
{
    interface IAlbum : IDisposable
    {
        int AlbumID { get; set; }
        string AlbumInformation { get; set; }
        int AlbumLikeCount { get; set; }
        int AlbumOrder { get; set; }
        string AlbumTitle { get; set; }
        string CoverUrl { get; set; }
        int AlbumTypeID { get; set; }
        bool IsActive { get; set; }
    }
}
