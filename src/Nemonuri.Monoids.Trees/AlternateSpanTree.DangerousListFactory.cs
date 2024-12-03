namespace Nemonuri.Monoids.Trees;

public readonly ref partial struct AlternateSpanTree<TAlternate>
    where TAlternate : unmanaged
{
    public readonly ref struct DangerousListFactory
    {
        public AlternateSpanTree<TAlternate> AlternateSpanTree {get;}

        public IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TAlternate> TreeAlternatePremise {get;}

        internal DangerousListFactory
        (
            AlternateSpanTree<TAlternate> alternateSpanTree,
            IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TAlternate> treeAlternatePremise
        )
        {
            AlternateSpanTree = alternateSpanTree;
            TreeAlternatePremise = treeAlternatePremise;
        }

        public int DangerousSpanSnapshotSpanLength => TreeAlternatePremise.OutElementsLength;

        public DangerousAlternateSpanTreeList<TAlternate> Create(Span<DangerousSpanSnapshot<TAlternate>> dangerousSpanSnapshotSpan)
        {
            Guard.IsEqualTo(DangerousSpanSnapshotSpanLength, dangerousSpanSnapshotSpan.Length);

            TreeAlternatePremise.DecomposeInChain(AlternateSpanTree.InnerReadOnlySpan, dangerousSpanSnapshotSpan);

            return new DangerousAlternateSpanTreeList<TAlternate>(new DangerousSpanList<TAlternate>(dangerousSpanSnapshotSpan));
        }

        public bool TryCreate(Span<DangerousSpanSnapshot<TAlternate>> dangerousSpanSnapshotSpan, out DangerousAlternateSpanTreeList<TAlternate> outDangerousSpanList)
        {
            Guard.IsEqualTo(DangerousSpanSnapshotSpanLength, dangerousSpanSnapshotSpan.Length);

            if (!TreeAlternatePremise.TryDecomposeInChain(AlternateSpanTree.InnerReadOnlySpan, dangerousSpanSnapshotSpan))
            {
                outDangerousSpanList = default;
                return false;
            }

            outDangerousSpanList = new DangerousAlternateSpanTreeList<TAlternate>(new DangerousSpanList<TAlternate>(dangerousSpanSnapshotSpan));
            return true;
        }
    }
    
}
