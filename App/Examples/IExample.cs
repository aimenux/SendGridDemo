using System.Threading;
using System.Threading.Tasks;

namespace App.Examples
{
    public interface IExample
    {
        string Name { get; }

        string Description { get; }

        Task RunAsync(CancellationToken cancellationToken = default);
    }
}