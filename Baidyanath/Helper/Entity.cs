using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baidyanath.Helper
{
    public class ServiceEntity : IDisposable
    {
        public Int64 ServiceID { get; set; }
        public Int64 ServiceParentID { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceInformation { get; set; }
        public string ServiceTitleInd { get; set; }
        public string ServiceInformationInd { get; set; }
        public string ServiceIcon { get; set; }
        public int ServiceLikeCount { get; set; }
        public int ServiceOrder { get; set; }


        #region Memory
        private bool disposed = false;
        ~ServiceEntity()
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

    public class MantraEntity : IDisposable
    {
        public Int64? MantraID { get; set; }
        public Int64? MantraParentID { get; set; }
        public string MantraTitle { get; set; }
        public string MantraInformation { get; set; }
        public string MantraTitleInd { get; set; }
        public string MantraInformationInd { get; set; }
        public string MantraIcon { get; set; }
        public int? MantraLikeCount { get; set; }
        public int? MantraOrder { get; set; }
        public int? ChildCount { get; set; }


        #region Memory
        private bool disposed = false;
        ~MantraEntity()
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

    public class InformationEntity : IDisposable
    {
       
        public Int64? InformationID { get; set; }
        public Int64? InfParentID { get; set; }
        public string InfTitle { get; set; }
        public string Information { get; set; }
        public string InfTitleInd { get; set; }
        public string InformationInd { get; set; }
        public int? InfOrder { get; set; }
        public int? ChildCount { get; set; }


        #region Memory
        private bool disposed = false;
        ~InformationEntity()
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