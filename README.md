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




Swagger Json :

{
"openapi": "3.0.1",
"info": {
"title": "Your API",
"version": "v1"
},
"paths": {
"/api/app/user/update-profile-picture": {
"put": {
"tags": [
"User App Service"
],
"requestBody": {
"content": {
"multipart/form-data": {
"schema": {
"type": "object",
"properties": {
"file": {
"type": "string",
"format": "binary"
}
}
},
"encoding": {
"file": {
"style": "form"
}
}
}
}
},
"responses": {
"200": {
"description": "Success",
"content": {
"text/plain": {
"schema": {
"type": "boolean"
}
},
"application/json": {
"schema": {
"type": "boolean"
}
},
"text/json": {
"schema": {
"type": "boolean"
}
}
}
}
}
}
},
"/api/app/user/get-user-name": {
"get": {
"tags": [
"User App Service"
],
"responses": {
"200": {
"description": "Success",
"content": {
"text/plain": {
"schema": {
"type": "string"
}
},
"application/json": {
"schema": {
"type": "string"
}
},
"text/json": {
"schema": {
"type": "string"
}
}
}
}
}
}
}
},
"components": {}
}
