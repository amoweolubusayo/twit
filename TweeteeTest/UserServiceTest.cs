using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tweetee.Application.Models;
using tweetee.Controllers;
using Microsoft.AspNetCore.Mvc;
using tweetee.Application.Entities;
using tweetee.Services;
namespace TweeteeTest

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