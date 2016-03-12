using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Baidyanath.Helper;
using Baidyanath.Extension;
using Baidyanath.Models.Account;
using Baidyanath.Models.Helper;

namespace Baidyanath.Models.DataAccess
{
    public class DBQuery : IDisposable
    {
        #region ///Information
        public async Task<DataTable> GetInfoForUserView(Int64 InfTypeID, bool IsActive = true)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.AddInParameter("@InfTypeID", InfTypeID, DbType.Int64);
                    dbHelper.AddInParameter("@IsActive", Convert.ToInt16(IsActive ? 1 : 0), DbType.Int16);
                    var dataSet = dbHelper.GetDataSet("SelectUserViewLatestPost", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }

        public async Task<DataTable> GetInfomationDescription(Int64? InformationID, bool IsActive = true)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.AddInParameter("@InformationID", InformationID ?? 0, DbType.Int64);
                    dbHelper.AddInParameter("@IsActive", Convert.ToInt16(IsActive ? 1 : 0), DbType.Int16);
                    var dataSet = dbHelper.GetDataSet("SelectLatestPostDetail", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }
        #endregion

        #region /// MantraController
        public async Task<DataTable> MantraDetails(Int64 ParentID, bool IsActive = true)
        {
            return await Task.Run(() =>
           {
               return MantraDetails(0, ParentID, true);
           });
        }
        public async Task<DataTable> MantraDetails(Int64 MantraID, Int64 ParentID, bool IsActive = true)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    if (MantraID > 0)
                    {
                        dbHelper.AddInParameter("@MantraID", MantraID, DbType.Int64);
                        dbHelper.AddInParameter("@MantraParentID", -1, DbType.Int64);
                    }
                    else
                    {
                        dbHelper.AddInParameter("@MantraID", -1, DbType.Int64);
                        dbHelper.AddInParameter("@MantraParentID", ParentID, DbType.Int64);
                    }

                    dbHelper.AddInParameter("@IsActive", Convert.ToInt16(IsActive ? 1 : 0), DbType.Int16);
                    var dataSet = dbHelper.GetDataSet("GetMantra", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }

        #endregion

        #region /// ServicesController
        public async Task<DataTable> SerViceDetails(string ServiceType, bool IsActive = true)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    Int16 active = Convert.ToInt16(IsActive ? 1 : 0);

                    dbHelper.AddInParameter("@ServiceType", ServiceType, DbType.String);
                    dbHelper.AddInParameter("@IsActive", active, DbType.Int16);
                    var dataSet = dbHelper.GetDataSet("GetServices", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }
        #endregion

        #region /// Account Controller

        #region /// User Registration
        public async Task<string> IsValid(string UserName, string Password)
        {
            return await Task.Run(() =>
            {
                string resullt = string.Empty;
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.AddInParameter("@UserName", UserName, DbType.String);
                    dbHelper.AddInParameter("@Password", Encryption.Encode(Password), DbType.String);
                    var reader = dbHelper.GetDataReader(@"SELECT Email, FName FROM Users WHERE UserID = @UserName AND Password = @Password");

                    if (reader.Read())
                    {
                        resullt = reader.GetString(1);
                    }
                    reader.Dispose();
                    dbHelper.Dispose();

                }
                return resullt;
            });

        }

        public async Task<bool> IsAvailable(string UserID)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.AddInParameter("@UserID", UserID, DbType.String);
                    var reader = dbHelper.GetDataReader(@"SELECT Email, FName FROM Users WHERE UserID = @UserID");


                    if (reader.Read())
                    {
                        reader.Dispose();
                        dbHelper.Dispose();
                        return true;
                    }
                    else
                    {
                        reader.Dispose();
                        dbHelper.Dispose();
                        return false;
                    }
                }
            });
        }

        public async Task<bool> RegisterUser(UserRegistrationModel userRegistration)
        {
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dt = await DataTableHelper.GetRegistrationDataTable();
                await dt.AddRowToRegistrationDataTabel(userRegistration);

                dbHelper.AddInParameter("@UserObject", dt);
                dbHelper.ExecuteQueryDataTable("RegisterUser", CommandType.StoredProcedure);

            }
            return true;
        }
        #endregion

        #region ///User Profile select View
        public async Task<DataTable> GetUserProfileHeaderData(string UserID)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.AddInParameter("@UserID", UserID, DbType.String);
                    var dataSet = dbHelper.GetDataSet("GetUserProfileHeaderData", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }
        public async Task<DataTable> GetUserProfileBasicData(string UserID)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.AddInParameter("@UserID", UserID, DbType.String);
                    var dataSet = dbHelper.GetDataSet("GetUserProfileBasicData", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }
        public async Task<DataTable> GetUserProfileLocationData(string UserID)
        {
            DataTable dataTable = null;
            return await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.AddInParameter("@UserID", UserID, DbType.String);
                    var dataSet = dbHelper.GetDataSet("GetUserProfileLocationData", CommandType.StoredProcedure);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }

                    return dataTable;
                }
            });
        }
        #endregion
        #region ///User Profile Update View

        public async Task<bool> UpdatetUserProfileBasicData(UserProfileBasicModel model)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = await DataTableHelper.GetRegistrationDataTable();
                await dt.AddRowToUserProfileBasicTabel(model);

                //dbHelper.AddInParameter("@UserObject", dt);
                //dbHelper.ExecuteQueryDataTable("RegisterUser", CommandType.StoredProcedure);

            }
            return true; 
        }
        public async Task<bool> UpdatetUserProfileLocationData(UserProfileLocationModel model)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = await DataTableHelper.GetRegistrationDataTable();
                await dt.AddRowToUserProfileLocationTabel(model);

                //dbHelper.AddInParameter("@UserObject", dt);
                //dbHelper.ExecuteQueryDataTable("RegisterUser", CommandType.StoredProcedure);

            }
            return true;
        }
        #endregion

        #endregion

        #region /// Shared
        public string GetTotalHit()
        {
            Int64? resullt = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                var reader = dbHelper.GetDataReader("SelectTotalVistors", CommandType.StoredProcedure);

                if (reader.Read())
                {
                    resullt = reader.GetInt64(0);
                }
                reader.Close();
                reader.Dispose();
                dbHelper.CurrentCommand.Connection.Close();
                dbHelper.Dispose();

            }
            return Convert.ToString(resullt);


        }

        public async void InsertTotalHit()
        {
            await Task.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    dbHelper.ExecuteQuery("InsTotalVistors");
                }
            });
        }

        #endregion

        #region /// Memory
        private bool disposed = false;

        ~DBQuery()
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