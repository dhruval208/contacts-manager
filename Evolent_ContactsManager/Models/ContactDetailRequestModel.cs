using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ContactsManager.API.Models
{
    public class ContactDetailRequestModel
    {
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
        /// Identifies Phone Number of Customer. (Only 10 digit mobile number with country code is accepted. e.g. +919730823490)
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Identifies if respective Contact Information is Active or Not
        /// </summary>
        public string Status { get; set; }

        public void ValidateRequest()
        {
            if (string.IsNullOrEmpty(FirstName))
                throw new ArgumentNullException("FirstName");

            if (FirstName.Length > 20)
                throw new ArgumentException("FirstName length can not be greater than 20");

            var isValidFirstName = Regex.IsMatch(FirstName, "^[a-z0-9 ]+$", RegexOptions.IgnoreCase);
            if (!isValidFirstName)
                throw new ArgumentException("Special Characters are not allowed in FirstName");

            if (string.IsNullOrEmpty(LastName))
                throw new ArgumentNullException("LastName");

            if (LastName.Length > 20)
                throw new ArgumentException("LastName length can not be greater than 20");

            var isValidLastName = Regex.IsMatch(LastName, "^[a-z0-9 ]+$", RegexOptions.IgnoreCase);
            if (!isValidLastName)
                throw new ArgumentException("Special Characters are not allowed in LastName");

            if (string.IsNullOrEmpty(Email))
                throw new ArgumentNullException("Email");

            var isValidEmail = Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isValidEmail)
                throw new ArgumentException("Invalid Email");

            if (string.IsNullOrEmpty(PhoneNumber))
                throw new ArgumentNullException("PhoneNumber");

            /* Supported Phone Number formats are:
             * "+91-9678967101", "9678967101", "+91-9678-967101", "+91-96789-67101", "+919678967101"
             */
            var isValidPhoneNumber = Regex.IsMatch(PhoneNumber, @"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", RegexOptions.IgnoreCase);

            if (!isValidPhoneNumber)
                throw new ArgumentException("Invalid PhoneNumber");

            if (Status.ToLower() != "active" && Status.ToLower() != "inactive")
                throw new ArgumentException("Status can be either Active or InActive");
        }
    }

}