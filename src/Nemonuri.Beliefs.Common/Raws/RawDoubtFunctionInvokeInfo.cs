namespace Nemonuri.Beliefs.Raws;

public struct RawDoubtFunctionInvokeInfo
{
    public DoubtFunction DoubtFunction;
    public uint Index;

    public RawDoubtFunctionInvokeInfo(DoubtFunction doubtFunction, uint index)
    {
        DoubtFunction = doubtFunction;
        Index = index;
    }
}
