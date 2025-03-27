namespace Nemonuri.Beliefs.Raws;

public struct RawDoubtFunctionalUnion
{
    public DoubtFunctionKind Kind;
    public DoubtFunctional? Dimension0;
    public DoubtFunctional1D? Dimension1;
    public DoubtFunctional2D? Dimension2;

    public RawDoubtFunctionalUnion(DoubtFunctional dimension0)
    {
        Kind = DoubtFunctionKind.None;
        Dimension0 = dimension0;
        Dimension1 = null;
        Dimension2 = null;
    }

    public RawDoubtFunctionalUnion(DoubtFunctional1D dimension1)
    {
        Kind = DoubtFunctionKind.Dimension1;
        Dimension0 = null;
        Dimension1 = dimension1;
        Dimension2 = null;
    }

    public RawDoubtFunctionalUnion(DoubtFunctional2D dimension2)
    {
        Kind = DoubtFunctionKind.Dimension2;
        Dimension0 = null;
        Dimension1 = null;
        Dimension2 = dimension2;
    }
}