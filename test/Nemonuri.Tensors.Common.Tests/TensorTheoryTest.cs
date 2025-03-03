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
    public void Indexes_NormalizedPermutationGroup__ProjectToSpan__Destination_Is_Expected
    (
        int[] startIndexes,
        int[] normalizedPermutationGroup,
        float[] expectedDestination
    )
    {
        //Model
        Tensor<float> source = CreateTensor1();
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

    public static Tensor<float> CreateTensor1()
    {
        float[] flattened1 =
        [
            1.1f, 1.2f, 1.3f,
            2.1f, 2.2f, 2.3f,
            3.1f, 3.2f, 3.3f
        ];
        return Tensor.Create(flattened1, [3,3]);
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
}
