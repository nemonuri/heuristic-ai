namespace Nemonuri.Monoids.Trees;

public static class DangerousUnmanagedAlternateBasedTreeTheory
{
    public static void DecomposAndInvoke
    <
        TUnmanagedAlternate,
        TLeaf,
        TExtraContext
    >
    (
        ReadOnlySpan<TUnmanagedAlternate> alternateTree,

        AlternateTreeAction<TUnmanagedAlternate, TLeaf, TExtraContext> alternateTreesAction,
        LeafAction<TUnmanagedAlternate, TLeaf, TExtraContext> leafAction,
        
        IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TUnmanagedAlternate> treeAlternatePremise,
        IUnmanagedAlternatePremise<TLeaf, TUnmanagedAlternate> leafAlternatePremise1,

        TExtraContext extraContext
    )
        where TUnmanagedAlternate : unmanaged
#if NET9_0_OR_GREATER
        where TLeaf : allows ref struct
        where TExtraContext : allows ref struct
#endif
    {
#if false
        int consumedSpanLength = 0;

        if 
        (
            leafAlternatePremise1.TryMapToDomain
            (
                alternateTree.Slice(consumedSpanLength, leafAlternatePremise1.OutAlternateSpanLength), 
                out TLeaf? outLeaf1
            )
        )
        {
            leafAction1(outLeaf1, extraContext);
        }
        else if
        (
            TryDecomposeToTrees(tree, out IEnumerable<TTree>? trees1)
        )
        {
            treesAction(trees1);
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("Should not reach here");
        }  
#endif

        DecomposAndInvokeContext<TUnmanagedAlternate, TLeaf, TExtraContext> context = new
        (
            alternateTree,
            alternateTreesAction,
            leafAction,
            treeAlternatePremise,
            leafAlternatePremise1,
            extraContext
        );

        
    }
}