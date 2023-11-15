using BE.Auto.Api.Configuration;
using BE.Auto.Api.Conventions;
using BE.Auto.Api.Providers;
using Microsoft.AspNetCore.Mvc;

namespace BE.Auto.Api.Extensions;

public static class BuilderExtensions
{
    public static IMvcBuilder AddAutoApi(this IMvcBuilder builder, Action<IOptions> opt)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.ConfigureApplicationPartManager(applicationPartManager =>
        {
            applicationPartManager.FeatureProviders.Add(new Provider());
        });


        builder.Services.Configure<MvcOptions>(options =>
        {
            var autoControllerOptions = new Options();
            opt(autoControllerOptions);
            options.Conventions.Add(new Convention(autoControllerOptions));
        });

        return builder;
    }
    public static IMvcBuilder AddAutoApi(this IMvcBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.ConfigureApplicationPartManager(applicationPartManager =>
        {
            applicationPartManager.FeatureProviders.Add(new Provider());
        });

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Conventions.Add(new Convention(new Options()));
        });

        return builder;
    }

    public static IMvcCoreBuilder AddAutoApi(this IMvcCoreBuilder builder, Action<IOptions> opt)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }



        builder.ConfigureApplicationPartManager(applicationPartManager =>
        {
            applicationPartManager.FeatureProviders.Add(new Provider());
        });


        builder.Services.Configure<MvcOptions>(options =>
        {
            var autoControllerOptions = new Options();
            opt(autoControllerOptions);
            options.Conventions.Add(new Convention(autoControllerOptions));
        });

        return builder;
    }
    public static IMvcCoreBuilder AddAutoApi(this IMvcCoreBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.ConfigureApplicationPartManager(applicationPartManager =>
        {
            applicationPartManager.FeatureProviders.Add(new Provider());
        });

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Conventions.Add(new Convention(new Options()));
        });

        return builder;
    }

 
}