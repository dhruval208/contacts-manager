﻿#region Namespaces

using ContactsManager.API.Controllers;
using ContactsManager.API.Models;
using ContactsManager.Data.Models;
using ContactsManager.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http.Results;


#endregion

namespace Evolent_ContactsManager.Tests
{
    /// <summary>
    /// Contacts Controller Unit Tests
    /// </summary>
    [TestClass]
    public class ContactsControllerTests
    {
        #region Variables and Constructor

        /// <summary>
        /// Mock of IContactsService
        /// </summary>
        Mock<IContactsService> contactsService;

        /// <summary>
        /// ContactsControllerTests - Constructor
        /// </summary>
        public ContactsControllerTests()
        {
            contactsService = new Mock<IContactsService>();
        }

        #endregion

        #region Get Contacts

        [TestMethod]
        public async Task GetContact_HappyPath()
        {
            contactsService.Setup(x => x.GetAllContacts()).ReturnsAsync(new List<ContactInformation> {
                new ContactInformation
                {
                    Email = "dhruval1@gmail.com",
                    FirstName = "Dhruval",
                    LastName = "Dave",
                    PhoneNumber = "9730823490",
                    Status = true,
                    UserId = Guid.NewGuid()
                },
                new ContactInformation
                {
                    Email = "dhruval2@gmail.com",
                    FirstName = "Dhruval1",
                    LastName = "Dave",
                    PhoneNumber = "9730823491",
                    Status = true,
                    UserId = Guid.NewGuid()
                },
                new ContactInformation
                {
                    Email = "dhruval3@gmail.com",
                    FirstName = "Dhruval2",
                    LastName = "Dave",
                    PhoneNumber = "9730823492",
                    Status = true,
                    UserId = Guid.NewGuid()
                }
            });
            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.GetContacts();

            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<IEnumerable<ContactDetailResponseModel>>));
            var contentResult = response as OkNegotiatedContentResult<IEnumerable<ContactDetailResponseModel>>;

            foreach (var item in contentResult.Content)
            {
                Assert.IsNotNull(item.Email);
                Assert.IsNotNull(item.FirstName);
                Assert.IsNotNull(item.LastName);
                Assert.IsNotNull(item.PhoneNumber);
                Assert.IsNotNull(item.Status);
            }

            //Now we have already mocked, So we can verify the same with respect to indexes.
            var firstContactDetail = contentResult.Content.First();
            var lastContactDetail = contentResult.Content.Last();

            Assert.AreEqual(firstContactDetail.Email, "dhruval1@gmail.com");
            Assert.AreEqual(firstContactDetail.FirstName, "Dhruval");
            Assert.AreEqual(firstContactDetail.LastName, "Dave");
            Assert.AreEqual(firstContactDetail.PhoneNumber, "9730823490");
            Assert.AreEqual(firstContactDetail.Status, "Active");

