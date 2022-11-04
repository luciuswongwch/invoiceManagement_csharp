using InvoiceManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace InvoiceManagement.Authorization
{
    public class InvoiceCreatorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
    {
        UserManager<IdentityUser> _userManager;

        public InvoiceCreatorAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, 
            Invoice invoice)
        {
            if (requirement.Name != InvoiceConstant.IndexOperationName && (context.User == null || invoice == null))
                return Task.CompletedTask;

            if (requirement.Name != InvoiceConstant.CreateOperationName &&
                requirement.Name != InvoiceConstant.ReadOperationName &&
                requirement.Name != InvoiceConstant.UpdateOperationName &&
                requirement.Name != InvoiceConstant.DeleteOperationName)
                return Task.CompletedTask;

            if (invoice.InvoiceCreatorId == _userManager.GetUserId(context.User))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
