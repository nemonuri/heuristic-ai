using System.ComponentModel;

namespace Nemonuri.Ordinals;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly record struct ExclusiveMaximumOrdinal
{
    private readonly nint _innerValue;

    public ExclusiveMaximumOrdinal(nint innerValue)
    {
        _innerValue = innerValue;
    }

    public nint InnerValue => _innerValue;

    /// <summary>
    /// For syntax sugar. <br/>
    /// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges
    /// </summary>
    public int Count => checked((int)_innerValue);

    public nint Cardinality => _innerValue;

    public MinorizedOrdinal this[nint index] => new (index, this);

    public MinorizedOrdinal this[int index] => this[(nint)index];

    public MinorizedOrdinalSegment Slice(nint start, nint cardinality) => new(start, cardinality, this);

    public MinorizedOrdinalSegment Slice(int start, int length) => Slice((nint)start, (nint)length);

    public MinorizedOrdinalSegment ToMinorizedOrdinalSegment() => Slice(0, _innerValue);
}