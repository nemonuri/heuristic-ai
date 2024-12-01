using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface IMagmaOperationPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain Operate(TDomain left, TDomain right);

    bool TryOperate(TDomain left, TDomain right, [NotNullWhen(true)] out TDomain? outElement);
}
