using Nemonuri.Maths.Permutations;

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
        bool useGettingItemIndexesPermutationGroup,
        ReadOnlySpan<int> gettingItemIndexesPermutationGroup,
        bool useSettingSuccessorIndexesPermutationGroup,
        ReadOnlySpan<int> settingSuccessorIndexesPermutationGroup,
        Span<T> destination,
        out int projectedCount,
        out bool overflowed
    )
    {
        Guard.IsEqualTo(indexes.Length, source.Rank);
        if (useGettingItemIndexesPermutationGroup)
        {
            Guard.IsTrue(PermutationTheory.IsNormalizedPermutationGroup(gettingItemIndexesPermutationGroup));
            Guard.IsEqualTo(indexes.Length, gettingItemIndexesPermutationGroup.Length);
        }
        if (useSettingSuccessorIndexesPermutationGroup)
        {
            Guard.IsTrue(PermutationTheory.IsNormalizedPermutationGroup(settingSuccessorIndexesPermutationGroup));
            Guard.IsEqualTo(indexes.Length, settingSuccessorIndexesPermutationGroup.Length);
        }

        //--- Create gettingItemIndexesPermutationGroup ---
        Span<int> inverseGettingItemIndexesPermutationGroup = 
            useGettingItemIndexesPermutationGroup ? stackalloc int[gettingItemIndexesPermutationGroup.Length] : [];

        if (useGettingItemIndexesPermutationGroup)
        {
            PermutationTheory.GetInverseNormalizedPermutationGroup
            (
                source: gettingItemIndexesPermutationGroup,
                destination: inverseGettingItemIndexesPermutationGroup,
                guardingSourceIsNormalizedPermutationGroup: false
            );
        }
        //---|

        //--- Create settingSuccessorIndexesPermutationGroup ---
        Span<int> inverseSettingSuccessorIndexesPermutationGroup = 
            useSettingSuccessorIndexesPermutationGroup ? stackalloc int[settingSuccessorIndexesPermutationGroup.Length] : [];

        if (useSettingSuccessorIndexesPermutationGroup)
        {
            PermutationTheory.GetInverseNormalizedPermutationGroup
            (
                source: settingSuccessorIndexesPermutationGroup,
                destination: inverseSettingSuccessorIndexesPermutationGroup,
                guardingSourceIsNormalizedPermutationGroup: false
            );
        }
        //---|

        //Todo: 군 연산으로, 더 효율적으로 만들 수 있을텐데

        //--- Create permutatedLengths ---
        Span<nint> permutatedLengths = stackalloc nint[source.Lengths.Length];
        if (useSettingSuccessorIndexesPermutationGroup)
        {
            PermutationTheory.ApplyMultiProjection
            (
                source: source.Lengths,
                projectionIndexes: settingSuccessorIndexesPermutationGroup,
                destination: permutatedLengths
            );
        }
        else
        {
            source.Lengths.CopyTo(permutatedLengths);
        }
        //---|

        overflowed = false;
        projectedCount = 0;
        while (true)
        {
            if (!(projectedCount < destination.Length))
            {
                break;
            }

            if (useGettingItemIndexesPermutationGroup) {PermutationTheory.ApplyMultiProjection(indexes, gettingItemIndexesPermutationGroup, indexes);}
            destination[projectedCount] = source[indexes];
            if (useGettingItemIndexesPermutationGroup) {PermutationTheory.ApplyMultiProjection(indexes, inverseGettingItemIndexesPermutationGroup, indexes);}
            projectedCount++;

            if (useSettingSuccessorIndexesPermutationGroup) {PermutationTheory.ApplyMultiProjection(indexes, settingSuccessorIndexesPermutationGroup, indexes);}
            SetSuccessorIndexes(indexes, permutatedLengths, out overflowed);
            if (useSettingSuccessorIndexesPermutationGroup) {PermutationTheory.ApplyMultiProjection(indexes, inverseSettingSuccessorIndexesPermutationGroup, indexes);}

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

    public static void ApplyMultiProjectionAndAdjustLevel<T>
    (
        Span<T> targetSpan,
        ReadOnlySpan<int> levelUpProjectionIndexes,
        ReadOnlySpan<int> levelDownProjectionIndexes,
        ref int permutationLevel,
        int goalPermutationLevel
    )
        where T : unmanaged
    {
        while (permutationLevel != goalPermutationLevel)
        {
            if (permutationLevel < goalPermutationLevel)
            {
                PermutationTheory.ApplyMultiProjection(targetSpan, levelUpProjectionIndexes, targetSpan);
                permutationLevel++;
            }
            else if (permutationLevel > goalPermutationLevel)
            {
                PermutationTheory.ApplyMultiProjection(targetSpan, levelDownProjectionIndexes, targetSpan);
                permutationLevel--;
            }
            else
            {
                ThrowHelper.ThrowInvalidDataException();
            }
        }
    }
}
