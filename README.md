# contacts-manager
Contacts Manager - Repository Pattern | CRUD Operations

Tools & Technologies Used:
- Visual Studio 2017
- .NET Framework 4.5
- Microsoft SQL Server 2014 (Management Studio)
- Followed Repository Pattern to perform CRUD operations.
- Swagger

Contacts Manager is an API application which consist total number of 4 API's
 - GetContacts: Returns Active Contacts Information [HTTPGET]
 - SaveContacts: Saving Contact Information to the system [HTTPPOST]
 - UpdateContacts: Update Contact Detail based on ContactId [HTTPPUT]
 - DeleteContacts: Delete Contact Detail with respect to Contact Id [HTTPDELETE]
 
 All API's are deployed at: http://traintkt.in/swagger
 (On deployed version, Update and Delete contacts are defined as HttpPost|HttpGet as HttpPut and HttpDelete verbs are not supported on GoDaddy Hosting. Need to activate these verbs over there)
 
 ## Setup of an Application:
 - Application is developed with a Code First Approch and Generic Repository Pattern. It will automatically create Database as well as "ContactInformation" table.
 - Connection strings are predefined with Windows Authentication. If it still fails to establish then please make accordingly changes in connection strings at these places:
 1) ..\Evolent_ContactsManager\Web.config
 2) ..\ContactsManager.Data\App.config
 
 
## Get All Contacts:
- Returns all the Active as well as InActive Contact Information
 
 ## Add Contact:
 ```
 {
  "FirstName": "Dhruval",
  "LastName": "Dave",
  "Email": "dhruval20@gmail.com",
  "PhoneNumber": "+91 9730823490",
  "Status": "Active"
}
```

- **FirstName** - Is mandatory and accepts only string chars. Max Length allowed is 20
- **LastName** - Is mandatory and accepts only string chars. Max Length allowed is 20
- **Email** - Is mandatory and accepts only valid email addresses.
- **PhoneNumber** - Is mandatory and accepts only valid phone numbers. Valid Phone Number formats are "xxxxxxxxxx", "+xx xx xxxxxxxx", "xxx-xxxx-xxxx"
- **Status** - Is mandatory and Values supported are "Active" or "InActive"

**Taken Considerations:**
- Duplication is considered for the both Email And PhoneNumber

## Update Contact:
```
{
  "FirstName": "Dhruval",
  "LastName": "Dave",
  "Email": "dhruval20@gmail.com",
  "PhoneNumber": "+91 9730823490",
  "Status": "Active"
}
 ```
 Also need to pass the Guid of ContactId should be passed in request as a URL Parameter.
 
- **FirstName** - Is mandatory and accepts only string chars. Max Length allowed is 20
- **LastName** - Is mandatory and accepts only string chars. Max Length allowed is 20
- **Email** - Is mandatory and accepts only valid email addresses.
- **PhoneNumber** - Is mandatory and accepts only valid phone numbers. Valid Phone Number formats are "xxxxxxxxxx", "+xx xx xxxxxxxx", "xxx-xxxx-xxxx"
- **Status** - Is mandatory and Values supported are "Active" or "InActive"

**Taken Considerations:**
- Duplication is considered for the both Email And PhoneNumber

## Delete Contact:
- Based on ContactId and Status in URL Parameters, Application will Inactive specific record
