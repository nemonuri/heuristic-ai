namespace Nemonuri.Monoids.FreeMonoids;

public interface IFreeMonoidTreePremise<TTree, TLeaf> :
    IMonoidPremise<TTree, TLeaf>,
    IMonoidPremise<TTree, IReadOnlyList<TLeaf>>
{

}
