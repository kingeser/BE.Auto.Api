using BE.Auto.Api.Enums;

namespace BE.Auto.Api.Configuration;

public class RouteOptions : IRouteOptions
{
    public RouteOptions(string rootPath, RouteCaseType caseType, params string[] ignoredKeywords)
    {
        IgnoredKeywords = ignoredKeywords;
        RootPath = rootPath;
        CaseType = caseType;
    }

    public RouteOptions(RouteCaseType caseType)
    {
        CaseType = caseType;
    }

    public RouteOptions()
    {
        
    }

    public string[] IgnoredKeywords { get; set; }
    public string RootPath { get; set; }
    public RouteCaseType CaseType { get; set; }
}