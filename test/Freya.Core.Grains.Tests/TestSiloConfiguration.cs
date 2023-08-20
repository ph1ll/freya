namespace Freya.Core.Grains.Tests;

using Microsoft.Extensions.DependencyInjection;
using Orleans.Storage;
using Orleans.TestingHost;

public class TestSiloConfiguration : ISiloConfigurator
{
    public void Configure(ISiloBuilder siloBuilder)
    {
        siloBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<IGrainStorage>(new InMemoryGrainStorage());
        });
    }
}
