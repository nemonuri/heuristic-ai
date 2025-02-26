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

        Span<nint> cutIndexes = stackalloc nint[startIndexes.Length];

        while (true)
        {
            AdjustIndexes(source.Rank - 1, 1, cutIndexes, source.Lengths, out isFinished);
            if (isFinished)
            {
                break;
            }
        }
        
    }

/**
Source:
https://github.com/dotnet/runtime/blob/main/src/libraries/System.Numerics.Tensors/src/System/Numerics/Tensors/netcore/TensorSpanHelpers.cs
*/
    public static void AdjustIndexes(int curIndex, nint addend, Span<nint> curIndexes, scoped ReadOnlySpan<nint> length, out bool isFinished)
    {
        isFinished = false;

        if (addend <= 0 || curIndex < 0)
        {
            isFinished = true;
            return;
        }
            
        curIndexes[curIndex] += addend;

        (nint Quotient, nint Remainder) result = Math.DivRem(curIndexes[curIndex], length[curIndex]);

        AdjustIndexes(curIndex - 1, result.Quotient, curIndexes, length, out isFinished);
        curIndexes[curIndex] = result.Remainder;
    }
}

//public delegate 