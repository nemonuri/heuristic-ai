using Nemonuri.Beliefs.Raws;

namespace Nemonuri.Beliefs;

public readonly struct DoubtFunctionInvokeInfo
{
    public RawDoubtFunctionInvokeInfo Raw {get;}

    public DoubtFunctionInvokeInfo(RawDoubtFunctionInvokeInfo raw)
    {
        Raw = raw;
    }

    public DoubtFunctionInvokeInfo
    (
        DoubtFunction doubtFunction, 
        uint index
    ) : this(new (doubtFunction, index))
    {}

    public DoubtFunction DoubtFunction => Raw.DoubtFunction;
    public uint Index => Raw.Index;
}