namespace Nemonuri.ManagedPointers.Tensors;

public readonly ref partial struct DangerousSlicedReadOnlyTensorSpanList<T>
{
    private readonly ReadOnlyTensorSpan<T> _entireReadOnlyTensorSpan;
    private readonly DangerousSpanList<NRange> _innerNRangeDangerousSpanList;

    public DangerousSlicedReadOnlyTensorSpanList
    (
        ReadOnlyTensorSpan<T> entireReadOnlyTensorSpan, 
        DangerousSpanList<NRange> innerNRangeDangerousSpanList
    )
    {
        _entireReadOnlyTensorSpan = entireReadOnlyTensorSpan;
        _innerNRangeDangerousSpanList = innerNRangeDangerousSpanList;
    }

    public ReadOnlyTensorSpan<T> EntireReadOnlyTensorSpan => _entireReadOnlyTensorSpan;

    public DangerousSpanList<NRange> InnerDangerousNRangeSpanList => _innerNRangeDangerousSpanList;

    public int Count => _innerNRangeDangerousSpanList.Count;

    public ReadOnlyTensorSpan<T> this[ReadOnlySpan<NRange> ranges] => _entireReadOnlyTensorSpan[ranges];

    public ReadOnlyTensorSpan<T> this[int index] => this[_innerNRangeDangerousSpanList[index]];
}
