namespace Nemonuri.Monoids;

public interface IListPartitionPremise<in T, in TList>
    where TList : 
        IReadOnlyList<T>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
{
    int GetOutRangesSpanLength(TList target);

    void Partition(TList target, Span<Range> outRanges);

    bool TryPartition(TList target, Span<Range> outRanges);
}