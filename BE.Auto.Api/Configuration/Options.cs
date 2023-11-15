
using BE.Auto.Api.Enums;

namespace BE.Auto.Api.Configuration;

public class Options : IOptions
{
    public IRouteOptions RouteOptions { get; set; } = new RouteOptions()
    {
        CaseType = RouteCaseType.KebabCase,
        IgnoredKeywords = new []
        {
            "App",
            "ApplicationService",
            "AppService",
            "Manager",
            "Service",
            "Async",
            "Request",
            "Find",
            "Get",
            "GetAll",
            "GetList",
            "Put",
            "Update",
            "Delete",
            "Remove",
            "Patch",
            "Save",
            "Insert",
            "Add"

        },
        RootPath = "api",

    };
}