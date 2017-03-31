using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;


namespace StudentEnrollment.Tests.ProxyUnitTests
{
    [TestClass]
    public class LocalProxyUnitTests : APIProxy
    {
        LocalProxy proxy;
        public LocalProxyUnitTests()
        {
            this.proxy = new LocalProxy(new TestPathData());
        }
        [TestMethod]
        public void getStudentUnitTest()
        {
            Student student = proxy.getStudent(1);

            Assert.IsNotNull(student);

        }
        [TestMethod]
        public void createStudentUnitTest()
        {
            Student student = proxy.getStudent(1);

            Assert.IsNotNull(student);
          
        }
    }
}
