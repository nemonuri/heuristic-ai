namespace Nemonuri.Monoids.FreeMonoids.Infrastructure;

public interface IFreeMonoidTreeInfrastructurePremise<TTree, TLeaf> 
{
    IFreeMonoidPremise<TTree, TTree> TreeAndTreePremise {get;}
    IFreeMonoidPremise<TTree, TLeaf> TreeAndLeafPremise {get;}
}