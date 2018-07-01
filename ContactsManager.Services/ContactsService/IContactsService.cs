#region Namespaces

using ContactsManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace ContactsManager.Services
{
    /// <summary>
    /// IContactsService interface
    /// </summary>
    public interface IContactsService
    {
        /// <summary>
        /// AddContact
        /// </summary>
        /// <param name="contactInformation">ContactInformation</param>
        Task AddContact(ContactInformation contactInformation);

        /// <summary>
        /// UpdateContact
        /// </summary>
        /// <param name="contactInformation">ContactInformation</param>
        Task UpdateContact(ContactInformation contactInformation);

        /// <summary>
        /// IsAnyExists
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        Task<bool> IsAnyExists(Expression<Func<ContactInformation, bool>> predicate);

        /// <summary>
        /// GetAllActiveContacts
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>ContactInformation</returns>
        Task<IEnumerable<ContactInformation>> GetAllContacts();

        Task<ContactInformation> GetContactById(Guid contactId);
    }
}
