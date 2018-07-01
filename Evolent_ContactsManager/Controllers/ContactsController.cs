#region Namespaces

using ContactsManager.API.Models;
using ContactsManager.Data.Models;
using ContactsManager.Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        [SwaggerResponse(HttpStatusCode.OK, "List of Active | InActive Contacts", typeof(IEnumerable<ContactDetailResponseModel>))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Active | InActive Contact Details Found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Exception Occured to Generate Response", typeof(Exception))]
        public async Task<IHttpActionResult> GetContacts()
        {
            try
            {
                //Get contacts with status is true || false
                var allContacts = await _contactsService.GetAllContacts();

                if (allContacts.Count() == 0)
                    return NotFound();

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
        public async Task<IHttpActionResult> AddContact(ContactDetailRequestModel contactDetailRequestModel)
        {
            try
            {
                contactDetailRequestModel.ValidateRequest();

                //FirstName and LastName can be duplicated in Database.
                //Email Address and PhoneNumber should be unique in Database.
                //Checked for the duplication of EmailAddress and PhoneNumber combination. Should not be duplicated with either Active or InActive contacts.
                var isEmailExists = await _contactsService.IsAnyExists(x => x.Email == contactDetailRequestModel.Email && x.PhoneNumber == contactDetailRequestModel.PhoneNumber);

                if (isEmailExists)
                    return BadRequest("Email Address & PhoneNumber already exists !");

                await _contactsService.AddContact(new ContactInformation
                {
                    Email = contactDetailRequestModel.Email,
                    FirstName = contactDetailRequestModel.FirstName,
                    LastName = contactDetailRequestModel.LastName,
                    PhoneNumber = contactDetailRequestModel.PhoneNumber,
                    Status = contactDetailRequestModel.Status.ToLower() == "active" ? true : false,
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
        public async Task<IHttpActionResult> EditContact(ContactDetailRequestModel contactDetailRequestModel, Guid contactId)
        {
            try
            {
                contactDetailRequestModel.ValidateRequest();

                var isUserIdExists = await _contactsService.IsAnyExists(x => x.UserId == contactId);

                if (!isUserIdExists)
                    throw new ArgumentException("Invalid ContactId. Please enter valid ContactId");

                //Here,We will check for any other user's contact email and phone. If not exists then valid, Otherwise invalid
                var isEmailExists = await _contactsService.IsAnyExists(x => x.Email == contactDetailRequestModel.Email && x.PhoneNumber == contactDetailRequestModel.PhoneNumber && x.UserId != contactId);

                if (isEmailExists)
                    throw new ArgumentException("Email & Phone already exists. Please enter valid email address");

                var contactInformation = await _contactsService.GetContactById(contactId);
                contactInformation.Email = contactDetailRequestModel.Email;
                contactInformation.FirstName = contactDetailRequestModel.FirstName;
                contactInformation.LastName = contactDetailRequestModel.LastName;
                contactInformation.PhoneNumber = contactDetailRequestModel.PhoneNumber;
                contactInformation.Status = contactDetailRequestModel.Status.ToLower() == "active" ? true : false;

                await _contactsService.UpdateContact(contactInformation);

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
        /// <returns>HttpStatusCode</returns>
        [Route("delete/{contactId}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, "Contact Details Successfully Deleted", typeof(HttpStatusCode))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Exception Occured while Deleting Contact Detail", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid ContactId provided")]
        public async Task<IHttpActionResult> DeleteContact(Guid contactId)
        {
            try
            {
                //If any ContactInformation exists with ActiveStatus, Then only mark it as Deleted.
                var isUserIdExists = await _contactsService.IsAnyExists(x => x.UserId == contactId && x.Status == true);

                if (!isUserIdExists)
                    throw new ArgumentException("Invalid ContactId. Please enter valid ContactId");

                var contactInformation = await _contactsService.GetContactById(contactId);
                contactInformation.Status = false;

                await _contactsService.UpdateContact(contactInformation);

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
