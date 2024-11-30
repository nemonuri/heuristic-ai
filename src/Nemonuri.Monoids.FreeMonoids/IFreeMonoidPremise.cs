using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids.FreeMonoids;

public interface IFreeMonoidPremise<TDomain, TGenerator> : 
    IMonoidPremise<TDomain, TGenerator>
{
    IReadOnlyList<TGenerator> DecomposeInChain(TDomain domain);

    bool TryDecomposeInChain(TDomain domain, [NotNullWhen(true)] out IReadOnlyList<TGenerator>? outGenerators);

    TDomain OperateInChain(IReadOnlyList<TGenerator> generators);

    bool TryOperateInChain(IReadOnlyList<TGenerator> generators, [NotNullWhen(true)] out TDomain outDomain);
}