#region Namespaces

using ContactsManager.API.Models;
using ContactsManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace ContactsManager.API
{
    public static class ObjectTransformer
    {
        #region Contact Detail Transformations

        /// <summary>
        /// Transform Model object to the API Response Object
        /// </summary>
        /// <param name="contactInformation"></param>
        /// <returns></returns>
        public static IEnumerable<ContactDetailResponseModel> TransformToReponseObject(this IEnumerable<ContactInformation> contactInformation)
        {
            var listOfContactDetails = new List<ContactDetailResponseModel>();

            foreach (var contactDetail in contactInformation)
            {
                listOfContactDetails.Add(new ContactDetailResponseModel
                {
                    ContactId = contactDetail.UserId,
                    Email = contactDetail.Email,
                    FirstName = contactDetail.FirstName,
                    LastName = contactDetail.LastName,
                    PhoneNumber = contactDetail.PhoneNumber,
                    Status = contactDetail.Status ? "Active" : "InActive"
                });
            }

            return listOfContactDetails;
        }

        #endregion
    }
}