using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface IIdentityPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain Identity {get;}
}

public interface IMagmaOperationPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain Operate(TDomain left, TDomain right);

    bool TryOperate(TDomain left, TDomain right, [NotNullWhen(true)] out TDomain? outElement);
}

public interface IMagmaDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    void Decompose(TDomain element, out TDomain outLeft, out TDomain outRight);

    bool TryDecompose(TDomain element, [NotNullWhen(true)] out TDomain? outLeft, [NotNullWhen(true)] out TDomain? outRight);
}

public interface ISemigroupOperationPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain OperateInChain(IEnumerable<TDomain> elements);

    bool TryOperateInChain(IEnumerable<TDomain> elements, [NotNullWhen(true)] out TDomain? outElement);
}

public interface ISemigroupDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain DecomposeInChain(IEnumerable<TDomain> elements);

    bool TryDecomposeInChain(IEnumerable<TDomain> elements, [NotNullWhen(true)] out TDomain? outElement);
}

public interface IMonoidOperationPremise<TDomain> :
    IIdentityPremise<TDomain>,
    ISemigroupOperationPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{}

public interface IMonoidDecompositionPremise<TDomain> :
    IIdentityPremise<TDomain>,
    ISemigroupDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{}

public interface IAlternatePremise<TDomain, TAlternate>
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

public interface IMonoidPremise<TDomain> :
    IMonoidOperationPremise<TDomain>,
    IMonoidDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{}

#if false

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

#endif