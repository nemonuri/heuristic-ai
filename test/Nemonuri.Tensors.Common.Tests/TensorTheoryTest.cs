using System.Numerics.Tensors;
using Nemonuri.Maths.Permutations;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nemonuri.Tensors.Common.Tests;

public class TensorTheoryTest
{
    private readonly ITestOutputHelper _output;

    public TensorTheoryTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [MemberData(nameof(Data1))]
    public void Tensor3x3_StartIndexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
    (
        int[] startIndexes,
        int[] normalizedPermutationGroup,
        float[] expectedDestination
    )
    {
        StartIndexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
        (
            CreateTensor3x3(),
            startIndexes,
            normalizedPermutationGroup,
            PermutationMode.None,
            PermutationMode.Normal,
            expectedDestination
        );
    }
    public static Tensor<float> CreateTensor3x3()
    {
        float[] flattened =
        [
            1.1f, 1.2f, 1.3f,
            2.1f, 2.2f, 2.3f,
            3.1f, 3.2f, 3.3f
        ];
        return Tensor.Create(flattened, [3,3]);
    }
    public static TheoryData<int[], int[], float[]> Data1 =>
    new ()
    {
        {[0,0], [0,1], [1.1f, 1.2f]},
        {[0,0], [0,1], [1.1f, 1.2f, 1.3f]},
        {[0,0], [0,1], [1.1f, 1.2f, 1.3f, 2.1f]},
        {[0,0], [1,0], [1.1f, 2.1f]},
        {[0,0], [1,0], [1.1f, 2.1f, 3.1f]},
        {[0,0], [1,0], [1.1f, 2.1f, 3.1f, 1.2f]},

        {[1,1], [0,1], [2.2f, 2.3f, 3.1f, 3.2f, 3.3f]},
        {[1,1], [0,1], [2.2f, 2.3f, 3.1f, 3.2f, 3.3f, 0f]},
        {[1,2], [0,1], [2.3f, 3.1f, 3.2f, 3.3f, 0f, 0f]},

        {[1,1], [1,0], [2.2f, 3.2f, 1.3f, 2.3f, 3.3f]},
        {[1,1], [1,0], [2.2f, 3.2f, 1.3f, 2.3f, 3.3f, 0f]},
        {[1,2], [1,0], [2.3f, 3.3f, 0f, 0f, 0f, 0f]}
    };

    [Theory]
    [MemberData(nameof(Data2))]
    public void Tensor5x3_StartIndexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
    (
        int[] startIndexes,
        int[] normalizedPermutationGroup,
        PermutationMode gettingItemIndexesPermutationMode,
        PermutationMode settingSuccessorIndexesPermutationMode,
        float[] expectedDestination
    )
    {
        StartIndexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
        (
            CreateTensor5x3(),
            startIndexes,
            normalizedPermutationGroup,
            gettingItemIndexesPermutationMode,
            settingSuccessorIndexesPermutationMode,
            expectedDestination
        );
    }
    public static Tensor<float> CreateTensor5x3()
    {
        float[] flattened =
        [
            1.1f, 1.2f, 1.3f, 1.4f, 1.5f,
            2.1f, 2.2f, 2.3f, 2.4f, 2.5f,
            3.1f, 3.2f, 3.3f, 3.4f, 3.5f
        ];
        return Tensor.Create(flattened, [3,3]);
    }
    public static TheoryData<int[], int[], PermutationMode, PermutationMode, float[]> Data2 =>
    new ()
    {
        {[0,0], [0,1], PermutationMode.None, PermutationMode.None, [1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 2.1f, 2.2f]},
        {[0,0], [1,0], PermutationMode.None, PermutationMode.None, [1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 2.1f, 2.2f, 2.3f, 2.4f]}
    };


