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
        uint cardinality,
        out double[] innerDoubts
    )
    {
        Guard.IsNotNull(doubtFunctional);
        Guard.IsNotNull(prevIndexes);
        Guard.IsNotNull(prevDoubtFunctions);
        Guard.IsEqualTo(prevIndexes.Length, prevDoubtFunctions.Length);

        innerDoubts = new double[cardinality];

        for (uint i = 0; i < cardinality; i++)
        {
            innerDoubts[i] = doubtFunctional.Invoke(prevIndexes, prevDoubtFunctions, currentIndex, currentDoubtFunction, i);
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
        uint currentIndex,
        DoubtFunction currentDoubtFunction,
        uint doubtFunctionCardinality,
        uint doubtFunctionalCardinality,
        out double[][] innerDoubtArrays
    )
    {
        innerDoubtArrays = new double[doubtFunctionCardinality][];
        DoubtFunction[] resultDoubtFunctions = new DoubtFunction[doubtFunctionalCardinality];

        for (uint i = 0; i < doubtFunctionalCardinality; i++)
        {
            resultDoubtFunctions[i] = CreateDoubtFunction
            (
                doubtFunctional,
                prevIndexes,
                prevDoubtFunctions,
                currentIndex,
                currentDoubtFunction,
                doubtFunctionCardinality,
                out double[] innerDoubts
            );
            innerDoubtArrays[i] = innerDoubts;
        }

        return resultDoubtFunctions;
    }
}