namespace Freya.Core.Grains.Tests;

using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Orleans.Storage;

public class InMemoryGrainStorage : IGrainStorage
{
    public ConcurrentDictionary<string, string> _storage { get; } = new();

    public Task ClearStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
    {
        grainState = grainState ?? throw new ArgumentNullException(nameof(grainState));

        _storage.TryRemove($"{stateName}{grainId}{grainState.GetType().FullName}", out _);
        return Task.CompletedTask;
    }

    public Task ReadStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
    {
        grainState = grainState ?? throw new ArgumentNullException(nameof(grainState));

        if (
            _storage.TryGetValue(
                $"{stateName}{grainId}{grainState.GetType().FullName}",
                out var json
            )
        )
        {
            grainState = JsonSerializer.Deserialize<IGrainState<T>>(json)!;
        }

        return Task.CompletedTask;
    }

    public Task WriteStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
    {
        grainState = grainState ?? throw new ArgumentNullException(nameof(grainState));

        return Task.FromResult(
            _storage.AddOrUpdate(
                $"{stateName}{grainId}{grainState.GetType().FullName}",
                JsonSerializer.Serialize(grainState),
                (key, oldValue) => JsonSerializer.Serialize(grainState)
            )
        );
    }
}
