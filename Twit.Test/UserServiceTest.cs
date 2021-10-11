using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twit.Application.Models;
using Twit.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Twit.Core.Entities;
using Twit.Core.Services;
namespace TwitTest

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