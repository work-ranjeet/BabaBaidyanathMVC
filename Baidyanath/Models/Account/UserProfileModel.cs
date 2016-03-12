using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Baidyanath.Models.DataAccess;
using Baidyanath.Helper;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Baidyanath.Resources;

namespace Baidyanath.Models.Account
{
    public class UserProfileHeaderModel : IDisposable
    {
        public string UserID { get; set; }

        public string Email { get; set; }

        public double ProfileCompletionPercent { get; set; }

        public DateTime ProfileCreatedOn { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string Horoscope { get; set; }


        #region Memory
        private bool disposed = false;
        ~UserProfileHeaderModel()
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

    public class UserProfileBasicModel : IDisposable
    {
        public string UserID { get; set; }

        [Required(ErrorMessageResourceName = "EnterFirstName", ErrorMessageResourceType = typeof(Resource))]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessageResourceName = "InvalidName", ErrorMessageResourceType = typeof(Resource))]
        public string FName { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessageResourceName = "InvalidName", ErrorMessageResourceType = typeof(Resource))]
        public string MName { get; set; }

        [Required(ErrorMessageResourceName = "EnterLastName", ErrorMessageResourceType = typeof(Resource))]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessageResourceName = "InvalidName", ErrorMessageResourceType = typeof(Resource))]
        public string LName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceName = "DateOfBirth", ErrorMessageResourceType = typeof(Resource))]
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string MariedStatus { get; set; }

        public string SunShine { get; set; }

        #region Memory
        private bool disposed = false;
        ~UserProfileBasicModel()
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

    public class UserProfileLocationModel : IDisposable
    {
        public string UserID { get; set; }

        [Required(ErrorMessageResourceName = "EnterAddress", ErrorMessageResourceType = typeof(Resource))]
        public string Address1 { get; set; }

        [Required(ErrorMessageResourceName = "EnterAddress", ErrorMessageResourceType = typeof(Resource))]
        public string Address2 { get; set; }

        [Required(ErrorMessageResourceName = "EnterCity", ErrorMessageResourceType = typeof(Resource))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "EnterState", ErrorMessageResourceType = typeof(Resource))]
        public string State { get; set; }

        [Required(ErrorMessageResourceName = "EnterCountry", ErrorMessageResourceType = typeof(Resource))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceName = "EnterMobileNo", ErrorMessageResourceType = typeof(Resource))]
        public string Mobile { get; set; }

        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterEmail", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        #region Memory
        private bool disposed = false;
        ~UserProfileLocationModel()
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

    public class UserProfile : IDisposable
    {
        public UserProfileHeaderModel Header { get; set; }
        public UserProfileBasicModel Basic { get; set; }
        public UserProfileLocationModel Location { get; set; }

        public async Task<UserProfile> GetUserProfile(string Email)
        {
            return await Task.Run(() =>
           {
               DBQuery dbQuery = new DBQuery();

               Task<Task<UserProfileHeaderModel>> headertask = Task.Factory.StartNew(() => dbQuery.GetUserProfileHeaderData(Email).Result.GetUserProfileHeader());
               Task<Task<UserProfileBasicModel>> basictask = Task.Factory.StartNew(() => dbQuery.GetUserProfileBasicData(Email).Result.GetUserProfileBasic());
               Task<Task<UserProfileLocationModel>> locationtask = Task.Factory.StartNew(() => dbQuery.GetUserProfileLocationData(Email).Result.GetUserProfileLocation());

               Task.WaitAll(headertask, basictask, locationtask);

               return new UserProfile() { Header = headertask.Result.Result, Basic = basictask.Result.Result, Location = locationtask.Result.Result };
           });
            
        }
        
        #region Memory
        private bool disposed = false;
        ~UserProfile()
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