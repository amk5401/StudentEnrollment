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
            LocalProxy proxy = new LocalProxy(new TestPathData());
            Student student = proxy.getStudent(1);

            Assert.IsNotNull(student);
          
        }
    }
}
