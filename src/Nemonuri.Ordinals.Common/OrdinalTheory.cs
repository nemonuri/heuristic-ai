using System.Numerics;

namespace Nemonuri.Ordinals;

public static class OrdinalTheory
{
    public static void AdjustOrdinalSequence
    (
        int currentSequenceIndex, 
        nint addingValueAtCurrentSequenceIndex,
        Span<nint> ordinalSequence,
        scoped ReadOnlySpan<nint> cardinalitySequence,
        out nint overflowedCarry
    )
    {
        if (addingValueAtCurrentSequenceIndex <= 0)
        {
            overflowedCarry = 0;
            return;
        }

        if (currentSequenceIndex < 0)
        {
            overflowedCarry = addingValueAtCurrentSequenceIndex;
            return;
        }

        ordinalSequence[currentSequenceIndex] += addingValueAtCurrentSequenceIndex;

        (nint quotient, nint remainder) = Math.DivRem(ordinalSequence[currentSequenceIndex], cardinalitySequence[currentSequenceIndex]);
        AdjustOrdinalSequence(currentSequenceIndex - 1, quotient, ordinalSequence, cardinalitySequence, out overflowedCarry);
        ordinalSequence[currentSequenceIndex] = remainder;
    }

    public static bool MoveNext
    (
        Span<nint> ordinalSequence,
        scoped ReadOnlySpan<nint> cardinalitySequence
    )
    {
        AdjustOrdinalSequence(ordinalSequence.Length - 1, 1, ordinalSequence, cardinalitySequence, out nint overflowedCarry);
        return overflowedCarry == 0;
    }

    public static bool MoveNext
    (
        scoped ref bool initialized,
        Span<nint> ordinalSequence,
        scoped ReadOnlySpan<nint> cardinalitySequence
    )
    {
        if (!initialized)
        {
            ordinalSequence.Clear();
            initialized = true;
            return true;
        }
        else
        {
            return MoveNext(ordinalSequence, cardinalitySequence);
        }
    }

    public static bool IsValidCardinality(nint cardinality) => cardinality >= 0;

    public static bool IsValidCardinalitySpan(ReadOnlySpan<nint> cardinalitySpan)
    {
        foreach (nint cardinality in cardinalitySpan)
        {
            if (!IsValidCardinality(cardinality))
            {
                return false;
            }
        }

        return true;
    }

    public static nint GetFlatCardinality(ReadOnlySpan<nint> cardinalitySpan)
    {
        checked
        {
            if (!IsValidCardinalitySpan(cardinalitySpan))
            {
                return ThrowHelper.ThrowInvalidOperationException<nint>(/* TODO */);
            }

            if (cardinalitySpan.IsEmpty) {return 0;}

            nint result = 1;
            foreach (var cardinality in cardinalitySpan)
            {
                result *= cardinality;
            }

            return result;
        }
    }

    public static BigInteger GetFlatCardinalityAsBigInteger(ReadOnlySpan<nint> cardinalitySpan)
    {
        if (!IsValidCardinalitySpan(cardinalitySpan))
        {
            return ThrowHelper.ThrowInvalidOperationException<BigInteger>(/* TODO */);
        }

        if (cardinalitySpan.IsEmpty) {return 0;}

        BigInteger result = BigInteger.One;
        foreach (var cardinality in cardinalitySpan)
        {
            result *= cardinality;
        }

        return result;
    }
}