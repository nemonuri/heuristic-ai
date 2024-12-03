namespace Nemonuri.Monoids.Trees;

public class DefaultDangerousUnmanagedAlternateBasedTreeDecompositionPremise<TDomain, TUnmanagedAlternate>
    where TUnmanagedAlternate : unmanaged
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    public DefaultDangerousUnmanagedAlternateBasedTreeDecompositionPremise
    (
        IUnmanagedAlternatePremise<TDomain, TUnmanagedAlternate> unmanagedAlternatePremise,
        IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TUnmanagedAlternate> alternateSpanDecompositionPremise
    )
    {
        UnmanagedAlternatePremise = unmanagedAlternatePremise;
        AlternateSpanDecompositionPremise = alternateSpanDecompositionPremise;
    }
    
    public IUnmanagedAlternatePremise<TDomain, TUnmanagedAlternate> UnmanagedAlternatePremise {get;}

    public IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TUnmanagedAlternate> AlternateSpanDecompositionPremise {get;}

    public int InvokeForLeafOrTreesRequiredByteSpanLength => AlternateSpanDecompositionPremise.OutElementsLength;

    public bool TryAlterToLeaf(TTree tree, [NotNullWhen(true)] out TDomain? outLeaf) =>
        UnmanagedAlternatePremise.TryMapToAlternate(tree, out outLeaf);

    public bool TryDecomposeToTrees(TTree tree, Span<TTree> outElements) =>
        AlternateSpanDecompositionPremise.TryDecomposeInChain(tree, outElements);

    

    public void InvokeForLeafOrTrees<TContext>
    (
        TTree tree,
        Action<TDomain, TContext> leafAction,
        ReadOnlySpanAction<TTree, TContext> treesAction,
        TContext context,
        Span<byte> requiredByteSpan
    )
#if NET9_0_OR_GREATER
    where TContext : allows ref struct
#endif
    {
        Guard.IsEqualTo(InvokeForLeafOrTreesRequiredByteSpanLength, requiredByteSpan.Length);
            
        if (TryAlterToLeaf(tree, out TDomain? leaf1))
        {
            leafAction(leaf1, context);
        }
        else if(TryDecomposeToTrees(tree, out IEnumerable<TTree>? trees1))
        {
            treesAction(trees1);
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException("Should not reach here");
        }   
    }

}