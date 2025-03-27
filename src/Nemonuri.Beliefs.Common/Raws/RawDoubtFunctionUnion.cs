namespace Nemonuri.Beliefs.Raws;

public struct RawDoubtFunctionUnion
{
    public DoubtFunctionKind Kind;
    public DoubtFunction? Dimension1;
    public Doubt2DFunction? Dimension2;

    public RawDoubtFunctionUnion(DoubtFunction dimension1)
    {
        Kind = DoubtFunctionKind.Dimension1;
        Dimension1 = dimension1;
        Dimension2 = null;
    }

    public RawDoubtFunctionUnion(Doubt2DFunction dimension2)
    {
        Kind = DoubtFunctionKind.Dimension2;
        Dimension1 = null;
        Dimension2 = dimension2;
    }
}
