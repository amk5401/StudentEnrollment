using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentEnrollment.Proxy
{
    public class TestPathData : IPathData
    {
        public string GetPath(string path)
        {
            return System.IO.Path.Combine(@"C:\Users\user\Source\Repos\StudentEnrollment\StudentEnrollment\Views\jsonData\", path);
        }
    }
}