using ContactsManager.Data.Models;
using ContactsManager.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContactsManager.Services
{
    public class ContactsService : IContactsService
    {
        readonly IRepository<ContactInformation> _contactInformationRepository;
        public ContactsService(IRepository<ContactInformation> contactInformationRepository)
        {
            _contactInformationRepository = contactInformationRepository;
        }

        public void AddContact(ContactInformation contactInformation)
        {
            _contactInformationRepository.Insert(contactInformation);
        }

        public IEnumerable<ContactInformation> GetAllActiveContacts(Expression<Func<ContactInformation, bool>> predicate)
        {
            return _contactInformationRepository.GetAll(predicate);
        }

        public ContactInformation GetContactById(Guid contactId)
        {
            return _contactInformationRepository.Get(contactId);
        }

        public bool IsAnyExists(Expression<Func<ContactInformation, bool>> predicate)
        {
            return _contactInformationRepository.IsAnyExists(predicate);
        }

        public void UpdateContact(ContactInformation contactInformation)
        {
            _contactInformationRepository.Update(contactInformation);
        }
    }
}
