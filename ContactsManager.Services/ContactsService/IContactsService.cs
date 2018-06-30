using ContactsManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContactsManager.Services
{
    public interface IContactsService
    {
        void AddContact(ContactInformation contactInformation);
        void UpdateContact(ContactInformation contactInformation);
        bool IsAnyExists(Expression<Func<ContactInformation, bool>> predicate);
        IEnumerable<ContactInformation> GetAllActiveContacts(Expression<Func<ContactInformation, bool>> predicate);
        ContactInformation GetContactById(Guid contactId);
    }
}
