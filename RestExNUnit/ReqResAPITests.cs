using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNUnit
{
    [TestFixture]
    internal class ReqResAPITests
    {

        private RestClient client;
        private string baseUrl = "https://reqres.in/api/";
        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        public void GetSingleUser()
        {

            var req = new RestRequest("users/2", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var userdata = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
            UserData? user = userdata?.Data;
            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(user.Email);
        }
        [Test]
        public void CreateUser()
        {

            var req = new RestRequest("users", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "John Doe", job = "Software Developer" });

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            // Assert.IsNotEmpty(user.Email);

        }

        [Test]
        public void UpdateUser()
        {

            var req = new RestRequest("users/2", Method.Put);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { name = "John Doe Updated", job = "Senior Software Developer" });

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            // Assert.IsNotEmpty(user.Email);

        }

        [Test]
        public void DeleteUser()
        {

            var req = new RestRequest("users/2", Method.Delete);

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
            // Assert.IsNotEmpty(user.Email);

        }
        [Test]
        [Order(5)]
        public void GetNonExistingUser()
        {
            var req = new RestRequest("users/999", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));


        }
    }
}
