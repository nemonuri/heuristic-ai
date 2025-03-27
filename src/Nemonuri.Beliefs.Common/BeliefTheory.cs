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

    public static DoubtFunction Composit
    (
        this DoubtFunction doubtFunction,
        params Func<double, double>[] functions
    )
    {
        Guard.IsNotNull(doubtFunction);
        Guard.IsNotNull(functions);

        double ResultDoubtFunction(uint index)
        {
            double result = doubtFunction.Invoke(index);
            for (int i = 0; i < functions.Length; i++)
            {
                result = functions[i].Invoke(result);
            }
            return result;
        }

        return ResultDoubtFunction;
    }

    public static DoubtFunction CreateDoubtFunction
    (
        this DoubtFunctional doubtFunctional,
        DoubtFunction[] prevDoubtFunctions,
        uint[] prevIndexes,
        DoubtFunction currentDoubtFunction,
        uint currentIndex,
        DoubtFunction? nextDoubtFunction,
        Doubt2DFunction? nextDoubt2DFunction,
        uint nextDoubtFunctionCardinality,
        out double[] innerDoubts
    )
    {
        Guard.IsNotNull(doubtFunctional);
        Guard.IsNotNull(prevIndexes);
        Guard.IsNotNull(prevDoubtFunctions);
        Guard.IsEqualTo(prevIndexes.Length, prevDoubtFunctions.Length);

        innerDoubts = new double[nextDoubtFunctionCardinality];

        for (uint nextIndex = 0; nextIndex < nextDoubtFunctionCardinality; nextIndex++)
        {
            innerDoubts[nextIndex] = doubtFunctional.Invoke
            (
                prevDoubtFunctions,
                prevIndexes, 
                currentDoubtFunction,
                currentIndex, 
                nextDoubtFunction,
                nextDoubt2DFunction,
                nextIndex
            );
        }

        Guard.IsGreaterThanOrEqualTo(innerDoubts.Min(), 0);

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
        DoubtFunction[] prevDoubtFunctions,
        uint[] prevIndexes,
        DoubtFunction currentDoubtFunction,
        uint currentDoubtFunctionCardinality,
        DoubtFunction? nextDoubtFunction,
        Doubt2DFunction? nextDoubt2DFunction,
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
                prevDoubtFunctions,
                prevIndexes,
                currentDoubtFunction,
                currentIndex,
                nextDoubtFunction,
                nextDoubt2DFunction,
                nextDoubtFunctionCardinality,
                out double[] innerDoubts
            );
            innerDoubtArrays[currentIndex] = innerDoubts;
        }

        return resultDoubtFunctions;
    }
}