

namespace Nemonuri.Beliefs.Common.Tests;

public static class DefinedDoubtFunction
{
    public static DoubtFunction GetIndexFunction {get;} = GetIndexFunctionImpl;

    private static double GetIndexFunctionImpl(uint index)
    {
        return index;
    }
}

public static class DefinedDoubtFunctional
{
    public static DoubtFunctional IdentityFunctional {get;} = IdentityFunctionalImpl;

    private static double IdentityFunctionalImpl
    (
        DoubtFunction[] prevDoubtFunctions,
        uint[] prevIndexes,
        DoubtFunction currentDoubtFunction,
        uint currentIndex,
        DoubtFunction? nextDoubtFunction,
        Doubt2DFunction? nextDoubt2DFunction,
        uint nextIndex
    )
    {
        return currentDoubtFunction.Invoke(currentIndex);
    }

    public static DoubtFunctional DistanceFunctional {get;} = DistanceFunctionalImpl;

    private static double DistanceFunctionalImpl
    (
        DoubtFunction[] prevDoubtFunctions,
        uint[] prevIndexes,
        DoubtFunction currentDoubtFunction,
        uint currentIndex,
        DoubtFunction? nextDoubtFunction,
        Doubt2DFunction? nextDoubt2DFunction,
        uint nextIndex
    )
    {
        Guard.IsNotNull(nextDoubtFunction);

        double d0 = currentDoubtFunction(currentIndex);
        double d1 = nextDoubtFunction(nextIndex);

        return Math.Sqrt(d0*d0 + d1*d1);
    }
}