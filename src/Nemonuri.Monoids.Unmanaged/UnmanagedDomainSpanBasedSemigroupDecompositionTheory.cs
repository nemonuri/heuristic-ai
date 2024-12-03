namespace Nemonuri.Monoids.Unmanaged;

public static class UnmanagedDomainSpanBasedSemigroupDecompositionTheory
{
    public static int GetOutElementsByteLength<TDomain>
    (
        this ISpanBasedSemigroupDecompositionPremise<TDomain> premise
    )
    where TDomain : unmanaged
    {
        Guard.IsNotNull(premise);

        checked
        {
            int unitByteLength = Marshal.SizeOf<TDomain>();
            int unitCount = premise.OutElementsLength;
            return unitByteLength * unitCount;
        }
    }
}