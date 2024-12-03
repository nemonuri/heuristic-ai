using System.Buffers;
using Nemonuri.Monoids.Unmanaged;

namespace Nemonuri.Monoids.Trees;

public class DefaultSpanBasedTreeDecompositionPremise<TLeaf, TTree>
    where TTree : unmanaged
#if NET9_0_OR_GREATER
    where TLeaf : allows ref struct
#endif
{
    public DefaultSpanBasedTreeDecompositionPremise
    (
        IAlternatePremise<TTree, TLeaf> treeAndLeafAlternatePremise,
        ISpanBasedSemigroupDecompositionPremise<TTree> treeToTreeDecompositionPremise
    )
    {
        TreeAndLeafAlternatePremise = treeAndLeafAlternatePremise;
        TreeToTreeDecompositionPremise = treeToTreeDecompositionPremise;

        InvokeForLeafOrTreesRequiredByteSpanLength = treeToTreeDecompositionPremise.GetOutElementsByteLength();
    }
    
    public IAlternatePremise<TTree, TLeaf> TreeAndLeafAlternatePremise {get;}
    public ISpanBasedSemigroupDecompositionPremise<TTree> TreeToTreeDecompositionPremise {get;}

    public bool TryAlterToLeaf(TTree tree, [NotNullWhen(true)] out TLeaf? outLeaf) =>
        TreeAndLeafAlternatePremise.TryMapToAlternate(tree, out outLeaf);

    public bool TryDecomposeToTrees(TTree tree, Span<TTree> outElements) =>
        TreeToTreeDecompositionPremise.TryDecomposeInChain(tree, outElements);

    public int InvokeForLeafOrTreesRequiredByteSpanLength {get;}

    public void InvokeForLeafOrTrees<TContext>
    (
        TTree tree,
        Action<TLeaf, TContext> leafAction,
        ReadOnlySpanAction<TTree, TContext> treesAction,
        TContext context,
        Span<byte> requiredByteSpan
    )
#if NET9_0_OR_GREATER
    where TContext : allows ref struct
#endif
    {
        Guard.IsEqualTo(InvokeForLeafOrTreesRequiredByteSpanLength, requiredByteSpan.Length);
            
        if (TryAlterToLeaf(tree, out TLeaf? leaf1))
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