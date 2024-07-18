using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Cors.Extensions;

/// <summary>
///     Extensions methods for <see cref="CorsPolicyBuilder"/>s.
/// </summary>
public static class CorsPolicyExtensions
{
    /// <summary>
    /// Adds cors policies configured in the default section name "Cors" by resolving from a <see cref="IConfigurationRoot"/>
    /// </summary>
    /// <param name="serviceCollection">service collection used to add the policy to</param>
    /// <param name="configurationRoot">the <see cref="IConfigurationRoot"/> used to resolve the configuration</param>
    public static void AddCorsPolicies(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
    {
        serviceCollection.AddCorsPolicies(configurationRoot.GetSection("Cors"));
    }
    
    /// <summary>
    /// Adds cors policies configured in the default section name "Cors" by resolving from a <see cref="IConfiguration"/>
    /// </summary>
    /// <param name="serviceCollection">service collection used to add the policy to</param>
    /// <param name="configuration">the <see cref="IConfiguration"/> used to resolve the configuration</param>
    public static void AddCorsPolicies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddCorsPolicies(configuration.GetSection("Cors"));
    }

    /// <summary>
    /// Adds cors policies configured in the default section name "Cors"
    /// </summary>
    /// <param name="serviceCollection">service collection used to add the policy to</param>
    /// <param name="configurationSection">the <see cref="IConfigurationSection"/> used to resolve the configuration</param>
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
