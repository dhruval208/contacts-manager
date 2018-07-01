#region Namespaces

using ContactsManager.Data.Models;
using ContactsManager.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace ContactsManager.Services
{
    /// <summary>
    /// ContactsService
    /// </summary>
    public class ContactsService : IContactsService
    {
        #region Variables and Constructor

        /// <summary>
        /// ContactInformation
        /// </summary>
        readonly IRepository<ContactInformation> _contactInformationRepository;

        /// <summary>
        /// ContactsService - Constructor
        /// </summary>
        /// <param name="contactInformationRepository"></param>
        public ContactsService(IRepository<ContactInformation> contactInformationRepository)
        {
            _contactInformationRepository = contactInformationRepository;
        }

        #endregion

        /// <summary>
        /// Add Contact Information - Returns Void
        /// </summary>
        /// <param name="contactInformation">ContactInformation</param>
        public void AddContact(ContactInformation contactInformation)
        {
            _contactInformationRepository.Insert(contactInformation);
        }

        /// <summary>
        /// GetAllActiveContacts - Returns all the active contacts
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>ContactInformation</returns>
        public IEnumerable<ContactInformation> GetAllActiveContacts(Expression<Func<ContactInformation, bool>> predicate)
        {
            return _contactInformationRepository.GetAll(predicate);
        }

        /// <summary>
        /// GetContactById - Get All Contacts by ID
        /// </summary>
        /// <param name="contactId">Guid</param>
        /// <returns>ContactInformation</returns>
        public ContactInformation GetContactById(Guid contactId)
        {
            return _contactInformationRepository.Get(contactId);
        }

        /// <summary>
        /// IsAnyExists - To Check if predicate based records are exists or not.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        public bool IsAnyExists(Expression<Func<ContactInformation, bool>> predicate)
        {
            return _contactInformationRepository.IsAnyExists(predicate);
        }

        /// <summary>
        /// UpdateContact - Update contact details, Returns Void
        /// </summary>
        /// <param name="contactInformation">ContactInformation</param>
        public void UpdateContact(ContactInformation contactInformation)
        {
            _contactInformationRepository.Update(contactInformation);
        }
    }
}
