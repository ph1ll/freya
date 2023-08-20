namespace Freya.Core.GrainInterfaces;

using Orleans;

public interface ISiteGrain : IGrainWithGuidKey
{
    Task Ping();
}
