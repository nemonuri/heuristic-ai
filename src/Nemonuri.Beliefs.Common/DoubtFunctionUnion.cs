using Nemonuri.Beliefs.Raws;

namespace Nemonuri.Beliefs;

public readonly struct DoubtFunctionUnion
{
    public RawDoubtFunctionUnion Raw {get;}

    public DoubtFunctionUnion(RawDoubtFunctionUnion raw)
    {
        Raw = raw;
    }

    public DoubtFunctionKind Kind => Raw.Kind;
    public DoubtFunction? Dimension1 => Raw.Dimension1;
    public Doubt2DFunction? Dimension2 => Raw.Dimension2;
}