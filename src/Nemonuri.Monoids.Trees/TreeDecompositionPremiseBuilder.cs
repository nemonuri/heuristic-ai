namespace Nemonuri.Monoids.Trees;

public struct TreeDecompositionPremiseBuilder<TLeaf, TTree>
#if NET9_0_OR_GREATER
    where TLeaf : allows ref struct
    where TTree : allows ref struct
#endif
{
    public IMonoidDecompositionPremise<TTree>? TreeToTreeDecompositionPremise {get;set;}
    public IAlternatePremise<TTree, TLeaf>? TreeAndLeafAlternatePremise {get;set;}
}
