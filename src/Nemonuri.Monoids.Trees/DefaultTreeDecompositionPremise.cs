using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Monoids.Trees;

public class DefaultTreeDecompositionPremise<TLeaf, TTree>
{
    public DefaultTreeDecompositionPremise
    (
        IAlternatePremise<TTree, TLeaf> treeAndLeafAlternatePremise,
        ISemigroupDecompositionPremise<TTree> treeToTreeDecompositionPremise
    )
    {
        TreeAndLeafAlternatePremise = treeAndLeafAlternatePremise;
        TreeToTreeDecompositionPremise = treeToTreeDecompositionPremise;
    }
    
    public IAlternatePremise<TTree, TLeaf> TreeAndLeafAlternatePremise {get;}
    public ISemigroupDecompositionPremise<TTree> TreeToTreeDecompositionPremise {get;}

    public bool TryAlterToLeaf(TTree tree, [NotNullWhen(true)] out TLeaf? outLeaf) =>
        TreeAndLeafAlternatePremise.TryMapToAlternate(tree, out outLeaf);
    
    public bool TryDecomposeToTrees(TTree tree, [NotNullWhen(true)] out IEnumerable<TTree>? outTrees) =>
        TreeToTreeDecompositionPremise.TryDecomposeInChain(tree, out outTrees);
    
    public void InvokeForLeafOrTrees
    (
        TTree tree,
        Action<TLeaf> leafAction,
        Action<IEnumerable<TTree>> treesAction
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
}