using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Baidyanath.Areas.Admin.Models
{
    public class DataTableHelper
    {

        public static async Task<DataTable> GetAlbumDataTable()
        {
            return await Task.Run(() =>
            {
                DataTable registration = new DataTable();
                registration.Columns.Add(new DataColumn("AlbumType", typeof(string)));
                registration.Columns.Add(new DataColumn("AlbumTitle", typeof(string)));
                registration.Columns.Add(new DataColumn("AlbumInformation", typeof(string)));
                registration.Columns.Add(new DataColumn("CoverUrl", typeof(string)));                
                registration.Columns.Add(new DataColumn("AlbumLikeCount", typeof(int)));
                registration.Columns.Add(new DataColumn("AlbumOrder", typeof(int)));
                registration.Columns.Add(new DataColumn("IsActive", typeof(int)));
                return registration;
            });
        }

    }
}