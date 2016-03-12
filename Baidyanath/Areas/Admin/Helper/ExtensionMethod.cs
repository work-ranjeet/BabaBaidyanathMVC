using Baidyanath.Areas.Admin.Entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Baidyanath.Areas.Admin.Models
{
    public static class ExtensionMethod
    {
        public static async Task AddRowToAlbumDataTabel(this DataTable dt, CreateAlbum albumModel)
        {
            await Task.Run(() =>
            {
                DataRow dr = dt.NewRow();
                dr["AlbumType"] = albumModel.AlbumType;
                dr["AlbumTitle"] = albumModel.AlbumTitle;
                dr["AlbumInformation"] = albumModel.AlbumInformation;
                dr["AlbumLikeCount"] = albumModel.AlbumLikeCount;
                dr["AlbumOrder"] = albumModel.AlbumOrder;
                dr["IsActive"] = albumModel.IsActive;
                dt.Rows.Add(dr);
            });
        }
        public static async Task<BlockingCollection<Album>> GetAlbumObjectCollection(this DataTable dt)
        {
            return await Task.Run(() =>
            {
                BlockingCollection<Album> blockingCollection = new BlockingCollection<Album>();
                if (dt != null)
                {
                    dt.AsEnumerable().ToList().AsParallel().ForAll(row =>
                    {
                        blockingCollection.Add(new Album()
                        {
                            AlbumID = Convert.ToInt32(row["AlbumID"]),
                            AlbumInformation = Convert.ToString(row["AlbumInformation"]),
                            CoverUrl = Convert.ToString(row["CoverUrl"]),
                            AlbumLikeCount = Convert.ToInt32(row["AlbumLikeCount"]),
                            AlbumOrder = Convert.ToInt32(row["AlbumOrder"]),
                            AlbumTitle = Convert.ToString(row["AlbumTitle"]),
                            AlbumTypeID = Convert.ToInt32(row["AlbumTypeID"]),
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        });
                    });
                }

                return blockingCollection;
            });

        }
    }
}