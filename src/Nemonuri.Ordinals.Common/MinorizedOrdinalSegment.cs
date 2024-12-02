namespace Nemonuri.Ordinals;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct MinorizedOrdinalSegment
{
    private readonly nint _start;

    private readonly nint _cardinality;

    private readonly ExclusiveMaximumOrdinal _exclusiveMaximumOrdinal;

    public MinorizedOrdinalSegment(nint start, nint cardinality, ExclusiveMaximumOrdinal exclusiveMaximumOrdinal)
    {
        Guard.IsInRange(start, 0, exclusiveMaximumOrdinal.InnerValue);
        Guard.IsInRange(cardinality, 0, exclusiveMaximumOrdinal.InnerValue - start + 1);

        _start = start;
        _cardinality = cardinality;
        _exclusiveMaximumOrdinal = exclusiveMaximumOrdinal;
    }

    public nint Start => _start;

    public int Count => checked((int)_cardinality);

    public nint Cardinality => _cardinality;

    public nint ExclusiveEnd => _start + _cardinality;

    public ExclusiveMaximumOrdinal ExclusiveMaximumOrdinal => _exclusiveMaximumOrdinal;

    public MinorizedOrdinal this[nint index]
    {
        get
        {
            Guard.IsInRange(index, 0, _cardinality);
            return new(_start + index, _exclusiveMaximumOrdinal);
        }
    }

    public MinorizedOrdinal this[int index] => this[(nint)index];

    public MinorizedOrdinalSegment Slice(nint start, nint cardinality)
    {
        nint offset = _start + start;
        Guard.IsInRange(offset, 0, _start + _cardinality);
        Guard.IsInRange(cardinality, 0, _cardinality - start + 1);
        return new(offset, cardinality, _exclusiveMaximumOrdinal);
    }

    public MinorizedOrdinalSegment Slice(int start, int length) => Slice((nint)start, (nint)length);
}