namespace Freya.Core.Grains.Tests;

using System.Threading.Tasks;
using Freya.Core.GrainInterfaces;
using Orleans.TestingHost;

[Collection(TestClusterCollection.Name)]
public class SiteGrainTests
{
    private readonly TestCluster _cluster;

    public SiteGrainTests(TestClusterFixture fixture)
    {
        _cluster = fixture?.Cluster ?? throw new ArgumentNullException(nameof(fixture));
    }

    [Fact]
    public async Task OnActivateAsync_ShouldAddIdToRootGrainState()
    {
        // Arrange
        var root = _cluster.GrainFactory.GetGrain<IRootGrain>();
        var id = Guid.NewGuid();
        var site = _cluster.GrainFactory.GetGrain<ISiteGrain>(id);

        // Act
        await site.Ping();

        // Assert
        Assert.Contains(id, await root.GetSiteIdsAsync());
    }
}
