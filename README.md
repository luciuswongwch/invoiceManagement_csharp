User roles in the application
- default user
- manager user
- admin user

Default users:  
Can only perform crud operations to invoices created by the same user  
(Implemented by InvoiceCreatorAuthorizationHandler.cs)

Manager users:  
Can approve or reject any invoices submitted by other users.  
(Implemented by InvoiceManagerAuthorizationHandler.cs)

Admin users:  
Can perform crud operations to all invoices in the system. And approve/reject all invoices.  
(Implemented by InvoiceAdminAuthorizationHandler.cs)

---

Integrations with third-party services:
- MailKit and MimeKit for email sender

appsettings.json is included in .gitignore file to hide configuration strings  
SeedData.cs is included in .gitignore file to hide the login credentials of seeded users

---

## Deployment  

Live preview: https://invoicemanagementluciuswong.azurewebsites.net/  

Image preview:  

![invoiceManagement_snapshot.png](/invoiceManagement_snapshot.png)