# BE.Auto.Api

## Overview

The `BE.Auto.Api` module streamlines the configuration and automation of API-related settings for ASP.NET Core applications. This module simplifies the process of generating API controllers automatically from application services, offering features for effortless route generation, flexible case formatting, and comprehensive Swagger documentation.


## Configuration

Configure the module in your application startup using the following code:

```csharp
builder.Services.AddMvcCore().AddAutoApi(opt =>
{
    opt.RouteOptions.RootPath = "api/app";
    opt.RouteOptions.CaseType = RouteCaseType.KebabCase;
    opt.RouteOptions.IgnoredKeywords = new[] {"Async","App","Application","Service","Manager" };
});
```

Example Service Implementation
Consider the implementation of a service, UserAppService, as an example:
```csharp
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

```

Service Interfaces
Define service interfaces that extend IApplicationService:

```csharp
public interface IUserAppService : IApplicationService
{
    Task<bool> UpdateProfilePictureAsync(IFormFile file);
    Task<string> GetUserNameAsync();
}

public interface IApplicationService : ITransientDependency, IAutoApi
{
 
}
```

AutoApi Usage
Use IAutoApi or AutoApiAttribute on the service interfaces or classes.


Case Types
Choose from various case types for route formatting:

None
UpperCase
LowerCase
TitleCase
CamelCase
PascalCase
SnakeCase
KebabCase
SentenceCase
InverseCase

Swagger JSON
The Swagger JSON for the configured routes is generated as follows:


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




