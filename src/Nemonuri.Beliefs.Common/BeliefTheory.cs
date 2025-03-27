namespace Nemonuri.Beliefs;

public static class BeliefTheory 
{
    public static bool TryGetDoubt
    (
        this DoubtFunction doubtFunction,
        uint cardinality,
        uint index,
        out double doubt
    )
    {
        Guard.IsNotNull(doubtFunction);

        if (!(index < cardinality)) 
        {
            doubt = default;
            return false;
        }

        doubt = doubtFunction.Invoke(index);
        return true;
    }

    public static DoubtFunction CreateDoubtFunction
    (
        this DoubtFunctional doubtFunctional,
        uint[] prevIndexes,
        DoubtFunction[] prevDoubtFunctions,
        uint currentIndex,
        DoubtFunction currentDoubtFunction,
        uint nextDoubtFunctionCardinality,
        out double[] innerDoubts
    )
    {
        Guard.IsNotNull(doubtFunctional);
        Guard.IsNotNull(prevIndexes);
        Guard.IsNotNull(prevDoubtFunctions);
        Guard.IsEqualTo(prevIndexes.Length, prevDoubtFunctions.Length);

        innerDoubts = new double[nextDoubtFunctionCardinality];
        Guard.IsGreaterThanOrEqualTo(innerDoubts.Min(), 0);

        for (uint nextIndex = 0; nextIndex < nextDoubtFunctionCardinality; nextIndex++)
        {
            innerDoubts[nextIndex] = doubtFunctional.Invoke(prevIndexes, prevDoubtFunctions, currentIndex, currentDoubtFunction, nextIndex);
        }

        double[] localInnerDoubts = innerDoubts;
        double ResultDoubtFunction(uint index)
        {
            return localInnerDoubts[index];
        }

        return ResultDoubtFunction;
    }

    public static DoubtFunction[] CreateDoubtFunctions
    (
        this DoubtFunctional doubtFunctional,
        uint[] prevIndexes,
        DoubtFunction[] prevDoubtFunctions,
        DoubtFunction currentDoubtFunction,
        uint currentDoubtFunctionCardinality,
        uint nextDoubtFunctionCardinality,
        out double[][] innerDoubtArrays
    )
    {
        innerDoubtArrays = new double[currentDoubtFunctionCardinality][];
        DoubtFunction[] resultDoubtFunctions = new DoubtFunction[currentDoubtFunctionCardinality];

        for (uint currentIndex = 0; currentIndex < currentDoubtFunctionCardinality; currentIndex++)
        {
            resultDoubtFunctions[currentIndex] = CreateDoubtFunction
            (
                doubtFunctional,
                prevIndexes,
                prevDoubtFunctions,
                currentIndex,
                currentDoubtFunction,
                nextDoubtFunctionCardinality,
                out double[] innerDoubts
            );
            innerDoubtArrays[currentIndex] = innerDoubts;
        }

        return resultDoubtFunctions;
    }
}