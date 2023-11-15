using System.Reflection;
using BE.Auto.Api.Attributes;
using BE.Auto.Api.Interfaces;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BE.Auto.Api.Providers;

public class Provider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        return (typeof(IAutoApi).IsAssignableFrom(typeInfo) || typeInfo.GetCustomAttribute<AutoApiAttribute>() != null)
               && typeInfo.IsPublic
               && !typeInfo.IsAbstract
               && !typeInfo.IsGenericType
               && typeInfo.GetCustomAttribute<IgnoreAutoApiAttribute>() == null;

    }
}