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
        this DoubtFunctionalUnion doubtFunctionalUnion,
        DoubtFunctionInvokeInfo[] prevDoubtFunctionInfos,
        DoubtFunctionInvokeInfo currentDoubtFunctionInfo,
        DoubtFunctionUnion nextDoubtFunctionUnion,
        uint nextDoubtFunctionCardinality,
        out double[] innerDoubts
    ) =>
        CreateDoubtFunction
        (
            doubtFunctionalUnion,
            [..prevDoubtFunctionInfos.Select(a => a.DoubtFunction)],
            [..prevDoubtFunctionInfos.Select(a => a.Index)],
            currentDoubtFunctionInfo.DoubtFunction,
            currentDoubtFunctionInfo.Index,
            nextDoubtFunctionUnion,
            nextDoubtFunctionCardinality,
            out innerDoubts
        );

    public static DoubtFunction CreateDoubtFunction
    (
        this DoubtFunctionalUnion doubtFunctionalUnion,
        DoubtFunction[] prevDoubtFunctions,
        uint[] prevIndexes,
        DoubtFunction currentDoubtFunction,
        uint currentIndex,
        DoubtFunctionUnion nextDoubtFunctionUnion,
        uint nextDoubtFunctionCardinality,
        out double[] innerDoubts
    )
    {
        Guard.IsNotNull(prevIndexes);
        Guard.IsNotNull(prevDoubtFunctions);
        Guard.IsEqualTo(prevIndexes.Length, prevDoubtFunctions.Length);

        innerDoubts = new double[nextDoubtFunctionCardinality];

        for (uint nextIndex = 0; nextIndex < nextDoubtFunctionCardinality; nextIndex++)
        {
            double d = 0;
            if 
            (
                doubtFunctionalUnion.Kind == DoubtFunctionKind.None &&
                doubtFunctionalUnion.Dimension0 is {} doubtFunctional0
            )
            {
                d = doubtFunctional0.Invoke
                (
                    prevDoubtFunctions,
                    prevIndexes, 
                    currentDoubtFunction,
                    currentIndex,
                    nextIndex
                );
            }
            else if 
            (
                doubtFunctionalUnion.Kind == DoubtFunctionKind.Dimension1 &&
                doubtFunctionalUnion.Dimension1 is {} doubtFunctional1 &&
                nextDoubtFunctionUnion.Kind == DoubtFunctionKind.Dimension1 &&
                nextDoubtFunctionUnion.Dimension1 is {} doubtFunction1
            )
            {
                d = doubtFunctional1.Invoke
                (
                    prevDoubtFunctions,
                    prevIndexes, 
                    currentDoubtFunction,
                    currentIndex,
                    doubtFunction1,
                    nextIndex
                );
            }
            else if 
            (
                doubtFunctionalUnion.Kind == DoubtFunctionKind.Dimension2 &&
                doubtFunctionalUnion.Dimension2 is {} doubtFunctional2 &&
                nextDoubtFunctionUnion.Kind == DoubtFunctionKind.Dimension2 &&
                nextDoubtFunctionUnion.Dimension2 is {} doubtFunction2
            )
            {
                d = doubtFunctional2.Invoke
                (
                    prevDoubtFunctions,
                    prevIndexes, 
                    currentDoubtFunction,
                    currentIndex,
                    doubtFunction2,
                    nextIndex
                );
            }
            innerDoubts[nextIndex] = d;
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
        this DoubtFunctionalUnion doubtFunctionalUnion,
        DoubtFunctionInvokeInfo[] prevDoubtFunctionInfos,
        DoubtFunction currentDoubtFunction,
        uint currentDoubtFunctionCardinality,
        DoubtFunctionUnion nextDoubtFunctionUnion,
        uint nextDoubtFunctionCardinality,
        out double[][] innerDoubtArrays
    ) =>
        CreateDoubtFunctions
        (
            doubtFunctionalUnion,
            [..prevDoubtFunctionInfos.Select(a => a.DoubtFunction)],
            [..prevDoubtFunctionInfos.Select(a => a.Index)],
            currentDoubtFunction,
            currentDoubtFunctionCardinality,
            nextDoubtFunctionUnion,
            nextDoubtFunctionCardinality,
            out innerDoubtArrays
        );

    public static DoubtFunction[] CreateDoubtFunctions
    (
        this DoubtFunctionalUnion doubtFunctionalUnion,
        DoubtFunction[] prevDoubtFunctions,
        uint[] prevIndexes,
        DoubtFunction currentDoubtFunction,
        uint currentDoubtFunctionCardinality,
        DoubtFunctionUnion nextDoubtFunctionUnion,
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
                doubtFunctionalUnion,
                prevDoubtFunctions,
                prevIndexes,
                currentDoubtFunction,
                currentIndex,
                nextDoubtFunctionUnion,
                nextDoubtFunctionCardinality,
                out double[] innerDoubts
            );
            innerDoubtArrays[currentIndex] = innerDoubts;
        }

        return resultDoubtFunctions;
    }

    public static double[,] To2DArray
    (
        this double[][] innerDoubtArrays
    )
    {
        int yMax = innerDoubtArrays.Select(a => a.Length).Max();
        double[,] result = new double[innerDoubtArrays.Length, yMax];
        for (int x = 0; x < innerDoubtArrays.Length; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                result[x, y] = innerDoubtArrays[x][y];
            }
        }

        return result;
    }
}