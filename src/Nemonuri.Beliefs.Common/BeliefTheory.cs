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
        uint cardinality
    )
    {
        Guard.IsNotNull(doubtFunctional);
        Guard.IsNotNull(prevIndexes);
        Guard.IsNotNull(prevDoubtFunctions);
        Guard.IsEqualTo(prevIndexes.Length, prevDoubtFunctions.Length);

        double[] doubts = new double[cardinality];

        for (uint i = 0; i < cardinality; i++)
        {
            doubts[i] = doubtFunctional.Invoke(prevIndexes, prevDoubtFunctions, currentIndex, currentDoubtFunction, i);
        }

        double ResultDoubtFunction(uint index)
        {
            Dictionary<uint, double> cache = new ();
            if (!cache.TryGetValue(index, out double value))
            {
                value = doubtFunctional.Invoke(prevIndexes, prevDoubtFunctions, currentIndex, currentDoubtFunction, index);
                cache.Add(index, value);
            }
            return value;
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
        uint cardinality
    )
    {
        DoubtFunction[] resultDoubtFunctions = new DoubtFunction[cardinality];

        for (uint i = 0; i < cardinality; i++)
        {
            resultDoubtFunctions[i] = CreateDoubtFunction
            (
                doubtFunctional,
                prevIndexes,
                prevDoubtFunctions,
                currentIndex,
                currentDoubtFunction
            );
        }

        return resultDoubtFunctions;
    }
}