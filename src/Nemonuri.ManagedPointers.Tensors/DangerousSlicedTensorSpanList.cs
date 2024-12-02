using Nemonuri.ManagedPointers.Collections;
using Nemonuri.Ordinals.Tensors;
using System.Buffers;
using System.Numerics.Tensors;

namespace Nemonuri.ManagedPointers.Tensors;

public readonly ref struct DangerousSlicedReadOnlyTensorSpanList<T>
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

    public DangerousSlicedReadOnlyTensorSpanList
    (
        ReadOnlyTensorSpan<T> entireReadOnlyTensorSpan,
        ReadOnlySpan<DangerousSpanSnapshot<NRange>> nRangeDangerousSpanSnapshotSpan
    )
    : this
    (
        entireReadOnlyTensorSpan,
        new DangerousSpanList<NRange>(nRangeDangerousSpanSnapshotSpan)
    )
    {}

    public ReadOnlyTensorSpan<T> EntireReadOnlyTensorSpan => _entireReadOnlyTensorSpan;

    public DangerousSpanList<NRange> InnerDangerousNRangeSpanList => _innerNRangeDangerousSpanList;

    
}
