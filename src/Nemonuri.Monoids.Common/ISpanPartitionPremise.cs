namespace Nemonuri.Monoids;

public interface ISpanPartitionPremise<T>
{
    int OutRangesLength {get;}

    void Partition(ReadOnlySpan<T> target, Span<Range> outRanges);

    bool TryPartition(ReadOnlySpan<T> target, Span<Range> outRanges);
}
