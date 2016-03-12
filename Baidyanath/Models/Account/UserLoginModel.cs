using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Baidyanath.Models.DataAccess;
using Baidyanath.Resources;

namespace Baidyanath.Models.Account
{
    public class UserLoginModel : IDisposable
    {
        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterUserName", ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "EnterPassword", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public async Task<bool> IsValid(string UserName, string Password)
        {

            DBQuery dbQuery = new DBQuery();
            var val = await dbQuery.IsValid(UserName, Password);
            if (!string.IsNullOrEmpty(val))
            {
                this.UserName = val.ToString() + "_" + this.UserName;
                return true;
            }

            return false;
        }

        #region Memory
        private bool disposed = false;
        ~UserLoginModel()
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