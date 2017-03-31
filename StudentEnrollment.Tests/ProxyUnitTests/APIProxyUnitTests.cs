using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentEnrollment.Proxy;
namespace StudentEnrollment.Tests.ProxyUnitTests
{
    [TestClass]
    public class APIProxyUnitTests
    {
        [TestMethod]
        public void getFromAPIUnitTest() {
            APIProxy proxy = new APIProxy();

            Assert.AreEqual(proxy.getCourseList()[0].CourseCode, "SWEN-344") ;


        }

        
        
        }
    }

