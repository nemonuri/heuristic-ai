using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface IMonoidPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain Identity {get;}

    TDomain Operate(TDomain left, TDomain right);

    bool TryOperate(TDomain left, TDomain right, [NotNullWhen(true)] out TDomain? result);
}

public interface IDecomposableMonoidPremise<TDomain> : IMonoidPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    void Decompose(TDomain item, out TDomain outLeft, out TDomain outRight);

    bool TryDecompose(TDomain item, [NotNullWhen(true)] out TDomain? outLeft, out TDomain? outRight);
}

public interface IChainableMonoidPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain Identity {get;}

    TDomain Operate(TDomain left, TDomain right);

    bool TryOperate(TDomain left, TDomain right, [NotNullWhen(true)] out TDomain? result);
}

public interface IMonoidPremise<TDomain, TAlternate> : IMonoidPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
    where TAlternate : allows ref struct
#endif
{
    TDomain MapToDomain(TAlternate alternate);

    bool TryMapToDomain(TAlternate alternate, [NotNullWhen(true)] out TDomain? outDomain);

    TAlternate MapToAlternate(TDomain domain);

    bool TryMapToAlternate(TDomain domain, [NotNullWhen(true)] out TAlternate? outAlternate);
}
