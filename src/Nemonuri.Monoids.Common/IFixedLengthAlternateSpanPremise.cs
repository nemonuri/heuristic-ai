namespace Nemonuri.Monoids;

public interface IFixedLengthAlternateSpanPremise<TDomain, TAlternate>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    int AlternateSpanLength {get;}

    void MapToAlternateSpan(TDomain domain, Span<TAlternate> outAlternateSpan);

    bool TryMapToAlternateSpan(TDomain domain, Span<TAlternate> outAlternateSpan);

    TDomain MapToDomain(ReadOnlySpan<TAlternate> alternateSpan);

    bool TryMapToDomain(ReadOnlySpan<TAlternate> alternateSpan, [NotNullWhen(true)] out TDomain? outDomain);
}
