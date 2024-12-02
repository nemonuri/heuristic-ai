namespace Nemonuri.Ordinals;

public readonly ref struct RefOnlyMinorizedOrdinal
{
    private readonly ref nint _refValue;
    private readonly ref readonly nint _refStrictSupremum;

    public RefOnlyMinorizedOrdinal(ref nint refValue, ref readonly nint refStrictSupremum)
    {
        _refValue = ref refValue;
        _refStrictSupremum = ref refStrictSupremum;
    }

    public ref nint RefValue => ref _refValue;

    public nint StrictSupremum => _refStrictSupremum;
}