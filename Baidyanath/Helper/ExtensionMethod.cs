using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Baidyanath.Models.Account;

namespace Baidyanath.Helper
{
    public static class ExtensionMethod
    {
        public static async Task AddRowToRegistrationDataTabel(this DataTable dt, UserRegistrationModel userRegistration)
        {
            await Task.Run(() =>
            {
                DataRow dr = dt.NewRow();
                dr["UserID"] = userRegistration.UserID;
                dr["GroupType"] = GroupType.General;
                dr["Email"] = userRegistration.Email;
                dr["FName"] = userRegistration.FirstName;
                dr["MName"] = string.IsNullOrEmpty(userRegistration.MiddleName) ? string.Empty : userRegistration.MiddleName;
                dr["LName"] = userRegistration.LastName;
                dr["Password"] = Encryption.Encode(userRegistration.Password);
                dr["Gender"] = userRegistration.Gender;
                dr["Dob"] = userRegistration.Dob;
                dr["MariedSatus"] = userRegistration.IsMarried.ToUpper() == "SINGLE" ? 0 : 1;
                dr["Mobile"] = userRegistration.Mobile;
                dt.Rows.Add(dr);
            });
        }

        public static async Task<BlockingCollection<ServiceEntity>> GetServiceObjectCollection(this DataTable dt)
        {
            return await Task.Run(() =>
            {
                BlockingCollection<ServiceEntity> blockingCollection = new BlockingCollection<ServiceEntity>();
                dt.AsEnumerable().ToList().AsParallel().ForAll(row =>
                {
                    blockingCollection.Add(new ServiceEntity()
                    {
                        ServiceID = Convert.ToInt64(row["ServiceID"]),
                        ServiceParentID = Convert.ToInt64(row["ServiceParentID"]),
                        ServiceTitle = Convert.ToString(row["ServiceTitle"]),
                        ServiceInformation = Convert.ToString(row["ServiceInformation"]),
                        ServiceTitleInd = Convert.ToString(row["ServiceTitleInd"]),
                        ServiceInformationInd = Convert.ToString(row["ServiceInformationInd"]),
                        ServiceIcon = Convert.ToString(row["ServiceIcon"]),
                        ServiceLikeCount = Convert.ToInt32(row["ServiceLikeCount"]),
                        ServiceOrder = Convert.ToInt32(row["ServiceOrder"])
                    });
                });

                return blockingCollection;
            });

        }

        public static async Task<BlockingCollection<MantraEntity>> GetMantraObjectCollection(this DataTable dt)
        {
            return await Task.Run(() =>
            {
                BlockingCollection<MantraEntity> blockingCollection = new BlockingCollection<MantraEntity>();
                if (dt == null)
                    return blockingCollection;

                dt.AsEnumerable().ToList().AsParallel().ForAll(row =>
                {
                    blockingCollection.Add(new MantraEntity()
                    {
                        MantraID = Convert.ToInt64(row["MantraId"]),
                        MantraParentID = Convert.ToInt64(row["MantraParentID"]),
                        MantraTitle = string.IsNullOrEmpty(Convert.ToString(row["MantraTitle"])) ? Convert.ToString(row["MantraTitleInd"]) : Convert.ToString(row["MantraTitle"]),
                        MantraInformation = string.IsNullOrEmpty(Convert.ToString(row["MantraInformation"])) ? Convert.ToString(row["MantraInformationInd"]) : Convert.ToString(row["MantraInformation"]),
                        MantraTitleInd = Convert.ToString(row["MantraTitleInd"]),
                        MantraInformationInd = Convert.ToString(row["MantraInformationInd"]),
                        MantraIcon = Convert.ToString(row["MantraIcon"]),
                        MantraLikeCount = Convert.ToInt32(row["MantraLikeCount"]),
                        MantraOrder = Convert.ToInt32(row["MantraOrder"]),
                        ChildCount = Convert.ToInt32(row["ChildCount"])
                    });
                });

                return blockingCollection;
            });

        }

        public static async Task<BlockingCollection<InformationEntity>> GetInformationObjectCollection(this DataTable dt)
        {
            return await Task.Run(() =>
            {
                BlockingCollection<InformationEntity> blockingCollection = new BlockingCollection<InformationEntity>();
                if (dt == null)
                    return blockingCollection;

                dt.AsEnumerable().ToList().AsParallel().ForAll(row =>
                {
                    blockingCollection.Add(new InformationEntity()
                    {
                        InformationID = Convert.ToInt64(row["InformationID"]),
                        InfTitle = string.IsNullOrEmpty(Convert.ToString(row["InfTitle"])) ? Convert.ToString(row["InfTitleInd"]) : Convert.ToString(row["InfTitle"]),
                        Information = string.IsNullOrEmpty(Convert.ToString(row["Information"])) ? Convert.ToString(row["InformationInd"]) : Convert.ToString(row["Information"]),
                        InfTitleInd = Convert.ToString(row["InfTitleInd"]),
                        InformationInd = Convert.ToString(row["InformationInd"]),
                        InfOrder = Convert.ToInt32(row["InfOrder"])
                    });
                });

                return blockingCollection;
            });

        }

