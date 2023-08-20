namespace Orleans;

public static class IGrainWithSingletonKeyExtensions
{
    public static T GetGrain<T>(this IGrainFactory grainFactory)
        where T : IGrainWithSingletonKey
    {
        grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        return grainFactory.GetGrain<T>(Guid.Empty);
    }
}
