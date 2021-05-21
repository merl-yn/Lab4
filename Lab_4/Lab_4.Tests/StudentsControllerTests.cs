using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Lab_4.Domain.Abstraction;
using Lab_4.Domain.Entity;
using Lab_4.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lab_4.Tests
{
    [TestFixture]
    public class StudentsControllerTests
    {
        private APIWebApplicationFactory _factory;
        private HttpClient _client;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new APIWebApplicationFactory();
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(svc =>
                {
                    svc.AddSingleton<IRepository<StudentEntity>, StudentsRepositoryMock>();
                });
            }).CreateClient();
        }

        [Test]
        public async Task Should_Return4Students_OnGetAllRequest()
        {
            var result = await _client.GetAsync("/students");
            var content = await result.Content.ReadAsStringAsync();

            var entities = JsonConvert.DeserializeObject<IEnumerable<StudentEntity>>(content);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.AreEqual(entities.Count(), 4);
        }

        [Test]
        public async Task Should_ReturnVasyaStudent_OnFirstPassportId()
        {
            var result = await _client.GetAsync("/students/passport-1");
            var content = await result.Content.ReadAsStringAsync();

            var entity = JsonConvert.DeserializeObject<StudentEntity>(content);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.AreEqual(entity.FirstName, "Vasya");
        }
        
        [Test]
        public async Task Should_Return50Percentage_OnNonResidentsPercentageEndpoint()
        {
            var result = await _client.GetAsync("/students/nonResidentsPercentage");
            var content = await result.Content.ReadAsStringAsync();

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.AreEqual(Convert.ToDouble(content), 50);
        }
        
        [Test]
        public async Task Should_Return2Students_OnGetStudentsForHostelEndpoint()
        {
            var result = await _client.GetAsync("/students/forHostel");
            var content = await result.Content.ReadAsStringAsync();

            var entities = JsonConvert.DeserializeObject<IEnumerable<StudentEntity>>(content);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.AreEqual(entities.Count(), 2);
        }
    }
}