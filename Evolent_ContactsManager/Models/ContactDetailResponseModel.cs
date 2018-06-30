#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace ContactsManager.API.Models
{
    public class ContactDetailResponseModel
    {
        /// <summary>
        /// Identifies Unique ContactId
        /// </summary>
        public Guid ContactId { get; set; }

        /// <summary>
        /// Identifies FirstName of Customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Identifies LastName of Customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Identifies Email Address of Customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identifies Phone Number of Customer. (Only 10 digit mobile number with country code is accepted. e.g. +91 9730823490)
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Identifies if respective Contact Information is Active or Not
        /// </summary>
        public string Status { get; set; }
    }
}