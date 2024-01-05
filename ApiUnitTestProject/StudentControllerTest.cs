//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using AutoFixture;
//using UniversityWebAPI.Controllers;
//using UniversityWebAPI.Models;
//using UniversityWebAPI.Context;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ApiUnitTestProject
//{
//    [TestClass]
//    public class StudentControllerTest
//    {
//        private readonly Mock<AppDbContext> _mockContext;
//        private readonly StudentController _controller;
//        private readonly Fixture _fixture;

//        public StudentControllerTest()
//        {
//            _fixture = new Fixture();
//            _mockContext = new Mock<AppDbContext>();
//            _controller = new StudentController(_mockContext.Object);
//        }

//        [TestMethod]
//        public async Task GetAllStudents_ReturnsOk()
//        {
//            // Arrange
//            var studentList = _fixture.CreateMany<Student>(3).ToList();
//            _mockContext.Setup(service => service.GetStudents()).ReturnsAsync(studentList);

//            // Act
//            var result = await controller.GetStudentList();

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
//            var actualStudents = ((OkObjectResult)result).Value as List<StudentModel>;
//            Assert.AreEqual(studentList.Count, actualStudents?.Count);
//        }

       
//    }
//}
