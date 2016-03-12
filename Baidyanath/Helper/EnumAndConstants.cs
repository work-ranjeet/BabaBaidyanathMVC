using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using Baidyanath.Models.DataAccess;
using System.Data;

namespace Baidyanath.Helper
{
    public class ServiceType
    {
        public static readonly string Service = "Services";
        public static readonly string Mantra = "Mantra";
    }

    public class GroupType
    {
        public static readonly string General = "General";
        public static readonly string Admin = "Admin";

    }

    public class InformationType
    {
        public static readonly string Tourism = "Tourism";
        public static readonly string LatestPost = "LatestPost";
    }

    public class AlbumTypes
    {
        public static readonly string Image = "Image";
        public static readonly string Video = "Video";
    }

}