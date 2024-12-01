using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids.FreeMonoids;

using System.Collections.Generic;
using CommunityToolkit.Diagnostics;
using Infrastructure;

public class DefaultFreeMonoidTreePremise<TTree, TLeaf>
{
    public IReadOnlyListBasedReversibleChainOperationPremise<TTree, TTree> InnerChainOperationPremise1 {get;}
    public IReadOnlyListBasedReversibleChainOperationPremise<TTree, TLeaf> InnerChainOperationPremise2 {get;}
    public IFreeMonoidPremise<TTree, TLeaf> InnerTreeAndLeafPremise {get;}

    public class ChainOperationPremise : IReadOnlyListBasedReversibleChainOperationPremise<TTree, TTree>
    {
    }

    public class TreeAndLeafPremise : IFreeMonoidPremise<TTree, TLeaf>
    {
        public TTree MapToDomain(TLeaf alternate)
        {
            throw new NotImplementedException();
        }

        public bool TryMapToDomain(TLeaf alternate, [NotNullWhen(true)] out TTree? outDomain)
        {
            throw new NotImplementedException();
        }

        public TLeaf MapToAlternate(TTree domain)
        {
            throw new NotImplementedException();
        }

        public bool TryMapToAlternate(TTree domain, [NotNullWhen(true)] out TLeaf? outAlternate)
        {
            throw new NotImplementedException();
        }

        public TTree Identity => throw new NotImplementedException();

        public TTree Operate(TTree left, TTree right)
        {
            throw new NotImplementedException();
        }

        public bool TryOperate(TTree left, TTree right, [NotNullWhen(true)] out TTree? result)
        {
            throw new NotImplementedException();
        }

        public TTree OperateInChain(IReadOnlyList<TLeaf> source)
        {
            throw new NotImplementedException();
        }

        public bool TryOperateInChain(IReadOnlyList<TLeaf> source, [NotNullWhen(true)] out TTree? outTarget)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TLeaf> DecomposeInChain(TTree target)
        {
            throw new NotImplementedException();
        }

        public bool TryDecomposeInChain(TTree target, [NotNullWhen(true)] out IReadOnlyList<TLeaf>? outSource)
        {
            throw new NotImplementedException();
        }
    }
}

