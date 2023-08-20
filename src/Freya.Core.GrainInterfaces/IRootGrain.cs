namespace Freya.Core.GrainInterfaces;

using System.Collections.ObjectModel;
using Orleans;

public interface IRootGrain : IGrainWithSingletonKey
{
    Task RegisterSiteAsync(Guid id);
    Task UnregisterSiteAsync(Guid id);
    Task<ReadOnlyCollection<Guid>> GetSiteIdsAsync();
}
