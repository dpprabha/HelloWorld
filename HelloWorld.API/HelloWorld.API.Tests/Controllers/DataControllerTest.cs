using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloWorld.API;
using HelloWorld.API.Controllers;
using System.Web.Mvc;
using HelloWorld.Model;
using HelloWorld.Services;
using Moq;

namespace HelloWorld.API.Tests.Controllers
{
    [TestClass]
    public class DataControllerTest
    {
        [TestMethod]
        public void WriteHelloWorld()
        {

            // Arrange   
            var dbContext = new Mock<IDbContext>();
            var datas = new InMemoryDbSet<Data>
            {
                new Data { Text = "Hello World!" }
            };

            dbContext.Setup(x => x.Datas).Returns(datas);
        
            DataController dataController = new DataController(dbContext.Object);

            // Act
            string result = dataController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Hello World!", result);
        }
    }
}
