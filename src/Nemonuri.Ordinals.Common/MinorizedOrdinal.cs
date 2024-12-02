namespace Nemonuri.Ordinals;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct MinorizedOrdinal
{
    private readonly nint _innerOrdinal;

    private readonly ExclusiveMaximumOrdinal _exclusiveMaximumOrdinal;

    public MinorizedOrdinal(nint innerOrdinal, ExclusiveMaximumOrdinal exclusiveMaximumOrdinal)
    {
        Guard.IsInRange(innerOrdinal, 0, exclusiveMaximumOrdinal.InnerValue);

        _innerOrdinal = innerOrdinal;
        _exclusiveMaximumOrdinal = exclusiveMaximumOrdinal;
    }

    public MinorizedOrdinal(nint innerOrdinal, nint exclusiveMaximumOrdinal) : this
    (
        innerOrdinal,
        new ExclusiveMaximumOrdinal(exclusiveMaximumOrdinal)
    )
    {}

    public nint InnerOrdinal => _innerOrdinal;

    public ExclusiveMaximumOrdinal ExclusiveMaximumOrdinal => _exclusiveMaximumOrdinal;

    public MinorizedOrdinalSegment ToSegment() => new (_innerOrdinal, 1, _exclusiveMaximumOrdinal);
}
