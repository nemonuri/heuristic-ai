using Nemonuri.Beliefs.Raws;

namespace Nemonuri.Beliefs;

public readonly struct DoubtFunctionalUnion
{
    public RawDoubtFunctionalUnion Raw {get;}

    public DoubtFunctionalUnion(RawDoubtFunctionalUnion raw)
    {
        Raw = raw;
    }

    public DoubtFunctionalUnion(DoubtFunctional dimension0) :
        this(new RawDoubtFunctionalUnion(dimension0))
    {}

    public DoubtFunctionalUnion(DoubtFunctional1D dimension1) :
        this(new RawDoubtFunctionalUnion(dimension1))
    {}

    public DoubtFunctionalUnion(DoubtFunctional2D dimension2) :
        this(new RawDoubtFunctionalUnion(dimension2))
    {}

    public DoubtFunctionKind Kind => Raw.Kind;
    public DoubtFunctional? Dimension0 => Raw.Dimension0;
    public DoubtFunctional1D? Dimension1 => Raw.Dimension1;
    public DoubtFunctional2D? Dimension2 => Raw.Dimension2;
}