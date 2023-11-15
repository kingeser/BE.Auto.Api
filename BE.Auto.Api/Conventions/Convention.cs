using System.Text;
using BE.Auto.Api.Attributes;
using BE.Auto.Api.Configuration;
using BE.Auto.Api.Enums;
using BE.Auto.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BE.Auto.Api.Conventions;

public class Convention : IApplicationModelConvention
{
    private readonly IOptions _options;

    public Convention(IOptions options)
    {

        _options = options;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            ConfigureController(controller);
        }

    }

    private static string _NormalizeText(string str)
    {
        var result = "";
        foreach (var c in str)
        {
            if (char.IsUpper(c))
            {
                result += " ";
            }
            result += c;
        }

        return result.Trim();
    }
    private void ConfigureController(ControllerModel controller)
    {
        controller.ApiExplorer.IsVisible = true;

        controller.Selectors.ClearEmptySelectors();

        var controllerRouteModel = controller.Selectors.Where(t => t.AttributeRouteModel != null).Select(t => t.AttributeRouteModel).FirstOrDefault();

        foreach (var action in controller.Actions)
        {
            var isActionIgnored = action.Attributes.Any(t => t.GetType() == typeof(IgnoreAutoApiAttribute));

            if (isActionIgnored)
            {
                action.ApiExplorer.IsVisible = false;
                continue;
            }

            if (!action.ActionMethod.IsPublic) continue;

            action.ApiExplorer.IsVisible = true;

            action.Selectors.ClearEmptySelectors();



            if (action.Selectors.Count <= 0)
            {
                AddApplicationServiceSelector(action, controllerRouteModel);
            }
            else
            {
                NormalizeSelectorRoutes(action, controllerRouteModel);
            }

            foreach (var parameter in action.Parameters)
            {
                if (parameter.BindingInfo != null)
                {
                    continue;
                }

                if (parameter.ParameterType.IsClass && parameter.ParameterType != typeof(string) && parameter.ParameterType != typeof(IFormFile))
                {
                    var httpMethods = action.Selectors.SelectMany(temp => temp.ActionConstraints).OfType<HttpMethodActionConstraint>().SelectMany(temp => temp.HttpMethods).ToList();

                    if (httpMethods.Contains("GET") || httpMethods.Contains("DELETE") || httpMethods.Contains("TRACE") || httpMethods.Contains("HEAD"))
                    {
                        continue;
                    }

                    parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                }
            }

        }

        controller.ControllerName = _NormalizeText(controller.ControllerName);

    }
    private void NormalizeSelectorRoutes(ActionModel action, AttributeRouteModel? controllerAttributeRouteModel)
    {
        foreach (var selector in action.Selectors)
        {

            if (selector.AttributeRouteModel == null ||
                selector.AttributeRouteModel?.Attribute?.GetType()?.BaseType == typeof(HttpMethodAttribute))
            {
                selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(CalculateRouteTemplate(action, controllerAttributeRouteModel)));
            }


            if (selector.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault() == null)
            {
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { FindHttpMethod(action) }));
            }
        }
    }
    private void AddApplicationServiceSelector(ActionModel action, AttributeRouteModel? controllerAttributeRouteModel)
    {
        if (action.Selectors.Any(temp => temp.AttributeRouteModel != null)) return;


        var selector = new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(CalculateRouteTemplate(action, controllerAttributeRouteModel)))
        };
        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { FindHttpMethod(action) }));

        action.Selectors.Add(selector);


    }
    private string CalculateRouteTemplate(ActionModel action, AttributeRouteModel? controllerAttributeRouteModel)
    {
        var routeTemplate = new StringBuilder();

        if (!string.IsNullOrEmpty(_options.RouteOptions.RootPath))
        {
            routeTemplate.Append(_options.RouteOptions.RootPath);
        }

        if (controllerAttributeRouteModel?.Template != null)
        {
            if (controllerAttributeRouteModel.Template.StartsWith("/"))
            {

                routeTemplate.Append(NormalizeRoute(controllerAttributeRouteModel.Template) ?? controllerAttributeRouteModel.Template);

            }
            else
            {
                routeTemplate.Append($"/{NormalizeRoute(controllerAttributeRouteModel.Template) ?? controllerAttributeRouteModel.Template}");

            }

        }
        else
        {
            routeTemplate.Append($"/{NormalizeRoute(action.Controller.ControllerName) ?? action.Controller.ControllerName}");
        }

        if (action.Parameters.Any(temp => temp.ParameterName == "id"))
        {
            routeTemplate.Append("/{id}");
        }

        var actionName = NormalizeRoute(action.ActionName) ?? action.ActionName;


        if (!string.IsNullOrEmpty(actionName))
        {
            routeTemplate.Append($"/{actionName}");
        }

        var template = routeTemplate.ToString();

        return template;
    }

    private string NormalizeRoute(string template)
    {

        foreach (var routeOptionsIgnoredKeyword in (_options.RouteOptions.IgnoredKeywords ?? new string[] { }).OrderByDescending(t=>t.Length))
        {
            if (template.StartsWith(routeOptionsIgnoredKeyword, StringComparison.OrdinalIgnoreCase))
            {
                template = template.Remove(0, routeOptionsIgnoredKeyword.Length);
            }
            if (template.EndsWith(routeOptionsIgnoredKeyword, StringComparison.OrdinalIgnoreCase))
            {
                template = template.Remove(template.Length - routeOptionsIgnoredKeyword.Length, routeOptionsIgnoredKeyword.Length);
            }

        }

        switch (_options.RouteOptions.CaseType)
        {
            case RouteCaseType.UpperCase:
                template = template.ToUppercase();
                break;
            case RouteCaseType.LowerCase:
                template = template.ToLowercase();
                break;
            case RouteCaseType.TitleCase:
                template = template.ToTitleCase();
                break;
            case RouteCaseType.CamelCase:
                template = template.ToCamelCase();
                break;
            case RouteCaseType.PascalCase:
                template = template.ToPascalCase();
                break;
            case RouteCaseType.SnakeCase:
                template = template.ToSnakeCase();
                break;
            case RouteCaseType.KebabCase:
                template = template.ToKebabCase();
                break;
            case RouteCaseType.SentenceCase:
                template = template.ToSentenceCase();
                break;
            case RouteCaseType.InverseCase:
                template = template.ToInverseCase();
                break;
            case RouteCaseType.None:
                //Ignored
                break;
            default:
                //Ignored
                break;
        }
        return template;
    }

    private static string FindHttpMethod(ActionModel action)
    {
        var actionName = action.ActionName;

        if (actionName.StartsWith("Get",StringComparison.OrdinalIgnoreCase) || actionName.StartsWith("Check", StringComparison.OrdinalIgnoreCase) || actionName.StartsWith("Find", StringComparison.OrdinalIgnoreCase))
        {
            return "GET";
        }

        if (actionName.StartsWith("Put", StringComparison.OrdinalIgnoreCase) || actionName.StartsWith("Update", StringComparison.OrdinalIgnoreCase))
        {
            return "PUT";
        }

        if (actionName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase) || actionName.StartsWith("Remove", StringComparison.OrdinalIgnoreCase))
        {
            return "DELETE";
        }

        return actionName.StartsWith("Patch", StringComparison.OrdinalIgnoreCase) ? "PATCH" : "POST";
    }


}