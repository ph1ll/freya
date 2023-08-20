namespace Freya.Core.Grains.Tests;

[CollectionDefinition(TestClusterCollection.Name)]
public class TestClusterCollection : ICollectionFixture<TestClusterFixture>
{
    public const string Name = nameof(TestClusterCollection);
}
