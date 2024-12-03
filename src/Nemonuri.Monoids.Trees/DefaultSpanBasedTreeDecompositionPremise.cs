namespace Nemonuri.Monoids.Trees;

public class DefaultSpanBasedTreeDecompositionPremise<TLeaf, TTree>
{
    public DefaultSpanBasedTreeDecompositionPremise
    (
        IAlternatePremise<TTree, TLeaf> treeAndLeafAlternatePremise,
        ISpanBasedSemigroupDecompositionPremise<TTree> treeToTreeDecompositionPremise
    )
    {
        TreeAndLeafAlternatePremise = treeAndLeafAlternatePremise;
        TreeToTreeDecompositionPremise = treeToTreeDecompositionPremise;
    }
    
    public IAlternatePremise<TTree, TLeaf> TreeAndLeafAlternatePremise {get;}
    public ISpanBasedSemigroupDecompositionPremise<TTree> TreeToTreeDecompositionPremise {get;}

    public bool TryAlterToLeaf(TTree tree, [NotNullWhen(true)] out TLeaf? outLeaf) =>
        TreeAndLeafAlternatePremise.TryMapToAlternate(tree, out outLeaf);

    public bool TryDecomposeToTrees(TTree tree, Span<TTree> outElements) =>
        TreeToTreeDecompositionPremise.TryDecomposeInChain(tree, outElements);

#if false
    public void InvokeForLeafOrTrees
    (
        TTree tree,
        Action<TLeaf> leafAction,
        Action<ReadOnlySpan<TTree>> treesAction
    )
    {
        if (TryAlterToLeaf(tree, out TLeaf? leaf1))
        {
            leafAction(leaf1);
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
#endif
}