using InvoiceManagement.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Authorization
{
    public class InvoiceManagerAuthorizationHandler 
        : AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Invoice invoice)
        {
            if (requirement.Name != InvoiceConstant.IndexOperationName && (context.User == null || invoice == null))
                return Task.CompletedTask;

            if (requirement.Name != InvoiceConstant.IndexOperationName &&
                requirement.Name != InvoiceConstant.ReadOperationName &&
                requirement.Name != InvoiceConstant.ApproveOperationName &&
                requirement.Name != InvoiceConstant.RejectOperationName)
                return Task.CompletedTask;

            if (context.User.IsInRole(InvoiceConstant.InvoiceManagerRole))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
