using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Proxy;

namespace StudentEnrollment.Tests.ProxyUnitTests
{
    [TestClass]
    public class LocalProxyUnitTests
    {
        [TestMethod]
        public void createStudentUnitTest()
        {
            LocalProxy proxy = new LocalProxy();

            Assert.AreEqual(proxy.getStudent(0).FirstName, "Bob");
        }
    }
}
