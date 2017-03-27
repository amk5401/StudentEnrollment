using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public static class ModelFactory
    {
        public static Model buildModelFromJson(String modelType, String json)
        {
            if (modelType == null || modelType.Equals(""))
            {
                return null;
            }
            return null;
        }
                
        }
    }
}
