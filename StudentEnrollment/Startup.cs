using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentEnrollment.Startup))]
namespace StudentEnrollment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
