namespace Nemonuri.Monoids.Trees;

public interface ITreeDecompositionPremise<TLeaf, TTree> :
    IMonoidDecompositionPremise<TTree>,
    IAlternatePremise<TTree, TLeaf>
#if NET9_0_OR_GREATER
    where TLeaf : allows ref struct
    where TTree : allows ref struct
#endif
{

}
