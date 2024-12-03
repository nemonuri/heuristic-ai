using System.Net.Http.Headers;

namespace Nemonuri.Monoids.Trees;

public ref struct 
DecomposAndInvokeContext<TUnmanagedAlternate, TLeaf, TExtraContext>
    where TUnmanagedAlternate : unmanaged
#if NET9_0_OR_GREATER
    where TLeaf : allows ref struct
    where TExtraContext : allows ref struct
#endif
{
    public ReadOnlySpan<TUnmanagedAlternate> AlternateTree {get;}

    //public int ConsumedLength {get; internal set;}

    //public AlternateTreeAction<TUnmanagedAlternate, TLeaf, TExtraContext> AlternateTreesAction {get;}

    //public LeafAction<TUnmanagedAlternate, TLeaf, TExtraContext> LeafAction {get;}

    //public IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TUnmanagedAlternate> TreeAlternatePremise {get;}

    //public IUnmanagedAlternatePremise<TLeaf, TUnmanagedAlternate> LeafAlternatePremise {get;}

    //public TExtraContext ExtraContext {get;}

    public DecomposAndInvokeContext
    (
        ReadOnlySpan<TUnmanagedAlternate> alternateTree
        //AlternateTreeAction<TUnmanagedAlternate, TLeaf, TExtraContext> alternateTreesAction,
        //LeafAction<TUnmanagedAlternate, TLeaf, TExtraContext> leafAction,
        //IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TUnmanagedAlternate> treeAlternatePremise,
        //IUnmanagedAlternatePremise<TLeaf, TUnmanagedAlternate> leafAlternatePremise,
        //TExtraContext extraContext
    )
    {
        AlternateTree = alternateTree;
        //ConsumedLength = 0;
        //AlternateTreesAction = alternateTreesAction;
        //LeafAction = leafAction;
        //TreeAlternatePremise = treeAlternatePremise;
        //LeafAlternatePremise = leafAlternatePremise;
        //ExtraContext = extraContext;
    }


    
}