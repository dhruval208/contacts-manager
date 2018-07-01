#region Namespaces

using ContactsManager.Data.Models;
using ContactsManager.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public async Task AddContact(ContactInformation contactInformation)
        {
            await _contactInformationRepository.Insert(contactInformation);
        }

        /// <summary>
        /// GetAllActiveContacts - Returns all the active contacts
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>ContactInformation</returns>
        public async Task<IEnumerable<ContactInformation>> GetAllContacts()
        {
            return await _contactInformationRepository.GetAll();
        }

        /// <summary>
        /// GetContactById - Get All Contacts by ID
        /// </summary>
        /// <param name="contactId">Guid</param>
        /// <returns>ContactInformation</returns>
        public async Task<ContactInformation> GetContactById(Guid contactId)
        {
            return await _contactInformationRepository.Get(contactId);
        }

        /// <summary>
        /// IsAnyExists - To Check if predicate based records are exists or not.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        public async Task<bool> IsAnyExists(Expression<Func<ContactInformation, bool>> predicate)
        {
            return await _contactInformationRepository.IsAnyExists(predicate);
        }

        /// <summary>
        /// UpdateContact - Update contact details, Returns Void
        /// </summary>
        /// <param name="contactInformation">ContactInformation</param>
        public async Task UpdateContact(ContactInformation contactInformation)
        {
            await _contactInformationRepository.Update(contactInformation);
        }
    }
}
