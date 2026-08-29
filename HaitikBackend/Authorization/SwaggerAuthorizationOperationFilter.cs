using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HaitikBackend.Authorization;

public sealed class SwaggerAuthorizationOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor action &&
            action.ControllerTypeInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length == 0 &&
            action.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length == 0)
        {
            operation.Security =
            [new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", null, null)] = []
            }];
        }
    }
}
