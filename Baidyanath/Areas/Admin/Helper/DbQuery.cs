using Baidyanath.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Baidyanath.Areas.Admin.Models
{
    public class DbQuery
    {
        public async Task<bool> CreateAlbum(CreateAlbum CreateAlbumModel)
        {
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dt = await DataTableHelper.GetAlbumDataTable();
                await dt.AddRowToAlbumDataTabel(CreateAlbumModel);

                dbHelper.AddInParameter("@AlbumTable", dt);
                dbHelper.ExecuteQueryDataTable("InsertAlbums", CommandType.StoredProcedure);

            }
            return true;
        }

        public async Task<DataTable> GetAlbums(int? AlbumTypeID, bool IsActive = true)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {

                    dbHelper.AddInParameter("@AlbumTypeID", AlbumTypeID, DbType.Int32);
                    dbHelper.AddInParameter("@IsActive", Convert.ToInt16(IsActive ? 1 : 0), DbType.Int16);
                    var dataSet = dbHelper.GetDataSet("GetAlbums", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }
    }
}