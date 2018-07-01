namespace ContactsManager.Data.Models
{
    public partial class ContactInformation
    {
        /// <summary>
        /// UserId
        /// </summary>
        public System.Guid UserId { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }
    }
}
