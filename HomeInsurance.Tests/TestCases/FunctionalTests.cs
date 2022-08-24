﻿using HomeInsurance.BusinessLayer.Inerfaces;
using HomeInsurance.BusinessLayer.Services;
using HomeInsurance.BusinessLayer.Services.Repository;
using HomeInsurance.BusinessLayer.ViewModels;
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
    public class FunctionalTests
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
        private static string type = "Functional";


        public FunctionalTests(ITestOutputHelper output)
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
            _policy = new Policy()
            {
                PolicyKey = "1_1",
                PolicyEffectiveDate = DateTime.Now,
                PolicyEndDate = DateTime.Now.AddYears(1),
                PolicyStatus = "Active",
                PolicyTerm = 1
            };
            _quote = new Quote()
            {
                QuoteId = 1,
                AdditionalLivingproperty=1.00,
                Deductable=200,
                DetachedStructure=1.23,
                Dwelling=5.5,
                MedicalExpense=789,
                PersonalProperty=6,
                Premium=500,
                UserId=1
            };

        }

        [Fact]
        public async Task<bool> Testfor_SignUp()
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
                if (result != null)
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
        public async Task<bool> Testfor_Valid_ViewPolicy()
        {
            //Arrange
            var res = false;
            string testName; string status;
            var policy = new Policy()
            {
                PolicyKey = "p1",
                QuoteId = 1
            };
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.ViewPolicy(policy.PolicyKey)).ReturnsAsync(policy);
                var result = await _adminServices.ViewPolicy(policy.PolicyKey);
                //Assertion
                if (result != null)
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
        public async Task<bool> Testfor_Valid_BuyPolicy()
        {
            //Arrange
            var res = false;
            string testName; string status;
            int qouteId = 1;
            var policyModel = new PolicyModel()
            {
                PolicyEffectiveDate = DateTime.Now
            };
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                customerservice.Setup(repos => repos.BuyPolicy(qouteId,policyModel)).ReturnsAsync(_policy);
                var result = await _customerServices.BuyPolicy(qouteId, policyModel);
                //Assertion
                if (result != null)
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
        public async Task<bool> Testfor_RetrieveQuote()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                customerservice.Setup(repos => repos.RetrieveQuote(_user.UserId)).ReturnsAsync(_quote);
                var result = await _customerServices.RetrieveQuote(_user.UserId);
                //Assertion
                if (result != null)
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
        public async Task<bool> Testfor_SearchUser()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.SearchUser(_user.UserId)).ReturnsAsync(_user);
                var result = await _adminServices.SearchUser(_user.UserId);
                //Assertion
                if (result != null)
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
        public async Task<bool> Testfor_RenewPolicy()
        {
            //Arrange
            var res = false;
            string testName; string status;
            var policy = new Policy()
            {
                PolicyKey = "p1",
                QuoteId = 1
            };
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.RenewPolicy(policy.PolicyKey)).ReturnsAsync(_policy);
                var result = await _adminServices.RenewPolicy(policy.PolicyKey);
                //Assertion
                if (result != null)
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
        public async Task<bool> Testfor_CancelPolicy()
        {
            //Arrange
            var res = false;
            string testName; string status;
            var policy = new Policy()
            {
                PolicyKey = "p1",
                QuoteId = 1
            };
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                adminservice.Setup(repos => repos.CancelPolicy(policy.PolicyKey)).ReturnsAsync(_policy);
                var result = await _adminServices.CancelPolicy(policy.PolicyKey);
                //Assertion
                if (result != null)
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
