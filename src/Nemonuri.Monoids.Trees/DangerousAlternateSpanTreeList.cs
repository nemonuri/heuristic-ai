namespace Nemonuri.Monoids.Trees;

public readonly ref partial struct DangerousAlternateSpanTreeList<TAlternate>
    where TAlternate : unmanaged
{
    private readonly DangerousSpanList<TAlternate> _innerDangerousSpanList;

    public DangerousAlternateSpanTreeList(DangerousSpanList<TAlternate> innerDangerousSpanList)
    {
        _innerDangerousSpanList = innerDangerousSpanList;
    }

    public DangerousSpanList<TAlternate> InnerDangerousSpanList => _innerDangerousSpanList;

    public int Count => _innerDangerousSpanList.Count;

    public AlternateSpanTree<TAlternate> this[int index]
    {
        get
        {
            Span<TAlternate> span = _innerDangerousSpanList[index];
            return new (span);
        }
    }

    public Enumerator GetEnumerator() => new (this);
}
