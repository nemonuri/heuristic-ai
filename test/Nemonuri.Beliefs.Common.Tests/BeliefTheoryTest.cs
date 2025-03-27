

namespace Nemonuri.Beliefs.Common.Tests;

public class BeliefTheoryTest
{
    [Fact]
    public void Test1()
    {
        BeliefTheory.CreateDoubtFunctions
        (
            DefinedDoubtFunctional.IdentityFunctional,
            [],
            [],
            DefinedDoubtFunction.GetIndexFunction,
            10,
            null,
            null,
            8,
            out var innerDoubtArrays
        );

        Assert.NotNull(innerDoubtArrays);
    }

    [Fact]
    public void Test2()
    {
        BeliefTheory.CreateDoubtFunctions
        (
            DefinedDoubtFunctional.DistanceFunctional,
            [],
            [],
            DefinedDoubtFunction.GetIndexFunction,
            10,
            DefinedDoubtFunction.GetIndexFunction,
            null,
            8,
            out var innerDoubtArrays
        );

        Assert.NotNull(innerDoubtArrays);
    }

    [Fact]
    public void Test3()
    {
        BeliefTheory.CreateDoubtFunctions
        (
            DefinedDoubtFunctional.DistanceFunctional,
            [],
            [],
            DefinedDoubtFunction.GetIndexFunction.Composit(v => v-5),
            10,
            DefinedDoubtFunction.GetIndexFunction.Composit(v => v-4),
            null,
            8,
            out var innerDoubtArrays
        );

        Assert.NotNull(innerDoubtArrays);
    }
}
