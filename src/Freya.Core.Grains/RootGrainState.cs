namespace Freya.Core.Grains;

using System.Collections.ObjectModel;

public class RootGrainState
{
    public ReadOnlyCollection<Guid> SiteIds { get; set; } = new ReadOnlyCollection<Guid>(Array.Empty<Guid>());
}
