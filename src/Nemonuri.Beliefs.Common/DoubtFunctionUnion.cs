using Nemonuri.Beliefs.Raws;

namespace Nemonuri.Beliefs;

public readonly struct DoubtFunctionUnion
{
    public RawDoubtFunctionUnion Raw {get;}

    public DoubtFunctionUnion(RawDoubtFunctionUnion raw)
    {
        Raw = raw;
    }

    public DoubtFunctionUnion(DoubtFunction dimension1) : 
        this(new RawDoubtFunctionUnion(dimension1))
    {}

    public DoubtFunctionUnion(Doubt2DFunction dimension2) : 
        this(new RawDoubtFunctionUnion(dimension2))
    {}

    public DoubtFunctionKind Kind => Raw.Kind;
    public DoubtFunction? Dimension1 => Raw.Dimension1;
    public Doubt2DFunction? Dimension2 => Raw.Dimension2;
}
