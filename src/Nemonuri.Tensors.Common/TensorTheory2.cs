using Nemonuri.Maths.Permutations;

namespace Nemonuri.Tensors;

public static partial class TensorTheory
{
    public static void ApplyMultiProjectionWithLevel<T>
    (
        Span<T> targetSpan,
        ReadOnlySpan<int> levelUpProjectionIndexes,
        ReadOnlySpan<int> levelDownProjectionIndexes,
        ref int permutationLevel,
        int goalPermutationLevel
    )
    {
        Span<T>
        while (permutationLevel != goalPermutationLevel)
        {
            if (permutationLevel < goalPermutationLevel)
            {
                PermutationTheory.ApplyMultiProjection(targetSpan, levelUpProjectionIndexes, targetSpan);
                permutationLevel++;
            }
            else if (permutationLevel > goalPermutationLevel)
            {
                PermutationTheory.ApplyMultiProjection(indexes, inverseNormalizedPermutationGroup, indexes);
                permutationLevel--;
            }
            else
            {
                ThrowHelper.ThrowInvalidDataException();
            }
        }
    }
}