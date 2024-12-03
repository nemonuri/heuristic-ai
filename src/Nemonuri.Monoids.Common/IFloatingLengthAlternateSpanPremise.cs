namespace Nemonuri.Monoids;

public interface IFloatingLengthAlternateSpanPremise<TDomain, TAlternate>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    int GetOutAlternateSpanLength(TDomain domain);

    void MapToAlternateSpan(TDomain domain, Span<TAlternate> outAlternateSpan);

    bool TryMapToAlternateSpan(TDomain domain, Span<TAlternate> outAlternateSpan);

    TDomain MapToDomain(ReadOnlySpan<TAlternate> alternateSpan, out int consumedSpanLength);

    bool TryMapToDomain(ReadOnlySpan<TAlternate> alternateSpan, [NotNullWhen(true)] out TDomain? outDomain, out int consumedSpanLength);
}