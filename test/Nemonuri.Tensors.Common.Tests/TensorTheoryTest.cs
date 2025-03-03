using System.Numerics.Tensors;
using Xunit.Abstractions;

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
        float[] expectedDestination
    )
    {
        StartIndexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
        (
            CreateTensor5x3(),
            startIndexes,
            normalizedPermutationGroup,
            expectedDestination
        );
    }
    public static Tensor<float> CreateTensor5x3()
    {
        float[] flattened2 =
        [
            1.1f, 1.2f, 1.3f, 1.4f, 1.5f,
            2.1f, 2.2f, 2.3f, 2.4f, 2.5f,
            3.1f, 3.2f, 3.3f, 3.4f, 3.5f
        ];
        return Tensor.Create(flattened2, [3,3]);
    }
    public static TheoryData<int[], int[], float[]> Data2 =>
    new ()
    {
        {[0,0], [0,1], [1.1f, 1.2f]}
    };


    internal void StartIndexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
    (
        Tensor<float> source,
        int[] startIndexes,
        int[] normalizedPermutationGroup,
        float[] expectedDestination   
    )
    {
        //Model
        Span<float> actualDestination = stackalloc float[expectedDestination.Length];
        nint[] currentIndexes = startIndexes.Select(i => (nint)i).ToArray();

        //Act
        TensorTheory.ProjectToSpan
        (
            source,
            currentIndexes,
            normalizedPermutationGroup,
            actualDestination,
            out _,
            out _
        );

        //Assert
        _output.WriteLine
        (
            $"""
            startIndexes: {LogTheory.ConvertSpanToLogString(startIndexes)}
            normalizedPermutationGroup: {LogTheory.ConvertSpanToLogString(normalizedPermutationGroup)}
            currentIndexes: {LogTheory.ConvertSpanToLogString(currentIndexes)}
            expectedDestination: {LogTheory.ConvertSpanToLogString(expectedDestination)}
            actualDestination: {LogTheory.ConvertSpanToLogString(actualDestination)}
            """
        );
        Assert.Equal(expectedDestination, actualDestination);
    }


}
