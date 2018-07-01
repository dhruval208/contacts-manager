#region Namespaces

using ContactsManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
        void AddContact(ContactInformation contactInformation);

        /// <summary>
        /// UpdateContact
        /// </summary>
        /// <param name="contactInformation">ContactInformation</param>
        void UpdateContact(ContactInformation contactInformation);

        /// <summary>
        /// IsAnyExists
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        bool IsAnyExists(Expression<Func<ContactInformation, bool>> predicate);

        /// <summary>
        /// GetAllActiveContacts
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>ContactInformation</returns>
        IEnumerable<ContactInformation> GetAllActiveContacts(Expression<Func<ContactInformation, bool>> predicate);

        ContactInformation GetContactById(Guid contactId);
    }
}
