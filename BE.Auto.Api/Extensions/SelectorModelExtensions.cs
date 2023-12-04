using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BE.Auto.Api.Extensions;

public static class SelectorModelExtensions
{
    public static void ClearEmptySelectors(this IList<SelectorModel> selectors)
    {

        for (var i = selectors.Count - 1; i >= 0; i--)
        {
            var selector = selectors[i];

            if (selector.AttributeRouteModel == null && selector.ActionConstraints.Count <= 0 && selector.EndpointMetadata.Count <= 0)
            {
                selectors.Remove(selector);
            }




        }
    }
}