    internal void StartIndexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
    (
        Tensor<float> source,
        int[] startIndexes,
        int[] normalizedPermutationGroup,
        PermutationMode gettingItemIndexesPermutationMode,
        PermutationMode settingSuccessorIndexesPermutationMode,
        float[] expectedDestination   
    )
    {
        //Model
        Span<float> actualDestination = stackalloc float[expectedDestination.Length];
        nint[] currentIndexes = startIndexes.Select(i => (nint)i).ToArray();

        //Act
        TensorTheory.ProjectToSpan
        (
            source: source,
            indexes: currentIndexes,
            normalizedPermutationGroup: normalizedPermutationGroup,
            gettingItemIndexesPermutationMode: default,
            settingSuccessorIndexesPermutationMode: default,
            destination: actualDestination,
            out _,
            out _
        );

        //Assert
        _output.WriteLine
        (
            $"""
            startIndexes: {LogTheory.ToLogString(startIndexes)}
            normalizedPermutationGroup: {LogTheory.ToLogString(normalizedPermutationGroup)}
            gettingItemIndexesPermutationMode: {gettingItemIndexesPermutationMode}
            settingSuccessorIndexesPermutationMode: {settingSuccessorIndexesPermutationMode}
            currentIndexes: {LogTheory.ToLogString(currentIndexes)}
            expectedDestination: {LogTheory.ToLogString(expectedDestination)}
            actualDestination: {LogTheory.ToLogString(actualDestination)}
            """
        );
        Assert.Equal(expectedDestination, actualDestination);
    }

    [Theory]
    [MemberData(nameof(Data3))]
    public void Source_NormalizedPermutationGroup_GoalPermutationLevels__ApplyMultiProjectionAndAdjustLevel__Destination_Is_Expected
    (
        float[] source,
        int[] normalizedPermutationGroup,
        int[] goalPermutationLevels,
        float[] expectedDestination
    )
    {
        //Model
        Span<float> actualDestination = stackalloc float[source.Length];
        source.AsSpan().CopyTo(actualDestination);
        Span<int> inverseNormalizedPermutationGroup = stackalloc int[normalizedPermutationGroup.Length];
        PermutationTheory.GetInverseNormalizedPermutationGroup
        (
            source: normalizedPermutationGroup,
            destination: inverseNormalizedPermutationGroup
        );
        int permutationLevel = 0;

        //Act
        for (int i = 0; i < goalPermutationLevels.Length; i++)
        {
            int goalPermutationLevel = goalPermutationLevels[i];
            TensorTheory.ApplyMultiProjectionAndAdjustLevel
            (
                targetSpan: actualDestination,
                levelUpProjectionIndexes: normalizedPermutationGroup,
                levelDownProjectionIndexes: inverseNormalizedPermutationGroup,
                permutationLevel: ref permutationLevel,
                goalPermutationLevel: goalPermutationLevel
            );
        }

        //Assert
        _output.WriteLine
        (
            $"""
            source: {LogTheory.ToLogString(source)}
            normalizedPermutationGroup: {LogTheory.ToLogString(normalizedPermutationGroup)}
            inverseNormalizedPermutationGroup: {LogTheory.ToLogString(inverseNormalizedPermutationGroup)}
            goalPermutationLevels: {LogTheory.ToLogString(goalPermutationLevels)}
            expectedDestination: {LogTheory.ToLogString(expectedDestination)}
            actualDestination: {LogTheory.ToLogString(actualDestination)}
            """
        );
        Assert.Equal(expectedDestination, actualDestination);
    }
    public static TheoryData<float[], int[], int[], float[]> Data3 =>
    new ()
    {
        {[1,2,3], [0,1,2], [1], [1,2,3]},
        {[1,2,3], [0,1,2], [-1], [1,2,3]},
        {[4,7,9,9.5f], [3,0,1,2], [1], [9.5f,4,7,9]},
        {[4,7,9,9.5f], [3,0,1,2], [2], [9,9.5f,4,7]},
        {[4,7,9,9.5f], [3,0,1,2], [-1], [7,9,9.5f,4]},
        {[4,7,9,9.5f], [3,0,1,2], [1,-1], [7,9,9.5f,4]},
        {[4,7,9,9.5f], [3,0,1,2], [1,2,-1,0], [4,7,9,9.5f]}
    };
}
