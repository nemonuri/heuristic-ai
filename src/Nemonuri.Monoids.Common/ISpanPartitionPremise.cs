namespace Nemonuri.Monoids;

public interface ISpanPartitionPremise<T>
{
    int GetOutRangesSpanLength(ReadOnlySpan<T> target);

    void Partition(ReadOnlySpan<T> target, Span<Range> outRanges);

    bool TryPartition(ReadOnlySpan<T> target, Span<Range> outRanges);
}
