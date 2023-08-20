namespace Freya.Core.Grains;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Freya.Core.GrainInterfaces;
using Orleans.Runtime;

public class RootGrain : Grain, IRootGrain
{
    private readonly IPersistentState<RootGrainState> _state;

    public RootGrain([PersistentState(nameof(RootGrainState))] IPersistentState<RootGrainState> state)
    {
        _state = state;
    }

    public Task<ReadOnlyCollection<Guid>> GetSiteIdsAsync() => Task.FromResult(_state.State.SiteIds);

    public async Task RegisterSiteAsync(Guid id)
    {
        if (!_state.State.SiteIds.Contains(id))
        {
            _state.State.SiteIds = new ReadOnlyCollection<Guid>(_state.State.SiteIds.Append(id).ToArray());
            await _state.WriteStateAsync();
        }
    }

    public async Task UnregisterSiteAsync(Guid id)
    {
        _state.State.SiteIds = new ReadOnlyCollection<Guid>(_state.State.SiteIds.Where(x => !x.Equals(id)).ToArray());
        await _state.WriteStateAsync();
    }
}