            Assert.AreEqual(lastContactDetail.Email, "dhruval3@gmail.com");
            Assert.AreEqual(lastContactDetail.FirstName, "Dhruval2");
            Assert.AreEqual(lastContactDetail.LastName, "Dave");
            Assert.AreEqual(lastContactDetail.PhoneNumber, "9730823492");
            Assert.AreEqual(lastContactDetail.Status, "Active");
        }

        [TestMethod]
        public async Task GetContact_NoContent()
        {
            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.GetContacts();

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        #endregion

        #region Add Contacts

        [TestMethod]
        public async Task AddContact_HappyPath()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>())).Returns(Task.FromResult(true));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            });

            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<string>));

            var contentResult = response as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Contact Detail Successfully Created", contentResult.Content);
        }

        [TestMethod]
        public async Task AddContact_Duplicate_Email()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(true);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            });


            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("Email Address & PhoneNumber already exists !"));
        }

        [TestMethod]
        public async Task AddContact_Empty_FirstName()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            });

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("FirstName"));
        }

        [TestMethod]
        public async Task AddContact_Length_FirstName()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "DHRUVAL891011121314151617",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            });

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("FirstName length can not be greater than 20"));
        }

        [TestMethod]
        public async Task AddContact_Invalid_FirstName()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactsManager.API.Models.ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "$Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            });

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("Special Characters are not allowed in FirstName"));
        }

        [TestMethod]
        public async Task AddContact_Empty_LastName()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactsManager.API.Models.ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "",
                PhoneNumber = "9730823490",
                Status = "Active"
            });

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("LastName"));
        }

        [TestMethod]
        public async Task AddContact_Length_LastName()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave1235891011121314151617",
                PhoneNumber = "9730823490",
                Status = "Active"
            });

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("LastName length can not be greater than 20"));
        }

        [TestMethod]
        public async Task AddContact_Invalid_LastName()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.AddContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.AddContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "$Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            });

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("Special Characters are not allowed in LastName"));
        }

        #endregion

        #region Edit Contacts

        [TestMethod]
        public async Task EditContact_HappyPath()
        {

            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsInOrder(Task.FromResult<bool>(true), Task.FromResult<bool>(false));
            contactsService.Setup(c => c.UpdateContact(It.IsAny<ContactInformation>())).Returns(Task.FromResult(true));
            contactsService.Setup(c => c.GetContactById(It.IsAny<Guid>())).ReturnsAsync(new ContactInformation
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = true,
                UserId = Guid.NewGuid()
            });

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.EditContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            }, Guid.NewGuid());

            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<string>));

            var contentResult = response as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Contact Details Successfully Updated", contentResult.Content);
        }

        [TestMethod]
        public async Task EditContact_ContactId_NotExixts()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsInOrder(Task.FromResult(false), Task.FromResult(false));
            contactsService.Setup(c => c.UpdateContact(It.IsAny<ContactInformation>()));
            contactsService.Setup(c => c.GetContactById(It.IsAny<Guid>())).ReturnsAsync(new ContactInformation
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = true,
                UserId = Guid.NewGuid()
            });

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.EditContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            }, Guid.NewGuid());

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("Invalid ContactId. Please enter valid ContactId"));
        }

        [TestMethod]
        public async Task EditContact_Duplicate_Email()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsInOrder(Task.FromResult(true), Task.FromResult(true));
            contactsService.Setup(c => c.UpdateContact(It.IsAny<ContactInformation>()));
            contactsService.Setup(c => c.GetContactById(It.IsAny<Guid>())).ReturnsAsync(new ContactInformation
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = true,
                UserId = Guid.NewGuid()
            });

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.EditContact(new ContactDetailRequestModel
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = "Active"
            }, Guid.NewGuid());

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message.Contains("Email & Phone already exists. Please enter valid email address"));
        }

        #endregion

        #region Delete Contact

        [TestMethod]
        public async Task DeleteContact_HappyPath()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(true);
            contactsService.Setup(c => c.UpdateContact(It.IsAny<ContactInformation>())).Returns(Task.FromResult(true));
            contactsService.Setup(c => c.GetContactById(It.IsAny<Guid>())).ReturnsAsync(new ContactInformation
            {
                Email = "dhruval20@gmail.com",
                FirstName = "Dhruval",
                LastName = "Dave",
                PhoneNumber = "9730823490",
                Status = false,
                UserId = Guid.NewGuid()
            });

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.DeleteContact(Guid.NewGuid());

            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<string>));

            var contentResult = response as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Contact Information Successfully Deleted", contentResult.Content);
        }

        [TestMethod]
        public async Task DeleteContact_Invalid_ContactId()
        {
            contactsService.Setup(c => c.IsAnyExists(It.IsAny<Expression<Func<ContactInformation, bool>>>())).ReturnsAsync(false);
            contactsService.Setup(c => c.UpdateContact(It.IsAny<ContactInformation>()));

            var contactsController = new ContactsController(contactsService.Object);
            var response = await contactsController.DeleteContact(Guid.NewGuid());

            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));

            var contentResult = response as BadRequestErrorMessageResult;
            Assert.AreEqual("Invalid ContactId. Please enter valid ContactId", contentResult.Message);
        }

        #endregion
    }

    /// <summary>
    /// MoqExtensions - Used to moq the same method multiple times with different scenarios.
    /// </summary>
    public static class MoqExtensions
    {
        /// <summary>
        /// Returns In Order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="setup"></param>
        /// <param name="results"></param>
        public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup,
          params TResult[] results) where T : class
        {
            setup.Returns(new Queue<TResult>(results).Dequeue);
        }
    }
}
