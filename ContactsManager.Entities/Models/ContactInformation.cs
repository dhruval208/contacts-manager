using ContactsManager.Entities.Models;

namespace ContactsManager.Data.Models
{
    public partial class ContactInformation
    {
        public System.Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}
