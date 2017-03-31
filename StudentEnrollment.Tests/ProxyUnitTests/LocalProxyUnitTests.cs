using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;


namespace StudentEnrollment.Tests.ProxyUnitTests
{
    [TestClass]
    public class LocalProxyUnitTests
    {
        [TestMethod]
        public void createStudentUnitTest()
        {
            var test = System.IO.Directory.GetCurrentDirectory();
            LocalProxy proxy = new LocalProxy();
            Student student = proxy.getStudent(1);

            Assert.IsNotNull(student);
          
        }
    }
}
