﻿using HomeInsurance.BusinessLayer.Inerfaces;
using HomeInsurance.BusinessLayer.Services;
using HomeInsurance.BusinessLayer.Services.Repository;
using HomeInsurance.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HomeInsurance.Tests.TestCases
{
    public class BoundaryTests
    {
        private readonly ITestOutputHelper _output;

        private readonly IAdminServices _adminServices;
        private readonly ICustomerServices _customerServices;

        public readonly Mock<IAdminRepository> adminservice = new Mock<IAdminRepository>();
        public readonly Mock<ICustomerRepository> customerservice = new Mock<ICustomerRepository>();
        private readonly User _user;
        private readonly Residence _residence;
        private readonly Quote _quote;
        private readonly Policy _policy;
        private readonly Property _property;
        private static string type = "Boundary";


        public BoundaryTests(ITestOutputHelper output)
        {
            _adminServices = new AdminServices(adminservice.Object);
            _customerServices = new CustomerServices(customerservice.Object);

            _output = output;

            _user = new User()
            {
                UserId = 1234,
                FirstName = "Rose",
                LastName = "N",
                UserName = "Rose",
                Mobile = "9908765433",
                Password = "12345678",
                Email = "abc@gmail.com",
                DOB = new DateTime(1985, 2, 3),
                IsRetired = true,
                IsValid = true,
                SocialSecurityNumber = 987654321,
                UserType = "Admin"
            };

            _residence = new Residence()
            {
                QuoteId = "Q1",
                AddressLine1 = "Bangalore",
                City = "Bangalore",
                Zip = 675201
            };
            _property = new Property()
            {
                PropertyId = 12345,
                QuoteId = 1,
                UserId = 1234
            };

        }

        [Fact]
        public async Task<bool> Testfor_ValidContactNumber()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.SignUp(_user)).ReturnsAsync(_user);
                var result = await _adminServices.SignUp(_user);
                //Assertion
                if (result.Mobile.Length == 10)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }

        [Fact]
        public async Task<bool> Testfor_ValidSSN_Number()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.SignUp(_user)).ReturnsAsync(_user);
                var result = await _adminServices.SignUp(_user);
                //Assertion
                if (result.SocialSecurityNumber.ToString().Length == 9)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }

        [Fact]
        public async Task<bool> Testfor_Valid_UserNameLength()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.SignUp(_user)).ReturnsAsync(_user);
                var result = await _adminServices.SignUp(_user);
                //Assertion
                if (result.UserName.Length <50 && result.UserName.Length>2)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }

    }
}
