using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Baidyanath.Models.DataAccess;

namespace Baidyanath.Models.Tourism
{

    public class TourismModel
    {
        private ConcurrentDictionary<string, string> _tourism;
        public ConcurrentDictionary<string, string> TourismDictionary
        {
            get
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "hi-IN")
                {
                }
                else
                {
                    if (_tourism == null)
                    {
                        GetToourismDetails();
                    }
                }
                return _tourism;
            }
        }

       

        public IEnumerable<ConcurrentDictionary<string, string>> TourismType
        {
            get
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "hi-IN")
                {
                    using (DBHelper dbHelper = new DBHelper())
                    {
                        string query = @" Select InformationID, InfTitleInd from Informations Info" +
                                        " left outer join InformationType InfType ON info.InfTYpeID = InfType.InfTYpeID" +
                                        " where InfType.InfType = @Tourism";

                        dbHelper.AddInParameter("@Tourism", "Tourism", DbType.String);

                        var reader = dbHelper.GetDataSet(query);

                        if (reader != null && reader.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in reader.Tables[0].Rows)
                            {
                                var dic = new ConcurrentDictionary<string, string>();
                                dic.TryAdd(dr["InformationID"].ToString(), dr["InfTitleInd"].ToString());
                                yield return dic;
                            }
                        }

                        query = null;
                        dbHelper.Dispose();
                    }
                }
                else
                {
                    using (DBHelper dbHelper = new DBHelper())
                    {
                        string query = @" Select InformationID, InfTitle from Informations Info" +
                                        " left outer join InformationType InfType ON info.InfTYpeID = InfType.InfTYpeID" +
                                        " where InfType.InfType = @Tourism";

                        dbHelper.AddInParameter("@Tourism", "Tourism", DbType.String);

                        var reader = dbHelper.GetDataSet(query);

                        if (reader != null && reader.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in reader.Tables[0].Rows)
                            {
                                var dic = new ConcurrentDictionary<string, string>();
                                dic.TryAdd(dr["InformationID"].ToString(), dr["InfTitle"].ToString());
                                yield return dic;
                            }
                        }

                        query = null;
                        dbHelper.Dispose();
                    }


                }
            }
        }

        public void GetToourismDetails(string TourismType = "Tourism")
        {

            using (DBHelper dbHelper = new DBHelper())
            {
                string query = @" Select InfTitle,InfTitleInd, Information, InformationInd from Informations Info" +
                                " left outer join InformationType InfType ON info.InfTYpeID = InfType.InfTYpeID" +
                                " where InfType.InfType = @Tourism";

                dbHelper.AddInParameter("@Tourism", TourismType, DbType.String);

                var reader = dbHelper.GetDataSet(query);

                this._tourism = new ConcurrentDictionary<string, string>();
               // this._tourismHindi = new ConcurrentDictionary<string, string>();
                if (reader != null && reader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in reader.Tables[0].Rows)
                    {
                        this._tourism.TryAdd(dr["InfTitle"].ToString(), dr["Information"].ToString());
                       // this._tourismHindi.TryAdd(dr["InfTitleInd"].ToString(), dr["InformationInd"].ToString());
                    }
                }
                //while (reader.Read())
                //{
                //    reader.GetString(1);
                //}

                query = null;
                reader.Dispose();
                dbHelper.Dispose();
            }
        }
    }
}