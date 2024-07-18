using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Cors.Extensions;

/// <summary>
///     Extensions methods for <see cref="CorsPolicyBuilder"/>s.
/// </summary>
public static class CorsPolicyBuilderExtensions
{
    /// <summary>
    /// Adds cors policies configured in the default section name "Cors"
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configurationRoot"></param>
    public static void AddCorsPolicies(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
    {
        serviceCollection.AddCorsPolicies(configurationRoot.GetSection("Cors"));
    }

    public static void AddCorsPolicies(
        this IServiceCollection serviceCollection,
        IConfigurationSection configurationSection)
    {
        CorsPolicyOptions? corsPolicyOptions = configurationSection.Get<CorsPolicyOptions>();

        if (corsPolicyOptions != null)
        {
            serviceCollection.AddCors(
                builder =>
                {
                    foreach (KeyValuePair<string, CorsPolicy> corsPolicy in corsPolicyOptions)
                    {
                        if (corsPolicy.Key.Equals("default", StringComparison.InvariantCultureIgnoreCase))
                        {
                            builder.AddDefaultPolicy(corsPolicy.Value);
                        }
                        else
                        {
                            builder.AddPolicy(corsPolicy.Key, corsPolicy.Value);
                        }
                    }
                });
        }
    }
}
