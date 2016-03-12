using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Baidyanath.Models.Helper
{
    public static class DataTableHelper
    {
        public static async Task<DataTable> GetRegistrationDataTable()
        {
            return await Task.Run(() =>
            {
                DataTable registration = new DataTable();
                registration.Columns.Add(new DataColumn("UserID", typeof(string)));
                registration.Columns.Add(new DataColumn("GroupType", typeof(string)));
                registration.Columns.Add(new DataColumn("Email", typeof(string)));
                registration.Columns.Add(new DataColumn("FName", typeof(string)));
                registration.Columns.Add(new DataColumn("MName", typeof(string)));
                registration.Columns.Add(new DataColumn("LName", typeof(string)));
                registration.Columns.Add(new DataColumn("Password", typeof(string)));
                registration.Columns.Add(new DataColumn("Gender", typeof(string)));

                registration.Columns.Add(new DataColumn("Dob", typeof(DateTime)));
                registration.Columns.Add(new DataColumn("MariedSatus", typeof(Int16)));
                registration.Columns.Add(new DataColumn("Mobile", typeof(string)));

                return registration;
            });
        }


    }
}