using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace InvoiceManagement.Authorization
{
    public class InvoiceOperation
    {
        public static OperationAuthorizationRequirement Index = new OperationAuthorizationRequirement
        { Name = InvoiceConstant.IndexOperationName };

        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement
            { Name = InvoiceConstant.CreateOperationName};

        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement
            { Name = InvoiceConstant.ReadOperationName };

        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement
            { Name = InvoiceConstant.UpdateOperationName };

        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement
            { Name = InvoiceConstant.DeleteOperationName };

        public static OperationAuthorizationRequirement Approve = new OperationAuthorizationRequirement
            { Name = InvoiceConstant.ApproveOperationName };

        public static OperationAuthorizationRequirement Reject = new OperationAuthorizationRequirement
            { Name = InvoiceConstant.RejectOperationName };
    }
}
