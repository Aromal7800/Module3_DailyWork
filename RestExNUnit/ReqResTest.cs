using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using RestExNUnit.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNUnit
{
    [TestFixture]
    public class ReqResTest : CoreCodes
    {
        

        
        [Test]
        public void GetSingleUser()
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser test started");

            var req = new RestRequest("users/2", Method.Get);
            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API  Response :{response.Content}");
                var userdata = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                UserData? user = userdata?.Data;
                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.That(user.Id, Is.EqualTo(2));
                Log.Information("user id matches with fetch");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("GetSingleUser All tests passed");
                test.Pass("GetSingleUser All tests passed");
            }
            catch (AssertionException) 
            {
                test.Fail("GetSingleUser test failed ");
            }
        }
        [Test]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("CreateUser test started");
            var req = new RestRequest("users", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "John Doe", job = "Software Developer" });

            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API  Response :{response.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("user Created and returned");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Create user all tests passed ");
                test.Pass("Create user test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Create user Test Failed.");
            }
            // Assert.IsNotEmpty(user.Email);

        }

        [Test]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update User");
            Log.Information("Update User test started");
            var req = new RestRequest("users/2", Method.Put);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "John Doe Updated", job = "Senior Software Developer" });

            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API  Response :{response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("user Created and returned");
                Log.Information("Update user all tests passed ");
                test.Pass("Update user all tests passed ");
            }
            catch (AssertionException)
            {
                test.Fail("Update user test failed");
            }
            // Assert.IsNotEmpty(user.Email);

        }

        [Test]
        [Order(4)]
        [TestCase(2)]
        public void DeleteUser(int usrid)
        {
            test = extent.CreateTest("Delete User");
            Log.Information("Delete user test started");
            var req = new RestRequest("users/"+usrid, Method.Delete);

            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information("User Deleted");
                Log.Information("Delete User Test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Delete User Test Fail");
            }
            // Assert.IsNotEmpty(user.Email);

        }
        [Test]
        [Order(5)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("Get Non Existing User");
            Log.Information("GetNonExistingUser test started");
            var req = new RestRequest("users/999", Method.Get);
            var response = client.Execute(req);
            try { 
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("GetNonExistingUser Test Passed");
                test.Pass("GetNonExistingUser Test Passed");
            }
            catch(AssertionException)
            {
                test.Fail("GetNonExistingUser Test Failed");
            }

        }
    }
}
