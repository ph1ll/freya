namespace Freya.Core.Grains.Tests;

using System.Threading.Tasks;
using Freya.Core.GrainInterfaces;
using Orleans.TestingHost;

[Collection(TestClusterCollection.Name)]
public class RootGrainTests
{
    private readonly TestCluster _cluster;

    public RootGrainTests(TestClusterFixture fixture)
    {
        _cluster = fixture?.Cluster ?? throw new ArgumentNullException(nameof(fixture));
    }

    [Fact]
    public async Task RegisterSiteAsync_ShouldAddIdToState()
    {
        // Arrange
        var root = _cluster.GrainFactory.GetGrain<IRootGrain>();
        var id = Guid.NewGuid();

        // Act
        await root.RegisterSiteAsync(id);

        // Assert
        Assert.Contains(id, await root.GetSiteIdsAsync());
    }

    [Fact]
    public async Task RegisterSiteAsync_ShouldNotAddIdToState_WhenIdAlreadyExists()
    {
        // Arrange
        var root = _cluster.GrainFactory.GetGrain<IRootGrain>();
        var id = Guid.NewGuid();
        await root.RegisterSiteAsync(id);

        // Act
        await root.RegisterSiteAsync(id);

        // Assert
        Assert.Single((await root.GetSiteIdsAsync()).Where(x => x.Equals(id)));
    }

    [Fact]
    public async Task UnregisterSiteAsync_ShouldRemoveIdFromState()
    {
        // Arrange
        var root = _cluster.GrainFactory.GetGrain<IRootGrain>();
        var id = Guid.NewGuid();
        await root.RegisterSiteAsync(id);

        // Act
        await root.UnregisterSiteAsync(id);

        // Assert
        Assert.DoesNotContain(id, await root.GetSiteIdsAsync());
    }
}
