namespace Freya.Core.Grains;

using System.Threading;
using System.Threading.Tasks;
using Freya.Core.GrainInterfaces;

public class SiteGrain : Grain, ISiteGrain
{
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        await GrainFactory.GetGrain<IRootGrain>().RegisterSiteAsync(this.GetPrimaryKey());
    }

    public Task Ping() => Task.CompletedTask;
}