        #region /// User Profile View
        public static async Task<UserProfileHeaderModel> GetUserProfileHeader(this DataTable dt)
        {
            return await Task.Run(() =>
            {
                UserProfileHeaderModel userProfileHeader = new UserProfileHeaderModel();
                if (dt != null)
                {
                    DataRow row = dt.Rows[0];

                    userProfileHeader.UserID = Convert.ToString(row["UserID"]);
                    userProfileHeader.Email = Convert.ToString(row["Email"]);
                    userProfileHeader.ProfileCreatedOn = Convert.ToDateTime(row["ProfileCreatedOn"]);

                    if (row["ProfileCompletionPercent"] != null)
                        userProfileHeader.ProfileCompletionPercent = Convert.ToDouble(row["ProfileCompletionPercent"]);

                    if (row["LastLoginDate"] != null && !string.IsNullOrEmpty(Convert.ToString(row["LastLoginDate"])))
                        userProfileHeader.LastLoginDate = Convert.ToDateTime(row["LastLoginDate"]);

                    if (row["Horoscope"] != null && !string.IsNullOrEmpty(Convert.ToString(row["Horoscope"])))
                        userProfileHeader.Horoscope = Convert.ToString(row["Horoscope"]);
                }

                return userProfileHeader;
            });
        }
        public static async Task<UserProfileBasicModel> GetUserProfileBasic(this DataTable dt)
        {
            return await Task.Run(() =>
            {
                UserProfileBasicModel userProfileBasicModel = new UserProfileBasicModel();
                DataRow row = dt.Rows[0];

                userProfileBasicModel.UserID = Convert.ToString(row["UserID"]);
                userProfileBasicModel.FName = Convert.ToString(row["FName"]);
                userProfileBasicModel.MName = Convert.ToString(row["MName"]);
                userProfileBasicModel.LName = Convert.ToString(row["LName"]);
                userProfileBasicModel.DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]);
                userProfileBasicModel.Age = Convert.ToInt32(row["Age"]);
                userProfileBasicModel.Gender = Convert.ToString(row["Gender"]);
                userProfileBasicModel.MariedStatus = Convert.ToString(row["MariedStatus"]);
                userProfileBasicModel.SunShine = Convert.ToString(row["SunShine"]);

                return userProfileBasicModel;
            });
        }
        public static async Task<UserProfileLocationModel> GetUserProfileLocation(this DataTable dt)
        {
            return await Task.Run(() =>
            {
                UserProfileLocationModel userProfileLocationModel = new UserProfileLocationModel();
                DataRow row = dt.Rows[0];

                userProfileLocationModel.UserID = Convert.ToString(row["UserID"]);
                userProfileLocationModel.Address1 = Convert.ToString(row["Address1"]);
                userProfileLocationModel.Address2 = Convert.ToString(row["Address2"]);
                userProfileLocationModel.City = Convert.ToString(row["City"]);
                userProfileLocationModel.State = Convert.ToString(row["State"]);
                userProfileLocationModel.Country = Convert.ToString(row["Country"]);
                userProfileLocationModel.Mobile = Convert.ToString(row["Mobile"]);
                userProfileLocationModel.Email = Convert.ToString(row["Email"]);


                return userProfileLocationModel;
            });
        }
        public static async Task AddRowToUserProfileBasicTabel(this DataTable dt, UserProfileBasicModel basic)
        {
            await Task.Run(() =>
            {
                DataRow dr = dt.NewRow();
                //dr["GroupType"] = GroupType.General;
                //dr["Email"] = basic.Email;
                //dr["FName"] = basic.FirstName;
                //dr["MName"] = string.IsNullOrEmpty(basic.MiddleName) ? string.Empty : basic.MiddleName;
                //dr["LName"] = basic.LastName;
                //dr["Password"] = Encryption.Encode(basic.Password);
                //dr["Gender"] = basic.Gender;
                //dr["Dob"] = basic.Dob;
                //dr["MariedSatus"] = basic.IsMarried.ToUpper() == "SINGLE" ? 0 : 1;
                //dr["Mobile"] = basic.Mobile;
                dt.Rows.Add(dr);
            });
        }
        public static async Task AddRowToUserProfileLocationTabel(this DataTable dt, UserProfileLocationModel basic)
        {
            await Task.Run(() =>
            {
                DataRow dr = dt.NewRow();
                //dr["GroupType"] = GroupType.General;
                //dr["Email"] = basic.Email;
                //dr["FName"] = basic.FirstName;
                //dr["MName"] = string.IsNullOrEmpty(basic.MiddleName) ? string.Empty : basic.MiddleName;
                //dr["LName"] = basic.LastName;
                //dr["Password"] = Encryption.Encode(basic.Password);
                //dr["Gender"] = basic.Gender;
                //dr["Dob"] = basic.Dob;
                //dr["MariedSatus"] = basic.IsMarried.ToUpper() == "SINGLE" ? 0 : 1;
                //dr["Mobile"] = basic.Mobile;
                dt.Rows.Add(dr);
            });
        }
        #endregion

    }
}