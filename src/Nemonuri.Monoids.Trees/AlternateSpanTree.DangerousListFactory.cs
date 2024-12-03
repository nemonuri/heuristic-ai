namespace Nemonuri.Monoids.Trees;

public readonly ref partial struct AlternateSpanTree<TAlternate>
    where TAlternate : unmanaged
{
    public readonly ref struct DangerousListFactory
    {
        public AlternateSpanTree<TAlternate> AlternateSpanTree {get;}

        public ISpanPartitionPremise<TAlternate> SpanPartitionPremise {get;}

        public int DangerousSpanSnapshotSpanLength {get;}

        internal DangerousListFactory
        (
            AlternateSpanTree<TAlternate> alternateSpanTree,
            ISpanPartitionPremise<TAlternate> spanPartitionPremis
        )
        {
            AlternateSpanTree = alternateSpanTree;
            SpanPartitionPremise = spanPartitionPremis;
            DangerousSpanSnapshotSpanLength = spanPartitionPremis.GetOutRangesSpanLength(alternateSpanTree.InnerReadOnlySpan);
        }

        public DangerousAlternateSpanTreeList<TAlternate> Create(Span<DangerousSpanSnapshot<TAlternate>> dangerousSpanSnapshotSpan)
        {
            Guard.IsEqualTo(DangerousSpanSnapshotSpanLength, dangerousSpanSnapshotSpan.Length);

            DangerousSpanList<TAlternate> innerDangerousSpanList = 
                DangerousSpanPartitionTheory
                    .GetDangerousSpanListFactory(AlternateSpanTree.InnerReadOnlySpan, SpanPartitionPremise)
                    .Create(dangerousSpanSnapshotSpan);

            return new DangerousAlternateSpanTreeList<TAlternate>(innerDangerousSpanList);
        }
    }
    
}
