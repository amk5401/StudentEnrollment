using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentEnrollment.Proxy
{
    public class ServerPathData : IPathData
    {
        public string GetPath(string path)
        {
            return HttpContext.Current.Server.MapPath("~/Views/jsonData/" + path);
        }
    }
}