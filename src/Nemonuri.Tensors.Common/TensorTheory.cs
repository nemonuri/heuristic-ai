﻿using Nemonuri.Maths.Permutations;

namespace Nemonuri.Tensors;

[Experimental(Experimentals.TensorTDiagId, UrlFormat = Experimentals.SharedUrlFormat)]
public static partial class TensorTheory
{
    public static void ProjectToSpan<T>
    (
        TexturePixelValueFactory<T> source,
        ReadOnlySpan<nint> lengths,
        Span<nint> indexes,
        Span<T> destination,
        out int projectedCount,
        out bool overflowed
    )
    {
        Guard.IsEqualTo(lengths.Length, indexes.Length);

        overflowed = false;
        projectedCount = 0;
        while (true)
        {
            if (!(projectedCount < destination.Length))
            {
                break;
            }

            destination[projectedCount] = source(indexes);
            projectedCount++;

            SetSuccessorIndexes(indexes, lengths, out overflowed);

            if (overflowed)
            {
                break;
            }
        }
    }

    public static void ProjectToSpan<T>
    (
        scoped ReadOnlyTensorSpan<T> source,
        Span<nint> indexes,
        ReadOnlySpan<bool> indexReverseModes,
        PermutationGroup gettingItemIndexesPermutationGroup,
        PermutationGroup settingSuccessorIndexesPermutationGroup,
        Span<T> destination,
        out int projectedCount,
        out bool overflowed
    )
    {
        Guard.IsEqualTo(indexes.Length, source.Rank);
        Guard.IsEqualTo(indexes.Length, gettingItemIndexesPermutationGroup.Length);
        Guard.IsEqualTo(indexes.Length, settingSuccessorIndexesPermutationGroup.Length);
        Guard.IsEqualTo(indexes.Length, indexReverseModes.Length);

        //--- Create inverse permutation group ---
        gettingItemIndexesPermutationGroup.TryGetInversePermutationGroup
        (
            stackalloc int[gettingItemIndexesPermutationGroup.Length],
            out PermutationGroup inverseGettingItemIndexesPermutationGroup
        );

        settingSuccessorIndexesPermutationGroup.TryGetInversePermutationGroup
        (
            stackalloc int[settingSuccessorIndexesPermutationGroup.Length],
            out PermutationGroup inverseSettingSuccessorIndexesPermutationGroup
        );
        //---|

        //--- Create permutatedLengths ---
        Span<nint> permutatedLengths = stackalloc nint[source.Lengths.Length];
        source.Lengths.CopyTo(permutatedLengths);

        inverseGettingItemIndexesPermutationGroup.TryApply(permutatedLengths, permutatedLengths);
        settingSuccessorIndexesPermutationGroup.TryApply(permutatedLengths, permutatedLengths);
        //---|

        //--- Create permutatedIsReverseIndexes ---
        Span<bool> permutatedIndexeReverseModes = stackalloc bool[indexReverseModes.Length];
        gettingItemIndexesPermutationGroup.TryApply(indexReverseModes, permutatedIndexeReverseModes);
        //---|

        overflowed = false;
        projectedCount = 0;
        Span<nint> reversedIndexes = stackalloc nint[indexes.Length];
        while (true)
        {
            if (!(projectedCount < destination.Length))
            {
                break;
            }

            gettingItemIndexesPermutationGroup.TryApply(indexes, indexes);
            //--- Get reversedIndexes ---
            if (!indexReverseModes.IsEmpty)
            {
                for (int i = 0; i < indexes.Length; i++)
                {
                    reversedIndexes[i] = indexReverseModes[i] ? source.Lengths[i] - indexes[i] : indexes[i];
                }
            }
            //---|
            destination[projectedCount] = source[indexReverseModes.IsEmpty ? indexes : reversedIndexes];
            inverseGettingItemIndexesPermutationGroup.TryApply(indexes, indexes);
            projectedCount++;

            settingSuccessorIndexesPermutationGroup.TryApply(indexes, indexes);
            SetSuccessorIndexes(indexes, permutatedLengths, out overflowed);
            inverseSettingSuccessorIndexesPermutationGroup.TryApply(indexes, indexes);

            if (overflowed)
            {
                break;
            }
        }
    }

    public static void ProjectToSpan<T>
    (
        scoped ReadOnlyTensorSpan<T> source,
        Span<nint> indexes,
        ReadOnlySpan<bool> indexReverseModes,
        bool useGettingItemIndexesPermutationGroup,
        ReadOnlySpan<int> gettingItemIndexesPermutationGroup,
        bool useSettingSuccessorIndexesPermutationGroup,
        ReadOnlySpan<int> settingSuccessorIndexesPermutationGroup,
        Span<T> destination,
        out int projectedCount,
        out bool overflowed
    ) =>
        ProjectToSpan
        (
            source,
            indexes,
            indexReverseModes,
            useGettingItemIndexesPermutationGroup ? new PermutationGroup(gettingItemIndexesPermutationGroup) : default,
            useSettingSuccessorIndexesPermutationGroup ? new PermutationGroup(settingSuccessorIndexesPermutationGroup) : default,
            destination,
            out projectedCount,
            out overflowed
        );

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

    public static void ProjectToTensorSpan<T>
    (
        scoped ReadOnlySpan<T> source,
        TensorSpan<T> destination,
        Span<nint> indexes,
        out int projectedCount,
        out bool overflowed
    )
    {
        Guard.IsEqualTo(indexes.Length, destination.Rank);

        overflowed = false;
        projectedCount = 0;
        while (true)
        {
            if (!(projectedCount < source.Length))
            {
                break;
            }

            destination[indexes] = source[projectedCount];
            projectedCount++;

            SetSuccessorIndexes(indexes, destination.Lengths, out overflowed);

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

    public static bool TrySetSuccessorIndexes(Span<nint> indexes, scoped ReadOnlySpan<nint> lengths)
    {
        SetSuccessorIndexes(indexes, lengths, out bool overflowed);
        return !overflowed;
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

    public static void FillTextureTensor<T>
    (
        TensorSpan<T> textureTensor,
        TexturePixelValueFactory<T> texturePixelValueFactory
    )
    {
        Guard.IsNotNull(texturePixelValueFactory);
        Span<nint> indexes = stackalloc nint[textureTensor.Rank];
        indexes.Clear();

        while (TrySetSuccessorIndexes(indexes, textureTensor.Lengths))
        {
            textureTensor[indexes] = texturePixelValueFactory.Invoke(indexes);
        }
    }

    public static void CreateOrUpdateTextureTensor<T>
    (
        [NotNull] ref Tensor<T>? textureTensor,
        ReadOnlySpan<nint> lengths,
        TexturePixelValueFactory<T> texturePixelValueFactory
    )
    {
        Guard.IsNotNull(texturePixelValueFactory);

        textureTensor ??= Tensor.Create<T>(lengths);

        Guard.IsTrue(lengths.SequenceEqual(textureTensor.Lengths));

        FillTextureTensor(textureTensor.AsTensorSpan(), texturePixelValueFactory);
    }
}
