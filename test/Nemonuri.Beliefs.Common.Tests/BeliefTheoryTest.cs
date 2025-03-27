

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
            8,
            out var innerDoubtArrays
        );

        Assert.NotNull(innerDoubtArrays);
    }
}
