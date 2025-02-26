namespace Nemonuri.Tensors;

[Experimental(Experimentals.TensorTDiagId, UrlFormat = Experimentals.SharedUrlFormat)]
public static class TensorTheory
{
    public static void Project<T>
    (
        scoped ReadOnlyTensorSpan<T> source,
        scoped ReadOnlySpan<nint> startIndexes,
        Span<T> destination,
        Span<nint> endIndexes,
        out int projectedCount,
        out bool isFinished
    )
    {
        Guard.IsEqualTo(startIndexes.Length, source.Rank);
        Guard.IsEqualTo(endIndexes.Length, source.Rank);

        Span<nint> curIndexes = stackalloc nint[startIndexes.Length];
        startIndexes.CopyTo(curIndexes);

        projectedCount = 0;
        while (true)
        {
            //destination[projectedCount] = 

            
            //AdjustIndexes(source.Rank - 1, 1, curIndexes, source.Lengths, out bool overflowed);
            SetIndexesToSuccessor(curIndexes, source.Lengths, out bool overflowed);
            

            if (overflowed)
            {
                break;
            }

            projectedCount++;
        }
        
    }

    public static void SetIndexesToSuccessor(Span<nint> indexes, scoped ReadOnlySpan<nint> lengths, out bool overflowed)
    {
        AdjustIndexes(indexes.Length - 1, 1, indexes, lengths, out overflowed);
    }

/**
Source:
https://github.com/dotnet/runtime/blob/main/src/libraries/System.Numerics.Tensors/src/System/Numerics/Tensors/netcore/TensorSpanHelpers.cs
*/
    public static void AdjustIndexes(int curIndex, nint addend, Span<nint> curIndexes, scoped ReadOnlySpan<nint> lengths, out bool overflowed)
    {
        if (addend <= 0)
        {
            overflowed = false;
            return;
        }
        if (curIndex < 0)
        {
            overflowed = true;
            return;
        }
            
        curIndexes[curIndex] += addend;

        (nint Quotient, nint Remainder) result = Math.DivRem(curIndexes[curIndex], lengths[curIndex]);

        AdjustIndexes(curIndex - 1, result.Quotient, curIndexes, lengths, out overflowed);
        curIndexes[curIndex] = result.Remainder;
    }
}

//public delegate 