# BE.Auto.Api


builder.Services.AddMvcCore().AddAutoApi(opt =>
{
    opt.RouteOptions.RootPath = "api/app";
    opt.RouteOptions.CaseType = RouteCaseType.KebabCase;
    opt.RouteOptions.IgnoredKeywords = new[] {"Async","App","Application","Service","Manager" };
});



public class UserAppService : IUserAppService
{

    public async Task<bool> UpdateProfilePictureAsync(IFormFile file)
    {

        if (file.Length == 0)
            return false;

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return true;
    }

    public async Task<string> GetUserNameAsync()
    {
        return await Task.FromResult("Burak ESER");
    }
}


public interface IUserAppService : IApplicationService
{

    public Task<bool> UpdateProfilePictureAsync(IFormFile file);

    public Task<string> GetUserNameAsync();

}


public interface IApplicationService : ITransientDependency,IAutoApi
{

}

Use IAutoApi or AutoApiAttribute


Case Types:

    None,
    UpperCase,
    LowerCase,
    TitleCase,
    CamelCase,
    PascalCase,
    SnakeCase,
    KebabCase,
    SentenceCase,
    InverseCase

