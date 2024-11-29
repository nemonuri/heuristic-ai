#if false

namespace Nemonuri.Ordinals;

using ManagedPointers;
using Raws;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public record struct MinorizedOrdinal : IRefRawProvider<RawMinorizedOrdinal>
{
    private RawMinorizedOrdinal _raw;

    public MinorizedOrdinal(nint value, nint strictSupremum)
    {
        _raw = new(value,strictSupremum);
    }

    public MinorizedOrdinal(in RawMinorizedOrdinal raw)
    {
        _raw = raw;
    }

    public readonly nint Value => _raw.Value;

    public readonly nint StrictSupremum => _raw.StrictSupremum;

    public readonly bool IsValid => _raw.IsValid;

    [UnscopedRef]
    public ref RawMinorizedOrdinal Raw => ref _raw;

    public void SetValue(nint value)
    {
        _raw.Value = value;
        //TODO: Validate
    }
}

#endif