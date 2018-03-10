using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleService.Controllers;
using SimpleService.Dto;
using SimpleService.Models;

namespace SimpleService.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestGetSingle()
        {
            var testJSONs = GetTestJSONs();

            var controller = new JSONFileController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.GetFiles(999);
            var contentResult = actionResult as OkNegotiatedContentResult<JSONFile>;

            // Assert
            Assert.IsNull(contentResult);
            
        }

        private List<JSONFile> GetTestJSONs()
        {
            var testFiles = new List<JSONFile>();
            testFiles.Add(new JSONFile { Id = 1, File = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}" });
            testFiles.Add(new JSONFile { Id = 2, File = "{\"name\":\"david\",\"age\":22,\"class\":\"mca\"}" });
            testFiles.Add(new JSONFile { Id = 3, File = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\",\"surname\":\"wall\"}" });
            testFiles.Add(new JSONFile { Id = 4, File = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\",\"surname\":\"smith\"}" });

            return testFiles;
        }
    }
}
