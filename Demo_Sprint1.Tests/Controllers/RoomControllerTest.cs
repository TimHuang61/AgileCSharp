using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Demo_Sprint1;
using Demo_Sprint1.Controllers;
using Demo_Sprint1.Models;
using NSubstitute;
using NUnit.Framework;

namespace Demo_Sprint1.Tests.Controllers
{
    [TestFixture]
    public class RoomControllerTest
    {
        [Test]
        public void ConstructingWithoutRepositoryThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomController(null));
        }

        [Test]
        public void ConstructingWithValidParametersDoesNotThrowException()
        {
            Assert.DoesNotThrow(() => CreateRoomController());
        }

        [Test]
        public void GetCreateRendersView()
        {
            // Arrange
            var controller = CreateRoomController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void GetCreateSetsViewModel()
        {
            // Arrange
            var controller = CreateRoomController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.That(result.Model, Is.InstanceOf<CreateRoomViewModel>());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void PostCreateNewRoomWithInvalidRoomNameCausesValidationError(string roomName)
        {
            // Arrange
            var controller = CreateRoomController();
            var model = new CreateRoomViewModel { NewRoomName = roomName };
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, context, results);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void PostCreateNewRoomWithInvalidRoomNameShowCreateView(string roomName)
        {
            // Arrange
            var controller = CreateRoomController();
            var model = new CreateRoomViewModel { NewRoomName = roomName };
            controller.ViewData.ModelState.AddModelError("Room Name","Room name is required");

            // Act
            var result = controller.Create(model);
            var viewResult = result as ViewResult;

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(viewResult.View, Is.Null);
            Assert.That(viewResult.Model, Is.EqualTo(model));
        }

        private RoomController CreateRoomController()
        {
            return new RoomController(Substitute.For<IRoomRepository>());
        }
    }
}
