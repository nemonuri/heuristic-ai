namespace Nemonuri.Tensors;

[Experimental(Experimentals.TensorTDiagId, UrlFormat = Experimentals.SharedUrlFormat)]
public static class TensorTheory
{
    public static void ProjectToSpan<T>
    (
        scoped ReadOnlyTensorSpan<T> source,
        Span<nint> indexes,
        Span<T> destination,
        out int projectedCount,
        out bool overflowed
    )
    {
        Guard.IsEqualTo(indexes.Length, source.Rank);

        overflowed = false;
        projectedCount = 0;
        while (true)
        {
            if (!(projectedCount < destination.Length))
            {
                break;
            }

            destination[projectedCount] = source[indexes];
            projectedCount++;

            SetSuccessorIndexes(indexes, source.Lengths, out overflowed);            

            if (overflowed)
            {
                break;
            }
        }
    }

    public static void SetSuccessorIndexes(Span<nint> indexes, scoped ReadOnlySpan<nint> lengths, out bool overflowed)
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