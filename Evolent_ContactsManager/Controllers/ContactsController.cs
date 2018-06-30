﻿#region Namespaces

using ContactsManager.API.Models;
using ContactsManager.Data.Models;
using ContactsManager.Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

#endregion

namespace ContactsManager.API.Controllers
{
    /// <summary>
    /// ContactsController
    /// Contains Below mentioned API's
    /// - GetContacts: Retrives all the contacts whose status is true (Active)
    /// - AddContact: Adds new contact information to the system. Will validate each request parameters according to the business rules.
    /// - EditContact: Edits contact information based on contact information unique Id.
    /// - DeleteContact: Updates the status of specific contact Can be either true (Active) || false (InActive)
    /// </summary>
    [RoutePrefix("v1/contacts")]
    public class ContactsController : ApiController
    {
        #region Variables and Constructor

        /// <summary>
        /// IContactsService interface variable
        /// </summary>
        readonly IContactsService _contactsService;

        /// <summary>
        /// ContactsController Constructor
        /// </summary>
        /// <param name="contactsService">IContactsService</param>
        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        #endregion

        /// <summary>
        /// Returns Active Contacts Information.
        /// </summary>
        /// <returns>List of ContactDetail</returns>
        [Route("get")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "List of Active Contacts", typeof(IEnumerable<ContactDetailResponseModel>))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Active Contact Details Found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Exception Occured to Generate Response", typeof(Exception))]
        public IHttpActionResult GetContacts()
        {
            try
            {
                //Get contacts with status is true
                var allContacts = _contactsService.GetAllActiveContacts(x => x.Status == true);

                if (allContacts.Count() == 0)
                    return Content(HttpStatusCode.NoContent, "No Records Exists");

                return Ok(allContacts.TransformToReponseObject());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Saving Contact Information to the system.
        /// </summary>
        /// <returns>HttpStatusCode</returns>
        [Route("save")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Contact Details Successfully Saved", typeof(HttpStatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid - Empty Information Found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Exception Occured to Add Contact Detail", typeof(Exception))]
        public IHttpActionResult AddContact(ContactDetailRequestModel contactDetail)
        {
            try
            {
                contactDetail.ValidateRequest();

                //FirstName and LastName can be duplicated in Database.
                //Email Address and PhoneNumber should be unique in Database.
                //Checked for the duplication of EmailAddress and PhoneNumber combination. Should not be duplicated with either Active or InActive contacts.
                var isEmailExists = _contactsService.IsAnyExists(x => x.Email == contactDetail.Email && x.PhoneNumber == contactDetail.PhoneNumber);

                if (isEmailExists)
                    return BadRequest("Email Address already exists !");

                _contactsService.AddContact(new ContactInformation
                {
                    Email = contactDetail.Email,
                    FirstName = contactDetail.FirstName,
                    LastName = contactDetail.LastName,
                    PhoneNumber = contactDetail.PhoneNumber,
                    Status = contactDetail.Status.ToLower() == "active" ? true : false,
                    UserId = Guid.NewGuid()
                });

                return Ok("Contact Detail Successfully Created");
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update Contact Detail based on ContactId
        /// </summary>
        /// <param name="contactDetailRequestModel">ContactDetailRequestModel</param>
        /// <param name="contactId">Guid</param>
        /// <returns>HttpStatusCode</returns>
        [Route("edit/{contactId}")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, "Contact Details Successfully Updated", typeof(HttpStatusCode))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Duplicate-Invalid-Missing Information Provided")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Exception Occured while Updating Contact Detail", typeof(Exception))]
        public IHttpActionResult EditContact(ContactDetailRequestModel contactDetailRequestModel, Guid contactId)
        {
            try
            {
                contactDetailRequestModel.ValidateRequest();

                var isUserIdExists = _contactsService.IsAnyExists(x => x.UserId == contactId);

                if (!isUserIdExists)
                    throw new ArgumentException("Invalid ContactId. Please enter valid ContactId");

                var isEmailExists = _contactsService.IsAnyExists(x => x.Email == contactDetailRequestModel.Email);

                if (isEmailExists)
                    throw new ArgumentException("Email already exists. Please enter valid email address");

                var contactInformation = _contactsService.GetContactById(contactId);
                contactInformation.Email = contactDetailRequestModel.Email;
                contactInformation.FirstName = contactDetailRequestModel.FirstName;
                contactInformation.LastName = contactDetailRequestModel.LastName;
                contactInformation.PhoneNumber = contactDetailRequestModel.PhoneNumber;
                contactInformation.Status = contactDetailRequestModel.Status.ToLower() == "active" ? true : false;

                _contactsService.UpdateContact(contactInformation);

                return Ok("Contact Details Successfully Updated");
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete Contact Detail with respect to Contact Id
        /// </summary>
        /// <param name="contactId">Guid</param>
        /// <param name="status">string</param>
        /// <returns>HttpStatusCode</returns>
        [Route("delete/{contactId}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, "Contact Details Successfully Deleted", typeof(HttpStatusCode))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Exception Occured while Deleting Contact Detail", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Duplicate-Invalid-Missing Information Provided")]
        public IHttpActionResult DeleteContact(Guid contactId, string status)
        {
            try
            {
                if (status.ToLower() != "active" && status.ToLower() != "inactive")
                    throw new ArgumentException("Status can be either Active or InActive");

                var isUserIdExists = _contactsService.IsAnyExists(x => x.UserId == contactId);

                if (!isUserIdExists)
                    throw new ArgumentException("Invalid ContactId. Please enter valid ContactId");

                var contactInformation = _contactsService.GetContactById(contactId);
                contactInformation.Status = status.ToLower() == "active" ? true : false;

                _contactsService.UpdateContact(contactInformation);

                return Ok("Contact Information Successfully Deleted");
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}