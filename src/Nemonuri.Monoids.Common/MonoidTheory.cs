using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public static class MonoidTheory
{
    public static TDomain OperateAlternate<TMonoidPremise, TDomain, TAlternate>
    (
        this TMonoidPremise monoidPremise,
        TDomain left, 
        TAlternate right
    )
        where TMonoidPremise : 
            IMonoidPremise<TDomain, TAlternate>
#if NET9_0_OR_GREATER
            , allows ref struct
        where TDomain : allows ref struct
        where TAlternate : allows ref struct
#endif
    {
        TDomain rightAsDomain = monoidPremise.MapToDomain(right);
        return monoidPremise.Operate(left, rightAsDomain);
    }

    public static bool TryOperateAlternate<TMonoidPremise, TDomain, TAlternate>
    (
        this TMonoidPremise monoidPremise,
        TDomain left, 
        TAlternate right,
        [NotNullWhen(true)] out TDomain? outDomain
    )
        where TMonoidPremise : 
            IMonoidPremise<TDomain, TAlternate>
#if NET9_0_OR_GREATER
            , allows ref struct
        where TDomain : allows ref struct
        where TAlternate : allows ref struct
#endif
    {
        if 
        (
            monoidPremise.TryMapToDomain(right, out TDomain? rightAsDomain) &&
            monoidPremise.TryOperate(left, rightAsDomain, out outDomain)
        )
        {
            return true;
        }
        else
        {
            outDomain = default;
            return false;
        }
    }
}