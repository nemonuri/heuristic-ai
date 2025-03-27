



namespace Nemonuri.Beliefs.Common.Tests;

public static class DefinedDoubtFunction
{
    public static DoubtFunction GetIndexFunction {get;} = IdentityFunctionImpl;

    private static double IdentityFunctionImpl(uint index)
    {
        return index;
    }
}

public static class DefinedDoubtFunctional
{
    public static DoubtFunctional IdentityFunctional {get;} = IdentityFunctionalImpl;

    private static double IdentityFunctionalImpl
    (
        uint[] prevIndexes,
        DoubtFunction[] prevDoubtFunctions,
        uint currentIndex,
        DoubtFunction currentDoubtFunction,
        uint nextIndex
    )
    {
        return currentDoubtFunction.Invoke(currentIndex);
    }

    public static DoubtFunctional DistanceFunctional {get;} = DistanceFunctionalImpl;

    private static double DistanceFunctionalImpl
    (
        uint[] prevIndexes, 
        DoubtFunction[] prevDoubtFunctions, 
        uint currentIndex, 
        DoubtFunction currentDoubtFunction, 
        uint nextIndex
    )
    {
        uint prevIndex = prevIndexes[^1];
        DoubtFunction prevDoubtFunction = prevDoubtFunctions[^1];

        double d0 = prevDoubtFunction(prevIndex);
        double d1 = currentDoubtFunction(currentIndex);

        
    }
}