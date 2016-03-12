using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Models.DataAccess;
using Baidyanath.Resources;
using Baidyanath.Helper;

namespace Baidyanath.Models.Account
{
    public class UserRegistrationModel : IDisposable
    {
        private bool disposed = false;

        [Display(Name = "EnterUserID", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterUserID", ErrorMessageResourceType = typeof(Resource))]
        [Remote("IsUserAvailable", "Account", AdditionalFields = "UserID", ErrorMessageResourceName = "AlreadyExists", ErrorMessageResourceType = typeof(Resource))]
        public string UserID { get; set; }

        [Display(Name = "EnterEmail", ResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterEmail", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }


        [Display(Name = "FirstName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterFirstName", ErrorMessageResourceType = typeof(Resource))]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessageResourceName = "InvalidName", ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }


        [Display(Name = "MiddleName", ResourceType = typeof(Resource))]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessageResourceName = "InvalidName", ErrorMessageResourceType = typeof(Resource))]
        public string MiddleName { get; set; }


        [Required(ErrorMessageResourceName = "EnterLastName", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessageResourceName = "InvalidName", ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterPassword", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "ReEnterPassword", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "ReEnterPassword", ErrorMessageResourceType = typeof(Resource))]
        [System.Web.Mvc.CompareAttribute("Password", ErrorMessageResourceName = "PasswordMismatch", ErrorMessageResourceType = typeof(Resource))]
        public string CnfPassword { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Resource))]
        [DataType(DataType.Date)]
        public string Dob { get; set; }


        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        public string Gender { get; set; }

        [Display(Name = "Iam", ResourceType = typeof(Resource))]
        public string IsMarried { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "MobileNo", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterMobileNo", ErrorMessageResourceType = typeof(Resource))]
        public string Mobile { get; set; }

        #region Memory
        ~UserRegistrationModel()
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