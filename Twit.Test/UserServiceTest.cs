using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using twit.Application.Models;
using twit.Controllers;
using Microsoft.AspNetCore.Mvc;
using twit.Application.Entities;
using twit.Services;
namespace twitTest

{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void UserService_CheckImplementationOfGettingAllUsers()
        {

            IUserService userService = new UserService();
            var result = userService.GetAll();
            Assert.AreEqual("Test", result.ElementAt(0).FirstName);
        }
    }
}