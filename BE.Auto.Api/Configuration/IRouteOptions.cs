using BE.Auto.Api.Enums;

namespace BE.Auto.Api.Configuration;

public interface IRouteOptions
{
    public string[] IgnoredKeywords { get; set; }
    public string RootPath { get; set; }
    public RouteCaseType CaseType { get; set; }
}